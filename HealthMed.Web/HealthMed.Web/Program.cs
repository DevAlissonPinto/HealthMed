using HealthMed.Application;
using HealthMed.Domain.Interfaces.Application;
using HealthMed.Domain.Interfaces.Services;
using HealthMed.Domain.Services;
using HealthMed.Web.Components;
//using HealthMed.Web.Security;
using Microsoft.AspNetCore.Components.Authorization;
//using System.Net.Http;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

//builder.Services.AddScoped<CookieHandler>();

builder.Services.AddAuthorizationCore();

//builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();
//builder.Services.AddScoped(x =>
//    (ICookieAuthenticationStateProvider)x.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Host.ConfigureServices((context, services) => 
{
    HealthMed.Infra.Ioc.HealthMedIoc.Initialize(services, context.Configuration);
});

builder.Services.AddHttpClient<IBrapiService, BrapiService>();
//builder.Services.AddHttpClient("HealthMed", opt => { opt.BaseAddress = new Uri("https://HealthMed-windows-api-a0guf8cqhhgdb7ct.eastus-01.azurewebsites.net"); }).AddHttpMessageHandler<CookieHandler>(); ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();

app.Run();
