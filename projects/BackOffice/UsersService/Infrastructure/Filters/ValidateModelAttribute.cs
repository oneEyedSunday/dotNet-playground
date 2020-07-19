using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace UsersService.Infrastructure.Filters
{
    public class ValidateModelAttribute: ActionFilterAttribute
    {
        private readonly ILogger<ValidateModelAttribute> _logger;
        private readonly IWebHostEnvironment _env;
        public ValidateModelAttribute(
            IWebHostEnvironment env,
            ILogger<ValidateModelAttribute> logger
        )
        {
            _env = env;
            _logger = logger;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                _logger.LogError($"Bad data passed to model {context.HttpContext.Request.Path}");
                
                var json = new JsonErrorResponse {
                    Messages = context.ModelState
                };

                if (_env.IsDevelopment())
                {
                    json.DeveloperMessage = context.ModelState.ToString();
                }

                // displays Joi'ish error message
                // context.Result = new UnprocessableEntityObjectResult(json);
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
                context.HttpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            }
        }

        private class JsonErrorResponse
        {
            public ModelStateDictionary Messages { get; set; }

            public object DeveloperMessage { get; set; }
        }
    }

    
}