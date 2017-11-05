using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuVanLaptoop.Models;

namespace TuVanLaptoop.Controllers
{
    public class ChatBotController : Controller
    {
        TuVanLaptopEntities db = new TuVanLaptopEntities();
        // GET: Home
        public ActionResult GetResult()
        {
            
            //List<ChiTietLaptop> ctlt = db.ChiTietLaptops.OrderBy(n => n.GiaBan).ToList();
            return View(db.Laptops.OrderBy(n => n.NgayCapNhat).Where(n=>n.HeDieuHanh.Name=="Dell").ToList());
        }
    }
}