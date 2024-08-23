using DataAPI.Data;
using DataAPI.DTOs.ContactMessages;
using DataAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactMessagesController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public ContactMessagesController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetContactMessages([FromRoute] int id)
    {
        var contactMessage = await _appDbContext.ContactMessages.FindAsync(id);

        if (contactMessage is null)
            return NotFound("Contact message not found");

        return Ok(contactMessage);
    }

    [HttpPost("createContactMessages")]
    public async Task<IActionResult> CreateContactMessages([FromBody] CreateContactMessages createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var newContactMessage = new ContactMessages
        {
            Name = createDto.Name,
            Email = createDto.Email,
            Subject = createDto.Subject,
            Message = createDto.Message,
            SentDate = createDto.SentDate,
            IsRead = createDto.IsRead,
            Reply = createDto.Reply,
            ReplyDate = createDto.ReplyDate
        };

        _appDbContext.ContactMessages.Add(newContactMessage);
        await _appDbContext.SaveChangesAsync();

        return Ok(newContactMessage);
    }

    [HttpPut("updateContactMessages/{id}")]
    public async Task<IActionResult> UpdateContactMessages([FromBody] UpdateContactMessages updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existContactMessage = await _appDbContext.ContactMessages.FindAsync(updateDto.Id);

        if (existContactMessage is null)
            return NotFound("Contact message not found");

        existContactMessage.Name = updateDto.Name;
        existContactMessage.Email = updateDto.Email;
        existContactMessage.Subject = updateDto.Subject;
        existContactMessage.Message = updateDto.Message;
        existContactMessage.SentDate = updateDto.SentDate;
        existContactMessage.IsRead = updateDto.IsRead;
        existContactMessage.Reply = updateDto.Reply;
        existContactMessage.ReplyDate = updateDto.ReplyDate;

        _appDbContext.ContactMessages.Update(existContactMessage);
        await _appDbContext.SaveChangesAsync();

        return Ok(existContactMessage);
    }

    [HttpDelete("deleteContactMessages/{id}")]
    public async Task<IActionResult> DeleteContactMessages([FromRoute] int id)
    {
        var existContactMessage = await _appDbContext.ContactMessages.FindAsync(id);

        if (existContactMessage == null)
        {
            return NotFound("Contact message not found");
        }

        _appDbContext.ContactMessages.Remove(existContactMessage);
        await _appDbContext.SaveChangesAsync();

        return Ok();
    }
}
