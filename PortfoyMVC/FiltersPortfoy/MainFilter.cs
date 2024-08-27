using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DataAPI.DTOs.AboutMe;
using DataAPI.DTOs.PersonalInfo;

namespace PortfoyMVC.FiltersPortfoy
{
	public class MainFilter : IAsyncActionFilter
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public MainFilter(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			// AboutMe - Section
			var aboutMeArray = await AboutMeDetails();
			if (context.Controller is Controller controller)
			{
				controller.ViewBag.Introduction = aboutMeArray[0];
				controller.ViewBag.ImageUrl1 = aboutMeArray[1];
				controller.ViewBag.ImageUrl2 = aboutMeArray[2];
			}

			//PersonalInfoSection
			var personalInfoArray = await PersonalInfoDetails();
			if (context.Controller is Controller controller2)
			{
				controller2.ViewBag.About = personalInfoArray[0];
				controller2.ViewBag.Name = personalInfoArray[1];
				controller2.ViewBag.Surname = personalInfoArray[2];
				controller2.ViewBag.BirthDate = personalInfoArray[3];
			}


			await next(); // Bu, Action'ın yürütülmesine devam edilmesini sağlar
		}

		private async Task<string[]> AboutMeDetails()
		{
			var dataClient = _httpClientFactory.CreateClient("ApiClientData");
			var response = await dataClient.GetAsync($"api/aboutme");

			var aboutMeDto = await response.Content.ReadFromJsonAsync<DetailsAboutMeDto>();

			string[] aboutMeDetails = new string[3];

			aboutMeDetails[0] = aboutMeDto.Introduction;
			aboutMeDetails[1] = aboutMeDto.ImageUrl1;
			aboutMeDetails[2] = aboutMeDto.ImageUrl2;

			return aboutMeDetails;
		}

		private async Task<string[]> PersonalInfoDetails()
		{
			var dataClient = _httpClientFactory.CreateClient("ApiClientData");
			var response = await dataClient.GetAsync($"api/personalinfo/personal-info");

			var personalInfoDetailsDto = await response.Content.ReadFromJsonAsync<PersonalInfoDetailsDto>();

			string[] personalInfoDetails = new string[4];

			personalInfoDetails[0] = personalInfoDetailsDto.About;
			personalInfoDetails[1] = personalInfoDetailsDto.Name;
			personalInfoDetails[2] = personalInfoDetailsDto.Surname;
			personalInfoDetails[3] = personalInfoDetailsDto.BirthDate.ToString("d");

			return personalInfoDetails;
		}
	}
}
