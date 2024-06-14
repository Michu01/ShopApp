using Api.Extensions;
using Api.Filters;

using Application;

using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

using Infrastructure;
using Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var keyVaultUrl = builder.Configuration["KeyVault:Url"]!;
var keyVaultTenantId = builder.Configuration["KeyVault:TenantId"];
var keyVaultClientId = builder.Configuration["KeyVault:ClientId"];
var keyVaultClientSecret = builder.Configuration["KeyVault:ClientSecret"];

var credential = new ClientSecretCredential(keyVaultTenantId, keyVaultClientId, keyVaultClientSecret);
var client = new SecretClient(new Uri(keyVaultUrl), credential);

builder.Configuration.AddAzureKeyVault(client, new KeyVaultSecretManager());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureServices(builder.Configuration, builder.Environment.IsDevelopment());
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