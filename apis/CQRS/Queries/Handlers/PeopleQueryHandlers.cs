using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CQRSDemo.Queries;
using CQRSDemo.Models;
using CQRSDemo.DTO;
using AutoMapper;


namespace CQRSDemo.Queries.Handlers
{
    public class PeopleQueryHandler
    {
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;

        public PeopleQueryHandler(DbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public IEnumerable<PersonDTO> Handle(GetAllPeopleQuery _)
        {
           return _mapper.Map<PersonDTO[]>(_dbContext.Set<Person>()
            .Include(p => p.Goals)
            .ToArray());
        }
    }
}
