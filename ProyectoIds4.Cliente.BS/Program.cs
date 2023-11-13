using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using ProyectoIds4.Cliente.BS.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
    .AddCookie()
    .AddOpenIdConnect(options =>
    {
        options.Authority = "https://localhost:7132";
        options.ClientId = "oidcMVCApp";
        options.ClientSecret = "ProCodeGuide";
        options.CallbackPath = "/signin-oidc";
        options.ResponseType = "code";
        options.SaveTokens = true;
        options.Scope.Add("openid");
        options.Scope.Add("weatherApi.read");
        options.UsePkce = true;
        options.ResponseMode = "query";
        options.SignedOutRedirectUri = "https://localhost:7167/signin-oidc-callback";
        options.TokenValidationParameters.ValidateIssuer = false;
        options.TokenValidationParameters.NameClaimType = "name";

        options.Events = new OpenIdConnectEvents
        {
            OnRemoteFailure = context =>
            {
                context.Response.Redirect("/Account/Login");
                context.HandleResponse();
                return Task.CompletedTask;
            }
        };
    });

//builder.Services.AddMvcCore(options =>
//{
//    var policy = new AuthorizationPolicyBuilder()
//        .RequireAuthenticatedUser() // site-wide auth
//        .Build();
//    options.Filters.Add(new AuthorizeFilter(policy));
//});

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

app.MapControllers();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
