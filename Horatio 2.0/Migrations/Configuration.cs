namespace Horatio_2._0.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Horatio_2._0.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Horatio_2._0.Models.ApplicationDbContext";
        }

        protected override void Seed(Horatio_2._0.Models.ApplicationDbContext context)
        {
            context.Topics.AddOrUpdate(y => y.Theme,
            new Topic() { TopicID = 1, Theme = "Sport" },
            new Topic() { TopicID = 2, Theme = "Art" },
            new Topic() { TopicID = 3, Theme = "Community" },
            new Topic() { TopicID = 4, Theme = "Career" },
            new Topic() { TopicID = 5, Theme = "Nature" },
            new Topic() { TopicID = 6, Theme = "Group" },
            new Topic() { TopicID = 7, Theme = "Sponsored" },
            new Topic() { TopicID = 8, Theme = "Miscellaneous" });
        }
    }
}
