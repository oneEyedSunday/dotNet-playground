using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CQRSDemo.Queries;
using CQRSDemo.Models;


namespace CQRSDemo.Queries.Handlers
{
    public class PeopleQueryHandler
    {
        private readonly DbContext _dbContext;

        public PeopleQueryHandler(DbContext context)
        {
            _dbContext = context;
        }

        public IEnumerable<Person> Handle(GetAllPeopleQuery _)
        {
           return _dbContext.Set<Person>()
            .Include(p => p.Goals)
            .ToArray();
        }
    }
}
