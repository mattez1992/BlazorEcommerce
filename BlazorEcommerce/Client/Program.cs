global using BlazorEcommerce.Shared.Models;
global using BlazorEcommerce.Shared;
global using System.Net.Http.Json;
global using Microsoft.AspNetCore.Components;
global using BlazorEcommerce.Client.Services.ProductServices;
global using BlazorEcommerce.Client.Services.CategoryServices;
global using BlazorEcommerce.Shared.Dtos;
global using BlazorEcommerce.Client.Services.CartItemServices;
global using Blazored.LocalStorage;
global using BlazorEcommerce.Client.Services.AuthServices;
global using Microsoft.AspNetCore.Components.Authorization;
global using BlazorEcommerce.Client.Services.OrderServices;
global using BlazorEcommerce.Client.Services.AddressServices;
using BlazorEcommerce.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IProductSerivice, ProductSerivice>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICartItemService, CartItemService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IProductTypeService, ProductTypeService>();

// this will make the custom authProvide work also add cascadingAuthenticationState in app.rzor
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
// end custom authProvider
await builder.Build().RunAsync();
