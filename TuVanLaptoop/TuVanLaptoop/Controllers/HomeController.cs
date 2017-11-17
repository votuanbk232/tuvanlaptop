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
        public ActionResult Index(int? page, string username)
        {
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                //lấy username sau khi đăng nhập thành công, username chuyển từ controller Login tới
                //ko dùng cách này, do khi redict tới một trang khác thì usernam ko đc lưu ở partial chức năng người dùng
                //TempData["user"] = username;

                int pageSize = 8;
                int pageNumber = (page ?? 1);
                //List<ChiTietLaptop> ctlt = db.ChiTietLaptops.OrderBy(n => n.GiaBan).ToList();
                return View(db.Laptops.OrderBy(n => n.NgayCapNhat).ToPagedList(pageNumber, pageSize));
            }
        }
        public string getTuVan()
        {
            string tuvan = Session["TuVan"] as string;
            if (tuvan == null)
            {
                
                Session["GioHang"] = "";
            }
            return tuvan;
        }
        public ActionResult DoTinCayPartial()
        {
            return PartialView();
        }


    }
}