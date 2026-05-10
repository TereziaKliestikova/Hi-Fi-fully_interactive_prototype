using ErrorOr;
using HIPA_BE.Contracts.Admin.Learning;
using HIPA_BE.Contracts.Generic;
using HIPA_BE.Data;
using HIPA_BE.Models;
using HIPA_BE.Models.Admin.FlagModels;
using HIPA_BE.Models.Admin.SampleImageModels;
using HIPA_BE.Models.LearningModels;
using HIPA_BE.Models.PdfFileModels;
using HIPA_BE.ServiceErrors;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Directory = System.IO.Directory;
using HipaDirectory = HIPA_BE.Models.LearningModels.Directory;
using HIPA_BE.Models.SampleImageModels;

namespace HIPA_BE.Services;

public class LearningService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    private readonly string _directoryRootPath = "/media/pdfs/";

    public LearningService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<ErrorOr<DirectoryCreatedResponse>> CreateRootDirectory(string name, StudyCategory study)
    {
        try
        {
            var directoryPath = Path.Combine(_directoryRootPath, study.ToString(), name);
            if (Directory.Exists(directoryPath))
                return Errors.Models.DbError;

            var directoryModel = new HipaDirectory
            {
                Name = name,
                Path = directoryPath,
                NestingLevel = 0,
                IsPublic = false,
                StudyCategory = study,
            };

            var entity = _context.Directories.Add(directoryModel);
            await _context.SaveChangesAsync();

            Directory.CreateDirectory(directoryPath);

            return new DirectoryCreatedResponse(entity.Entity.Id, name);
        }
        catch (Exception ex)
        {
            return Errors.Models.DbError;
        }
    }

    public async Task<ErrorOr<DirectoryCreatedResponse>> CreateDirectory(int parentId, string name)
    {
        try
        {
            var parent = await _context.Directories.FirstOrDefaultAsync(x => x.Id == parentId);
            if (parent == null)
                return Errors.Models.DbError;

            if (parent.NestingLevel > 1)
                return Errors.Models.DbError;

            var directoryPath = Path.Combine(parent.Path, name);
            if (Directory.Exists(directoryPath))
                return Errors.Models.DbError;

            var directoryModel = new HipaDirectory
            {
                Name = name,
                Path = directoryPath,
                NestingLevel = parent.NestingLevel + 1,
                IsPublic = false,
                StudyCategory = parent.StudyCategory,
                Parent = parent,
            };

            var entity = _context.Directories.Add(directoryModel);
            await _context.SaveChangesAsync();

            Directory.CreateDirectory(directoryPath);

            return new DirectoryCreatedResponse(entity.Entity.Id, name);
        }
        catch (Exception ex)
        {
            return Errors.Models.DbError;
        }
    }

    public async Task<ErrorOr<List<ItemWithNameDto>>> GetDirectoryNotPublicParentNames(int id)
    {
        var directory = await _context.Directories
            .Include(x => x.Parent)
            .ThenInclude(x => x.Parent)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        
        if (directory == null)
            return Errors.Models.DbError;
        
        List<ItemWithNameDto> parentNames = new();
            
        var parent1 = directory.Parent;
        if (parent1 != null)
        {
            if (!parent1.IsPublic)
            {
                parentNames.Add(new ItemWithNameDto(parent1.Name));
            }
            
            var parent2 = parent1.Parent;
            if (parent2 != null && !parent1.IsPublic)
            {
                parentNames.Add(new ItemWithNameDto(parent2.Name));
            }
        }
        return parentNames;
    }

    public async Task<ErrorOr<Success>> ChangeDirectoryVisibility(int id, bool isPublic)
    {
        try
        {
            var directory = await _context.Directories
                .Include(x => x.Parent)
                .ThenInclude(x => x.Parent)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (directory == null)
                return Errors.Models.DbError;
            
            var parent1 = directory.Parent;
            if (parent1 != null)
            {
                if (isPublic)
                {
                    parent1.IsPublic = true;
                }
                var parent2 = parent1.Parent;
                if (parent2 != null)
                {
                    if (isPublic)
                    {
                        parent2.IsPublic = true;
                    }
                }
            }


            directory.IsPublic = isPublic;
            await _context.SaveChangesAsync();

            return Result.Success;
        }
        catch (Exception ex)
        {
            return Errors.Models.DbError;
        }
    }

    public async Task<ErrorOr<Success>> UpdateDirectoryInfo(int id, UpdateDirectoryInfoRequest newData)
    {
        try
        {
            var directory = await _context.Directories.FirstOrDefaultAsync(x => x.Id == id);
            if (directory == null)
                return Errors.Models.DbError;

            directory.Description = newData.Description;
            var keyWords = newData.KeyWords;

            if (string.IsNullOrWhiteSpace(keyWords))
                keyWords = null;

            directory.KeyWords = keyWords;
            await _context.SaveChangesAsync();

            return Result.Success;
        }
        catch (Exception ex)
        {
            return Errors.Models.DbError;
        }
    }

    public async Task<ErrorOr<Success>> DeleteDirectory(int id)
    {
        try
        {
            var directory = await _context.Directories.FirstOrDefaultAsync(x => x.Id == id);
            if (directory == null)
                return Errors.Models.DbError;

            //If directory was already deleted by some other factor, like redeploy then we just delete the
            //database entry.
            if (Directory.Exists(directory.Path))
            {
                Directory.Delete(directory.Path, true);
            }

            _context.Directories.Remove(directory);
            await _context.SaveChangesAsync();
            return Result.Success;
        }
        catch (Exception ex)
        {
            return Errors.Models.DbError;
        }
    }

    public async Task<ErrorOr<Success>> DeleteFile(int id)
    {
        var filePath = "";
        PdfFile file = null;
        try
        {
            file = await _context.PdfFiles.FirstOrDefaultAsync(x => x.ID == id);
            if (file == null)
                return Errors.Models.DbError;

            filePath = Path.Combine(file.Path, file.Name);
            
            File.Delete(filePath);

            _context.PdfFiles.Remove(file);
            await _context.SaveChangesAsync();
            return Result.Success;
        }
        catch (Exception ex)
        {
            if (!File.Exists(filePath))
            {
                _context.PdfFiles.Remove(file);
                await _context.SaveChangesAsync();
            }
            return Errors.Models.DbError;
        }
    }

    public async Task<ErrorOr<List<DirectoryTreeDto>>> GetDirectoryTreeForStudy(StudyCategory study, bool fullAccess)
    {
        try
        {

            var folderTree = await _context.Directories
                .Where(d => d.StudyCategory == study && d.NestingLevel == 0 && (fullAccess || d.IsPublic))
                .Select(d => new DirectoryTreeDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    IsPublic = d.IsPublic,
                    
                    // Description = d.Description,
                    // KeyWords = d.KeyWords,
                    // Files = d.Files,
                    Children = d.ChildDirectories
                    .Where(child => fullAccess || child.IsPublic)
                    .Select(child => new DirectoryTreeDto
                    {
                        Id = child.Id,
                        Name = child.Name,
                        IsPublic = child.IsPublic,
                        // Description = child.Description,
                        // KeyWords = child.KeyWords,
                        // Files = child.Files,
                        Children = child.ChildDirectories
                        .Where(grandChild => fullAccess || grandChild.IsPublic)
                        .Select(grandChild => new DirectoryTreeDto
                        {
                            Id = grandChild.Id,
                            Name = grandChild.Name,
                            IsPublic = grandChild.IsPublic,
                            // Description = grandChild.Description,
                            // KeyWords = grandChild.KeyWords,
                            // Files = grandChild.Files,
                            Children = new List<DirectoryTreeDto>()
                        }).ToList()
                    }).ToList()
                })
                .ToListAsync();

            return folderTree;
        }
        catch (Exception ex)
        {
            return Errors.Models.DbError;
        }
    }

    public async Task<ErrorOr<List<SampleImageAdminDto>>> GetDirectorySampleImages(int id, string userId)
    {
        try
        {
            var sampleImages = await _context.Directories
                .Where(x => x.Id == id)
                .SelectMany(d => d.SampleImages)
                .Select(si => new SampleImageAdminDto
                {
                    ID = si.ID,
                    Name = si.Name,
                    IsVisible = si.IsVisible,
                    FlagType = _context.FlagTypes.Where(ft => ft.ID == _context.SampleImageFlags
                            .Where(sif => sif.SampleImageID == si.ID && sif.UserID == userId)
                            .Select(sif => sif.FlagTypeID).FirstOrDefault())
                        .Select(ft => new FlagTypeDto
                        {
                            ID = ft.ID,
                            Name = ft.Name,
                            Color = ft.Color
                        }).FirstOrDefault(),
                    HasAnnotation = _context.SampleImageAnnotations.Any(sia => sia.SampleImageID == si.ID),
                    BodySystemNames = si.Organ.BodySystems.Select(bs => bs.Name).ToList(),
                    OrganName = si.Organ.Name,
                    CaustryFile = si.CaustryFile,
                    KeyWords = si.KeyWords,
                    Path = si.Path,
                })
                .ToListAsync();

            if (sampleImages == null)
                return Errors.Models.DbError;

            return sampleImages;
        }
        catch (Exception ex)
        {
            return Errors.Models.DbError;
        }
    }


        public async Task<ErrorOr<List<SampleImageDiagnosisDto>>> GetDirectorySampleImagesStudent(int id, string userId)
    {
        try
        {
            var sampleImages = await _context.Directories
                .Where(x => x.Id == id)
                .SelectMany(d => d.SampleImages)
                .Select(si => new SampleImageDiagnosisDto
                {
                    ID = si.ID,
                    Name = si.Name,
                    HasAnnotation = _context.SampleImageAnnotations.Any(sia => sia.SampleImageID == si.ID),
                    Diagnosis = si.Diagnosis.Name,
                    IsFavorite = _context.FavoriteSamples.Any(sif => sif.SampleImageID == si.ID && sif.UserID == userId),
                    CaustryFile = si.CaustryFile,
                    Note = _context.SampleImageNotes
                        .Where(sin => sin.SampleImageID == si.ID && sin.UserID == userId)
                        .Select(sif => sif.Note).FirstOrDefault(),
                    KeyWords = si.KeyWords,
                    OrganName = si.Organ.Name,
                    BodySystemNames = si.Organ.BodySystems.Select(bs => bs.Name).ToList(),
                })
                .ToListAsync();

            if (sampleImages == null)
                return Errors.Models.DbError;

            return sampleImages;
        }
        catch (Exception ex)
        {
            return Errors.Models.DbError;
        }
    }
    public async Task<ErrorOr<List<PdfFileDto>>> GetDirectoryFiles(int id){
        try{
            var files = await _context.PdfFiles
            .Where(f => f.DirectoryId == id)
            .Select(f => new PdfFileDto
            {
                ID = f.ID,
                Name = f.Name,
                Path = f.Path,
            })
            .ToListAsync();

            return files;
        }
        catch (Exception ex)
        {
            return Errors.Models.DbError;
        }
    }

    public async Task<ErrorOr<PdfFileDto>> AddFileToDirectory(string fileName, string folderId)
    {
        try
        {
            var directory = await _context.Directories
            .Include(f => f.Files)
            .FirstOrDefaultAsync(f => f.Id.ToString() == folderId);

            if (directory == null)
            {
                return Errors.Models.DbError;
            }

            var newFile = new PdfFile
            {
                Name = fileName,
                Path = directory.Path,
            };

            directory.Files.Add(newFile);
            await _context.SaveChangesAsync();
            return new PdfFileDto()
            {
                ID = newFile.ID,
                Name = newFile.Name,
                Path = newFile.Path,
            };
        }
        catch (Exception ex)
        {
            return Errors.Models.DbError;
        }
    }

    public async Task<ErrorOr<Success>> AddSampleImagesToDirectory(int id, List<int> imagesToAdd)
    {
        try
        {
            var directory = await _context.Directories
                .Include(d => d.SampleImages)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (directory == null)
                return Errors.Models.DbError;

            var existingImageIds = directory.SampleImages.Select(si => si.ID).ToHashSet();
            var newIds = imagesToAdd.Distinct().Where(id => !existingImageIds.Contains(id));

            foreach (var imageId in newIds)
            {
                var proxyImage = new SampleImage { ID = imageId };
                _context.Attach(proxyImage); //Does not affect the entity in database
                directory.SampleImages.Add(proxyImage);
            }

            await _context.SaveChangesAsync();

            return Result.Success;
        }
        catch (Exception ex)
        {
            return Errors.Models.DbError;
        }
    }
    
    public async Task<ErrorOr<Success>> DeleteSampleImagesFromDirectory(int id, List<int> imagesToDelete)
    {
        try
        {
            var directory = await _context.Directories
                    .Include(d => d.SampleImages)
                    .FirstOrDefaultAsync(x => x.Id == id);
            if (directory == null)
                return Errors.Models.DbError;


            var toUnlink = directory.SampleImages
                .Where(si => imagesToDelete.Contains(si.ID))
                .ToList();


            foreach (var img in toUnlink)
            {
                directory.SampleImages.Remove(img);
            }
            await _context.SaveChangesAsync();
            
            return Result.Success;
        }
        catch
        {
            return Errors.Models.DbError;
        }
    }

    
    public async Task<ErrorOr<DirectoryDetailDto>> GetDirectoryDetails(int id, bool fullAccess){
        try
        {
            var directory = await _context.Directories
                .Where(d => d.Id == id && (fullAccess || d.IsPublic))
                .Include(d => d.ChildDirectories)
                .Include(d => d.Files)
                .Select(d => new DirectoryDetailDto()
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    IsPublic = d.IsPublic,
                    KeyWords = d.KeyWords,
                    Level = d.NestingLevel,
                    Files = d.Files.Select(f => new PdfFileDto()
                    {
                        ID = f.ID,
                        Name = f.Name,
                        Path = f.Path,
                    }).ToList(),
                    Children = d.ChildDirectories
                    .Where(chd => fullAccess || chd.IsPublic)
                    .Select(chd => new DirectoryListItemDto()
                    {
                        Id = chd.Id,
                        Name = chd.Name,
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (directory == null)
                return Errors.Models.DbError;


            return directory;
        }
        catch (Exception ex)
        {
            return Errors.Models.DbError;
        }
    }
}