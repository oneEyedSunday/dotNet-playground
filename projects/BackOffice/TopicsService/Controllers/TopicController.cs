using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TopicsService.Domain.Exceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TopicsService.Controllers
{
    [Route("api/[controller]")]
    public class TopicController : Controller
    {
        private Topic[] Topics = {
            new Topic { IsAvailable = false, Name = "Football", Description = "All banter about the beautiful game", Id = 1 },
            new Topic { IsAvailable = true, Name = "Mekwe Twitter", Description = "🌚", Id = 69 },
            new Topic { IsAvailable = false, Name = "Bruno Fernandez", Description = "I'd admit Bruno has changed United's fortunes", Id = 10 },
            new Topic { IsAvailable = true, Name = "Shekpeteri Twitter", Description = "🌚 + 🍻", Id = 420 },
            new Topic { IsAvailable = false, Name = "Suit & Tie Twitter", Description = "These guys really dont tweet with context", Id = 4 }
        };
        private readonly ILogger<TopicController> _logger;

        public TopicController(ILogger<TopicController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(Topics.Where(_topic => _topic.IsAvailable));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            try
            {
                _logger.LogInformation($"Getting topic by id {id}");
                var topic = Topics.First(_topic => _topic.IsAvailable && _topic.Id == id);
                return Ok(topic);
            }
            catch (System.InvalidOperationException ex)
            {
                _logger.LogWarning(ex.Message);
                throw new TopicDomainNotFoundException(id);
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw ex;
            }
        }
    }
}
