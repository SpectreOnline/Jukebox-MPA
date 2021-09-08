using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
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
        //public virtual DbSet<Playlist> Playlists { get; set; }
        public virtual DbSet<Song> Songs { get; set; }

        public System.Data.Entity.DbSet<Jukebox.Playlist> Playlists { get; set; }
        /*
protected override void OnModelCreating(DbModelBuilder modelBuilder)
{
modelBuilder.Entity<Playlist>()
.HasMany(e => e.Songs)
.WithOptional(e => e.Playlist)
.HasForeignKey(e => e.Playlists_ID);
}
*/
    }
}
