﻿namespace NSE.WebApp.MVC.Extensions;

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
			await HandleRequestExceptionAsync(context, ex);
		}
	}

	private static Task HandleRequestExceptionAsync(HttpContext context, CustomHttpRequestException ex)
	{
		if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
		{
			context.Response.Redirect("/login");
			return Task.CompletedTask;
		}

		context.Response.StatusCode = (int)ex.StatusCode;
		return Task.CompletedTask;
	}
}
