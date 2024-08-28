using AdminPanelMVC.Models.AuthAdmin;
using Ardalis.Result;
using AuthAPI.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanelMVC.Controllers;

public class AuthAdminController : Controller
{
	private readonly IHttpClientFactory _httpClientFactory;
	public IActionResult Index()
	{
		return View();
	}
	public AuthAdminController(IHttpClientFactory httpClientFactory)
	{
		_httpClientFactory = httpClientFactory;
	}

	[Route("/login")]
	[HttpGet]
	public IActionResult Login()
	{
		return View();
	}

	[Route("/login")]
	[HttpPost]
	public async Task<IActionResult> Login([FromForm] LoginViewModel loginViewModel)
	{
		if (!ModelState.IsValid)
		{
			return View(loginViewModel);
		}

		var loginDto = new LoginDto
		{
			Email = loginViewModel.Email,
			Password = loginViewModel.Password,
			Project = "admin"
		};

		var client = _httpClientFactory.CreateClient("ApiClient");
		var response = await client.PostAsJsonAsync("api/auth/login", loginDto);

		if (response.IsSuccessStatusCode)
		{
			var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponseDto>();

			HttpContext.Response.Cookies.Append("auth-cookie", tokenResponse.AccessToken, new CookieOptions
			{
				HttpOnly = true,
				Secure = true,
				SameSite = SameSiteMode.Strict,
				Expires = tokenResponse.Expiration
			});

			return RedirectToAction("Index", "Home");
		}
		else
		{
			var errorMessage = await response.Content.ReadAsStringAsync();
			ModelState.AddModelError(string.Empty, errorMessage);
			return View(loginViewModel);
		}
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
			return View(registerViewModel);

		var registerDto = new RegisterDto
		{
			Username = registerViewModel.Username,
			Email = registerViewModel.Email,
			Password = registerViewModel.Password,
			Project = registerViewModel.Project
		};

		var client = _httpClientFactory.CreateClient("ApiClient");
		var response = await client.PostAsJsonAsync("api/auth/register", registerDto);

		if (response.IsSuccessStatusCode)
		{
			ViewBag.SuccessMessage = "Registration is successful. You can log in.";
			ModelState.Clear();
		}
		else
		{
			var errorMessage = await response.Content.ReadAsStringAsync();
			ModelState.AddModelError(string.Empty, errorMessage);
			return View(registerViewModel);
		}

		return RedirectToAction("Login");
	}

	[Route("/forgot-password")]
	[HttpGet]
	public IActionResult ForgotPassword()
	{
		return View();
	}

	[Route("/forgot-password")]
	[HttpPost]
	public async Task<IActionResult> ForgotPassword([FromForm] ForgotPasswordViewModel forgotPasswordViewModel)
	{
		if (!ModelState.IsValid)
		{
			return View(forgotPasswordViewModel);
		}

		var forgotPasswordDto = new ForgotPasswordRequestDto
		{
			Email = forgotPasswordViewModel.Email
		};

		var client = _httpClientFactory.CreateClient("ApiClient");
		var response = await client.PostAsJsonAsync("api/auth/forgot-password", forgotPasswordDto);

		if (response.IsSuccessStatusCode)
		{
			ViewBag.SuccessMessage = "Password reset email has been sent. Please check your email.";
			ModelState.Clear();
		}
		else
		{
			var errorMessage = await response.Content.ReadAsStringAsync();
			ViewBag.ErrorMessage = errorMessage;
			return View(forgotPasswordViewModel);
		}

		return View(forgotPasswordViewModel);
	}

	[Route("/renew-password/{verificationCode}")]
	[HttpGet]
	public IActionResult RenewPassword([FromRoute] string verificationCode)
	{
		if (string.IsNullOrEmpty(verificationCode))
		{
			return RedirectToAction(nameof(ForgotPassword));
		}

		var model = new RenewPasswordViewModel { VerificationCode = verificationCode };
		return View(model);
	}

	[Route("/renew-password/{verificationCode}")]
	[HttpPost]
	public async Task<IActionResult> RenewPassword([FromForm] RenewPasswordViewModel renewPasswordViewModel)
	{
		if (!ModelState.IsValid)
		{
			return View(renewPasswordViewModel);
		}

		var input = renewPasswordViewModel.VerificationCode;
		int index = input.IndexOf('=') + 1;  // '=' karakterinin pozisyonunu bul ve sonrasına git
		string verificationCode = input.Substring(index);

		var renewPasswordDto = new RenewPasswordRequestDto
		{
			VerificationCode = verificationCode,
			NewPassword = renewPasswordViewModel.NewPassword
		};

		var client = _httpClientFactory.CreateClient("ApiClient");
		var response = await client.PostAsJsonAsync("api/auth/renew-password", renewPasswordDto);

		if (response.IsSuccessStatusCode)
		{
			ViewBag.SuccessMessage = "Password reset successfully. You can log in.";
			return RedirectToAction(nameof(Login));
		}
		else
		{
			ModelState.AddModelError(string.Empty, "Password reset failed. Please try again.");
			View(renewPasswordViewModel);
		}

		return View(renewPasswordViewModel);
	}

	[Route("/logout")]
	[HttpGet]
	public IActionResult Logout()
	{
		HttpContext.Response.Cookies.Delete("auth-cookie");
		return RedirectToAction(nameof(Login));
	}
}
