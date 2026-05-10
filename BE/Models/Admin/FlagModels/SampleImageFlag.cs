using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HIPA_BE.Models.Admin.FlagModels
{
    public class SampleImageFlag
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public required string UserID { get; set; }

        [Required]
        [ForeignKey("SampleImage")]
        public required int SampleImageID { get; set; }
        public SampleImage SampleImage{ get; set; }

        [Required]
        [ForeignKey("FlagType")]
        public required int FlagTypeID { get; set; }
        [Required]
        public required FlagType FlagType { get; set; }
    }
}