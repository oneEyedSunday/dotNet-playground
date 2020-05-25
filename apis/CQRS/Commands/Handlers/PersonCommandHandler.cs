using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CQRSDemo.Commands;
using CQRSDemo.Models;
using CQRSDemo.Services;
using AutoMapper;

namespace CQRSDemo.Commands.Handlers
{
    public class PeopleCommandHandler
    {
        private readonly DbContext _dbContext;
        private readonly CommandStoreService _commandStoreService;


        public PeopleCommandHandler(DbContext context, CommandStoreService commandStore)
        {
            _dbContext = context;
            _commandStoreService = commandStore;
        }

        public void Handle(AddPersonCommand command)
        {
            var person = new Person {
                FirstName = command.FirstName,
                MiddleName = command.MiddleName,
                LastName = command.LastName,
                NIN = command.NIN,
                Dob = command.Dob
            };

            _dbContext.Add(person);
            _dbContext.SaveChanges();

            _commandStoreService.Push(command);
        }
    }
}
