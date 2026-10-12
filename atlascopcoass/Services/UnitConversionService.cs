using atlascopcoass.Models;

namespace atlascopcoass.Services;

public class UnitConversionService : IUnitConversionService
{
    private readonly UnitRegistry _registry;

    public UnitConversionService(UnitRegistry registry)
    {
        _registry = registry;
    }

    public ConversionResponse Convert(ConversionRequest request)
    {
        if (!_registry.TryGet(request.FromUnit, out var fromUnit) || fromUnit is null)
            throw new ArgumentException($"Unit '{request.FromUnit}' is not recognized.");

        if (!_registry.TryGet(request.ToUnit, out var toUnit) || toUnit is null)
            throw new ArgumentException($"Unit '{request.ToUnit}' is not recognized.");

        if (fromUnit.Category != toUnit.Category)
            throw new InvalidOperationException($"Cannot convert between different categories: '{fromUnit.Category}' and '{toUnit.Category}'.");

        double convertedValue;

        if (fromUnit.Category == UnitCategory.Temperature)
        {
            double baseValue = fromUnit.CustomToBase!(request.Value);
            convertedValue = toUnit.CustomFromBase!(baseValue);
        }
        else
        {
            double baseValue = request.Value * fromUnit.ConversionFactorToBase;
            convertedValue = baseValue / toUnit.ConversionFactorToBase;
        }

        return new ConversionResponse(
            OriginalValue: request.Value,
            FromUnit: fromUnit.DisplayName,
            ConvertedValue: Math.Round(convertedValue, 6),
            ToUnit: toUnit.DisplayName,
            Category: fromUnit.Category.ToString()
        );
    }

    public IEnumerable<object> GetSupportedUnits() =>
        _registry.GetAll().Select(u => new
        {
            u.Id,
            u.DisplayName,
            Category = u.Category.ToString()
        });
}