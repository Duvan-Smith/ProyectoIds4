using ProyectoIds4.AppCore.Base.Cofiguration;

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
        options.Authority = "https://localhost:7164";
        options.ClientId = "oidcMVCApp";
        options.ClientSecret = "ProCodeGuide";
        options.ResponseType = "code";
        options.UsePkce = true;
        options.ResponseMode = "query";
        options.Scope.Add("weatherApi.read");
        options.SaveTokens = true;
    });

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.ConfigureAplicacionCore();

//builder.Services.AddHttpClient<IIdentityServer4Service, IdentityServer4Service>(client =>
//{
//    client.BaseAddress = new Uri("https://localhost:7245/");
//});

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
