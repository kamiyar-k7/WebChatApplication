using WebChatBlazor.Components;
using WebChatBlazor.Services.Base;
using WebChatBlazor.Services.UserServices;

var builder = WebApplication.CreateBuilder(args);

// Connect Api
builder.Services.AddHttpClient<IClient, Client>(url => url.BaseAddress = new Uri("https://localhost:7019"));

#region Injects
builder.Services.AddScoped<IUserServices, UserServices>();


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
