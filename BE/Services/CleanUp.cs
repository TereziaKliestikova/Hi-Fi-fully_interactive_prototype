using Microsoft.EntityFrameworkCore;
using HIPA_BE.Data; 
using tusdotnet.Stores; 
using HIPA_BE.Controllers.Admin;
using log4net;
using System.Reflection;

public class DbCleanupService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType ?? typeof(AdminController));
    private readonly TusDiskStore _tusStore;
    private readonly TimeSpan _checkInterval;
    private readonly TimeSpan _howOldItemsToDelete;

    public DbCleanupService(IServiceScopeFactory scopeFactory, IConfiguration configuration, TusDiskStore tusStore)
    {
        _scopeFactory = scopeFactory;
        _tusStore = tusStore;
        _checkInterval = TimeSpan.FromMinutes(configuration.GetValue<double>("TusConfig:DbCleanupInterval"));
        _howOldItemsToDelete = TimeSpan.FromMinutes(configuration.GetValue<double>("TusConfig:Expiration") + 1.0);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.Info("DbCleanupService started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    var thresholdTime = DateTime.UtcNow - _howOldItemsToDelete;
                    _logger.Info($"Will delete items marked as UPLOADING older than {thresholdTime}");
                    // Найти записи для удаления
                    var recordsToDelete = await _context.SampleImages
                        .Where(si => si.LastModified < thresholdTime && si.State == "UPLOADING")
                        .ToListAsync(stoppingToken);

                    if (!recordsToDelete.Any())
                    {
                        _logger.Info("Nothig to delete.");
                    }
                    else
                    {
                        foreach (var record in recordsToDelete)
                        {
                            _logger.Info($"Deleting SampleImage: {record.ID}");

                            var linkedRecords = await _context.SampleImageAnnotations
                                .Where(a => a.SampleImageID == record.ID)
                                .ToListAsync(stoppingToken);

                            if (linkedRecords.Any())
                            {
                                _logger.Info($"Deleting {linkedRecords.Count} related SampleImageAnnotations records.");
                                _context.SampleImageAnnotations.RemoveRange(linkedRecords);
                            }
                            if (!string.IsNullOrEmpty(record.Path))
                            {
                                try
                                {
                                    if (Directory.Exists(record.Path))
                                    {
                                        Directory.Delete(record.Path, true);
                                        _logger.Info($"Folder {record.Path} was deleted.");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.Error($"Error {record.Path}: {ex.Message}");
                                }
                            }
                            _context.SampleImages.Remove(record);
                        }

                        await _context.SaveChangesAsync(stoppingToken);
                    }

                    await CleanupTusFiles();
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error DbCleanupService: {ex.Message}");
            }

            _logger.Info($"Next DBCleanup check in {_checkInterval.TotalMinutes} minutes.");
            await Task.Delay(_checkInterval, stoppingToken);
        }
    }

    private async Task CleanupTusFiles()
    {
        try
        {
            var expiredFiles = await _tusStore.GetExpiredFilesAsync(CancellationToken.None);
            foreach (var fileId in expiredFiles)
            {
                try
                {
                    await _tusStore.DeleteFileAsync(fileId, CancellationToken.None);
                    _logger.Info($"Uncomplete file: {fileId} was deleted");
                }
                catch (Exception ex)
                {
                    _logger.Error($"Error when deleting uncomplete file {fileId}: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            _logger.Error($"TUS Error uncompleted files deleting: {ex.Message}");
        }
    }
}
