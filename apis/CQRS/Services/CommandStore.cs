using Microsoft.EntityFrameworkCore;
using CQRSDemo.Models;
using System;
using System.Text.Json;

namespace CQRSDemo.Services
{
    public class CommandStoreService
    {
        private readonly DbContext _dbContext;

        public CommandStoreService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Push(object command)
        {
            _dbContext.Set<Command>().Add(
                new Command
                {
                    Type = command.GetType().Name,
                    Data = JsonSerializer.Serialize(command),
                    CreatedAt = DateTime.Now
                }
            );
            _dbContext.SaveChanges();
        }
    }
}
