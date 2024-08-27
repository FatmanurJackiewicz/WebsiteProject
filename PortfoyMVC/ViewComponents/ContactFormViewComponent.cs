using Microsoft.AspNetCore.Mvc;
using PortfoyMVC.Models;

namespace PortfoyMVC.ViewComponents;

public class ContactFormViewComponent : ViewComponent
{
	public IViewComponentResult Invoke()
	{
		var model = new ContactFormViewModel(); // Modeli doldur veya boş bırak
		return View(model);
	}
}
