using AdminPanelMVC.Models.Skills;
using DataAPI.DTOs.Skills;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AdminPanelMVC.Controllers
{
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
            var response = await dataClient.GetAsync("api/skills");
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(SkillsNoPage));
            }

            var skillsDto = await response.Content.ReadFromJsonAsync<SkillsDetailsDto>();

            var skillsViewModel = new SkillsViewModel
            {
                Description = skillsDto.Description,
                SkillsList = skillsDto.SkillsList // Eklenmesi gereken alan
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

            var createSkillsDto = new CreateSkillsDto
            {
                Description = createSkillsViewModel.Description,
                SkillsList = createSkillsViewModel.SkillsList
            };

            var client = _httpClientFactory.CreateClient("ApiClientData");
            var response = await client.PostAsJsonAsync("api/skills/createSkills", createSkillsDto);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ViewBag.ErrorMessage = errorMessage;
                return View(createSkillsViewModel);
            }

            return RedirectToAction(nameof(SkillsDetails));
        }

        [Route("/update-skills")]
        [HttpGet]
        public async Task<IActionResult> UpdateSkills(int id) // ID ile formu almak için
        {
            var dataClient = _httpClientFactory.CreateClient("ApiClientData");
            var response = await dataClient.GetAsync($"api/skills/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(SkillsNoPage));
            }

            var skillsDto = await response.Content.ReadFromJsonAsync<UpdateSkillsDto>();

            var updateSkillsViewModel = new UpdateSkillsViewModel
            {
                Description = skillsDto.Description,
                SkillsList = skillsDto.SkillsList
            };

            return View(updateSkillsViewModel);
        }

        [Route("/update-skills")]
        [HttpPost]
        public async Task<IActionResult> UpdateSkills([FromForm] UpdateSkillsViewModel updateSkillsViewModel)
        {
            if (!ModelState.IsValid)
                return View(updateSkillsViewModel);

            var updateSkillsDto = new UpdateSkillsDto
            {
                Description = updateSkillsViewModel.Description,
                SkillsList = updateSkillsViewModel.SkillsList
            };

            var client = _httpClientFactory.CreateClient("ApiClientData");
            var response = await client.PutAsJsonAsync("api/skills/updateSkills", updateSkillsDto);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ViewBag.ErrorMessage = errorMessage;
                return View(updateSkillsViewModel);
            }

            return RedirectToAction(nameof(SkillsDetails));
        }
    }
}
