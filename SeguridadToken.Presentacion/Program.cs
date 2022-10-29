using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using SeguridadToken.Presentacion.Login;
using SeguridadToken.Presentacion.MyAuthentication;
using SeguridadToken.Presentacion.WeatherForecast;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "cookie";
    options.DefaultChallengeScheme = "oidc";
})
    .AddCookie("cookie")
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "https://localhost:7164";
        options.ClientId = "oidcMVCApp7193";
        options.ClientSecret = "ProCodeGuide";
        options.Scope.Add("weatherApi.read");
        options.ResponseType = "code";
        options.UsePkce = true;
        options.ResponseMode = "query";
        options.SaveTokens = true;
    });


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
//builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddHttpClient<ILoginService, LoginService>(client =>
    client.BaseAddress = new Uri("https://localhost:7245/")
);
builder.Services.AddHttpClient<IWeatherForecastService, WeatherForecastService>(client =>
    client.BaseAddress = new Uri("https://localhost:7245/")
);

builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<AuthenticationStateProvider, MyAuthenticationStateProviderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
