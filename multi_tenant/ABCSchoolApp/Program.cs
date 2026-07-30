using App.Infrastructure;
using App.Infrastructure.Extensions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace ABCSchoolApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            var apiSettings = builder.Configuration.GetSection("ApiSettings").Get<ApiSettings>()
    ?? throw new InvalidOperationException("Missing 'ApiSettings' section in appsettings.json");

            builder.Services.AddSingleton(apiSettings);

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.AddClientServices();
            await builder.Build().RunAsync();
        }
    }
}
