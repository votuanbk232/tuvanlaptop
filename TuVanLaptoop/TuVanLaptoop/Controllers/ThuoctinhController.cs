using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuVanLaptoop.Models;
using TuVanLaptoop.ViewModels;

namespace TuVanLaptoop.Controllers
{
    public class ThuoctinhController : Controller
    {
        TuVanLaptopEntities db = new TuVanLaptopEntities();
        public ActionResult ThuoctinhPartial()
        {
            ThuocTinhViewModel ttvm = new ThuocTinhViewModel();
            ttvm.hangsx = db.HangLapTops.ToList();
            ttvm.hedh = db.HeDieuHanhs.ToList();
            return PartialView(ttvm);
        }
    }
}