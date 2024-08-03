using HealthMed.Domain.Entities;
using HealthMed.Identity.Common;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
app.ConfigureDevEnvironment();

app.UseCors(Configuration.CorsPolicyName);
app.UseSecurity();

app.UseHttpsRedirection();

app.MapGet("/", () => new { message = "OK" });

app.MapGroup("v1/identity").WithTags("Identity").MapIdentityApi<User>();

app.MapGroup("v1/identity").WithTags("Identity").MapPost("/logout", async (SignInManager<User> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Ok();
}).RequireAuthorization();

app.MapGroup("v1/identity")
    .WithTags("Identity")
    .MapGet("/roles", (ClaimsPrincipal user) =>
    {

        if (user.Identity is null || !user.Identity.IsAuthenticated)
            return Results.Unauthorized();

        var identity = (ClaimsIdentity)user.Identity;
        var roles = identity.FindAll(identity.RoleClaimType).Select(c => new RoleClaim
        {
            Issuer = c.Issuer,
            OriginalIssuer = c.OriginalIssuer,
            Type = c.Type,
            Value = c.Value,
            ValueType = c.ValueType
        });

        return TypedResults.Json(roles);
    }).RequireAuthorization();


app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
