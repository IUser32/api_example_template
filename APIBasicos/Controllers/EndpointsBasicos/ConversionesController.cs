using Microsoft.AspNetCore.Mvc;

namespace api_example_template.Controllers.EndpointsBasicos;

[ApiController]
[Route("api/endpoints-basicos/conversiones")]
public class ConversionesController : ControllerBase
{
    [HttpGet("celsius-a-fahrenheit")]
    public ActionResult<decimal> CelsiusAFahrenheit(decimal celsius)
    {
        return Ok((celsius * 9 / 5) + 32);
    }

    [HttpGet("fahrenheit-a-celsius")]
    public ActionResult<decimal> FahrenheitACelsius(decimal fahrenheit)
    {
        return Ok((fahrenheit - 32) * 5 / 9);
    }

    [HttpGet("kilometros-a-millas")]
    public ActionResult<decimal> KilometrosAMillas(decimal kilometros)
    {
        return Ok(kilometros * 0.621371m);
    }

    [HttpGet("millas-a-kilometros")]
    public ActionResult<decimal> MillasAKilometros(decimal millas)
    {
        return Ok(millas * 1.60934m);
    }
}
