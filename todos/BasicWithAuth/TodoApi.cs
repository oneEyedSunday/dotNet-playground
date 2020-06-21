using System;
using System.Text.Json;
using System.Threading.Tasks;
// for static IEndpointRouteBuilder  extensions Map*
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace BasicWithAuth
{
    public class TodoApi: IApi
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        private async Task GetAllAsync(TodoDbContext dbContext, HttpContext context)
        {
            var todos = await dbContext.Todos.ToArrayAsync();

            context.Response.ContentType = "application/json";
            await JsonSerializer.SerializeAsync<Todo []>(context.Response.Body, todos, _options);
        }

        private async Task GetAsync(TodoDbContext dbContext, HttpContext context)
        {
            if (!context.Request.RouteValues.TryGetValue("id", out object idObj))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

            if (!Int32.TryParse(idObj.ToString(), out int id))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

            // Find by id
            var todo = await dbContext.Todos.FindAsync(id);

            if (todo == null)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return;
            }

            await JsonSerializer.SerializeAsync(context.Response.Body, todo, _options);
        }

        private async Task PostAsync(TodoDbContext dbContext, HttpContext context)
        {
            var todo = await JsonSerializer.DeserializeAsync<Todo>(context.Request.Body, _options);

            await dbContext.AddAsync(todo);
            await dbContext.SaveChangesAsync();
        }

        private async Task DeleteAsync(TodoDbContext dbContext, HttpContext context)
        {
             if (!context.Request.RouteValues.TryGetValue("id", out object idObj))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

            if (!Int32.TryParse(idObj.ToString(), out int id))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

             var todo = await dbContext.Todos.FindAsync(id);

            if (todo == null)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return;
            }

            dbContext.Todos.Remove(todo);
            await dbContext.SaveChangesAsync();

        }

        public async Task PutAsync(TodoDbContext dbContext, HttpContext context)
        {
            var todoUpdates = await JsonSerializer.DeserializeAsync<Todo>(context.Request.Body, _options);
            if (!context.Request.RouteValues.TryGetValue("id", out object idObj))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

            if (!Int32.TryParse(idObj.ToString(), out int id))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

             var todo = await dbContext.Todos.FindAsync(id);

            if (todo == null)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return;
            }

            todo.Name = todoUpdates.Name;
            await dbContext.SaveChangesAsync();

        } 
        public void MapRoutes(IEndpointRouteBuilder endpoints)
        {
            string prefix = "/api/v2/todos";
            endpoints.MapGet(prefix, WithDbContext(GetAllAsync)).RequireAuthorization();
            endpoints.MapGet(prefix + "/{id}", WithDbContext(GetAsync)).RequireAuthorization(TodoPolicy.User);
            endpoints.MapPost(prefix, WithDbContext(PostAsync)).RequireAuthorization();
            endpoints.MapPut(prefix + "/{id}", WithDbContext(PutAsync)).RequireAuthorization(TodoPolicy.User);
            endpoints.MapDelete(prefix + "/{id}", WithDbContext(DeleteAsync)).RequireAuthorization(TodoPolicy.Admin);
        }

        private RequestDelegate WithDbContext(Func<TodoDbContext, HttpContext, Task> bareHandler) =>
            context =>
            {
                var db = context.RequestServices.GetRequiredService<TodoDbContext>();
                return bareHandler(db, context);
            };
    }
}
