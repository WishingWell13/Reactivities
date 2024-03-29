using API.Extensions;
using Application.Activities;
using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//What is a service class C#?
//Services are used to fetch information from a data source (most likely a 
//repository), process the information and return the result to the caller. 
//https://stackoverflow.com/questions/5015925/correct-use-of-repository-service-classes#:~:text=Services%20are%20used%20to%20fetch,to%20achieve%20the%20wanted%20result.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);

//========Middleware======== //Order matters more here
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//When finished with this scope/method, anything inside it will be destroyed 
using var scope = app.Services.CreateScope();
//DataContext in other folder, so have to get it
var services = scope.ServiceProvider;

try
{
    //Try to create database
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    //Fill DataContext (which I assume is container) with sample data
    await Seed.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
    throw;
}

app.Run();
