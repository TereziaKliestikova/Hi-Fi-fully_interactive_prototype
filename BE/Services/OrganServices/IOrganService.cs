using ErrorOr;
using HIPA_BE.Models.OrganModels;
using HIPA_BE.Models.BaseModels;

namespace HIPA_BE.Services.OrganServices
{
    public interface IOrganService
    {
        /// <summary>
        /// Loads a list of all organs in the database
        /// </summary>
        /// <returns>
        /// Dto containing a list of all organs
        /// <example>
        /// Example of a response:
        /// <code>
        ///    {
        ///     "organs": [
        ///       {
        ///         "id": 1,
        ///         "name": "Brain",
        ///         "diagnoses": ["diagnosisA", "diagnosisB", ...]
        ///       },
        ///    }
        /// </code>
        /// <example>
        /// </returns>
        public Task<ErrorOr<OrgansListDto>> GetListOfAllOrgans();

        /// <summary>
        /// Returns an organ icon image from the file system based on the organ id
        /// </summary>
        /// <param name="iconName">name of the organ which is supposed to be returned</param>
        /// <returns>
        /// Byte array containing the image
        /// </returns>
        public Task<ErrorOr<byte[]>> GetOrganIcon(string iconName);

        /// <summary>
        /// Loads organ name, description and a list of sample images with diagnoses
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// <example>
        /// <code>
        /// {
        ///     "organDescription": {
        /// 	    "id": -3,
        /// 	    "name": "MOZOG",
        /// 	    "description": "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."
        ///     },
        ///     "sampleImages": [
        /// 	    {
        /// 		    "id": -6,
        /// 		    "name": "OBR3.3",
        /// 		    "diagnoses": "diagnosis1"
        /// 	    },
        ///     ]
        /// }
        /// </code>
        /// </example>
        /// </returns>
        public Task<ErrorOr<OrganDetailDto>> GetOrganDetailById(int id, string userId);

        /// <summary>
        /// Fetches the path to the icon specified by an ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Path to the icon</returns>
        public Task<ErrorOr<IconPathDto>> GetOrganIconPathById(int id);
    }
}