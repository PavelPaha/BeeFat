using BeeFat.Components;
using BeeFat.Components.Pages;
using BeeFat.Data;
using BeeFat.Helpers;
using BeeFat.Interfaces;
using BeeFat.Repositories;
using Blazorise;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddBlazorise();
builder.Services.AddBootstrapProviders();
builder.Services.AddServerSideBlazor();
builder.Services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<HomeHelper>();
builder.Services.AddScoped<IBaseRepository, FakeBeeFatRepository>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();

app.Run();
