namespace atlascopcoass.Models;

public enum UnitCategory
{
    Length,
    Mass,
    Temperature
}

public class UnitDefinition
{
    public required string Id { get; init; }
    public required string DisplayName { get; init; }
    public required UnitCategory Category { get; init; }

    // Linear conversion factor relative to base unit (Base: Meter, Kilogram)
    public double ConversionFactorToBase { get; init; } = 1.0;

    // Non-linear conversion delegates (Celsius base for Temperature)
    public Func<double, double>? CustomToBase { get; init; }
    public Func<double, double>? CustomFromBase { get; init; }
}