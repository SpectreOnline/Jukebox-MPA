using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Jukebox.Models;
using System.Linq;

namespace Jukebox
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=JukeboxSettings")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }

        public virtual DbSet<Song> Songs { get; set; }

        public virtual DbSet<Playlists> Playlists { get; set; }

        public virtual DbSet<Genres> Genres { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<Playlists>().Property<string>(new Func<Playlists, string>(x => x.SongList))
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<string>>(v));
            */
            /*
            modelBuilder.Entity<Playlist>()
            .HasMany(e => e.Songs)
            .WithOptional(e => e.Playlist)
            .HasForeignKey(e => e.Playlists_ID);
                   */
        }

    }
}
