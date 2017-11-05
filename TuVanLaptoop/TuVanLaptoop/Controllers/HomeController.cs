using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuVanLaptoop.Models;
using TuVanLaptoop.ViewModels;
using PagedList;

namespace TuVanLaptoop.Controllers
{
    public class HomeController : Controller
    {
        TuVanLaptopEntities db = new TuVanLaptopEntities();
        // GET: Home
        public ActionResult Index(int? page,string username)
        {
            TempData["user"] = username;
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            //List<ChiTietLaptop> ctlt = db.ChiTietLaptops.OrderBy(n => n.GiaBan).ToList();
            return View(db.Laptops.OrderBy(n => n.NgayCapNhat).ToPagedList(pageNumber,pageSize));
        }
        
    }
}