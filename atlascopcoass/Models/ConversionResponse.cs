namespace atlascopcoass.Models;

public record ConversionResponse(
    double OriginalValue,
    string FromUnit,
    double ConvertedValue,
    string ToUnit,
    string Category
);