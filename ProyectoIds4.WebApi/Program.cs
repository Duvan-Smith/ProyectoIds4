using Microsoft.AspNetCore.Authentication.JwtBearer;
using ProyectoIds4.AppCore.IdentityServer4;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddIdentityServerAuthentication("Bearer", options =>
{
    options.ApiName = "weatherApi";
    options.Authority = "https://localhost:7164";
});

builder.Services.ConfigureIdentityServer4Services();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    //app.UseSwaggerUI(c => c.SwaggerEndpoint("https://procodeguide.b-cdn.net/swagger/v1/swagger.json", "ProCodeGuide.IdServer4.WebAPI v1"));
    app.UseSwaggerUI(options =>
    {
        options.OAuth2RedirectUrl("https://localhost:7164/oauth2-redirect.html");
    });
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
