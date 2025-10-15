using ChatApi.Hubs;
using ChatApi.Hubs.Interfaces;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSignalR().AddAzureSignalR(builder.Configuration.GetConnectionString("Azure:SignalR:SchoolChat"));
builder.Services.AddSingleton<IChatsHub, ChatsHub>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseRouting();

app.UseAuthorization();

app.MapHub<ChatsHub>("/api/chat-hub");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
