using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using System.IdentityModel.Tokens.Jwt;
using WebChatBlazor.Components;
using WebChatBlazor.Components.CustomComponents;
using WebChatBlazor.Services.AuthServices;
using WebChatBlazor.Services.Base;
using WebChatBlazor.Services.ChatServices;
using static AuthStateProvider;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpContextAccessor();



// Connect Api
builder.Services.AddHttpClient<IClient, Client>(url => url.BaseAddress = new Uri("https://localhost:7019"));


#region Injects
//mud blazor
builder.Services.AddMudServices();


// jwt injects
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddSingleton<JwtSecurityTokenHandler>();
builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, BlazorAuthorizationMiddlewareResultHandler>();
builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(p => p.GetRequiredService<AuthStateProvider>());
//
builder.Services.AddScoped<IUserProvider, UserProvider>();

//Servcies
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IPrivateChatService, PrivateChatServices>();
builder.Services.AddScoped<IHomePageServcies, HomePageServcies>();

#endregion

builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options => { options.DetailedErrors = true; });

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();


app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
