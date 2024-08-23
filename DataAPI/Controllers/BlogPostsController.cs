using DataAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public BlogPostsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogPosts([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var educations = await _appDbContext.Educations.FindAsync(id);

            if (educations is null)
                return NotFound("Educations not found");

            return Ok(educations);
        }

    }
}
