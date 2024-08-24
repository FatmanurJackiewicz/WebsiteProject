using AdminPanelMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdminPanelMVC.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Deneme()
		{
			return View();
		}
	}
}
