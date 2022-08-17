using BuberDinner.Application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        var (statusCode, message) = exception switch
        {
            DuplicateEmailExceptions => (StatusCodes.Status409Conflict, "Email already exist."),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred."),
        };

        return Problem(title: message, statusCode: statusCode);
    }
}