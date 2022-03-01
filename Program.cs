using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using hamitsarmis.activitywebsite.backend.Data;
using hamitsarmis.activitywebsite.backend.Entities;
using hamitsarmis.activitywebsite.backend.Extensions;
using hamitsarmis.activitywebsite.backend.Interfaces;
using hamitsarmis.activitywebsite.backend.Middleware;
using hamitsarmis.activitywebsite.backend.Services;
using hamitsarmis.activitywebsite.backend.Helpers;

public class Program
{
    public static async Task Main(string[] args)
    {
        int.TryParse("2", out _);
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.AddJsonFile("appSettings.json");
        IConfiguration configuration = configurationBuilder.Build();
        builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddIdentityServices(builder.Configuration);
        builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

        var app = builder.Build();

        app.UseMiddleware<ExceptionMiddleware>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors(x => x.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithOrigins("http://localhost:4200"));
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<DataContext>();
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
                await context.Database.MigrateAsync();
                await Seed.SeedUsers(userManager, roleManager);
                await Seed.SeedEvents(context);
                await Seed.SeedMeals(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred during migration");
            }
        }
        app.Run();
    }
}
