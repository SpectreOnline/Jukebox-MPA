using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Jukebox.Models;
using Jukebox.Utilities;

namespace Jukebox.Controllers
{

    public class PlaylistsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Playlists
        public ActionResult Index()
        {
            if (JukeboxHelper.Session.UserId != null)
            {
                return View(db.Playlists.ToList());
            }
            else
            {
                return Redirect("~/Home/Login");
            }
        }

        // GET: Playlists/Details/5
        public ActionResult Details(int? id)
        {
            if (JukeboxHelper.Session.UserId != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                MultipleModels multipleModels = new MultipleModels();
                multipleModels.Songs = db.Songs;
                multipleModels.Playlists = db.Playlists;

                if (multipleModels == null)
                {
                    return HttpNotFound();
                }
                return View(multipleModels);
            }
            else 
            {
                return Redirect("~/Home/Login");
            }
        }

        // GET: Playlists/Create
        public ActionResult Create(string convertingQueue)
        {
            if (JukeboxHelper.Session.UserId != null)
            {
                if (convertingQueue == "true")
                {
                    JukeboxHelper.Session.ConvertingQueue = convertingQueue;
                }

                return View();
            }
            else 
            {
                return Redirect("~/Home/Login");
            }
        }

        // POST: Playlists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Creator,SongList")] Playlists playlist)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(playlist.SongList)) {
                    List<int> songList = playlist.SongList.Split(',').Select(int.Parse).ToList();
                    foreach (int songId in songList) {
                        var song = db.Songs.Find(songId);
                        playlist.DurationMinutes += song.DurationMinutes;
                        if (playlist.DurationSeconds + song.DurationSeconds >= 60)
                        {
                            var sumOfSeconds = playlist.DurationSeconds + song.DurationSeconds;
                            var minutesFromSeconds = (int)Math.Floor((decimal)(playlist.DurationSeconds + song.DurationSeconds) / 60);
                            sumOfSeconds %= 60;
                            playlist.DurationMinutes += minutesFromSeconds;
                            playlist.DurationSeconds = sumOfSeconds;
                        }
                        else
                        {
                            playlist.DurationSeconds += song.DurationSeconds;
                        }
                    }
                }

                db.Playlists.Add(playlist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(playlist);
        }

        // GET: Playlists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (JukeboxHelper.Session.UserId != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Playlists playlist = db.Playlists.Find(id);
                if (playlist == null)
                {
                    return HttpNotFound();
                }
                return View(playlist);
            } else 
            {
                return Redirect("~/Home/Login");
            }
        }

        // POST: Playlists/Edit/5
        // Bug: All the songs in the playlist are removed upon the edit of said playlist
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Creator,SongList,DurationSeconds,DurationMinutes")] Playlists playlist)
        {
            if (ModelState.IsValid)
            {

                db.Entry(playlist).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(playlist);
        }


        // GET: Playlists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Playlists playlist = db.Playlists.Find(id);
            if (playlist == null)
            {
                return HttpNotFound();
            }
            return View(playlist);
        }

        // POST: Playlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Playlists playlist = db.Playlists.Find(id);
            db.Playlists.Remove(playlist);
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
