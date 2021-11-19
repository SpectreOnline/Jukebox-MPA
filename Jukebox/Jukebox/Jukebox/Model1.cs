using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Jukebox.Models;
using System.Linq;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Jukebox
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=JukeboxSettings")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public DbSet<User> Users { get; set; }

        public virtual DbSet<Song> Songs { get; set; }

        public virtual DbSet<Playlists> Playlists { get; set; }

        public virtual DbSet<Genres> Genres { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }

    }
}
