using DataAPI.Data;
using DataAPI.DTOs.BlogPosts;
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


        [HttpPost("createBlogPost")]
        public async Task<IActionResult> CreateBlogPosts([FromBody] CreateBlogPostsDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newBlogPosts = new BlogPosts
            {
                Title = createDto.Title,
                Context = createDto.Context,
                PublishDate = createDto.PublishDate,
                AuthorId = createDto.AuthorId
            };

            _appDbContext.BlogPosts.Add(newBlogPosts);
            await _appDbContext.SaveChangesAsync();

            return Ok(newBlogPosts);
        }

        [HttpPut("updateBlogPost")]
        public async Task<IActionResult> UpdateBlogPost([FromBody] UpdateBlogPostsDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existBlogPost = await _appDbContext.BlogPosts.FindAsync(updateDto.Id);

            if (existBlogPost is null)
                return NotFound("BlogPost not found");


            existBlogPost.Title = updateDto.Title;
            existBlogPost.Context = updateDto.Context;
            existBlogPost.PublishDate = updateDto.PublishDate;
            existBlogPost.AuthorId = updateDto.AuthorId;

            _appDbContext.BlogPosts.Update(existBlogPost);
            await _appDbContext.SaveChangesAsync();

            return Ok(existBlogPost);
        }


        [HttpDelete("deleteBlogPost/{id}")]
        public async Task<IActionResult> DeleteexistBlogPost([FromRoute] int id)
        {
            var existBlogPost = await _appDbContext.BlogPosts.FindAsync(id);

            if (existBlogPost == null)
            {
                return NoContent();
            }

            _appDbContext.BlogPosts.Remove(existBlogPost);
            await _appDbContext.SaveChangesAsync();

            return Ok();
        }

    }
}
