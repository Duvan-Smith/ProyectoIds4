using ProyectoIds4.AppCore.IdentityServer4;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IIdentityServer4Service, IdentityServer4Service>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseSwagger();
    //app.UseSwaggerUI(c => c.SwaggerEndpoint("https://procodeguide.b-cdn.net/swagger/v1/swagger.json", "ProCodeGuide.IdServer4.WebAPI v1"));
    //app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();