using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DipChallengeFinal.Models;
using Microsoft.AspNet.Identity;

namespace DipChallengeFinal.Controllers
{
	[Authorize]
    public class GamesController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: Games
        public async Task<ActionResult> Index()
        {
			string userId = User.Identity.GetUserId();
			AspNetUser aspNetUser = db.AspNetUsers.SingleOrDefault(e => e.Id == userId);
			if (aspNetUser.UserRoles == 1 || aspNetUser.UserRoles == 2)
			{
				return View(await db.Games.Where (a=> a.Date > DateTime.Today).ToListAsync());
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
           
        }

        // GET: Games/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = await db.Games.FindAsync(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
			string userId = User.Identity.GetUserId();
			AspNetUser aspNetUser = db.AspNetUsers.SingleOrDefault(e => e.Id == userId);
			
			if (aspNetUser.EmailConfirmed && aspNetUser.UserRoles == 1)
			{
				return View();

			}
            else
			{
				//ViewData["Message"] = "Unauthorized user";
				return RedirectToAction("Index", "Games");
			}
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Time,Date,Venue,CourtFee,WhoPaid")] Game game)
        {
            if (ModelState.IsValid)
            {
				game.WhoPaid.ToLower();
                db.Games.Add(game);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(game);
        }

        // GET: Games/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = await db.Games.FindAsync(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Time,Date,Venue,CourtFee,WhoPaid")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = await db.Games.FindAsync(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Game game = await db.Games.FindAsync(id);
            db.Games.Remove(game);
            await db.SaveChangesAsync();
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
