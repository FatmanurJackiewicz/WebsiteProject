using DataAPI.Data;
using DataAPI.DTOs.Comments;
using DataAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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

            var comment = await _appDbContext.Comments.FindAsync(id);

            if (comment is null)
                return NotFound("Comments not found");

            return Ok(comment);
        }


        [HttpPost("createComment")]
        public async Task<IActionResult> CreateComments([FromBody] CreateCommentDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newComment = new Comments
            {
                Content = createDto.Content,
                CreatedDate = createDto.CreatedTime,
                IsApproved = createDto.IsApproved,
                BlogPostId = createDto.BlogPostId,
                UserId = createDto.UserId
            };

            _appDbContext.Comments.Add(newComment);
            await _appDbContext.SaveChangesAsync();

            return Ok(newComment);
        }

        [HttpPut("updateComment")]
        public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existComments = await _appDbContext.Comments.FindAsync(updateDto.Id);

            if (existComments is null)
                return NotFound("Comments not found");


            existComments.Content = updateDto.Content;
            existComments.CreatedDate = updateDto.CreatedTime;
            existComments.IsApproved = updateDto.IsApproved;
            existComments.BlogPostId = updateDto.BlogPostId;
            existComments.UserId = updateDto.UserId;
            
            _appDbContext.Comments.Update(existComments);
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

            _appDbContext.Comments.Remove(existComments);
            await _appDbContext.SaveChangesAsync();

            return Ok("Comment deleted");
        }


    }
}
