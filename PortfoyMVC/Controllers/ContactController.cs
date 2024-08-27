using DataAPI.DTOs.AboutMe;
using DataAPI.DTOs.ContactMessages;
using Microsoft.AspNetCore.Mvc;
using PortfoyMVC.Models;

namespace PortfoyMVC.Controllers;

public class ContactController : Controller
{
	private readonly IHttpClientFactory _httpClientFactory;

	public ContactController(IHttpClientFactory httpClientFactory)
	{
		_httpClientFactory = httpClientFactory;
	}

	[HttpPost]
	public async Task<IActionResult> SubmitContactForm([FromForm] ContactFormViewModel contactFormViewModel)
	{

		var contactFormDto = new CreateContactMessageDto
		{
			Name = contactFormViewModel.Name,
			Email = contactFormViewModel.Email,
			Subject = contactFormViewModel.Subject,
			Message = contactFormViewModel.Message,
			SentDate = contactFormViewModel.SentDate,
			IsRead = contactFormViewModel.IsRead,
			Reply = contactFormViewModel.Reply,
			ReplyDate = contactFormViewModel.ReplyDate
		};

		var client = _httpClientFactory.CreateClient("ApiClientData");
		var response = await client.PostAsJsonAsync("api/contactmessages/createContactMessages", contactFormDto);

		if (!response.IsSuccessStatusCode)
		{
			return RedirectToAction("Index", "Home");
		}

        TempData["SuccessMessage"] = "Contact messages başarıyla gönderildi.";

        return RedirectToAction("Index", "Home");
	}

}
