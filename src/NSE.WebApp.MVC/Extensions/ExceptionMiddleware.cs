using Refit;
using System.Net;

namespace NSE.WebApp.MVC.Extensions;

public class ExceptionMiddleware
{
	private readonly RequestDelegate _next;

	public ExceptionMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (CustomHttpRequestException ex)
		{
			await HandleRequestExceptionAsync(context, ex.StatusCode);
		}
		catch (ValidationApiException ex)
		{
            await HandleRequestExceptionAsync(context, ex.StatusCode);
        }
        catch (ApiException ex)
		{
            await HandleRequestExceptionAsync(context, ex.StatusCode);
        }

    }

	private static Task HandleRequestExceptionAsync(HttpContext context, HttpStatusCode statusCode)
	{
		if (statusCode == System.Net.HttpStatusCode.Unauthorized)
		{
            context.Response.Redirect($"/login?ReturnUrl={context.Request.Path}");
            return Task.CompletedTask;
		}

		context.Response.StatusCode = (int)statusCode;
		return Task.CompletedTask;
	}
}
