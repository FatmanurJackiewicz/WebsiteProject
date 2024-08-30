using AdminPanelMVC.Models.Experiences;
using DataAPI.DTOs.Experiences;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanelMVC.Controllers;

[Authorize]
public class ExperiencesAdminController : Controller
{

    private readonly IHttpClientFactory _httpClientFactory;

    public ExperiencesAdminController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [Route("/experiences")]
    [HttpGet]
    public async Task<IActionResult> ExperiencesDetails()
    {
        var dataClient = _httpClientFactory.CreateClient("ApiClientData");
        var response = await dataClient.GetAsync($"api/experiences");

        if (!response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(ExperiencesNoPage));
        }

        var experiencesDto = await response.Content.ReadFromJsonAsync<DetailsExperienceDto>();

        var experiencesViewModel = new ExperiencesViewModel
        {
            Title = experiencesDto.Title,
            Company = experiencesDto.Company,
            StartDate = experiencesDto.StartDate,
            EndDate = experiencesDto.EndDate,
            Description = experiencesDto.Description
        };

        return View(experiencesViewModel);
    }


    [Route("/experiences-noPage")]
    [HttpGet]
    public async Task<IActionResult> ExperiencesNoPage()
    {
        return View();
    }

    [Route("/create-experiences")]
    [HttpGet]
    public IActionResult CreateExperiences()
    {
        return View();
    }

    [Route("/create-experiences")]
    [HttpPost]
    public async Task<IActionResult> CreateExperiences([FromForm] CreateExperiencesViewModel createExperiencesViewModel)
    {
        if (!ModelState.IsValid)
            return View(createExperiencesViewModel);

        var createExperiencesDto = new CreateExperienceDto
        {
            Title = createExperiencesViewModel.Title,
            Company = createExperiencesViewModel.Company,
            StartDate = createExperiencesViewModel.StartDate,
            EndDate = createExperiencesViewModel.EndDate,
            Description = createExperiencesViewModel.Description
        };

        var client = _httpClientFactory.CreateClient("ApiClientData");
        var response = await client.PostAsJsonAsync("api/experiences/createExperiences", createExperiencesDto);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            ViewBag.ErrorMessage = errorMessage;
            return View(createExperiencesViewModel);
        }

        return RedirectToAction(nameof(ExperiencesDetails));
    }


    [Route("/update-experiences/")]
    [HttpGet]
    public async Task<IActionResult> UpdateExperiences()
    {
        var dataClient = _httpClientFactory.CreateClient("ApiClientData");
        var response = await dataClient.GetAsync("api/experiences/");

        if (!response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(ExperiencesNoPage));
        }

        var experienceDto = await response.Content.ReadFromJsonAsync<UpdateExperienceDto>();

        var updateExperiencesViewModel = new UpdateExperiencesViewModel
        {
            Title = experienceDto.Title,
            Company = experienceDto.Company,
            StartDate = experienceDto.StartDate,
            EndDate = experienceDto.EndDate,
            Description = experienceDto.Description
        };

        return View(updateExperiencesViewModel);
    }



    [Route("/update-experiences")]
    [HttpPost]
    public async Task<IActionResult> UpdateExperiences([FromForm] UpdateExperiencesViewModel updateExperiencesViewModel)
    {
        if (!ModelState.IsValid)
            return View(updateExperiencesViewModel);

        var updateExperienceDto = new UpdateExperienceDto
        {
            Title = updateExperiencesViewModel.Title,
            Company = updateExperiencesViewModel.Company,
            StartDate = updateExperiencesViewModel.StartDate,
            EndDate = updateExperiencesViewModel.EndDate,
            Description = updateExperiencesViewModel.Description
        };

        var client = _httpClientFactory.CreateClient("ApiClientData");
        var response = await client.PostAsJsonAsync($"api/experiences/updateExperiences", updateExperienceDto);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            ViewBag.ErrorMessage = errorMessage;
            return View(updateExperiencesViewModel);
        }

        return RedirectToAction(nameof(ExperiencesDetails));
    }





















}
