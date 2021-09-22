namespace Jukebox.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Jukebox.Model1>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Jukebox.Model1 context)
        {

            context.Genres.AddOrUpdate(g => g.GenreName,
                new Models.Genres { GenreName = "Rock"},
                new Models.Genres { GenreName = "Pop" },
                new Models.Genres { GenreName = "Country" },
                new Models.Genres { GenreName = "R&B and soul" },
                new Models.Genres { GenreName = "Hip Hop" },
                new Models.Genres { GenreName = "Electronic / dance" },
                new Models.Genres { GenreName = "Jazz" },
                new Models.Genres { GenreName = "Blues" },
                new Models.Genres { GenreName = "Classical and Opera" },
                new Models.Genres { GenreName = "Heavy metal" }
                );

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
