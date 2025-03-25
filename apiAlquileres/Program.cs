using Application.Services;
using Domain.Entities;
using Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin() // Permite cualquier origen
                  .AllowAnyMethod() // Permite cualquier método HTTP (GET, POST, etc.)
                  .AllowAnyHeader(); // Permite cualquier cabecera
        });
});
// Configurar Dependencias
builder.Services.AddScoped<IAlquileresService, AlquileresService>();
builder.Services.AddScoped<IAlquileresRepository, AlquileresRepository>();
builder.Services.AddScoped<ILibrosService, LibrosService>();
builder.Services.AddScoped<ILibrosRepository, LibrosRepository>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Usar CORS
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
