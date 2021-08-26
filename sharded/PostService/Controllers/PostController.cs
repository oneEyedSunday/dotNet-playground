using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostService.Data;
using PostService.Entities;

namespace PostService.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly DataAccess _dataAccess;

        public PostsController(DataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetLatestPosts(string category, int count)
        {
            return await _dataAccess.ReadLatestPosts(category, count);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
            await _dataAccess.CreatePost(post);
            return NoContent();
        }

        [HttpPost("initDb")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public ActionResult InitDatabase([FromQuery] int countUsers, [FromQuery] int countCategories)
        {
            // run this in the BG
            Task.Run(() => _dataAccess.InitDatabase(countUsers, countCategories));
            return Accepted();
        }
    }
}
