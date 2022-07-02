﻿using System;
using System.Globalization;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using GistCopy.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace GistCopy.WebApi.Middlewares;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExcepitonAsync(context, exception);
        }
    }

    private Task HandleExcepitonAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = "";

        switch (exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException.Errors);
                break;
            case NotFoundException:
                code = HttpStatusCode.NotFound;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) code;

        if (result == string.Empty)
        {
            result = JsonSerializer.Serialize(new {errpr = exception.Message});
        }

        return context.Response.WriteAsync(result);
    }
}