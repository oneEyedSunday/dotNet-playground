using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Todos
{
    public class TodoApi
    {
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public async Task GetAll(HttpContext context)
        {
            using var db = new TodoDbContext();
            var todos = await db.Todos.ToListAsync();

            context.Response.ContentType = "application/json";
            await JsonSerializer.SerializeAsync(context.Response.Body, todos, _options);
        }

        public async Task Get(HttpContext context)
        {
            var id = (string)context.Request.RouteValues["id"];
            if (id == null || !long.TryParse(id, out var todoId))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await JsonSerializer.SerializeAsync(context.Response.Body, new {
                    message = "An Error Occured"
                });
                return;
            }

            using var db = new TodoDbContext();
            var todo = await db.Todos.FindAsync(todoId);
            if (todo == null)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await JsonSerializer.SerializeAsync(context.Response.Body, new {
                    message = "Todo Not Found"
                });
                return;
            }

            context.Response.ContentType = "application/json";
            await JsonSerializer.SerializeAsync(context.Response.Body, todo, _options);
        }

        public async Task Post(HttpContext context)
        {
            var todo = await JsonSerializer.DeserializeAsync<Todo>(context.Request.Body, _options);

            using var db = new TodoDbContext();
            db.Todos.Add(todo);
            await db.SaveChangesAsync();
            await JsonSerializer.SerializeAsync(context.Response.Body, new {
                message = "Successfully created Todo"
            });
        }

        public async Task Delete(HttpContext context)
        {
            var id = (string)context.Request.RouteValues["id"];
            if (id == null || !long.TryParse(id, out var todoId))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

            using var db = new TodoDbContext();
            var todo = await db.Todos.FindAsync(todoId);
            if (todo == null)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await JsonSerializer.SerializeAsync(context.Response.Body, new {
                    message = "Todo Not Found"
                });
                return;
            }

            db.Todos.Remove(todo);
            await db.SaveChangesAsync();
            await JsonSerializer.SerializeAsync(context.Response.Body, new {
                message = "Todo Not Found"
            });
        }

        public void MapRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/api/todos", GetAll);
            endpoints.MapGet("/api/todos/{id}", Get);
            endpoints.MapPost("/api/todos", Post);
            endpoints.MapPost("/api/todos/{id}", Delete);
            endpoints.MapGet("/api", async (HttpContext context) =>
            {
                context.Response.StatusCode = StatusCodes.Status409Conflict;
                await JsonSerializer.SerializeAsync(context.Response.Body, new {
                    message = "Welcome to this api.",
                    meta = "Hey, I figured how to write arbitary json"
                });
                return;
            });
        }
    }
}