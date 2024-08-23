using DataAPI.Data;
using DataAPI.DTOs.BlogPosts;
using DataAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CommentsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComments([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var blogPosts = await _appDbContext.BlogPosts.FindAsync(id);

            if (blogPosts is null)
                return NotFound("Comments not found");

            return Ok(blogPosts);
        }


        [HttpPost("createComment")]
        public async Task<IActionResult> CreateComments([FromBody] CreateCommentsDto createDto)
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

        [HttpPut("updateComment")]
        public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentsDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existComments = await _appDbContext.Comments.FindAsync(updateDto.Id);

            if (existComments is null)
                return NotFound("Comments not found");


            existComments.Title = updateDto.Title;
            existComments.Context = updateDto.Context;
            existComments.PublishDate = updateDto.PublishDate;
            existComments.AuthorId = updateDto.AuthorId;

            _appDbContext.BlogPosts.Update(existComments);
            await _appDbContext.SaveChangesAsync();

            return Ok(existComments);
        }


        [HttpDelete("deleteComment/{id}")]
        public async Task<IActionResult> DeleteComments([FromRoute] int id)
        {
            var existComments = await _appDbContext.Comments.FindAsync(id);

            if (existComments == null)
            {
                return NoContent();
            }

            _appDbContext.BlogPosts.Remove(existComments);
            await _appDbContext.SaveChangesAsync();

            return Ok();
        }


    }
}
