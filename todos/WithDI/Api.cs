using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WithDI
{
    public class Api
    {
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public async Task GetAllAsync(TodoDbContext db, HttpContext context)
        {
            var todos = await db.Todos.Take(5).ToArrayAsync();
            await context.Response.WriteJsonAsync(todos, _options);
        }

        public async Task GetAsync(TodoDbContext db, HttpContext context)
        {
            if (context.Request.RouteValues.TryGet("id", out int _id))
            {
                Todo todo = await db.Todos.FindAsync(_id);
                if (todo != null)
                {
                    await context.Response.WriteJsonAsync(todo, _options);
                }
                else
                {
                    context.Response.StatusCode = 404;
                    return;
                }
            }
            else
            {
                context.Response.StatusCode = 400;
                return;
            }
        }

        public async Task PostAsync(TodoDbContext db, HttpContext context)
        {
            var todo = await context.Request.ReadJsonAsync<Todo>(_options);

            await db.Todos.AddAsync(todo);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(TodoDbContext db, HttpContext context)
        {
            if (!context.Request.RouteValues.TryGet("id", out int id))
            {
                context.Response.StatusCode = 400;
                return;
            }

            var todo = await db.Todos.FindAsync(id);
            if (todo == null)
            {
                context.Response.StatusCode = 404;
                return;
            }

            db.Todos.Remove(todo);
            await db.SaveChangesAsync();
        }

        private RequestDelegate WithDbContext(Func<TodoDbContext, HttpContext, Task> handler)
        {
            return context =>
            {
                var db = context.RequestServices.GetRequiredService<TodoDbContext>();
                return handler(db, context);
            };
        }

        public void MapRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/api/todos", WithDbContext(GetAllAsync));
            endpoints.MapGet("/api/todos/{id}", WithDbContext(GetAsync));
            endpoints.MapPost("/api/todos", WithDbContext(PostAsync));
            endpoints.MapDelete("/api/todos/{id}", WithDbContext(DeleteAsync));
        }
    }
}