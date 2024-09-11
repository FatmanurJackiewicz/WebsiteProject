using AdminPanelMVC.Controllers;
using AdminPanelMVC.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AdminPanelMVC
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews(options =>
			{
				options.Filters.Add(new UserIdentityFilter());
			});

			builder.Services.AddHttpClient("ApiClient", client =>
			{
				client.BaseAddress = new Uri("http://authapi.fatmanur-km102.gturkmen.net/");

			});/*.AddHttpMessageHandler<AuthenticationDelegatingHandler>();*/

			builder.Services.AddHttpClient("ApiClientData", client =>
			{
				client.BaseAddress = new Uri("http://dataapi.fatmanur-km102.gturkmen.net/");

			});/*.AddHttpMessageHandler<AuthenticationDelegatingHandler>();*/

			builder.Services.AddHttpClient("ApiClientFile", client =>
			{
				client.BaseAddress = new Uri("http://fileapi.fatmanur-km102.gturkmen.net/");

			});/*.AddHttpMessageHandler<AuthenticationDelegatingHandler>();*/

		

			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = builder.Configuration["Jwt:Issuer"],
					ValidAudience = builder.Configuration["Jwt:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
				};
				options.Events = new JwtBearerEvents
				{
					OnMessageReceived = context =>
					{
						if (context.Request.Cookies.ContainsKey("auth-cookie"))
						{
							context.Token = context.Request.Cookies["auth-cookie"];
						}
						return Task.CompletedTask;
					},
					OnChallenge = context =>
					{
						// Token geçersizse veya yoksa yönlendirme iþlemi
						context.Response.Redirect("/login");
						context.HandleResponse();
						return Task.CompletedTask;
					}
				};
				options.MapInboundClaims = true;
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			//app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
