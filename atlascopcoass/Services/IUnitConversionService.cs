using atlascopcoass.Models;

namespace atlascopcoass.Services;

public interface IUnitConversionService
{
    ConversionResponse Convert(ConversionRequest request);
    IEnumerable<object> GetSupportedUnits();
}