using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TuVanLaptoop.Models;

namespace TuVanLaptoop.Controllers
{
    public class NguoiDungController : Controller
    {
        TuVanLaptopEntities db = new TuVanLaptopEntities();
        public ActionResult ChucNangPartial()
        {
            return PartialView();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(KhachHang kh=null)
        {
            
            if (ModelState.IsValid)
            {
                db.KhachHangs.Add(kh);
                db.SaveChanges();
                ViewBag.ThongBao = "Chúc mừng bạn đăng ký thành công !";
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            ViewBag.ThongBao = "Đăng kí không thành công!";
            return View(kh);

        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string sTaiKhoan = f["txtTaiKhoan"].ToString();
            string sMatKhau = f.Get("txtMatKhau").ToString();
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);
            if (kh != null)
            {
                ViewBag.ThongBao = "Chúc mừng bạn đăng nhập thành công !";
                FormsAuthentication.SetAuthCookie(sTaiKhoan, false);
                Session["TaiKhoan"] = kh;
                //Session["UserName"] = kh.TaiKhoan.ToString();
                
                    TempData["message"] = "Bạn đã đăng nhập thành công";
                    return RedirectToAction("Index", "Home",new {username= kh.TaiKhoan.ToString() });
               
            }
            ViewBag.ThongBao = "Tên tài khoản hoặc mật khẩu không đúng!";
            return View();

        }
        public ActionResult DangXuat()
        {
            FormsAuthentication.SignOut();
            Session["TaiKhoan"] = null; //lưu session khi đăng nhập admin thành công

            return RedirectToAction("Index", "Home");
        }
    }
}