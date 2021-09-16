using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Jukebox.Models
{
    public partial class Genres
    {
        public int ID { get; set; }

        public string GenreName { get; set; }

        public string SongList { get; set; }

    }
}