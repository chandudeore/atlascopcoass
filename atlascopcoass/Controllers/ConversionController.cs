using Microsoft.AspNetCore.Mvc;
using atlascopcoass.Models;
using atlascopcoass.Services;

namespace atlascopcoass.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConversionController : ControllerBase
{
    private readonly IUnitConversionService _conversionService;

    public ConversionController(IUnitConversionService conversionService)
    {
        _conversionService = conversionService;
    }

    /// <summary>
    /// Converts a value from one unit to another.
    /// </summary>
    [HttpPost("convert")]
    [ProducesResponseType(typeof(ConversionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Convert([FromBody] ConversionRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = _conversionService.Convert(request);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Lists all supported units and their categories.
    /// </summary>
    [HttpGet("units")]
    public IActionResult GetUnits() => Ok(_conversionService.GetSupportedUnits());
}