using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BeeFat.Components;
using BeeFat.Components.Account;
using BeeFat.Components.Account.Domain.Helpers;
using BeeFat.Data;
using BeeFat.Repositories;

namespace BeeFat;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

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
        
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
            .Options;

        var userRepository = new UserRepository(configuration, dbContextOptions);
        var trackRepository = new TrackRepository(configuration, dbContextOptions);
        var journalRepository = new JournalRepository(configuration, dbContextOptions);
        var foodProductRepository = new FoodProductRepository(configuration, dbContextOptions);
        var journalFoodRepository = new JournalFoodRepository(configuration, dbContextOptions);

        builder.Services.AddSingleton(userRepository);
        builder.Services.AddSingleton(trackRepository);
        builder.Services.AddSingleton(journalRepository);
        builder.Services.AddSingleton(foodProductRepository);

        builder.Services.AddSingleton(new HomeHelper(userRepository, journalRepository, journalFoodRepository));
        builder.Services.AddSingleton(new TrackPickHelper(userRepository, trackRepository, journalRepository));
        builder.Services.AddSingleton(new UserProfileHelper(userRepository, trackRepository));
        builder.Services.AddSingleton<TrackViewerHelper>(provider =>
        {
            var trackPickHelper = provider.GetRequiredService<TrackPickHelper>();
            return new TrackViewerHelper(trackPickHelper, trackRepository);
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
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

        app.Run();
    }
}