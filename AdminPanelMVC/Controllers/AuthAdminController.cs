using AdminPanelMVC.Models.AuthAdmin;
using Ardalis.Result;
using AuthAPI.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanelMVC.Controllers;

public class AuthAdminController : Controller
{
	private readonly IHttpClientFactory _httpClientFactory;

    public AuthAdminController(IHttpClientFactory httpClientFactory)
    {
		_httpClientFactory = httpClientFactory;
    }

    [Route("/register")]
	[HttpGet]
	public IActionResult Register()
	{
		return View();
	}

	[Route("/register")]
	[HttpPost]
	public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var registerDto = new RegisterDto
		{
			Username = registerViewModel.Username,
			Email = registerViewModel.Email,
			Password = registerViewModel.Password
		};

		var client = _httpClientFactory.CreateClient("ApiClient");
		var response = await client.PostAsJsonAsync("api/auth/register", registerDto);

		if (response.IsSuccessStatusCode)
		{
			ViewBag.SuccessMessage = "Kayıt işlemi başarılı. Giriş yapabilirsiniz.";
			ModelState.Clear();
		}
		else
		{
			ModelState.AddModelError(string.Empty, "Kayıt işlemi başarısız. Lütfen tekrar deneyiniz.");
		}

		return View(registerViewModel);
	}
}
