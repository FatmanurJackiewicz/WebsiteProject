using DataAPI.DTOs.ContactMessages;
using Microsoft.AspNetCore.Mvc;
using PortfoyMVC.Models;
using System.Net;
using System.Net.Mail;

namespace PortfoyMVC.Controllers
{
	public class ContactController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private const string FromEmail = "denemebackend105@gmail.com"; // Gönderen e-posta adresi
		private const string FromEmailPassword = "boyc tvfg jkgp thpk"; // App-specific şifre
		private const string ToEmail = "fatmanurjackiewicz@gmail.com";  // Alıcı e-posta adresi

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

			// İletişim formu verilerini API'ye gönder
			var client = _httpClientFactory.CreateClient("ApiClientData");
			var response = await client.PostAsJsonAsync("api/contactmessages/createContactMessages", contactFormDto);

			if (!response.IsSuccessStatusCode)
			{
				return RedirectToAction("Index", "Home");
			}

			// İletişim formunu dolduran kişinin bilgilerini e-posta ile gönder
			await SendContactEmailAsync(contactFormViewModel);

			TempData["SuccessMessage"] = "Contact message sent successfully.";
			return RedirectToAction("Index", "Home");
		}

		private async Task SendContactEmailAsync(ContactFormViewModel contactForm)
		{
			const string smtpHost = "smtp.gmail.com";
			const int smtpPort = 587;

			using (SmtpClient client = new(smtpHost, smtpPort)
			{
				Credentials = new NetworkCredential(FromEmail, FromEmailPassword),
				EnableSsl = true
			})
			{
				MailMessage mail = new()
				{
					From = new MailAddress(FromEmail),
					Subject = contactForm.Subject,
					Body = $"<p><strong>İsim:</strong> {contactForm.Name}</p>" +
						   $"<p><strong>Email:</strong> {contactForm.Email}</p>" +
						   $"<p><strong>Mesaj:</strong> {contactForm.Message}</p>",
					IsBodyHtml = true
				};

				mail.To.Add(ToEmail);
				await client.SendMailAsync(mail);
			}
		}
	}
}
