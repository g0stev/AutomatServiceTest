using AutomatServiceTest.Abstraction.IServices;
using AutomatServiceTest.Context;
using AutomatServiceTest.Service.MappingProfiles;
using AutomatServiceTest.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddDbContext<AutomatServiceTestContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

services.AddAutoMapper(typeof(MappingProfile));

services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Тестовый проект API",
        Version = "v1",
        Description = "Тестовый проект",
    });
});

services.AddScoped<IInventoryService, InventoryService>();

// Add services to the container.
services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; 
    });
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
