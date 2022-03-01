using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using hamitsarmis.activitywebsite.backend.Entities;
using System.Text.Json;

namespace hamitsarmis.activitywebsite.backend.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            List<AppUser> users = new List<AppUser>();
            for (int i = 0; i < 1000; i++)
            {
                users.Add(new AppUser
                {
                    Email = $"hamitsarmis{i}@hamitsarmis.com",
                    PhoneNumber = "+90 (216) 893"+ i.ToString().PadLeft(4, '0'),
                    Created = DateTime.Now
                });
            }

            var roles = new List<AppRole>
            {
                new AppRole{Name = "Member"},
                new AppRole{Name = "Admin"}
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                user.UserName = user.Email.Substring(0, user.Email.IndexOf("@"));
                await userManager.CreateAsync(user, "Passw0rd");
                await userManager.AddToRoleAsync(user, "Member");
            }

            var admin = new AppUser
            {
                UserName = "admin"
            };

            await userManager.CreateAsync(admin, "Passw0rd");
            await userManager.AddToRolesAsync(admin, new[] { "Admin", "Member" });
        }

        public static async Task SeedEvents(DataContext context)
        {
            if (await context.Events.AnyAsync()) return;
            for (int i = 0; i < 1000; i++)
            {
                context.Events.Add(new Event
                {
                    Title = $"Event {i}",
                    ExpirationDate = DateTime.Now.Date.AddDays(i + 10),
                });
            }
            await context.SaveChangesAsync();
            for (int i = 1; i <= 100; i++)
            {
                var evnt = await context.Events.FindAsync(i);
                for (int j = 1; j <= 500; j++)
                {
                    var usr = await context.Users.FindAsync(j);
                    context.EventSubscriptions.Add(new EventSubscription()
                    {
                        Event = evnt,
                        User = usr,
                        Email = j % 2 == 0 ? usr.Email : null,
                        Phone = j % 2 == 1 ? usr.PhoneNumber : null
                    });
                }
            }
            await context.SaveChangesAsync();
        }

        public static async Task SeedMeals(DataContext context)
        {
            if (await context.Meals.AnyAsync()) return;
            for (int i = 0; i < 10; i++)
            {
                context.Meals.Add(new Meal
                {
                    Name = $"Meal name {i}",
                    Description = $"Meal description {i}",
                    Price = i,
                });
            }
            await context.SaveChangesAsync();
        }
    }
}
