using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UsersService.Domain.Requests;
using Microsoft.AspNetCore.Http;
using UsersService.Infrastructure.Notifiers;
using UsersService.Application.Events;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UsersService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {

        private readonly ILogger<RegistrationController> _logger;
        private readonly ISendMessage _sender;


        public RegistrationController(
            ILogger<RegistrationController> logger,
            ISendMessage sender
            )
        {
            _logger = logger;
            _sender = sender;
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
        public async Task<IActionResult> RegisterTopic(TopicRegistration request)
        {
            _logger.LogInformation($"Registering topic {request.topicId.ToString()} for user {request.userId}");
            await _sender.SendMessage(new UserSubscribedToTopicEvent {
                UserId = request.userId,
                TopicIds = request.topicId
            }, "topicsService");
            return Ok();
        }
    }
}
