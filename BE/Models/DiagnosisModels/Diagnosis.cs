using System.ComponentModel.DataAnnotations;

namespace HIPA_BE.Models
{
    public class Diagnosis
    {
        [Required]
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(60)]
        public required string Name { get; set; }
    }    
}