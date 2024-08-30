using DataAPI.DTOs.Skills;
using Microsoft.AspNetCore.Mvc;
using PortfoyMVC.Models;

namespace PortfoyMVC.ViewComponents;

public class SkillsViewComponent : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public SkillsViewComponent(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {

        var dataClient = _httpClientFactory.CreateClient("ApiClientData");
        var response = await dataClient.GetAsync($"api/skills");

        if (!response.IsSuccessStatusCode)
        {
            var errorResponse = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Bir hata oluştu: {errorResponse}");
            return View();
        }

        var skillsDetailsDto = await response.Content.ReadFromJsonAsync<SkillsDetailsDto>();

        var skillsPortfoyViewModel = new SkillsPortfoyViewModel
        {
            SkillsList = skillsDetailsDto.SkillsList
        };

        return View(skillsPortfoyViewModel);
    }
}
