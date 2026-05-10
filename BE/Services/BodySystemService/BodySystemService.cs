using HIPA_BE.Data;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using HIPA_BE.ServiceErrors;
using HIPA_BE.Models.SampleImageModels;
using HIPA_BE.Models.BodySystemModels;
using HIPA_BE.Models.BaseModels;

namespace HIPA_BE.Services.BodySystemService
{
    public class BodySystemService
    {
        private readonly AppDbContext _context;

        public BodySystemService(AppDbContext context)
        {
            _context = context;
        }

         public async Task<ErrorOr<BodySystemListDto>> GetListOfAllBodySystems()
        {
            try
            {

              var groupedSystems = await _context.BodySystems
                  .Include(bs => bs.Organs)
                  .Select(bs => new BodySystemDto {
                    ID = bs.ID,
                    Name = bs.Name,
                    IconPath = bs.IconPath,
                    Diagnoses = _context.SampleImages
                      .Include(si => si.Diagnosis)
                      .Where(si => bs.Organs.Any(o => o.ID == si.OrganID))
                      .Select(si => si.Diagnosis).ToList()
                  }).ToListAsync();

              if (groupedSystems == null) return Errors.Models.OrgansDbError;

              return new BodySystemListDto { BodySystems = groupedSystems };
            }
            catch (Exception)
            {
                return Errors.Models.DbError;
            }
        }

        public async Task<ErrorOr<BodySystemDetailDto>> GetBodySystemDetailById(string userId, int id)
        {
            try
            {
                var bodySystemDetail = await _context.BodySystems
                    .Where(bs => bs.ID == id)
                    .Select(bs => new BodySystemDetailDto
                    {
                        BodySystemDescription = new BodySystemDescriptionDto
                        {
                            ID = bs.ID,
                            Name = bs.Name,
                            Description = bs.Description
                        },
                        SampleImages = _context.SampleImages

                            .Where(si => bs.Organs.Any(o => o.ID == si.OrganID && si.IsVisible == true))
                            .Select(si => new SampleImageDiagnosisDto
                            {
                                ID = si.ID,
                                Name = si.Name,
                                // Check if there are any annotations for this sample image
                                HasAnnotation = _context.SampleImageAnnotations
                                    .Any(annotation => annotation.SampleImageID == si.ID),
                                Diagnosis = si.Diagnosis.Name,
                                IsFavorite = _context.FavoriteSamples
                                    .Any(favorite => favorite.UserID == userId && favorite.SampleImageID == si.ID),
                                CaustryFile = si.CaustryFile,
                                Note = _context.SampleImageNotes
                                .FirstOrDefault(note => note.SampleImageID == si.ID && note.UserID == userId).Note ?? "",
                                BodySystemNames = si.Organ.BodySystems.Select(bs => bs.Name).ToList(),
                                OrganName = si.Organ.Name,
                                KeyWords = si.KeyWords

                            })
                            .ToList()
                    })
                    .FirstOrDefaultAsync();

                if (bodySystemDetail == null) return Errors.Models.OrgansDbError;
                return bodySystemDetail;
            }
            catch (Exception)
            {

                return Errors.Models.DbError;
            }
        }

        public async Task<ErrorOr<IconPathDto>> GetBodySystemIconPathById(int id)
        {
            var bodySystem = await _context.BodySystems
                .Select(bs => new IconPathDto { ID = bs.ID, IconPath = "static/" + bs.IconPath })
                .FirstOrDefaultAsync(bs => bs.ID == id);

            if (bodySystem == null) return Errors.Models.OrgansDbError;

            return bodySystem;
        }

    }
}