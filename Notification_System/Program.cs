// Program.cs
using Microsoft.EntityFrameworkCore;
using Notification_System.Data;
using Notification_System.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<IMyEmailSender, MyEmailSender>();
builder.Services.AddScoped<NotificationManager>(); // Add this line

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Initialize subscribers when app starts
using (var scope = app.Services.CreateScope())
{
    var notificationManager = scope.ServiceProvider.GetRequiredService<NotificationManager>();
    await notificationManager.InitializeSubscribersAsync();
}

// Apply migrations and create database if it doesn't exist
ApplyDatabaseMigrations(app);

ConfigureMiddleware(app);

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

    services.AddSingleton<IMyEmailSender, MyEmailSender>();
    services.AddScoped<NotificationManager>();
    services.AddControllersWithViews();
}

void ApplyDatabaseMigrations(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

void ConfigureMiddleware(WebApplication app)
{
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
}