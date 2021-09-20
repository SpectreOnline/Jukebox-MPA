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

namespace Jukebox.Controllers
{
    public class SongsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Songs
        public ActionResult Index()
        {
            var songs = db.Songs;//.Include(s => s.Playlist);
            return View(songs.ToList());
        }

        public ActionResult Playlist()
        {
            var playlists = db.Playlists;
            return View(playlists.ToList());
        }

        // GET: Songs/Details/5
        public ActionResult Details(int? id)
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

        // GET: Songs/Create
        public ActionResult Create()
        {
            //ViewBag.Playlists_ID = new SelectList(db.Playlists, "ID", "Creator");
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Author,DurationMinutes,DurationSeconds,Playlists_ID")] Song song)
        {
            if (ModelState.IsValid)
            {
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
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Author,DurationMinutes,DurationSeconds,Playlists_ID")] Song song)
        {
            if (ModelState.IsValid)
            {
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

            if (playlist.SongList == null)
            {
                playlist.SongList = currentSongID.ToString();
            }
            else {
                playlist.SongList = playlist.SongList + ',' + currentSongID.ToString();
            }

            db.SaveChanges();

            return RedirectToAction("Index");

        }

        public ActionResult AddToQueue(int songID, string currentURL)
        {
            Session["tempPlaylist"] = Session["tempPlaylist"] + songID.ToString() + ',';

            string redirectURL = "~/" + currentURL;

            return Redirect(redirectURL);
        }

        public ActionResult ClearQueue(string currentURL)
        {
            Session["tempPlaylist"] = null;

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
