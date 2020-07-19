using System;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UsersService.Domain.Requests;
using Microsoft.AspNetCore.Http;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UsersService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {

        private readonly ILogger<RegistrationController> _logger;


        public RegistrationController(ILogger<RegistrationController> logger)
        {
            _logger = logger;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult RegisterTopic(TopicRegistration request)
        {
            _logger.LogInformation($"Registering topic {request.topicId} for user {request.userId}");
            return Ok();
        }
    }
}
