using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;


namespace Jukebox
{


    [Serializable]
    public partial class Song
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }

        public int DurationMinutes { get; set; }

        public int DurationSeconds { get; set; }

        public List<int> Playlists_ID { get; set; }

    }
}
