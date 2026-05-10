using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HIPA_BE.Models.SampleImageAnnotationModels
{
    public class SampleImageAnnotation
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(60)]
        public required string Name { get; set; }

        [Required]
        [StringLength(256)]
        public required string Description { get; set; }
        
        [Required]
        [ForeignKey("SampleImage")]
        public required int SampleImageID { get; set; }
        public SampleImage? SampleImage { get; set; }
        
        // column BoundingBox as geo-json
        [Required]
        [Column(TypeName = "jsonb")]
        public required string BoundingBox { get; set; }
    }    
}