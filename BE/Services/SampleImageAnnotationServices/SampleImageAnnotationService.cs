using HIPA_BE.Models.SampleImageAnnotationModels;
using ErrorOr;
using HIPA_BE.ServiceErrors;
using HIPA_BE.Data;
using Microsoft.EntityFrameworkCore;

namespace HIPA_BE.Services.SampleImageAnnotationServices
{
    public class SampleImageAnnotationService : ISampleImageAnnotationService
    {
        private readonly AppDbContext _context;

        public SampleImageAnnotationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<SampleImageAnnotationsListDto>> GetSampleImageAnnotationsById(int id)
        {
            try
            {
                // find every SampleImageAnnotation with the given id as SampleImageId
                // return a list of SampleImageAnnotationsListDto
                var sampleAnnotations = await _context.SampleImageAnnotations
                    .Where(sia => sia.SampleImageID == id)
                    .Select(sia => new SampleImageAnnotationDto
                    {
                        ID = sia.ID,
                        Name = sia.Name,
                        Description = sia.Description,
                        BoundingBox = sia.BoundingBox
                    })
                    .ToListAsync();
                
                if (sampleAnnotations == null) return Errors.Models.SampleImageAnnotationDbError;
                return new SampleImageAnnotationsListDto { SampleImageAnnotations = sampleAnnotations };
            }
            catch (Exception)
            {
                return Errors.Models.DbError;
            }
        }
    }
}