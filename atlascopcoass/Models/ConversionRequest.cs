using System.ComponentModel.DataAnnotations;

namespace atlascopcoass.Models;

public record ConversionRequest(
    [Required] double Value,
    [Required] string FromUnit,
    [Required] string ToUnit
);