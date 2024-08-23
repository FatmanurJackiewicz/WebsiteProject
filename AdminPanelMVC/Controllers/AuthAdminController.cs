using AdminPanelMVC.Models.AuthAdmin;
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
	[HttpGet]
	public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);


		return View();
	}
}
