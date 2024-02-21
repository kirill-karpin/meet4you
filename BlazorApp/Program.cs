using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorApp;
using BlazorApp.AuthProviders;
using BlazorApp.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IAuthenticationService,AuthenticationService>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthProvider>();
builder.Services.AddScoped<ProfileService, ProfileService>();


builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();


await builder.Build().RunAsync();