using AdminPanelMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace AdminPanelMVC.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			ViewBag.Username = GetUserName();
			return View();
		}

        [HttpGet]
        public IActionResult Deneme()
		{
			return View();
		}

		protected string? GetUserName()
		{
			var userNameClaim = User.FindFirst(ClaimTypes.Name)?.Value;
			
			return CapitalizeFirstLetter(userNameClaim);
		}

		public static string CapitalizeFirstLetter(string input)
		{
			if (string.IsNullOrEmpty(input))
				return input;

			return input.Substring(0, 1).ToUpper() + input.Substring(1);
		}
	}
}
