using BlazorApp.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorApp.Services;
using BlazorApp.Services.View;
using Client;
using Contracts.Services;
using Contracts.Services.Hub;
using Contracts.Services.Refresh;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<IToastService, ToastServiceImp>();
builder.Services.AddScoped<View>();
builder.Services.AddScoped<AuthenticationStateProvider, SimpleAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthService, AuthServiceImpl>();
builder.Services.AddScoped<IUserService, UserClient>();
builder.Services.AddScoped<IChatService, ChatClient>();
builder.Services.AddScoped<IMessageService, MessageClient>();
builder.Services.AddScoped<IRefreshService, RefreshServiceImp>();
builder.Services.AddScoped<HubService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.MapFallbackToPage("/chatApp/{param?}", "/_Host");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();