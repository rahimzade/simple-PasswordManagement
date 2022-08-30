using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PasswordManager.Data;
using PasswordManager.Data.Repository;
using PasswordManager.Entities;
using PasswordManager.MappingProfile;
using PasswordManager.Models;
using PasswordManager.Services;
using PasswordManager.Services.interfaces;
using PasswordManager.States;
using Serilog;
using System;


try
{
    var builder = WebApplication.CreateBuilder(args);

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .WriteTo.File(@"C:\logs\log.txt")
        .MinimumLevel.Error()
        .CreateLogger();

    Log.Information("Starting up");


    Serilog.Log.Information("Starting application");
    Serilog.Log.Error("Error");
    Serilog.Log.Fatal("Fatal");
    Serilog.Log.Debug("Debug");

    // Add services to the container.

    // For entity Framework
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

    builder.Configuration.GetSection("EncripyStrings").Get<EncripyStrings>();

    builder.Services.AddAutoMapper(config =>
    {
        config.AddProfile(new MappingProfile());
    });

    builder.Services.AddHttpClient();
    builder.Services.AddScoped<HttpClient>();

    builder.Services.AddScoped<IUserDataInitializer, UserDataInitializer>();

    builder.Services.AddTransient<IUserRepository, UserRepository>();
    builder.Services.AddTransient<IUserService, UserService>();
    builder.Services.AddScoped<UserStateService>();


    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    using (var scope = app.Services.CreateScope())
    {
        var dataInitializer = scope.ServiceProvider.GetRequiredService<IUserDataInitializer>();
        dataInitializer.InitializeData();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseRouting();

    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}
