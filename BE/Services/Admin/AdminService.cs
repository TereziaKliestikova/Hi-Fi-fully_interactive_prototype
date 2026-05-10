using System.Text.Json;
using ErrorOr;
using HIPA_BE.ServiceErrors;
using HIPA_BE.Models.SampleImageAnnotationModels;
using HIPA_BE.Data;
using HIPA_BE.Models;
using HIPA_BE.Models.Admin.SampleImageModels;
using HIPA_BE.Models.PdfFileModels;
using HIPA_BE.Contracts.Admin;
using HIPA_BE.Models.Admin.FlagModels;
using Microsoft.EntityFrameworkCore;
using log4net;
using System.Reflection;

// TODO: Admin service not fully implemented improvements required
// Now the service only supports:
//  - sample image can't be renamed, the name of
//    the image in the SampleImageAnnotation table
//    is received from the upload-sample-image-data endpoint
//  - this service does not handle new icon upload (body system and organ),
//    diagnosis assignment
//  - new organ or body system can't be assigned a new diagnosis.
//  - creation of new organ or body system is a bit hacky, right now new organ/body system
//    is created when a new name is passed to the endpoint, this should be potentially changed

namespace HIPA_BE.Services
{
    public class AdminService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType ?? typeof(AdminService));
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AdminService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ErrorOr<string>> AddCaustryFileToSampleImage(string name, string sampleId){
            try{
                var sampleImage = await _context.SampleImages
                    .FirstOrDefaultAsync(si => si.ID.ToString() == sampleId);

                if (sampleImage == null)
                {
                    return Errors.Models.DbError;
                }

                string caustryPath = Path.Combine(sampleImage.Path, "caustryFile");
                Directory.CreateDirectory(caustryPath);
                
                PdfFile caustry = new PdfFile {
                    Name = name,
                    Path = caustryPath
                };
                _context.PdfFiles.Add(caustry);
                await _context.SaveChangesAsync();

                sampleImage.CaustryFileID = caustry.ID;
                await _context.SaveChangesAsync();

                return caustryPath;

            }catch(Exception ex){
                return Errors.Models.DbError;
            }
        }

        public async Task<ErrorOr<Success>> DeleteCaustryFileFromSampleImage(int sampleId)
        {
            try
            {
                var imageWithCaustry = await _context.SampleImages
                    .Include(i => i.CaustryFile)
                    .FirstOrDefaultAsync(i => i.ID == sampleId);

                if (imageWithCaustry == null || imageWithCaustry.CaustryFile == null)
                    return Errors.Models.DbError;
                var path = imageWithCaustry.CaustryFile.Path;

                _context.PdfFiles.Remove(imageWithCaustry.CaustryFile);

                imageWithCaustry.CaustryFileID = null;
                imageWithCaustry.CaustryFile = null;

                if (Directory.Exists(path)){
                    foreach (var file in Directory.GetFiles(path)){
                        File.Delete(file);
                    }
                }
                
                await _context.SaveChangesAsync(); 
                return Result.Success;
            }
            catch (Exception ex)
            {
                return Errors.Models.DbError;
            }
            return Result.Success;
        }

        public async Task<ErrorOr<Success>> UpdateSampleImageAfterUpload(string groupId, string filePath = ""){
            try{
                SampleImage sampleImage = await _context.SampleImages.Where(si => si.GroupId == groupId).FirstAsync();         
                sampleImage.State = "AWAITING CONVERSION";
                
                // Update the path to include the filename if provided
                if (!string.IsNullOrEmpty(filePath))
                {
                    sampleImage.Path = filePath;
                }
                
                await _context.SaveChangesAsync();
                return Result.Success;
            }
            catch (Exception ex)
            {
                Log.Error($"Error while updating sample image after upload: {ex}");
                return Errors.Models.DbError;
            }
        }

        public async Task<ErrorOr<string>> SaveSampleImageData(StoreSampleImageDataRequest request)
        {
            try
            {
                string groupId = Guid.NewGuid().ToString();
                while (_context.SampleImages.Any(s => s.GroupId == groupId))
                {
                    groupId = Guid.NewGuid().ToString();
                }

                string uploadPath = _configuration.GetSection("TusConfig:UploadPath").Get<string>()
                    ?? throw new InvalidOperationException("TusConfig:UploadPath not configured");
                string sampleImageFinalPath = Path.Combine(Directory.GetParent(uploadPath)?.FullName ?? "", groupId);

                Directory.CreateDirectory(sampleImageFinalPath);

                SampleImage sampleImage = new SampleImage
                {
                    Name = request.SampleImageFileName,
                    Path = sampleImageFinalPath,
                    OrganID = request.OrganId,
                    KeyWords = request.KeyWords,
                    GroupId = groupId,
                    DiagnosisID = 1,
                    CaustryFileID = null,
                    State = "UPLOADING",
                    LastModified = DateTime.UtcNow
                };

                _context.SampleImages.Add(sampleImage);
                await _context.SaveChangesAsync();

                if (request.AnnotationFile != null)
                {
                    try
                    {
                        string fileContent = await new StreamReader(request.AnnotationFile.OpenReadStream()).ReadToEndAsync();
                        if (!string.IsNullOrEmpty(fileContent))
                        {
                            var options = new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            };

                            var geoJsonFeatureCollectionDto = JsonSerializer.Deserialize<GeoJsonFeatureCollectionDto>(fileContent, options);

                            if (geoJsonFeatureCollectionDto?.Features != null)
                            {
                                string geoJsonFileName = $"{request.SampleImageFileName}.geojson";
                                string annotationFinalPath = Path.Combine(sampleImageFinalPath, geoJsonFileName);
                                await using (var stream = new FileStream(annotationFinalPath, FileMode.Create))
                                {
                                    await request.AnnotationFile.CopyToAsync(stream);
                                }

                                var sampleImageAnnotations = new List<SampleImageAnnotation>();
                                foreach (var annot in geoJsonFeatureCollectionDto.Features)
                                {
                                    string nameToUse = "";
                                    if (annot.Properties.Classification != null)
                                        nameToUse = annot.Properties.Classification.Name;
                                    else if (annot.Properties.Name != null)
                                        nameToUse = annot.Properties.Name;
                                    else
                                    {
                                        nameToUse = $"Annotation_{sampleImageAnnotations.Count + 1}";
                                        Log.Info($"Using default name '{nameToUse}' for annotation without name. Object: {annot}");
                                    }

                                    string descriptionToUse = annot.Properties.Metadata?.ANNOTATION_DESCRIPTION ?? "Bez popisu.";

                                    sampleImageAnnotations.Add(new SampleImageAnnotation
                                    {
                                        Name = nameToUse,
                                        Description = descriptionToUse,
                                        SampleImageID = sampleImage.ID,
                                        BoundingBox = JsonSerializer.Serialize(annot)
                                    });
                                }

                                if (sampleImageAnnotations.Any())
                                {
                                    _context.SampleImageAnnotations.AddRange(sampleImageAnnotations);
                                    await _context.SaveChangesAsync();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"Error processing annotation file: {ex.Message}");
                    }
                }

                return groupId;
            }
            catch (Exception ex)
            {
                Log.Error($"Error in SaveSampleImageData: {ex}");
                return Errors.Models.DbError;
            }
        }

        public async Task<ErrorOr<List<FlagTypeDto>>> GetAllFlags()
        {
            try
            {
                var flags = await _context.FlagTypes
                    .Select(fg => new FlagTypeDto
                    {
                        ID = fg.ID,
                        Name = fg.Name,
                        Color = fg.Color
                    })
                    .ToListAsync();

                return flags;
            }
            catch (Exception ex)
            {
                return Errors.Models.DbError;
            }
        }

        public async Task<ErrorOr<Success>> CreateAdminFlag(FlagTypeDto flag)
        {
            try
            {
                if (flag.ID != 0)
                {
                    var existingFlag = await _context.FlagTypes.FindAsync(flag.ID);
                    if (existingFlag != null)
                    {
                        existingFlag.Name = flag.Name;
                        existingFlag.Color = flag.Color;

                        _context.FlagTypes.Update(existingFlag);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return Errors.Models.DbError;
                    }
                }
                else
                {
                    var newFlag = new FlagType
                    {
                        Name = flag.Name,
                        Color = flag.Color,
                    };

                    _context.FlagTypes.Add(newFlag);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return Errors.Models.DbError;
            }

            return Result.Success;
        }

        public async Task<ErrorOr<Success>> DeleteAdminFlag(int flagId)
        {
            try
            {
                var flag = await _context.FlagTypes
                    .Where(fg => fg.ID == flagId)
                    .Select(fg => new FlagType
                    {
                        ID = fg.ID,
                        Name = fg.Name,
                        Color = fg.Color
                    })
                    .FirstOrDefaultAsync();
                if (flag != null)
                {

                    _context.FlagTypes.Remove(flag);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return Errors.Models.DbError;
                }

                return Result.Success;
            }
            catch (Exception)
            {
                return Errors.Models.DbError;
            }
        }

        public async Task<ErrorOr<Success>> EditSampleImageInfo(SampleImageAdminDto sample, int organId)
        {
            try
            {
                var image = await _context.SampleImages
                    .FirstOrDefaultAsync(i => i.ID == sample.ID);
                
                if (image == null || organId == null)
                    return Errors.Models.DbError;
                
                image.Name = sample.Name;
                image.KeyWords = sample.KeyWords;
                image.OrganID = organId;
                await _context.SaveChangesAsync(); 
            }
            catch (Exception ex)
            {
                return Errors.Models.DbError;
            }

            return Result.Success;
        }
    }
}