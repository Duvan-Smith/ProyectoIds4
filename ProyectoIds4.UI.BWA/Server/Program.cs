using ProyectoIds4.AppCore.Base.Cofiguration;
using ProyectoIds4.UI.BWA.Server.HttpClienteWeatherForecast;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "cookie";
    options.DefaultChallengeScheme = "oidc";
})
    .AddCookie("cookie")
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "https://localhost:7132";
        options.ClientId = "oidcMVCApp";
        options.ClientSecret = "ProCodeGuide";
        //options.ClientSecret = "ProCodeGuide".ToSha256();
        options.ResponseType = "code";
        options.UsePkce = true;
        options.ResponseMode = "query";
        options.Scope.Add("weatherApi.read");
        options.SaveTokens = true;
        options.SignedOutRedirectUri = "https://localhost:7167/signin-oidc-callback";
    });

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//builder.Services.TryAddSingleton<IWeatherForecastClientHttp, WeatherForecastClientHttp>();
builder.Services.AddHttpClient<IWeatherForecastClientHttp, WeatherForecastClientHttp>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7245/");
});
builder.Services.ConfigureAplicacionCore();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
