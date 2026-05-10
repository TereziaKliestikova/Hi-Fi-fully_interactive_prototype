using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HIPA_BE.Models
{
    public class FavoriteSample
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public required string UserID { get; set; }
        [Required]
        [ForeignKey("SampleImage")]
        public required int SampleImageID { get; set; }
        public required SampleImage SampleImage { get; set; }
    }
}