global using Microsoft.AspNetCore.Components.Authorization;
using DeanerySystem.Abstractions;
using DeanerySystem.Authentication;
using DeanerySystem.Data;
using DeanerySystem.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<IPersonService, PersonService>()
                .AddScoped<IGroupService, GroupService>()
                .AddScoped<ISubjectService, SubjectService>()
                .AddScoped<IMarkService, MarkService>()
                .AddScoped<IPasswordHasher, PasswordHasher>();

var connectionString = builder.Configuration.GetConnectionString("DeanerySystem");
builder.Services.AddDbContext<DeaneryContext>(
    options => options.UseNpgsql(connectionString), 
    ServiceLifetime.Transient);

builder.Services.AddSingleton<AccountService>();
builder.Services.AddAuthenticationCore();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, DeaneryAuthenticationStateProvider>();

builder.Services.AddRadzenComponents();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<TooltipService>();
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

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
