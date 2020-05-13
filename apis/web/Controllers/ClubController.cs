using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using web.Models;
using web.DTO;

namespace web.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ClubController: ControllerBase
  {
    private readonly ILogger<ClubController> _logger;
    private IMapper _mapper;

    private static List<Club> clubs = new List<Club> {
      Club.FromName("Real Madrid C.F")
        .FoundedAt("1918-03-20")
        .WithStadia("Santiago Bernebeau")
        .WithNickNames(new string[] {
          "Los Merengues", "The Galaticos", "Los Blancos"
        })
    };


    public ClubController(ILogger<ClubController> logger, IMapper mapper)
    {
      _logger = logger;
      _mapper = mapper;
    }


    [HttpGet]
    public IEnumerable<ClubDTO> Get()
    {
      _logger.LogInformation($"Raw: {clubs.ElementAt(0).Name}");
      _logger.LogInformation($"Mapped: {_mapper.Map<ClubDTO>(clubs.ElementAt(0)).Name}");
      return _mapper.Map<List<Club>, ClubDTO[]>(clubs);
    }

    [HttpGet]
    [Route("{clubId}")]
    public ClubDTO GetAction()
    {
      _logger.LogInformation(String.Format("Name: {0} Nicknames: {1}", clubs.ElementAt(0).Name, String.Join(',', clubs.ElementAt(0).NickNames)));
      return _mapper.Map<ClubDTO>(clubs.ElementAt(0));
    }

  }
}
