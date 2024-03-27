using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorApp;
using BlazorApp.AuthProviders;
using BlazorApp.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IAuthenticationService,AuthenticationService>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthProvider>();
builder.Services.AddScoped<ProfileService, ProfileService>();
builder.Services.AddScoped<LocationService, LocationService>();


builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();

var host = builder.Build();

await host.RunAsync();