using HIPA_BE.Models.SampleImageModels;
using HIPA_BE.Models;
using ErrorOr;
using HIPA_BE.ServiceErrors;
using HIPA_BE.Data;
using Microsoft.EntityFrameworkCore;
using HIPA_BE.Models.Admin.SampleImageModels;
using HIPA_BE.Contracts.Admin;
using HIPA_BE.Models.Admin.FlagModels;
using log4net;
using System.Reflection;
using Ganss.Xss;

/// Potom vymyslim nieco lepsie mozno, ale ne slubujem (toto sluzi na zistenie userid pre Favorite samples)
using Microsoft.AspNetCore.Identity;
using HIPA_BE.Models;

namespace HIPA_BE.Services.SampleImageServices
{
    public class SampleImageService : ISampleImageService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType ?? typeof(SampleImageService));

        private readonly AppDbContext _context;
        private readonly HtmlSanitizer _sanitizer;

        public SampleImageService(AppDbContext context)
        {
            _context = context;
            _sanitizer = new HtmlSanitizer();

            SetupSanitizer();
        }

        public async Task<ErrorOr<List<SampleImageDiagnosisDto>>> GetAllSampleImages(string userId)
        {
            try
            {
                var sampleImages = await _context.SampleImages
                            .Where(si => si.IsVisible == true && si.State == "READY")
                            .Select(si => new SampleImageDiagnosisDto
                            {
                                ID = si.ID,
                                Name = si.Name,
                                HasAnnotation = _context.SampleImageAnnotations.Any(annotation => annotation.SampleImageID == si.ID),
                                Diagnosis = si.Diagnosis.Name,
                                IsFavorite =_context.FavoriteSamples.Any(favorite => favorite.UserID == userId && favorite.SampleImageID == si.ID),
                                CaustryFile = si.CaustryFile,
                                Note = _context.SampleImageNotes.FirstOrDefault(note => note.SampleImageID == si.ID && note.UserID == userId).Note ?? "",
                                BodySystemNames = si.Organ.BodySystems.Select(bs => bs.Name).ToList(),
                                OrganName = si.Organ.Name,
                                KeyWords = si.KeyWords
                            })
                            .ToListAsync();

                if (sampleImages == null) return Errors.Models.SampleImageDbError;
                return sampleImages;
            }
            catch (Exception)
            {
                return Errors.Models.DbError;
            }
        }


        public async Task<ErrorOr<List<SampleImageAdminDto>>> GetAllSampleImagesForAdmin(string userId)
        {
            try
            {

                var sampleImages = await _context.SampleImages
                    .Where(si => si.State != "UPLOADING")
                    .Include(si => si.Organ)
                    .Select(si => new SampleImageAdminDto
                    {
                        ID = si.ID,
                        Name = si.Name,
                        IsVisible = si.IsVisible,
                        FlagType = _context.FlagTypes.Where(ft => ft.ID == _context.SampleImageFlags.Where(sif => sif.SampleImageID == si.ID && sif.UserID == userId).Select(sif => sif.FlagTypeID).FirstOrDefault()).Select(ft => new FlagTypeDto
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
                        Note =  _context.SampleImageNotes.SingleOrDefault(x => x.SampleImageID == si.ID && x.UserID == userId).Note
                    })
                    .ToListAsync();

                if (sampleImages == null) return Errors.Models.SampleImageDbError;
                return sampleImages;
            }
            catch (Exception)
            {
                return Errors.Models.DbError;
            }
        }

        public async Task<ErrorOr<Success>> DeleteSampleImages(List<int> imageIds)
        {
            try
            {
                var deletedItems = await _context.SampleImages.Where(si => imageIds.Contains(si.ID)).ExecuteDeleteAsync();
                if (deletedItems == 0) return Errors.Models.SampleImageDbError;
            }
            catch (Exception)
            {
                return Errors.Models.DbError;
            }

            return Result.Success;
        }

        public async Task<ErrorOr<Success>> ModifySampleImages(List<int> imageIds, ModifyAction modifyAction)
        {
            try
            {
                switch (modifyAction)
                {
                    case ModifyAction.ToggleHide:
                        var sampleImages = await _context.SampleImages.Where(si => imageIds.Contains(si.ID) && si.State == "READY").ToListAsync();
                        var allInVisible = sampleImages.All(si => !si.IsVisible);
                        if (allInVisible)
                        {
                            sampleImages.ForEach(si => si.IsVisible = true);
                        }
                        else
                        {
                            sampleImages.ForEach(si => si.IsVisible = false);
                        }

                        await _context.SaveChangesAsync();
                        break;
                }
            }
            catch (Exception)
            {
                return Errors.Models.DbError;
            }

            return Result.Success;

        }


        public async Task<ErrorOr<SampleImageDto>> GetSampleImageSampleId(int sampleId, string userId)
        {
            try
            {
                // find the SampleImage with the given organId and sampleId
                // return a SampleImageDto
                var sampleImage = await _context.SampleImages
                    .Where(si => si.ID == sampleId && si.State != "UPLOADING")
                    .Select(si => new SampleImageDto
                    {
                        ID = si.ID,
                        Name = si.Name,
                        Path = si.Path,
                        HasAnnotation = _context.SampleImageAnnotations
                                                .Any(annotation => annotation.SampleImageID == si.ID),
                        Diagnosis = si.Diagnosis.Name,
                        IsFavorite = _context.FavoriteSamples
                                    .Any(favorite => favorite.UserID == userId && favorite.SampleImageID == si.ID),
                        CaustryFile = si.CaustryFile,
                        Note = _context.SampleImageNotes
                               .FirstOrDefault(note => note.SampleImageID == si.ID && note.UserID == userId).Note ?? "",

                        KeyWords = si.KeyWords
                    })
                    .FirstOrDefaultAsync();

                if (sampleImage == null) return Errors.Models.SampleImageDbError;
                return sampleImage;
            }
            catch (Exception)
            {
                return Errors.Models.DbError;
            }
        }

        public async Task<ErrorOr<List<string>>> GetUniqueSampleImagesKeywords(int organId)
        {
            try
            {
                var keywords = await _context.SampleImages
                    .Where(si => si.OrganID == organId && si.State != "UPLOADING")
                    .Select(si => si.KeyWords) // Поле KeyWords
                    .ToListAsync();

                if (!keywords.Any())
                {
                    return new List<string>();
                }

               var uniqueKeywords = string.Join(" ", keywords)
                    .Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(k => k.Trim().ToLowerInvariant())
                    .Distinct()
                    .ToList();



                return uniqueKeywords;
            }
            catch (Exception)
            {
                return Errors.Models.DbError;
            }
        }



        public async Task<ErrorOr<Success>> PostFavouiteSampleImageSampleId(string userId, int sampleId)
        {
            try
            {
                var favoriteSample = new FavoriteSample
                {
                    SampleImage = null,
                    UserID = userId,
                    SampleImageID = sampleId
                };

                _context.FavoriteSamples.Add(favoriteSample);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return Errors.Models.DbError;
            }

            return Result.Success;
        }


        public async Task<ErrorOr<Success>> DeleteFavouiteSampleImageSampleId(string userId, int sampleId)
        {
            try
            {
                var favRecord = await _context.FavoriteSamples
                       .Where(si => si.SampleImageID == sampleId && si.UserID == userId)
                       .Select(si => new FavoriteSample
                       {
                           ID = si.ID,
                           UserID = si.UserID,
                           SampleImageID = si.SampleImageID,
                           SampleImage = null
                       })
                       .FirstOrDefaultAsync();
                if (favRecord != null)
                {

                    _context.FavoriteSamples.Remove(favRecord);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return Errors.Models.DbError;
                }
            }
            catch (Exception ex)
            {
                return Errors.Models.DbError;
            }

            return Result.Success;
        }

        public async Task<ErrorOr<Success>> PostSampleImageNote(string userId, int sampleId, string note)
        {
            try
            {
                var noteRecord = await _context.SampleImageNotes
                    .Where(si => si.SampleImageID == sampleId && si.UserID == userId)
                    .FirstOrDefaultAsync();

                note = SanitizeNoteText(note);

                if (noteRecord == null)
                {
                    var sampleImageNote = new SampleImageNote
                    {
                        SampleImage = null,
                        UserID = userId,
                        SampleImageID = sampleId,
                        Note = note
                    };

                    _context.SampleImageNotes.Add(sampleImageNote);
                }
                else if (string.IsNullOrWhiteSpace(note))
                {
                    //Remove the note if no text has been submitted
                    _context.SampleImageNotes.Remove(noteRecord);
                }
                else
                {
                    noteRecord.Note = note;
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Errors.Models.DbError;
            }

            return Result.Success;
        }

        public async Task<ErrorOr<Success>> FlagSampleImage(int sampleId, int flagTypeId, string userId)
        {
            try
            {
                var sampleImageFlag = _context.SampleImageFlags
                    .Where(sif => sif.SampleImageID == sampleId && sif.UserID == userId)
                    .FirstOrDefault();

                var flagType = _context.FlagTypes.Where(ft => ft.ID == flagTypeId).FirstOrDefault();

                if (flagType == null) return Errors.Models.DbError;

                if (sampleImageFlag != null)
                {
                    sampleImageFlag.FlagTypeID = flagTypeId;
                    await _context.SaveChangesAsync();
                    return Result.Success;
                }

                var newSampleImageFlag = new SampleImageFlag
                {
                    UserID = userId,
                    SampleImageID = sampleId,
                    FlagTypeID = flagTypeId,
                    FlagType = flagType
                };

                _context.SampleImageFlags.Add(newSampleImageFlag);
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {
                return Errors.Models.DbError;
            }

            return Result.Success;
        }

        public async Task<ErrorOr<Success>> UnFlagSampleImage(int sampleId, string userId)
        {

            try
            {
                var sampleImageFlag = _context.SampleImageFlags
                    .Where(sif => sif.SampleImageID == sampleId && sif.UserID == userId)
                    .FirstOrDefault();

                if (sampleImageFlag == null) return Errors.Models.DbError;

                _context.SampleImageFlags.Remove(sampleImageFlag);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                return Errors.Models.DbError;
            }

            return Result.Success;
        }

        public async Task<ErrorOr<SampleImageMetadataDto>> GetSampleImageMetadataById(int sampleId)
        {
            try
            {
                var sampleImage = await _context.SampleImages
                    .Where(si => si.ID == sampleId && si.State == "READY")
                    .FirstOrDefaultAsync();

                if (sampleImage == null)
                    return Errors.Models.SampleImageDbError;

                if (string.IsNullOrEmpty(sampleImage.Metadata))
                    return Errors.Models.SampleImageMetadataNotFound;

                try
                {
                    var metadata = System.Text.Json.JsonSerializer.Deserialize<SampleImageMetadataDto>(sampleImage.Metadata);
                    if (metadata == null)
                        return Errors.Models.SampleImageMetadataInvalid;

                    return metadata;
                }
                catch (Exception)
                {
                    return Errors.Models.SampleImageMetadataInvalid;
                }
            }
            catch (Exception)
            {
                return Errors.Models.SampleImageDbError;
            }
        }

        private string SanitizeNoteText(string note)
        {
            return _sanitizer.Sanitize(note);
        }

        private void SetupSanitizer() {
            _sanitizer.AllowedTags.Clear();
            _sanitizer.AllowedTags.Add("p");
            _sanitizer.AllowedTags.Add("strong");
            _sanitizer.AllowedTags.Add("ol");
            _sanitizer.AllowedTags.Add("ul");
            _sanitizer.AllowedTags.Add("li");
            _sanitizer.AllowedTags.Add("span");
            _sanitizer.AllowedTags.Add("br");
            _sanitizer.AllowedTags.Add("em");
            _sanitizer.AllowedTags.Add("u");

            _sanitizer.AllowedAttributes.Clear();
            _sanitizer.AllowedAttributes.Add("class");
            _sanitizer.AllowedAttributes.Add("data-list");
            _sanitizer.AllowedAttributes.Add("contenteditable");
        }
    }
}