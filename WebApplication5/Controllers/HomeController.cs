using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;
using System.Data.Entity;
namespace WebApplication5.Controllers
{
    public class HomeController : Controller
    {
        ClubDbContext db = new ClubDbContext();
        public ActionResult Index()
        {
            return View(db.Sports.ToList()); 
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}