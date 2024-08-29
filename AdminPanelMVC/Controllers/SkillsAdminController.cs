using AdminPanelMVC.Models.PersonalInfo;
using AdminPanelMVC.Models.Skills;
using DataAPI.DTOs.PersonalInfo;
using DataAPI.DTOs.Skills;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanelMVC.Controllers;

[Authorize]
public class SkillsAdminController : Controller
{
	private readonly IHttpClientFactory _httpClientFactory;

	public SkillsAdminController(IHttpClientFactory httpClientFactory)
	{
		_httpClientFactory = httpClientFactory;
	}

	[Route("/skills")]
	[HttpGet]
	public async Task<IActionResult> SkillsDetails()
	{
		var dataClient = _httpClientFactory.CreateClient("ApiClientData");
		var response = await dataClient.GetAsync($"api/skills");
		if (!response.IsSuccessStatusCode)
		{
			return RedirectToAction(nameof(SkillsNoPage));
		}

		var skillsDto = await response.Content.ReadFromJsonAsync<SkillsDetailsDto>();

		var skillsViewModel = new SkillsViewModel
		{
			Description = skillsDto.Description,
		};

		return View(skillsViewModel);
	}


	public async Task<IActionResult> SkillsNoPage()
	{
		return View();
	}

	[Route("/create-skills")]
	[HttpGet]
	public IActionResult CreateSkills()
	{
		return View();
	}

	[Route("/create-skills")]
	[HttpPost]
	public async Task<IActionResult> CreateSkills([FromForm] CreateSkillsViewModel createSkillsViewModel)
	{
		if (!ModelState.IsValid)
			return View(createSkillsViewModel);

		// ViewModel'den DTO'ya dönüştürme işlemi yapılırken Skills listesini de ekliyoruz
		var createSkillsDto = new CreateSkillsDto
		{
			Description = createSkillsViewModel.Description,
			SkillsList = createSkillsViewModel.SkillsList // Skills listesini ekliyoruz
		};

		var client = _httpClientFactory.CreateClient("ApiClientData");
		var response = await client.PostAsJsonAsync("api/skills/createSkills", createSkillsDto);

		if (!response.IsSuccessStatusCode)
		{
			var errorMessage = await response.Content.ReadAsStringAsync();
			ViewBag.ErrorMessage = errorMessage;
			return View(createSkillsDto);
		}

		return RedirectToAction(nameof(SkillsDetails));
	}



	[Route("/update-skills")]
	[HttpGet]
	public IActionResult UpdatePersonalInfo()
	{
		return View();
	}


	[Route("/update-skills")]
	[HttpPost]
	public async Task<IActionResult> UpdateSkills([FromForm] UpdateSkillsViewModel updateSkillsViewModel)
	{
		if (!ModelState.IsValid)
			return View(updateSkillsViewModel);

		// ViewModel'den DTO'ya dönüştürme işlemi yapılırken Skills listesini de ekliyoruz
		var updateSkillsDto = new UpdateSkillsDto
		{ // Güncellemek istediğiniz kaydın Id'si
			Description = updateSkillsViewModel.Description,
			SkillsList = updateSkillsViewModel.SkillsList // Skills listesini ekliyoruz
		};

		var client = _httpClientFactory.CreateClient("ApiClientData");
		var response = await client.PutAsJsonAsync($"api/skills/updateSkills", updateSkillsDto);

		if (!response.IsSuccessStatusCode)
		{
			var errorMessage = await response.Content.ReadAsStringAsync();
			ViewBag.ErrorMessage = errorMessage;
			return View(updateSkillsViewModel);
		}

		return RedirectToAction(nameof(SkillsDetails));
	}


}
