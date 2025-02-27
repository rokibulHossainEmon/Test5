using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;
using WebApplication5.Models.ViewModel;

namespace WebApplication5.Controllers
{
    public class PlayersController : Controller
    {
        private readonly ClubDbContext db = new ClubDbContext();
        // GET: Players
        public ActionResult Index()
        {
            return View(db.Players.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.sports = new SelectList(db.Sports, "SportsId", "SportsName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(PlayerViewModel pvm)
        {
            if (ModelState.IsValid)
            {
                //for save image
                string filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(pvm.Picture.FileName));
                pvm.Picture.SaveAs(Server.MapPath(filePath));

                //for player
                Player player = new Player
                {
                    PlayerName = pvm.PlayerName,
                    JoinDate = pvm.JoinDate,
                    PlayerGrade = pvm.PlayerGrade,
                    SportsId = pvm.SportsId,
                    PicturePath = filePath
                };
                db.Players.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.sports = new SelectList(db.Sports, "SportsId", "SportsName");
            return View(pvm);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            PlayerViewModel pvm = new PlayerViewModel
            {
                PlayerId = player.PlayerId,
                PlayerName = player.PlayerName,
                JoinDate = player.JoinDate,
                PlayerGrade = player.PlayerGrade,
                SportsId = player.SportsId,
                PicturePath = player.PicturePath
            };
            ViewBag.sports = new SelectList(db.Sports, "SportsId", "SportsName");
            return View(pvm);
        }






    }

    
}