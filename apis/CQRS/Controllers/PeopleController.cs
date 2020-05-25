using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CQRSDemo.DTO;
using CQRSDemo.Queries;
using CQRSDemo.Queries.Handlers;
using CQRSDemo.Requests;
using CQRSDemo.Commands;
using CQRSDemo.Commands.Handlers;

namespace CQRSDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController: ControllerBase
    {
        public PeopleController(
            ILogger<PeopleController> logger,
            PeopleQueryHandler peopleQueryHandler,
            PeopleCommandHandler peopleCommandHandler
            )
        {
            _logger = logger;
            _peopleQueryHandler = peopleQueryHandler;
            _peopleCommandHandler = peopleCommandHandler;

        }

        private readonly ILogger<PeopleController> _logger;
        private readonly PeopleQueryHandler _peopleQueryHandler;

        private readonly PeopleCommandHandler _peopleCommandHandler;


        [HttpGet]
        public  IEnumerable<PersonDTO> Get()
        {
            _logger.LogInformation("Getting all persons");
            var query = new GetAllPeopleQuery();
            return _peopleQueryHandler.Handle(query);
        }

        [HttpPost]
        public void AddPerson([FromBody] AddPersonRequest request)
        {
            _logger.LogInformation("Adding a person");
            var command = AddPersonCommand.ConvertFromRequest(request);
            _peopleCommandHandler.Handle(command);
        }

    }
}
