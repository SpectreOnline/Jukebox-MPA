using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Jukebox.Models
{
    
    public class MultipleModels
    {
        public IEnumerable<Song> Songs { get; set; }

        public IEnumerable<Playlists> Playlists { get; set; }

        public IEnumerable<Genres> Genres { get; set; }

    }
}