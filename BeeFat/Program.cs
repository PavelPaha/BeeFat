using BeeFat;
using BeeFat.Components;
using BeeFat.Data;
using BeeFat.Helpers;
using BeeFat.Repositories;
using Blazorise;
using Blazorise.Bootstrap;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Metrics;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddBlazorise();
builder.Services.AddBootstrapProviders();
builder.Services.AddServerSideBlazor();
builder.Services.AddSyncfusionBlazor();


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
var foodProductRepository = new FoodProductRepository(configuration, dbContextOptions);
var journalFoodRepository = new JournalFoodRepository(configuration, dbContextOptions);
var foodRepository = new FoodRepository(configuration, dbContextOptions);

builder.Services.AddScoped<HomeChartHelper>();

builder.Services.AddSingleton(userRepository);
builder.Services.AddSingleton(trackRepository);
builder.Services.AddSingleton(journalRepository);
builder.Services.AddSingleton(foodProductRepository);
builder.Services.AddSingleton(foodRepository);

builder.Services.AddSingleton(new HomeHelper(userRepository, journalRepository, journalFoodRepository));
builder.Services.AddSingleton(new TrackPickHelper(userRepository, trackRepository, journalRepository));
builder.Services.AddSingleton(new UserProfileHelper(userRepository, trackRepository));
builder.Services.AddSingleton<TrackViewerHelper>(provider =>
{
    var trackPickHelper = provider.GetRequiredService<TrackPickHelper>();
    return new TrackViewerHelper(trackPickHelper, trackRepository);
});

builder.Services.AddSingleton(new FoodAdditionHelper(foodRepository, journalFoodRepository));
builder.Services.AddOpenTelemetry().WithMetrics(x =>
{
    x.AddPrometheusExporter();
    x.AddMeter(
        "Microsoft.AspNetCore.Hosting",
        "Microsoft.AspNetCore.Server.Kestrel"
        );
    x.AddView("request-duration",
        new ExplicitBucketHistogramConfiguration());
});

var dailyTaskScheduler = new DailyTaskScheduler(journalRepository, userRepository);

builder.Services.AddSingleton(dailyTaskScheduler);

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

app.MapPrometheusScrapingEndpoint();
app.UseBlazorFrameworkFiles();
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();

var mainThread = new Thread(() => app.Run());
mainThread.Start();

var secondThread = new Thread(async () => await dailyTaskScheduler.Start());
secondThread.Start();


