using ErrorOr;
using HIPA_BE.Data;
using HIPA_BE.Models.SampleImageModels;
using HIPA_BE.ServiceErrors;
using Duende.IdentityServer.Extensions;
using log4net;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace HIPA_BE.Services
{
    public class ConversionService
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType ?? typeof(ConversionService));
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public ConversionService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ErrorOr<List<ConversionJobDto>>> GetAllPendingConversionJobs()
        {
            _log.Info("Fetching all pending conversion jobs from the database.");
            try
            {
                var conversionJobs = await _context.SampleImages
                    .Where(si => si.State == "AWAITING CONVERSION")
                    .Select(si => new ConversionJobDto
                    {
                        SampleImageID = si.ID,
                        SampleImageName = si.Name!,
                        SampleImageGroupId = si.GroupId!,
                        SampleImagePath = si.Path!,
                        State = si.State
                    })
                    .ToListAsync();
                _log.Info($"Fetched {conversionJobs.Count} conversion jobs from the database.");

                return conversionJobs;
            }
            catch (Exception ex)
            {
                _log.Error("Error fetching conversion jobs from the database.", ex);
                return Errors.Models.DbError;
            }
        }

        public async Task<ErrorOr<ConversionJobDto>> UpdateConversionJob(int sampleImageId, ConversionJobDto conversionJob)
        {
            _log.Info("Updating conversion job in the database.");

            try
            {
                var sampleImage = await _context.SampleImages
                    .Where(si => si.ID == sampleImageId)
                    .FirstOrDefaultAsync();

                if (sampleImage == null)
                {
                    _log.Warn("SampleImage not found.");
                    return Errors.Models.SampleImageDbError;
                }

                if (conversionJob.State == "READY" && sampleImage.State != "READY")
                {
                    try
                    {
                        string wsiPath = Path.GetFullPath(_configuration.GetValue<string>("AssetConfiguration:SampleImagesPath") ?? "/media/wsi");
                        string sourceDir = Path.GetFullPath(Path.GetDirectoryName(sampleImage.Path)
                            ?? throw new DirectoryNotFoundException($"Invalid path: {sampleImage.Path}"));

                        string targetDir = Path.GetFullPath(Path.Combine(wsiPath, sampleImage.GroupId));

                        _log.Info($"Moving directory from {sourceDir} to {targetDir}");

                        if (Directory.Exists(targetDir))
                        {
                            var files = Directory.GetFiles(targetDir, "*", SearchOption.AllDirectories);
                            var dirInfo = new DirectoryInfo(targetDir);
                            _log.Warn($"Target directory '{targetDir}' already exists and will be deleted.");
                            Directory.Delete(targetDir, true);
                        }

                        Directory.CreateDirectory(Path.GetDirectoryName(targetDir));

                        Directory.Move(sourceDir, targetDir);
                        _log.Info($"Successfully moved directory to {targetDir}");

                        string dziPath = Directory.GetFiles(Path.Combine(targetDir, "dzi"), "*.dzi").FirstOrDefault();
                        if (dziPath == null)
                        {
                            _log.Error("No .dzi file found after moving directory");
                            return Errors.Models.DbError;
                        }

                        string dziFileName = Path.GetFileName(dziPath);
                        sampleImage.Path = $"/media/wsi/{sampleImage.GroupId}/dzi/{dziFileName}".Replace("\\", "/");
                        _log.Info($"Updated path to {sampleImage.Path}");
                    }
                    catch (Exception ex)
                    {
                        _log.Error($"Error moving directory to media/wsi: {ex.Message}");
                        return Errors.Models.DbError;
                    }
                }


                if (!conversionJob.SampleImageName.IsNullOrEmpty())
                {
                    sampleImage.Name = conversionJob.SampleImageName!;
                }

                if (!conversionJob.State.IsNullOrEmpty())
                {
                    sampleImage.State = conversionJob.State!;
                }

                if (conversionJob.Metadata != null)
                {
                    try
                    {
                        sampleImage.Metadata = JsonSerializer.Serialize(conversionJob.Metadata);
                    }
                    catch (Exception ex)
                    {
                        _log.Error("Error saving metadata: ", ex);
                    }
                }

                await _context.SaveChangesAsync();
                _log.Info($"Updated conversion job with ID {sampleImageId}.");

                SampleImageMetadataDto? deserialized_metadata = null;
                if (!string.IsNullOrEmpty(sampleImage.Metadata))
                {
                    try
                    {
                        deserialized_metadata = JsonSerializer.Deserialize<SampleImageMetadataDto>(sampleImage.Metadata);
                    }
                    catch (Exception ex)
                    {
                        _log.Error("Error deserializing metadata: ", ex);
                    }
                }

                var conversionJobReturn = new ConversionJobDto
                {
                    SampleImageID = sampleImage.ID,
                    SampleImageName = sampleImage.Name!,
                    SampleImageGroupId = sampleImage.GroupId!,
                    SampleImagePath = sampleImage.Path!,
                    State = sampleImage.State!,
                    Metadata = deserialized_metadata
                };

                return conversionJobReturn;
            }
            catch (Exception ex)
            {
                _log.Error("Error updating conversion job in the database.", ex);
                return Errors.Models.DbError;
            }
        }
    }
}
