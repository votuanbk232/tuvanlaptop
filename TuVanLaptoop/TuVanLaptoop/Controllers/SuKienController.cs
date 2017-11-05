using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuVanLaptoop.Models;
using TuVanLaptoop.ViewModels;

namespace TuVanLaptoop.Controllers
{
    public class SuKienController : Controller
    {
        TuVanLaptopEntities db = new TuVanLaptopEntities();

        // GET: SuKien
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SuKienPartial()
        {

            var model = new SuKienViewModel
            {
                GioiTinhs = new SelectList(db.GioiTinhs, "Name", "Name"),
                MucDichs = new SelectList(db.MucDiches, "Name", "Name"),
                NgheNghieps = new SelectList(db.NgheNghieps, "Name", "Name"),
                YeuCauGiaTiens = new SelectList(db.YeuCauGiaTiens, "Name", "Name"),
                HangLaptops = new SelectList(db.HangLapTops, "Name", "Name"),
                HeDieuHanhs = new SelectList(db.HeDieuHanhs, "Name", "Name")
            };

            //ViewBag.GioiTinhs = new SelectList(db.GioiTinhs.ToList(), "Id", "Name");
            //ViewBag.MucDiches = new SelectList(db.MucDiches.ToList(), "Id", "Name");
            //ViewBag.NgheNghieps = new SelectList(db.NgheNghieps.ToList(), "Id", "Name");
            //ViewBag.YeuCauGiaTiens = new SelectList(db.YeuCauGiaTiens.ToList(), "Id", "Name");
            //ViewBag.HangLapTops = new SelectList(db.HangLapTops.ToList(), "Id", "Name");
            //ViewBag.HeDieuHanhs = new SelectList(db.HeDieuHanhs.ToList(), "Id", "Name");

            return PartialView(model);
        }
    }
}