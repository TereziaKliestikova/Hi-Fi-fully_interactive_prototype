using HIPA_BE.Data;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using HIPA_BE.ServiceErrors;
using HIPA_BE.Models;
using HIPA_BE.Models.DiagnosisModels;

namespace HIPA_BE.Services.DiagnosisServices
{
    public class DiagnosisService : IDiagnosisService
    {
        private readonly AppDbContext _context;

        public DiagnosisService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<DiangosesListDto>> GetListOfAllDiagnoses()
        {
            try
            {
                var diagnosesList = await _context.Diagnoses
                    .Select(d => new Diagnosis { ID = d.ID, Name = d.Name })
                    .OrderBy(d => d.Name)
                    .ToListAsync();
                
                if (diagnosesList == null) return Errors.Models.DiagnosisDbError;
                return new DiangosesListDto {Diagnoses = diagnosesList};
            }
            catch (Exception)
            {
                return Errors.Models.DbError;
            }
        }
    }
}