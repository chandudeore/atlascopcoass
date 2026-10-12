using atlascopcoass.Models;

namespace atlascopcoass.Services;

public class UnitRegistry
{
    private readonly Dictionary<string, UnitDefinition> _units = new(StringComparer.OrdinalIgnoreCase);

    public UnitRegistry()
    {
        RegisterDefaultUnits();
    }

    public void Register(UnitDefinition unit) => _units[unit.Id] = unit;

    public bool TryGet(string unitId, out UnitDefinition? unit) => _units.TryGetValue(unitId, out unit);

    public IEnumerable<UnitDefinition> GetAll() => _units.Values;

    private void RegisterDefaultUnits()
    {
        // --- Length (Base: Meter) ---
        Register(new UnitDefinition { Id = "meter", DisplayName = "Meters", Category = UnitCategory.Length, ConversionFactorToBase = 1.0 });
        Register(new UnitDefinition { Id = "kilometer", DisplayName = "Kilometers", Category = UnitCategory.Length, ConversionFactorToBase = 1000.0 });
        Register(new UnitDefinition { Id = "centimeter", DisplayName = "Centimeters", Category = UnitCategory.Length, ConversionFactorToBase = 0.01 });
        Register(new UnitDefinition { Id = "millimeter", DisplayName = "Millimeters", Category = UnitCategory.Length, ConversionFactorToBase = 0.001 });
        Register(new UnitDefinition { Id = "mile", DisplayName = "Miles", Category = UnitCategory.Length, ConversionFactorToBase = 1609.344 });
        Register(new UnitDefinition { Id = "foot", DisplayName = "Feet", Category = UnitCategory.Length, ConversionFactorToBase = 0.3048 });
        Register(new UnitDefinition { Id = "inch", DisplayName = "Inches", Category = UnitCategory.Length, ConversionFactorToBase = 0.0254 });

        // --- Mass / Weight (Base: Kilogram) ---
        Register(new UnitDefinition { Id = "kilogram", DisplayName = "Kilograms", Category = UnitCategory.Mass, ConversionFactorToBase = 1.0 });
        Register(new UnitDefinition { Id = "gram", DisplayName = "Grams", Category = UnitCategory.Mass, ConversionFactorToBase = 0.001 });
        Register(new UnitDefinition { Id = "milligram", DisplayName = "Milligrams", Category = UnitCategory.Mass, ConversionFactorToBase = 0.000001 });
        Register(new UnitDefinition { Id = "pound", DisplayName = "Pounds", Category = UnitCategory.Mass, ConversionFactorToBase = 0.45359237 });
        Register(new UnitDefinition { Id = "ounce", DisplayName = "Ounces", Category = UnitCategory.Mass, ConversionFactorToBase = 0.02834952 });

        // --- Temperature (Base: Celsius) ---
        Register(new UnitDefinition
        {
            Id = "celsius",
            DisplayName = "Celsius",
            Category = UnitCategory.Temperature,
            CustomToBase = val => val,
            CustomFromBase = val => val
        });
        Register(new UnitDefinition
        {
            Id = "fahrenheit",
            DisplayName = "Fahrenheit",
            Category = UnitCategory.Temperature,
            CustomToBase = val => (val - 32.0) * 5.0 / 9.0,
            CustomFromBase = val => (val * 9.0 / 5.0) + 32.0
        });
        Register(new UnitDefinition
        {
            Id = "kelvin",
            DisplayName = "Kelvin",
            Category = UnitCategory.Temperature,
            CustomToBase = val => val - 273.15,
            CustomFromBase = val => val + 273.15
        });
    }
}