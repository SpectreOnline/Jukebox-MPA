using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Jukebox;
using Jukebox.Utilities;
using Jukebox.Models;

namespace Jukebox.Controllers
{
    public class SongsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Songs
        public ActionResult Index(string genre)
        {
            if (JukeboxHelper.Session.UserId != null)
            {
                MultipleModels multipleModels = new MultipleModels();

                if (multipleModels == null)
                {
                    return HttpNotFound();
                }

                if (genre != null && genre != "")
                {
                    multipleModels.Songs = db.Songs.Where(i => i.Genre == genre);
                }
                else
                {
                    multipleModels.Songs = db.Songs;
                }

                multipleModels.Genres = db.Genres;

                return View(multipleModels);
            }
            else
            {
                return Redirect("~/Home/Login");
            }

        }

        public ActionResult Playlist()
        {
            if (JukeboxHelper.Session.UserId != null)
            {
                var playlists = db.Playlists;
                return View(playlists.ToList());
            }
            else
            {
                return Redirect("~/Home/Login");
            }
        }

            // GET: Songs/Details/5
            public ActionResult Details(int? id)
        {
            if (JukeboxHelper.Session.UserId != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Song song = db.Songs.Find(id);
                if (song == null)
                {
                    return HttpNotFound();
                }
                return View(song);
            } else 
            {
                return Redirect("~/Home/Login");
            }
        }

        // GET: Songs/Create
        public ActionResult Create()
        {
            if (JukeboxHelper.Session.UserId != null)
            {
                MultipleModels multipleModels = new MultipleModels();
                multipleModels.Songs = db.Songs;
                multipleModels.Genres = db.Genres;

                if (multipleModels == null)
                {
                    return HttpNotFound();
                }

                return View(multipleModels);
            }
            else 
            {
                return Redirect("~/Home/login");
            }
        }

            // POST: Songs/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Author,Genre,DurationMinutes,DurationSeconds")] Song song)
        {
            if (ModelState.IsValid)
            {
                var minutesFromSeconds = (song.DurationSeconds - (song.DurationSeconds % 60)) / 60;
                song.DurationSeconds %= 60;
                song.DurationMinutes += minutesFromSeconds;
                db.Songs.Add(song);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.Playlists_ID = new SelectList(db.Playlists, "ID", "Creator", song.Playlists_ID);
            return View(song);
        }

        // GET: Songs/Edit/5
        public ActionResult Edit(int? id)
        {
            MultipleModels multipleModels = new MultipleModels();
            multipleModels.Songs = db.Songs;
            multipleModels.Genres = db.Genres;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            //ViewBag.Playlists_ID = new SelectList(db.Playlists, "ID", "Creator", song.Playlists_ID);
            return View(multipleModels);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Author,DurationMinutes,DurationSeconds,Genre")] Song song)
        {
            if (ModelState.IsValid)
            {
                var minutesFromSeconds = (song.DurationSeconds - (song.DurationSeconds % 60)) / 60;
                song.DurationSeconds %= 60;
                song.DurationMinutes += minutesFromSeconds;
                db.Entry(song).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(song);
        }
        
        public ActionResult AddToPlaylist(int currentPlaylistID, int currentSongID)
        {
            //Make a delete version of this

            //Make a single string containing all of the IDs and seperate them using commas

            Playlists playlist = db.Playlists.Find(currentPlaylistID);

            var song = db.Songs.Find(currentSongID);

            if (playlist.SongList == null || playlist.SongList == "")
            {
                playlist.SongList = currentSongID.ToString();
                playlist.DurationMinutes = song.DurationMinutes;
                playlist.DurationSeconds = song.DurationSeconds;
            }
            else
            {
                playlist.SongList = playlist.SongList + ',' + currentSongID.ToString();
                playlist.DurationMinutes += song.DurationMinutes;
                if (playlist.DurationSeconds + song.DurationSeconds >= 60)
                {
                    var sumOfSeconds = playlist.DurationSeconds + song.DurationSeconds;
                    var minutesFromSeconds = (int)Math.Floor((decimal)(playlist.DurationSeconds + song.DurationSeconds) / 60);
                    sumOfSeconds %= 60;
                    playlist.DurationMinutes += minutesFromSeconds;
                    playlist.DurationSeconds = sumOfSeconds;
                }
                else {
                    playlist.DurationSeconds += song.DurationSeconds;
                }
            }

            db.SaveChanges();

            return RedirectToAction("Index");

        }

        public ActionResult RemoveFromPlaylist(int currentPlaylistID, int currentSongID)
        {
            //Make a delete version of this

            //Make a single string containing all of the IDs and seperate them using commas

            Playlists playlist = db.Playlists.Find(currentPlaylistID);

            var song = db.Songs.Find(currentSongID);

            if (playlist.SongList != null || playlist.SongList != "")
            {

                List<int> songList = playlist.SongList.Split(',').Select(int.Parse).ToList();
                var songToRemove = songList.Single(r => r == currentSongID);
                songList.Remove(songToRemove);

                string songListString = "";

                foreach (var idNumber in songList)
                {
                    songListString = songListString + idNumber + ',' ;
                }

                playlist.SongList = songListString;

                playlist.DurationMinutes -= song.DurationMinutes;

                if (playlist.DurationSeconds - song.DurationSeconds < 0)
                {
                    playlist.DurationSeconds = (playlist.DurationSeconds + 60) - song.DurationSeconds;
                    playlist.DurationMinutes -= 1;
                }
                else
                {
                    playlist.DurationSeconds -= song.DurationSeconds;
                }

                db.SaveChanges();

                return RedirectToAction("Index");

                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        public ActionResult AddToQueue(int songID, string currentURL)
        {

            if (JukeboxHelper.Session.Queue.SongList == null)
            {
                JukeboxHelper.Session.Queue.SongList = songID.ToString();

                JukeboxHelper.Session.Song = db.Songs.Where(s => s.ID == songID).FirstOrDefault() ?? new Song();

                string redirectURL = "~/" + currentURL;

                return Redirect(redirectURL);
            }
            else
            {

                List<string> songObjects;

                songObjects = JukeboxHelper.Session.Queue.SongList.Split(',').ToList() ?? new List<string>();

                songObjects.Add(songID.ToString());

                JukeboxHelper.Session.Queue.SongList = string.Join(",", songObjects.ToArray());

                string redirectURL = "~/" + currentURL;

                return Redirect(redirectURL);
            }

        }

        public ActionResult ClearQueue(string currentURL)
        {
            JukeboxHelper.Session.Queue.SongList = null;

            JukeboxHelper.Session.Song = null;

            string redirectURL = "~/" + currentURL;

            return Redirect(redirectURL);
        }

        // GET: Songs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Song song = db.Songs.Find(id);
            db.Songs.Remove(song);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
