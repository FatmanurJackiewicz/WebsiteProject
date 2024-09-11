using PortfoyMVC.FiltersPortfoy;

namespace PortfoyMVC
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddHttpClient("ApiClient", client =>
			{
				client.BaseAddress = new Uri("https://localhost:7150/");

			});/*.AddHttpMessageHandler<AuthenticationDelegatingHandler>();*/

			builder.Services.AddHttpClient("ApiClientData", client =>
			{
				client.BaseAddress = new Uri("https://localhost:7241/");

			});/*.AddHttpMessageHandler<AuthenticationDelegatingHandler>();*/

			builder.Services.AddHttpClient("ApiClientFile", client =>
			{
				client.BaseAddress = new Uri("https://localhost:7069/");

			});/*.AddHttpMessageHandler<AuthenticationDelegatingHandler>();*/

			builder.Services.AddControllersWithViews(options =>
			{
				options.Filters.Add<MainFilter>(); // 'new MainFilter()' de?il, tür olarak ekliyoruz
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
