using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PortfoyMVC.Models;

[Bind("Name, Email, Subject, Message")]
public class ContactFormViewModel
{
	[Required, MaxLength(50)]
	public string Name { get; set; } = string.Empty;

	[Required, MaxLength(100)]
	public string Email { get; set; } = string.Empty;

	[Required, MaxLength(100)]
	public string Subject { get; set; } = string.Empty;

	[Required]
	public string Message { get; set; } = string.Empty;

	public DateTime SentDate { get; set; } = DateTime.UtcNow;

	public bool IsRead { get; set; } = false;

	public string Reply { get; set; } = string.Empty;

	public DateTime? ReplyDate { get; set; }
}
