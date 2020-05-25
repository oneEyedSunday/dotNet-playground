using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CQRSDemo.DTO;
using CQRSDemo.Queries;
using CQRSDemo.Queries.Handlers;

namespace CQRSDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController: ControllerBase
    {
        public PeopleController(
            ILogger<PeopleController> logger,
            PeopleQueryHandler peopleQueryHandler
            )
        {
            _logger = logger;
            _peopleQueryHandler = peopleQueryHandler;

        }

        private readonly ILogger<PeopleController> _logger;
        private readonly PeopleQueryHandler _peopleQueryHandler;


        [HttpGet]
        public  IEnumerable<PersonDTO> Get()
        {
            _logger.LogInformation("Getting all persons");
            var query = new GetAllPeopleQuery();
            return _peopleQueryHandler.Handle(query);
        }

    }
}
