using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIAvanzados.Seguridad;

public class ApiKeyAttribute : Attribute, IAsyncActionFilter
{
    private const string HeaderName = "X-API-Key";

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var apiKey = configuration["ApiKey"];

        if (!context.HttpContext.Request.Headers.TryGetValue(HeaderName, out var receivedApiKey))
        {
            context.Result = new UnauthorizedObjectResult("Debe enviar una API Key.");
            return;
        }

        if (string.IsNullOrWhiteSpace(apiKey) || receivedApiKey != apiKey)
        {
            context.Result = new UnauthorizedObjectResult("La API Key no es válida.");
            return;
        }

        await next();
    }
}
