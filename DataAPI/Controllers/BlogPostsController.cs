using DataAPI.Data;
using DataAPI.DTOs.Educations;
using DataAPI.Models;
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

            var blogPosts = await _appDbContext.BlogPosts.FindAsync(id);

            if (blogPosts is null)
                return NotFound("BlogPosts not found");

            return Ok(blogPosts);
        }


        [HttpPost("createBlogPosts")]
        public async Task<IActionResult> CreateBlogPosts([FromBody] CreateBlogPostsDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //business rules
            if (createDto.StartDate > createDto.EndDate)
                return BadRequest("Start date cannot be later than end date.");

            var newEducation = new Educations
            {
                Degree = createDto.Degree,
                School = createDto.School,
                StartDate = createDto.StartDate,
                EndDate = createDto.EndDate
            };

            _appDbContext.Educations.Add(newEducation);
            await _appDbContext.SaveChangesAsync();

            return Ok(newEducation);
        }
    }
}
