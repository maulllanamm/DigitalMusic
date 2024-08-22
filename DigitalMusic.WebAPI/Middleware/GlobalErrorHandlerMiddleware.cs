using DigitalMusic.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace DigitalMusic.WebAPI.Middleware
{
    public class GlobalErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (BadRequestException ex)
            {
                var detail = new ProblemDetails()
                {
                    Detail = "Validation errors occurred.",
                    Instance = httpContext.Request.Path,
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = "Bad Request",
                    Type = "https://example.com/bad-request", 
                    Extensions =
                {
                    ["errors"] = ex.Errors,
                    ["traceId"] = httpContext.TraceIdentifier,
                    ["timestamp"] = DateTime.UtcNow
                }
                };

                var response = JsonSerializer.Serialize(detail, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(response);
            }
            catch (NotFoundException ex)
            {

                var detail = new ProblemDetails()
                {
                    Detail = ex.Message,
                    Instance = httpContext.Request.Path,
                    Status = (int)HttpStatusCode.NotFound,
                    Title = "Resource not found",
                    Type = "https://example.com/not-found" 
                };

                var response = JsonSerializer.Serialize(detail, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(response);

                return;
            }
            catch (UnauthorizedException ex)
            {

                var detail = new ProblemDetails()
                {
                    Detail = ex.Message,
                    Instance = httpContext.Request.Path,
                    Status = (int)HttpStatusCode.Unauthorized,
                    Title = "Unauthorized",
                    Type = "https://example.com/unauthorize" 
                };

                var response = JsonSerializer.Serialize(detail, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(response);

                return;
            }
            catch (ForbiddenException ex)
            {

                var detail = new ProblemDetails()
                {
                    Detail = ex.Message,
                    Instance = httpContext.Request.Path,
                    Status = (int)HttpStatusCode.Forbidden,
                    Title = "Don't have permission",
                    Type = "https://example.com/forbidden" 
                };

                var response = JsonSerializer.Serialize(detail, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(response);

                return;
            }
            catch (Exception e)
            {
                var detail = new ProblemDetails()
                {
                    Detail = e.Message,
                    Instance = httpContext.Request.Path,
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = "An unexpected error occurred!",
                    Type = "https://example.com/internal-server-error", 
                    Extensions =
                {
                    ["traceId"] = httpContext.TraceIdentifier,
                    ["timestamp"] = DateTime.UtcNow,
                    ["stackTrace"] = e.StackTrace
                }
                };

                var response = JsonSerializer.Serialize(detail, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(response);
            }
        }
    }
}
