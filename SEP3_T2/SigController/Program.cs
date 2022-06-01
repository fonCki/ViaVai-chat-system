using Application;
using Contracts.DAO;
using Contracts.Services;
using RESTClient;
using SEP3_T2;
using SEP3_T2.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddScoped<IChatService, ChatServiceImp>();
builder.Services.AddScoped<IUserService, UserServerImp>();
builder.Services.AddScoped<IMessageService, MessageServerImp>();

builder.Services.AddSingleton<IControlStatusUser, ControlStatusImp>();

builder.Services.AddScoped<IChatDao, ChatDAO>();
builder.Services.AddScoped<IMessageDao, MessageDAO>();
builder.Services.AddScoped<IUserDao, UserDAO>();

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