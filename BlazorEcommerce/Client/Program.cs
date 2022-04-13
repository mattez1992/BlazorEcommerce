global using BlazorEcommerce.Shared.Models;
global using BlazorEcommerce.Shared;
global using System.Net.Http.Json;
using BlazorEcommerce.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorEcommerce.Client.Services.ProductServices;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IProductSerivice, ProductSerivice>();
await builder.Build().RunAsync();
