using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuVanLaptoop.Models;

namespace TuVanLaptoop.Controllers
{
    public class TimKiemController : Controller
    {
        public ActionResult KetQuaTimKiem(string txtTimKiem)
        {
            using (TuVanLaptopEntities db=new TuVanLaptopEntities())
            {
                if (!String.IsNullOrEmpty(txtTimKiem))
                {
                    List<Laptop> sanphams = db.Laptops.Where(x => x.Name.Contains(txtTimKiem)).ToList();
                    if (sanphams.Count == 0)
                    {
                        TempData["message"] = "Không có sản phẩm thỏa mãn";

                    }
                    TempData["message"] = "Có " + sanphams + " sản phẩm thỏa mãn";
                    return View(sanphams);

                }
                return RedirectToAction("Index", "Home");
            }
        }
    }
}