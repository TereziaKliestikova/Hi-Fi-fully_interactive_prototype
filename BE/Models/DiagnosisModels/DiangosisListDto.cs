using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HIPA_BE.Models.DiagnosisModels
{
    public class DiangosesListDto
    {
        [Required]
        public required List<Diagnosis> Diagnoses { get; set; }
    }
}