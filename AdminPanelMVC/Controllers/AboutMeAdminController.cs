using AdminPanelMVC.Models.AboutMe;
using DataAPI.DTOs.AboutMe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace AdminPanelMVC.Controllers;

[Authorize]
public class AboutMeAdminController : Controller
{
	private readonly IHttpClientFactory _httpClientFactory;

	public AboutMeAdminController(IHttpClientFactory httpClientFactory)
	{
		_httpClientFactory = httpClientFactory;
	}

	[Route("/aboutMe")]
	[HttpGet]
	public async Task<IActionResult> AboutMeDetails()
	{        
		var dataClient = _httpClientFactory.CreateClient("ApiClientData");
		var response = await dataClient.GetAsync($"api/aboutme");

		if (!response.IsSuccessStatusCode)
		{
			return RedirectToAction(nameof(AboutMeNoPage));
		}

		var aboutMeDto = await response.Content.ReadFromJsonAsync<DetailsAboutMeDto>();

		var aboutMeViewModel = new AboutMeViewModel
		{
			Introduction = aboutMeDto.Introduction,
			Image1Url = aboutMeDto.ImageUrl1,
			Image2Url = aboutMeDto.ImageUrl2
		};

		return View(aboutMeViewModel);
	}

	[Route("/aboutMe-nopage")]
	[HttpGet]
	public async Task<IActionResult> AboutMeNoPage()
	{
		return View();       
	}

	
	[Route("/create-aboutMe")]
	[HttpGet]
	public IActionResult CreateAboutMe()
	{       
		return View();
	}

	[Route("/create-aboutMe")]
	[HttpPost]
	public async Task<IActionResult> CreateAboutMe([FromForm] CreateAboutMeViewModel createAboutMeViewModel)
	{
		if (!ModelState.IsValid)
			return View(createAboutMeViewModel);

		var userId = GetUserId();

		var fileList = new List<IFormFile> { createAboutMeViewModel.Image1, createAboutMeViewModel.Image2 };

		var fileClient = _httpClientFactory.CreateClient("ApiClientFile");
		string[] urls = new string[2];
		var i = 0;
		foreach (var image in fileList)
		{
			var imageContent = new MultipartFormDataContent();
			imageContent.Add(new StreamContent(image.OpenReadStream()), "file", image.FileName);

			var imageResponse = await fileClient.PostAsync("api/file/upload", imageContent);

			if (!imageResponse.IsSuccessStatusCode)
			{
				var errorResponse = await imageResponse.Content.ReadAsStringAsync();
				ModelState.AddModelError(string.Empty, $"Görsel yüklenirken bir hata oluştu: {errorResponse}");
				return View(createAboutMeViewModel);
			}

			var responseContent = await imageResponse.Content.ReadFromJsonAsync<JsonElement>();
			urls[i] = responseContent.GetProperty("url").GetString();  // "url" anahtarını küçük harfle arıyoruz
			i++;
		}

		var createAboutMeDto = new CreateAboutMeDto
		{
			Introduction = createAboutMeViewModel.Introduction,
			ImageUrl1 = urls[0],
			ImageUrl2 = urls[1]
		};

		var client2 = _httpClientFactory.CreateClient("ApiClientData");
		var response2 = await client2.PostAsJsonAsync("api/aboutme/createAboutMe", createAboutMeDto);

		if (!response2.IsSuccessStatusCode)
		{
			var errorMessage = await response2.Content.ReadAsStringAsync();
			ViewBag.ErrorMessage = errorMessage;
			return View(createAboutMeViewModel);
		}

		return RedirectToAction(nameof(AboutMeDetails));
	}


	[Route("/update-aboutMe")]
	[HttpGet]
	public IActionResult UpdateAboutMe()
	{
		return View();
	}

	[Route("/update-aboutMe")]
	[HttpPost]
	public async Task<IActionResult> UpdateAboutMe([FromForm] UpdateAboutMeViewModel updateAboutMeViewModel)
	{
		if (!ModelState.IsValid)
			return View(updateAboutMeViewModel);

		var userId = GetUserId();

		var fileList = new List<IFormFile>();
		fileList.Add(updateAboutMeViewModel.Image1);
		fileList.Add(updateAboutMeViewModel.Image2);

		var fileClient = _httpClientFactory.CreateClient("ApiClientFile");
		string[] urls = new string[2];
		var i = 0;
		foreach (var image in fileList)
		{
			var imageContent = new MultipartFormDataContent();
			imageContent.Add(new StreamContent(image.OpenReadStream()), "file", image.FileName);

			var imageResponse = await fileClient.PostAsync("api/file/upload", imageContent);

			if (!imageResponse.IsSuccessStatusCode)
			{
				var errorResponse = await imageResponse.Content.ReadAsStringAsync();
				ModelState.AddModelError(string.Empty, $"Görsel yüklenirken bir hata oluştu: {errorResponse}");
				return View(updateAboutMeViewModel);
			}

			var responseContent = await imageResponse.Content.ReadFromJsonAsync<JsonElement>();
			urls[i] = responseContent.GetProperty("url").GetString();  // "url" anahtarını küçük harfle arıyoruz
			i++;
		}

		var updateAboutMeDto = new UpdateAboutMeDto
		{
			Introduction = updateAboutMeViewModel.Introduction,
			ImageUrl1 = urls[0],
			ImageUrl2 = urls[1]
		};

		var client2 = _httpClientFactory.CreateClient("ApiClientData");
		var response2 = await client2.PostAsJsonAsync("api/aboutme/updateAboutMe", updateAboutMeDto);

		if (!response2.IsSuccessStatusCode)
		{
			var errorMessage = await response2.Content.ReadAsStringAsync();
			ViewBag.ErrorMessage = errorMessage;
			return View(updateAboutMeViewModel);
		}

		return RedirectToAction(nameof(AboutMeDetails));
	}

	protected int? GetUserId()
	{
		var userIdClaim = User.FindFirst(ClaimTypes.Sid)?.Value;
		if (int.TryParse(userIdClaim, out int userId))
		{
			return userId;
		}
		return null;
	}
	public static string CapitalizeFirstLetter(string input)
	{
		if (string.IsNullOrEmpty(input))
			return input;

		return input.Substring(0, 1).ToUpper() + input.Substring(1);
	}

	private static string ExtractFileName(string path)
	{
		// Path'in son bölümünü alarak dosya adını elde eder
		return path.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries)[^1];
	}

	public class JsonData
	{
		public string DbPath { get; set; }
	}
}
