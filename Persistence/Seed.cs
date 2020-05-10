using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Persistence
{
    public class Seed
    {
        public static void SeedData(ApplicationDbContext _db)
        {
            if (!_db.Activities.Any())
            {
                var activities = new List<Activity>
                {
                    new Activity
                    {
                        Title = "Soccer Match",
                        Date = DateTime.Now.AddMonths(-2),
                        Description = "Activity 2 months ago",
                        Category = "Soccer",
                        City = "Hamilton",
                        Venue = "Westwood School",
                    },
                    new Activity
                    {
                        Title = "Beach Vollyball",
                        Date = DateTime.Now.AddMonths(-1),
                        Description = "Activity 1 month ago",
                        Category = "Vollyball",
                        City = "London",
                        Venue = "Little Beach",
                    },
                    new Activity
                    {
                        Title = "Flag Football",
                        Date = DateTime.Now.AddMonths(1),
                        Description = "Activity 1 month in future",
                        Category = "Football",
                        City = "Toronto",
                        Venue = "High Park",
                    },
                    new Activity
                    {
                        Title = "5k Run",
                        Date = DateTime.Now.AddMonths(2),
                        Description = "Activity 2 months in future",
                        Category = "Running",
                        City = "Hamilton",
                        Venue = "Bayfront",
                    },

                    new Activity
                    {
                        Title = "Swimming",
                        Date = DateTime.Now.AddMonths(3),
                        Description = "Activity 3 months in future",
                        Category = "Swimming",
                        City = "London",
                        Venue = "East Park London",
                    },
                    new Activity
                    {
                        Title = "Basketball",
                        Date = DateTime.Now.AddMonths(4),
                        Description = "Activity 4 months in future",
                        Category = "Basketball",
                        City = "Hamilton",
                        Venue = "Sir John A School Gym",
                    },
                    new Activity
                    {
                        Title = "Soccer for kids",
                        Date = DateTime.Now.AddMonths(5),
                        Description = "Activity 5 months in future",
                        Category = "Soccer",
                        City = "London",
                        Venue = "Bayfront Park",
                    },
                    new Activity
                    {
                        Title = "Baseball",
                        Date = DateTime.Now.AddMonths(6),
                        Description = "Activity 6 months in future",
                        Category = "Baseball",
                        City = "London",
                        Venue = "Midpark",
                    },
                    new Activity
                    {
                        Title = "Competeitive Skating",
                        Date = DateTime.Now.AddMonths(7),
                        Description = "Activity 2 months ago",
                        Category = "Skating",
                        City = "London",
                        Venue = "St. Juliens",
                    },
                    new Activity
                    {
                        Title = "Bowling",
                        Date = DateTime.Now.AddMonths(8),
                        Description = "Activity 8 months in future",
                        Category = "Bowling",
                        City = "London",
                        Venue = "Fleetway",
                    }

                };

                _db.Activities.AddRange(activities);
                _db.SaveChanges();
            }
        }

    }
}