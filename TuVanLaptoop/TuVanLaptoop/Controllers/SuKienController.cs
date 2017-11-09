using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuVanLaptoop.Filter;
using TuVanLaptoop.Models;
using TuVanLaptoop.ViewModels;

namespace TuVanLaptoop.Controllers
{
    [AdminFilter]
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
        //Thêm sự kiện sử dụng JSON
        //Tạo partial view khi ấn nút button "ThemSuKien"
        //include Jquery UI:
        //<script src = "~/Scripts/jquery-1.8.0.js" ></ script >
        //< script src="~/Scripts/jquery-ui-1.11.4.js"></script>
        //<link href = "~/Content/themes/base/all.css" rel="stylesheet" />

        public ActionResult ThemSuKienPartial()
        {
            SuKien v = new SuKien();
            return PartialView("ThemSuKienPartial", v);

        }
        public ActionResult SaveSuKien(SuKien sk)
        {
            using(TuVanLaptopEntities db=new TuVanLaptopEntities())
            {
                //sk chưa tồn tại ta mới thêm
                //if (SuKien.SaveSuKien(sk) != null)
                //{
                    SuKien.SaveSuKien(sk);
                if (SuKien.SaveSuKien(sk) != null)
                {
                    return Json(sk);
                }
                TempData["ThongBao"] = "Đã tồn tại sự kiện";
                return View();

                //}
                //return PartialView("ThemSuKienPartial", sk);

            }

        }

    }
}