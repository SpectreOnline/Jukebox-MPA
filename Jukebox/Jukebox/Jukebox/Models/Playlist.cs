
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;


namespace Jukebox {

    public partial class Playlists
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Creator { get; set; }
        public string SongList { get; set; }
        public int DurationMinutes { get; set; }
        public int DurationSeconds { get; set; }
    }
}