using FivePMSomewhereBlazorWASMApp;
using FivePMSomewhereEngine;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<ICountriesService, CountriesService>();
builder.Services.AddScoped<IFivePMSomewhereService, FivePMSomewhereService>();
builder.Services.AddScoped<ITimeZoneService, TimeZoneService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMudServices();

await builder.Build().RunAsync();
