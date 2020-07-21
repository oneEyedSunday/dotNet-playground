using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.EntityFrameworkCore;

namespace WithControllers
{
    [ApiController]
    [Route("/api/todos")]
    public class TodoController: ControllerBase
    {
        private readonly TodoDbContext _db;
        public TodoController(TodoDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        [HttpGet]
        public async Task<List<Todo>> GetAll()
        {
            return await _db.Todos.ToListAsync();
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Todo>> Get(int id)
        {

            Todo todo = await _db.Todos.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }


            return todo;
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> Post(Todo todo)
        {
            var dbAction = await _db.Todos.AddAsync(todo);
            await _db.SaveChangesAsync();
            return Ok(dbAction.Entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Todo todo = await _db.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            _db.Todos.Remove(todo);
            await _db.SaveChangesAsync();
            return Ok();
        }


    }
}