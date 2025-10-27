using Application.Interfaces;
using Application.Services;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using ChatApi.Hubs;
using ChatApi.Hubs.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Azure.SignalR;
using Microsoft.Identity.Web;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("SchoolChatSecretsUri")!);
builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSignalR().AddAzureSignalR(builder.Configuration["SignalR-SchoolChat-PrimaryConnectionString"]);//azure key vault
builder.Services.AddSingleton<IChatsHub, ChatsHub>();

builder.Services.AddTransient<IChatsService, ChatsService>();
//builder.Services.AddTransient<IChatsService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapHub<ChatsHub>("/api/chat-hub");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
