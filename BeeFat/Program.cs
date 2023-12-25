using BeeFat.Components;
using BeeFat.Data;
using BeeFat.Domain.Infrastructure;
using BeeFat.Helpers;
using BeeFat.Interfaces;
using BeeFat.Repositories;
using Blazorise;
using Microsoft.EntityFrameworkCore;
using Blazorise.Bootstrap;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddBlazorise();
builder.Services.AddBootstrapProviders();
builder.Services.AddServerSideBlazor();


builder.Services.AddCascadingAuthenticationState();



var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
    .Options;

builder.Services.AddSingleton<IConfiguration>(configuration);

var userRepository = new UserRepository(configuration, dbContextOptions);
var trackRepository = new TrackRepository(configuration, dbContextOptions);
var journalRepository = new JournalRepository(configuration, dbContextOptions);

builder.Services.AddSingleton(userRepository);
builder.Services.AddSingleton(trackRepository);
builder.Services.AddSingleton(journalRepository);

builder.Services.AddSingleton(new HomeHelper(userRepository));
builder.Services.AddSingleton(new TrackPickHelper(userRepository, trackRepository, journalRepository));
builder.Services.AddSingleton(new UserProfileHelper(userRepository));
builder.Services.AddSingleton(new TrackEditorHelper(trackRepository));


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
