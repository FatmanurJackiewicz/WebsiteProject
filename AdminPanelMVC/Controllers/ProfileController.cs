using AdminPanelMVC.Models.AuthAdmin;
using AdminPanelMVC.Models.Profile;
using AuthAPI.Dto;
using AuthAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Security.Claims;

namespace AdminPanelMVC.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProfileController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("/details")]
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var userId = GetUserId();

            if (userId == null)
                return RedirectToAction("Login", "AuthAdmin");

            var client = _httpClientFactory.CreateClient("ApiClientData");
            var response = await client.GetAsync($"api/auth/user/{userId}");

            if (!response.IsSuccessStatusCode)
            {

                return RedirectToAction("Error", "Home");
            }

            var user = await response.Content.ReadFromJsonAsync<UserDto>();

            var userViewModel = new UserViewModel
            {
                Username = user.Username,
                Email = user.Email
            };

            return View(userViewModel);
        }

        [Route("/details")]
        [HttpPost()]
        public async Task<IActionResult> Update([FromForm] UserViewModel updateViewModel)
        {
            if (!ModelState.IsValid)
                return View("Details");

            var oldEmail = GetUserEmail();

            var updateUserDto = new UpdateUserDto
            {
                OldEmail = oldEmail,
                Username = updateViewModel.Username,
                Email = updateViewModel.Email
            };

            var client = _httpClientFactory.CreateClient("ApiClient");
            var response = await client.PostAsJsonAsync("api/auth/updateUser", updateUserDto);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Güncelleme işlemi başarısız.");
                return View(nameof(Details), updateViewModel);
            }

            var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponseDto>();

            HttpContext.Response.Cookies.Append("auth-cookie", tokenResponse.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = tokenResponse.Expiration
            });

            return View(nameof(Details));
        }

        [Route("/delete")]
        [HttpPost()]
        public async Task<IActionResult> Delete()
        {
            var userId = GetUserId();
            var client = _httpClientFactory.CreateClient("ApiClient");
            var response = await client.PostAsJsonAsync($"api/auth/delete/{userId}", new {});          

            HttpContext.Response.Cookies.Delete("auth-cookie");

            return RedirectToAction("Login", "AuthAdmin");
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

        protected string? GetUserEmail()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.Email)?.Value;           
            return userIdClaim;
        }
    }
}
