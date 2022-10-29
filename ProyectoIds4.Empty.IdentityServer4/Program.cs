using ProyectoIds4.Empty.IdentityServer4AspNet.IdentityConfiguration;

string MiCors = "MiCords";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddPolicy(name: MiCors, builder =>
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddRazorPages();

builder.Services.AddIdentityServer(options =>
{
    //options.IssuerUri = "https://localhost:7167";
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
})
.AddInMemoryClients(Clients.Get())
.AddInMemoryIdentityResources(Resources.GetIdentityResources())
.AddInMemoryApiResources(Resources.GetApiResources())
.AddInMemoryApiScopes(Scopes.GetApiScopes())
.AddTestUsers(Users.Get())
.AddDeveloperSigningCredential();

builder.Services.AddAuthentication();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseIdentityServer();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages().RequireAuthorization();
app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
app.UseCors(MiCors);

app.Run();
