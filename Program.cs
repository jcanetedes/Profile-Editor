using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProfileEditor.Core.Authentication;
using ProfileEditor.Core.Services;
using Radzen;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddTransient<AuthenticationStateProvider, AuthStore>();
builder.Services.AddTransient<AuthHandler>();


builder.Services.AddRefitClient<ILoginService>()
        .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.devreg.org"));

builder.Services.AddRefitClient<IProfileService>()
        .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.devreg.org"));
        //.AddHttpMessageHandler<AuthHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();