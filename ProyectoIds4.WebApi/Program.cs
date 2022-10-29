using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
#region AddSwaggerGen
builder.Services.AddSwaggerGen(swa =>
{
    swa.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "'Bearer' + token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    swa.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
               Reference=new OpenApiReference
               {
                Type= ReferenceType.SecurityScheme,
                   Id="Bearer"
               },
               Scheme="oauth2",
               Name="Bearer",
               In=ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});
#endregion AddSwaggerGen

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(opt =>
{
    opt.Authority = "https://localhost:7164";
    opt.Audience = "weatherApi";
    opt.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
