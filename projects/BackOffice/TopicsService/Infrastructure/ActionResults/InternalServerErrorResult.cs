using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TopicsService.Infrastructure.ActionResults
{
    public class InternalServerErrorResult : ObjectResult
    {
        public InternalServerErrorResult(object error)
            : base(error)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}