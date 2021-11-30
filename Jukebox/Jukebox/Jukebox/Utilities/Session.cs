using Jukebox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jukebox.Utilities
{
    public class Session
    {
        private Playlists queue = new Playlists();

        public Playlists Queue
        {
            get => queue;
            set { queue = value; }
        }

        private string convertingQueue;

        public string ConvertingQueue 
        {
            get => convertingQueue;
            set { convertingQueue = value; }
        }

        private User userId;

        public User UserId
        {
            get => userId;
            set { userId = value; }
        }

        private string fullName;

        public string FullName 
        {
            get => fullName;
            set { fullName = value; }
        }

        private string email;

        public string Email 
        {
            get => email;
            set { email = value; }
        }

        private Song song = new Song();

        public Song Song
        {
            get => song;
            set { song = value; }
        }

    }
}