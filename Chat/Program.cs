using Application.Interfaces;
using Application.Services;
using Azure.Identity;
using ChatApi.Hubs;
using ChatApi.Hubs.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Azure.SignalR;
using Microsoft.Identity.Web;
using Scalar.AspNetCore;
using System.IdentityModel.Tokens.Jwt;
//"CertificateName": "SchoolChat-Azure-SSC"

var builder = WebApplication.CreateBuilder(args);

var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("SchoolChatSecretsUri")!);
builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"))
    .EnableTokenAcquisitionToCallDownstreamApi()
    .AddInMemoryTokenCaches();

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

builder.Services.AddSignalR().AddAzureSignalR(builder.Configuration["SignalR-SchoolChat-PrimaryConnectionString"]);//azure key vault
builder.Services.AddSingleton<IChatsHub, ChatsHub>();

builder.Services.AddTransient<IChatsService, ChatsService>();
builder.Services.AddTransient<IChatAccessService, ChatAccessService>();



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

app.UseHttpsRedirection();

app.MapControllers().RequireAuthorization();

app.Run();
