using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;
using WebApplication5.Models.ViewModel;

namespace WebApplication5.Controllers
{
    public class SportsController : Controller
    {
        private ClubDbContext db = new ClubDbContext();

        // GET: Sports
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult MasterView(int page = 1, string sort = "SportsId", string search = "")
        {
            ViewBag.NameSort = sort == "name" ? "name_desc" : "name";
            ViewBag.CurrentSort = sort;
            ViewBag.CurrentSearch = search;

            var data = db.Sports.AsEnumerable().Select(x => new SportsViewModel
            {
                SportsId = x.SportsId,
                SportsName = x.SportsName,
                playerCount = x.Players.Count()
            }).ToList();

            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(x => x.SportsName.ToLower().StartsWith(search.ToLower())).ToList();
            }

            switch (sort)
            {
                case "name":
                    data = data.OrderBy(x => x.SportsName).ToList();
                    break;
                case "name_desc":
                    data = data.OrderByDescending(x => x.SportsName).ToList();
                    break;
                case "SportsId_desc":
                    data = data.OrderByDescending(x => x.SportsId).ToList();
                    break;
                default:
                    data = data.OrderBy(x => x.SportsId).ToList();
                    break;
            }

            var modelData = data.ToPagedList(page, 5);
            return PartialView("_masterDetails", modelData);
        }
        public ActionResult PlayerList(int id)
        {
            return PartialView("_playerList", db.Players.Include("Sports").
            Where(x => x.SportsId == id).ToList());
        }

        public ActionResult CrudeUI(int page = 1)
        {
            return View(db.Sports.OrderBy(x => x.SportsId).ToPagedList(page, 5));
        }

        // GET: Sports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sports sports = db.Sports.Find(id);
            if (sports == null)
            {
                return HttpNotFound();
            }
            return View(sports);
        }
        // GET: Sports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SportsId,SportsName")] Sports sports)
        {
            if (ModelState.IsValid)
            {
                db.Sports.Add(sports);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sports);
        }

        // GET: Sports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sports sports = db.Sports.Find(id);
            if (sports == null)
            {
                return HttpNotFound();
            }
            return View(sports);
        }

        // POST: Sports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SportsId,SportsName")] Sports sports)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sports).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sports);
        }
        // GET: Sports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sports sports = db.Sports.Find(id);
            if (sports == null)
            {
                return HttpNotFound();
            }
            return View(sports);
        }

        // POST: Sports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sports sports = db.Sports.Find(id);
            db.Sports.Remove(sports);
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
