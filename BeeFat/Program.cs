using BeeFat;
using BeeFat.Components;
using BeeFat.Components.Account;
using BeeFat.Components.Account.Domain.Helpers;
using BeeFat.Data;
using BeeFat.Helpers;
using BeeFat.Repositories;
using Blazored.Modal;
using Blazorise;
using Blazorise.Bootstrap;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using Microsoft.JSInterop.WebAssembly;
using OpenTelemetry.Metrics;
using Syncfusion.Blazor;
using Blazored.Modal;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BeeFat;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        builder.Services.AddBlazorise()
            .AddBootstrapProviders();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddSyncfusionBlazor();
        builder.Services.AddBlazoredModal();

        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddScoped<IdentityUserAccessor>();
        builder.Services.AddScoped<IdentityRedirectManager>();
        builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

        builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                               throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
            .Options;

        builder.Services.AddSingleton(dbContextOptions);

        builder.Services.AddSingleton<IConfiguration>(configuration);

        builder.Services.AddScoped<HomeChartHelper>();

        builder.Services.AddSingleton<UserRepository>();
        builder.Services.AddSingleton<TrackRepository>();
        builder.Services.AddSingleton<JournalRepository>();
        builder.Services.AddSingleton<FoodProductRepository>();
        builder.Services.AddSingleton<JournalFoodRepository>();
        builder.Services.AddSingleton<FoodRepository>();
        
        builder.Services.AddScoped<HomeHelper>();
        builder.Services.AddScoped<TrackPickHelper>();
        builder.Services.AddScoped<UserProfileHelper>();
        builder.Services.AddScoped<TrackViewerHelper>();
        builder.Services.AddScoped<FoodAdditionHelper>();
        
        
        builder.Services.AddSingleton<DailyTaskScheduler>(provider =>
        {
            var journalRepository = provider.GetRequiredService<JournalRepository>();
            var userRepository = provider.GetRequiredService<UserRepository>();
            return new DailyTaskScheduler(journalRepository, userRepository);
        });
        
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

        

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
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
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        // Add additional endpoints required by the Identity /Account Razor components.
        app.MapAdditionalIdentityEndpoints();

        var mainThread = new Thread(() => app.Run());
        mainThread.Start();

        var dailyTaskScheduler = app.Services.GetRequiredService<DailyTaskScheduler>();
        var secondThread = new Thread(async () => await dailyTaskScheduler.Start());
        secondThread.Start();
    }
}