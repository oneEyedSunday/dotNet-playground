using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TopicsService.Domain.Exceptions;
using TopicsService.Infrastructure.ActionResults;
using Microsoft.AspNetCore.Mvc;

namespace TopicsService.Infrastructure.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;
        public HttpGlobalExceptionFilter(
            IWebHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger
        )
        {
            _env = env;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {

            if (context.Exception.GetType() == typeof(TopicDomainNotFoundException))
            {
                var json = new JsonErrorResponse
                {
                    Messages = new[] { context.Exception.Message }
                };

                context.Result = new NotFoundObjectResult(json);
                context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            else if (context.Exception is TopicDomainInvalidOperationException)
            {
                var json = new JsonErrorResponse
                {
                    Messages = new[] { "An unexpected error occurred. Try it again." }
                };

                if (_env.IsDevelopment())
                {
                    json.DeveloperMessage = context.Exception.ToString();
                }

                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            else
            {
                var json = new JsonErrorResponse
                {
                    Messages = new[] { "An unexpected error occurred. Try it again." }
                };

                if (_env.IsDevelopment())
                {
                    json.DeveloperMessage = context.Exception.ToString();
                }

                context.Result = new InternalServerErrorResult(json);
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
            context.ExceptionHandled = true;
        }

        private class JsonErrorResponse
        {
            public string[] Messages { get; set; }

            public object DeveloperMessage { get; set; }
        }
    }
}