using Application;
using Contracts.Services;
using SEP3_T2;
using SEP3_T2.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddScoped<IChatService, InMemoryChatService>();
builder.Services.AddScoped<IUserService, InMemoryUserService>();
builder.Services.AddScoped<IMessageService, MessageServerImp>();
builder.Services.AddSingleton<IControlStatusUser, ControlStatusImp>();
builder.Services.AddSignalR(o =>
{
    o.EnableDetailedErrors = true;
    o.MaximumReceiveMessageSize = 330240; // bytes
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>(ChatHub.HubUrl);

app.Run();