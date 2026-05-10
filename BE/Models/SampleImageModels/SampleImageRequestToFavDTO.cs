using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HIPA_BE.Models.SampleImageModels
{
    public class SampleImageRequestToFavDto
    {
        [Required]
        public int SampleId { get; set; }

    }
}