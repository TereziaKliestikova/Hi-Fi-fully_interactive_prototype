using System.ComponentModel.DataAnnotations;

namespace HIPA_BE.Contracts
{
    public record SampleImageNoteRequest([Required] string? Note);
}