using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using ReadyTech.BrewCoffee2023New.API.Properties;
using System.Net;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = new ConfigurationBuilder();
if (builder.Environment.IsDevelopment())
{
    configuration.SetBasePath(builder.Environment.ContentRootPath).AddJsonFile("appsettings.Development.json", false, true);
}
else
{
    configuration.SetBasePath(builder.Environment.ContentRootPath).AddJsonFile("appsettings.json", false, false);
}

var Configuration = configuration.AddEnvironmentVariables().Build();

builder.Services.AddOptions();
builder.Services.Configure<AppSettings>(options => Configuration.Bind(options));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Add a middleware to handle the exception.If the URL doesn't match the actions of the controllers, will go to the specific Error action.
app.UseStatusCodePagesWithReExecute("/api/error/{0}");

//Add an ExceptionHandler for the middleware.
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 200;
        context.Response.ContentType = "application/json; charset=utf-8";

        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerFeature>();
        await context.Response.WriteAsync(text: exceptionHandlerPathFeature?.Error.Message);
    });
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
