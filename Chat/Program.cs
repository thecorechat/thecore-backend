using Application.DI;
using Azure.Identity;
using ChatApi.Hubs;
using ChatApi.Hubs.Interfaces;
using Infrastructure.DI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Azure.SignalR;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Protocols.Configuration;
using Scalar.AspNetCore;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

var keyVaultEndpoint = new Uri(
    Environment.GetEnvironmentVariable("SchoolChatSecretsUri")
    ?? builder.Configuration["SchoolChatSecretsUri"]
    ?? throw new InvalidConfigurationException("Environment\\configuration variable is missing: SchoolChatSecretsUri"));
builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"))
    .EnableTokenAcquisitionToCallDownstreamApi()
    .AddInMemoryTokenCaches();

builder.WebHost.ConfigureKestrel(options =>
{
    int httpPort = 0;

    if (int.TryParse(builder.Configuration["ASPNETCORE_HTTP_PORT"], out int port1))
    {
        httpPort = port1;
    }
    else if (int.TryParse(Environment.GetEnvironmentVariable("ASPNETCORE_HTTP_PORT"), out int port2))
    {
        httpPort = port2;
    }
    else
    {
        throw new InvalidConfigurationException("Http ports isn't configured");
    }

    options.ListenAnyIP(httpPort);
}); // http only


builder.Services.AddControllers(conf =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    conf.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddPagination();

var azureSignalRConnectionString = builder.Configuration["SignalR-SchoolChat-PrimaryConnectionString"]
    ?? Environment.GetEnvironmentVariable("SignalR-SchoolChat-PrimaryConnectionString")
    ?? throw new InvalidConfigurationException("Environment\\configuration variable is missing: SignalR-SchoolChat-PrimaryConnectionString");
//azure key vault

builder.Services.AddSignalR().AddAzureSignalR(azureSignalRConnectionString);

//Presentation Layer Dependencies
builder.Services.AddSingleton<IChatsHub, ChatsHub>();

//Application Layer Dependencies
builder.Services.AddApplicationServices();

//Infrastructure Layer Dependencies
builder.Services.AddDBDependencies();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();

    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatsHub>("/api/chat-hub");


app.MapControllers().RequireAuthorization();

app.Run();
