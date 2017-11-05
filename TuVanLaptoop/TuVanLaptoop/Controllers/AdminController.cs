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
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            Admin lg = db.Admins.FirstOrDefault(n => n.Username == username);
            if(lg!=null && lg.Password.SequenceEqual(SHA1.Create().ComputeHash(Encoding.Unicode.GetBytes(password)))){
                FormsAuthentication.SetAuthCookie(username, false);
                Session["IsAdmin"] = true; //lưu session khi đăng nhập admin thành công
                return RedirectToAction("QuanLiSanPham", "Admin");
            }
            ViewBag.ThongBao = "Tài khoản hoặc mật khẩu không đúng";
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["IsAdmin"] = false; //lưu session khi đăng nhập admin thành công

            return RedirectToAction("Login","Admin");
        }
        [AdminFilter]
        public ActionResult QuanLiSanPham(int? page)
        {

            int pageNumber = (page ?? 1);
            int pageSize = 8;
            return View(db.Laptops.ToList().OrderBy(n => n.Id).ToPagedList(pageNumber, pageSize));
        }
        [AdminFilter]
        public ActionResult QuanLiSuKien(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 8;
            return View(db.SuKiens.ToList().OrderBy(n => n.Id).ToPagedList(pageNumber, pageSize));
        }
        [AdminFilter]
        public ActionResult QuanLiLuat(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 8;
            return View(db.Luats.ToList().OrderBy(n => n.Id).ToPagedList(pageNumber, pageSize));
        }
        
       

    }
}