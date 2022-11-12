using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProyectoIds.Cliente.BWA;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CustomAuthenticationMessageHandler>();
builder.Services.AddHttpClient("auth", opt => opt.BaseAddress = new Uri("https://localhost:7132"))
    .AddHttpMessageHandler<CustomAuthenticationMessageHandler>();
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("auth"));

builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.Authority = "https://localhost:7132";
    options.ProviderOptions.ClientId = "oidcMVCApp";
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.ResponseMode = "query";
    options.ProviderOptions.DefaultScopes.Add("openid");
    options.ProviderOptions.DefaultScopes.Add("profile");
    options.ProviderOptions.DefaultScopes.Add("email");
    options.ProviderOptions.DefaultScopes.Add("role");
    options.ProviderOptions.DefaultScopes.Add("weatherApi.read");
    builder.Configuration.Bind("Oidc", options.ProviderOptions);
});

await builder.Build().RunAsync();


public class CustomAuthenticationMessageHandler : AuthorizationMessageHandler
{
    public CustomAuthenticationMessageHandler(IAccessTokenProvider provder, NavigationManager nav) : base(provder, nav)
    {
        ConfigureHandler(new string[] { "https://localhost:7132" });
    }
}