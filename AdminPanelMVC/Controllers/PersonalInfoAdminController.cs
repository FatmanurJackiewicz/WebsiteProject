using AdminPanelMVC.Models.AboutMe;
using AdminPanelMVC.Models.PersonalInfo;
using Azure;
using DataAPI.DTOs.AboutMe;
using DataAPI.DTOs.PersonalInfo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanelMVC.Controllers;

[Authorize]
public class PersonalInfoAdminController : Controller
{
	private readonly IHttpClientFactory _httpClientFactory;

	public PersonalInfoAdminController(IHttpClientFactory httpClientFactory)
	{
		_httpClientFactory = httpClientFactory;
	}

	[Route("personal-details")]
	[HttpGet]
	public async Task<IActionResult> PersonalInfoDetails()
	{
		var dataClient = _httpClientFactory.CreateClient("ApiClientData");
		var response = await dataClient.GetAsync($"api/personalinfo/personal-info");

		if (!response.IsSuccessStatusCode)
		{
			return RedirectToAction(nameof(PersonalInfoNoPage));
		}

		var personalInfoDto = await response.Content.ReadFromJsonAsync<PersonalInfoDetailsDto>();

		var personalInfoViewModel = new PersonalInfoDetailsViewModel
		{
			About = personalInfoDto.About,
			Name = personalInfoDto.Name,
			Surname = personalInfoDto.Surname,
			BirthDate = personalInfoDto.BirthDate
		};

		return View(personalInfoViewModel);
	}

	[Route("/personalInfo-nopage")]
	[HttpGet]
	public async Task<IActionResult> PersonalInfoNoPage()
	{
		return View();
	}

	[Route("/create-personalInfo")]
	[HttpGet]
	public IActionResult CreatePersonalInfo()
	{
		return View();
	}

	[Route("/create-personalInfo")]
	[HttpPost]
	public async Task<IActionResult> CreatePersonalInfo([FromForm] CreatePersonalInfoViewModel createPersonalInfoViewModel)
	{
		if (!ModelState.IsValid)
			return View(createPersonalInfoViewModel);

		var createPersonalInfoDto = new CreatePersonalInfoDto
		{
			About = createPersonalInfoViewModel.About,
			Name = createPersonalInfoViewModel.Name,
			Surname = createPersonalInfoViewModel.Surname,
			BirthDate = createPersonalInfoViewModel.BirthDate
		};

		var client = _httpClientFactory.CreateClient("ApiClientData");
		var response = await client.PostAsJsonAsync("api/personalinfo/createPersonalInfo", createPersonalInfoDto);

		if (!response.IsSuccessStatusCode)
		{
			var errorMessage = await response.Content.ReadAsStringAsync();
			ViewBag.ErrorMessage = errorMessage;
			return View(createPersonalInfoDto);
		}

		return RedirectToAction(nameof(PersonalInfoDetails));
	}

	[Route("/update-personalInfo")]
	[HttpGet]
	public IActionResult UpdatePersonalInfo()
	{
		return View();
	}


	[Route("/update-personalInfo")]
	[HttpPost]
	public async Task<IActionResult> UpdatePersonalInfo([FromForm] UpdatePersonalInfoViewModel updatePersonalInfoViewModel)
	{
		if (!ModelState.IsValid)
			return View(updatePersonalInfoViewModel);

		var updatePersonalInfoDto = new UpdatePersonalInfoDto
		{
			About = updatePersonalInfoViewModel.About,
			Name = updatePersonalInfoViewModel.Name,
			Surname = updatePersonalInfoViewModel.Surname,
			BirthDate = updatePersonalInfoViewModel.BirthDate
		};

		var client = _httpClientFactory.CreateClient("ApiClientData");
		var response = await client.PostAsJsonAsync("api/personalinfo/updatePersonalInfo", updatePersonalInfoDto);

		if (!response.IsSuccessStatusCode)
		{
			var errorMessage = await response.Content.ReadAsStringAsync();
			ViewBag.ErrorMessage = errorMessage;
			return View(updatePersonalInfoViewModel);
		}

		return RedirectToAction(nameof(PersonalInfoDetails));
	}
}
