using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Blazored.LocalStorage;
using Charon.Services;

namespace Charon
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient<CredentialHandler>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddHttpClient("Hades.API", client => client.BaseAddress = new Uri("https://hades.thescriptgroup.in"))
                .AddHttpMessageHandler<CredentialHandler>();
            builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Hades.API"));
            builder.Services.AddSingleton<EventsService>();

            await builder.Build().RunAsync();
        }
    }
}
