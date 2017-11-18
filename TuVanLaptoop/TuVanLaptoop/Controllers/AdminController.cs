using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TuVanLaptoop.Filter;
using TuVanLaptoop.Models;

namespace TuVanLaptoop.Controllers
{
    
    public class AdminController : Controller
    {
        // GET: Admin
            TuVanLaptopEntities db = new TuVanLaptopEntities();
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            using(TuVanLaptopEntities db=new TuVanLaptopEntities())
            {
                Admin lg = db.Admins.FirstOrDefault(n => n.Username == username);
                if (lg != null && lg.Password.SequenceEqual(SHA1.Create().ComputeHash(Encoding.Unicode.GetBytes(password))))
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                    //Session["IsAdmin"] = true; //lưu session khi đăng nhập admin thành công
                    Session["IsAdmin"] = lg; //thử hiện username admin 
                    return RedirectToAction("QuanLiSanPham", "Admin");
                }
                ViewBag.ThongBao = "Tài khoản hoặc mật khẩu không đúng";
                return View();
            }
           
        }
        [AdminFilter]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            // Session["IsAdmin"] = false; //lưu session khi đăng nhập admin thành công
            Session["IsAdmin"] = null;
            return RedirectToAction("Login","Admin");
        }
        //[AdminFilter]
        public ActionResult QuanLiSanPham(int? page)
        {
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                int pageNumber = (page ?? 1);
                int pageSize = 8;
                return View(db.Laptops.ToList().OrderBy(n => n.Id).ToPagedList(pageNumber, pageSize));
            }
        }
        //[AdminFilter]
        public ActionResult QuanLiSuKien(int? page)
        {
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                int pageNumber = (page ?? 1);
                int pageSize = 8;
                return View(db.SuKiens.ToList().OrderBy(n => n.Id).ToPagedList(pageNumber, pageSize));
            }
        }
        //hiển thị thông tin sự kiện sử dụng jquery Datatables
        [AdminFilter]
        public ActionResult QuanLiSuKienUsingJquery()
        {
            return View();
        }
        [AdminFilter]
        public ActionResult loaddata()
        {
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                // dc.Configuration.LazyLoadingEnabled = false; // if your table is relational, contain foreign key
                var data = db.SuKiens.OrderBy(a => a.Id).ToList();
                return Json(new { data = data }, JsonRequestBehavior.AllowGet);
            }
        }
        //[AdminFilter]
        public ActionResult QuanLiLuat(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 8;
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                List<Luat> luats = db.Luats.ToList();
               foreach(var item in luats)
                {
                    item.sukienvetrais = Luat.ConvertIntArrayToStringArray(item.SuKienVT);
                    //item.sukienvephai = SuKien.getSuKienById(Convert.ToInt16(item.SukienVP));
                }
                //db.Luats.ToList().OrderBy(n => n.Id).ToPagedList(pageNumber, pageSize)
                return View(luats.OrderBy(n => n.Id).ToPagedList(pageNumber, pageSize));
            }
        }
        
       

    }
}