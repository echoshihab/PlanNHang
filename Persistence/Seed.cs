using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(ApplicationDbContext _db, UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser{
                        DisplayName = "Shihab",
                        UserName ="shihab",
                        Email = "shihab@test.com"
                    },
                    new AppUser{
                        DisplayName = "Darrell",
                        UserName= "darrell",
                        Email = "darrell@test.com"
                    },
                    new AppUser{
                        DisplayName = "Matthew",
                        UserName ="matthew",
                        Email = "matthew@test.com"
                    },
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }



            }


            if (!_db.Activities.Any())
            {
                var activities = new List<Activity>
                {
                    new Activity
                    {
                        Title = "Tour of Science Museum",
                        Date = DateTime.Now.AddMonths(-2),
                        Description = "Activity 2 months ago",
                        Category = "culture",
                        City = "Toronto",
                        Venue = "Toronto Science Museum",
                    },
                    new Activity
                    {
                        Title = "Pub Night",
                        Date = DateTime.Now.AddMonths(-1),
                        Description = "Activity 1 month ago",
                        Category = "drinks",
                        City = "Hamilton",
                        Venue = "London Tap House",
                    },
                    new Activity
                    {
                        Title = "Movie Night",
                        Date = DateTime.Now.AddMonths(1),
                        Description = "Activity 1 month in future",
                        Category = "film",
                        City = "London",
                        Venue = "Westmout Cineplex",
                    },
                    new Activity
                    {
                        Title = "Chinese Buffet",
                        Date = DateTime.Now.AddMonths(2),
                        Description = "Activity 2 months in future",
                        Category = "food",
                        City = "London",
                        Venue = "Mandarin",
                    },

                    new Activity
                    {
                        Title = "Ski at Blue Mountain",
                        Date = DateTime.Now.AddMonths(3),
                        Description = "Activity 3 months in future",
                        Category = "travel",
                        City = "Blue Mountain",
                        Venue = "Blue Mountain Lodge",
                    },
                    new Activity
                    {
                        Title = "City of Toronto Tour",
                        Date = DateTime.Now.AddMonths(4),
                        Description = "Activity 4 months in future",
                        Category = "culture",
                        City = "Toronto",
                        Venue = "Toronto",
                    },
                    new Activity
                    {
                        Title = "Drinks at Habz",
                        Date = DateTime.Now.AddMonths(5),
                        Description = "Activity 5 months in future",
                        Category = "drinks",
                        City = "London",
                        Venue = "Habz House",
                    },
                    new Activity
                    {
                        Title = "Netflix with friends",
                        Date = DateTime.Now.AddMonths(6),
                        Description = "Activity 6 months in future",
                        Category = "film",
                        City = "London",
                        Venue = "Habz House",
                    },
                    new Activity
                    {
                        Title = "East Side Marios",
                        Date = DateTime.Now.AddMonths(7),
                        Description = "Activity 2 months ago",
                        Category = "food",
                        City = "London",
                        Venue = "East Side Marios",
                    },
                    new Activity
                    {
                        Title = "Tiesto Concern",
                        Date = DateTime.Now.AddMonths(8),
                        Description = "Activity 8 months in future",
                        Category = "music",
                        City = "London",
                        Venue = "London Theatre",
                    }

                };

                _db.Activities.AddRange(activities);
                _db.SaveChanges();
            }
        }

    }
}