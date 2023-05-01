using System.Net;
using ManageContacts.Model.Abstractions.Responses;
using ManageContacts.Shared.Exceptions;
using Newtonsoft.Json;

namespace ManageContacts.WebApi.Middlewares;

public class HandleExceptionMiddleware : IMiddleware
{
    private readonly ILogger<HandleExceptionMiddleware> _logger;

    public HandleExceptionMiddleware(ILogger<HandleExceptionMiddleware> logger)
    {
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
            _logger.LogError(ex, ex.Message);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        BaseResponseModel response = new BaseResponseModel(HttpStatusCode.InternalServerError, exception.Message);
        
        if (exception is NotFoundException)
        {
            response.StatusCode = HttpStatusCode.NotFound;
        }
        else if (exception is BadRequestException)
        {
            response.StatusCode = HttpStatusCode.BadRequest;
        }
        else if (exception is ForbiddenRequestException)
        {
            response.StatusCode = HttpStatusCode.Forbidden;
        }
        else if (exception is UnauthorizedAccessException)
        {
            response.StatusCode = HttpStatusCode.Unauthorized;
        }
        else if (exception is InternalServerException)
        {
            response.StatusCode = HttpStatusCode.InternalServerError;
        }
        else if (exception is UnauthorizedException)
        {
            response.StatusCode = HttpStatusCode.Unauthorized;
        }
        else if (exception is InvalidDataException)
        {
            response.StatusCode = HttpStatusCode.BadRequest;
        }
        else response.StatusCode = HttpStatusCode.InternalServerError;

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.OK;

        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
}