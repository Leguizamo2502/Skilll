using Web.Services;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Services
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddApplicationServices();

//Jwt
//builder.Services.AddToken(builder.Configuration);

//Cors
builder.Services.AddCorsService(builder.Configuration);




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseRouting(); // Asegúrate de tener esto

app.UseCors(); // Agrega esto para habilitar CORS
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
