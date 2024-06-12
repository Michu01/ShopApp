using Api.Extensions;
using Api.Filters;

using Application;

using Infrastructure;
using Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

var app = builder.Build();

{
    using var scope = app.Services.CreateScope();

    await scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.MigrateAsync();
}

app.UseSwagger();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var global = app
    .MapGroup(string.Empty)
    .AddEndpointFilter<ExceptionFilter>();

global.MapEndpoints();

app.Run();