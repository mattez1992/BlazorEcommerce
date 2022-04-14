global using BlazorEcommerce.Shared.Models;
global using BlazorEcommerce.Shared;
global using System.Net.Http.Json;
global using Microsoft.AspNetCore.Components;
global using BlazorEcommerce.Client.Services.ProductServices;
global using BlazorEcommerce.Client.Services.CategoryServices;
using BlazorEcommerce.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IProductSerivice, ProductSerivice>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
await builder.Build().RunAsync();
