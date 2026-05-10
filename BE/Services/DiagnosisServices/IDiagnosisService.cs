using ErrorOr;
using HIPA_BE.Models;
using HIPA_BE.Models.DiagnosisModels;

namespace HIPA_BE.Services.DiagnosisServices
{
    public interface IDiagnosisService
    {
        /// <summary>
        /// Loads a list of all diagnoses in the database
        /// </summary>
        /// <returns>
        /// Dto containing a list of all diagnoses with IDs and names. Ordered by name in ascending order.
        /// <example>
        /// Example of a response:
        /// <code>
        ///     [
        ///       {
        ///         "id": 1,
        ///         "name": "diagnosis1"
        ///       },
        ///     ]
        /// </code>
        /// <example>
        /// </returns>
        public Task<ErrorOr<DiangosesListDto>> GetListOfAllDiagnoses();
    }
}