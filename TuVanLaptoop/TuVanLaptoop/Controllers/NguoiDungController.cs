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
            //người dùng đã đăng nhập, gửi tên user tới chức năng partial
            if (Session["TaiKhoan"] != null)
            {
                ViewBag.UserName = (Session["TaiKhoan"] as KhachHang).TaiKhoan.ToString();
                
                return PartialView();

            }
            if (Session["IsAdmin"] != null)
            {
                ViewBag.UserName = (Session["IsAdmin"] as Admin).Username.ToString();
                return PartialView();

            }
            else
            {
                ViewBag.UserName = "";
            }
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
                //kiểm tra email đã tồn tại hay chưa(chưa làm)
                //password hasing
                kh.MatKhau = Crypto.Hash(kh.MatKhau);
                kh.XacNhanMatKhau = Crypto.Hash(kh.XacNhanMatKhau);
                //lưu vào database
                using(TuVanLaptopEntities db=new TuVanLaptopEntities())
                {
                    db.KhachHangs.Add(kh);
                    db.SaveChanges();
                }
               
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
            using (TuVanLaptopEntities db=new TuVanLaptopEntities())
            {
                var v = db.KhachHangs.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan);
                if (v != null)
                {
                    if (string.Compare(Crypto.Hash(sMatKhau), v.MatKhau)==0)
                    {
                        ViewBag.ThongBao = "Chúc mừng bạn đăng nhập thành công !";
                        FormsAuthentication.SetAuthCookie(sTaiKhoan, false);
                        //KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == Crypto.Hash(sMatKhau));

                        Session["TaiKhoan"] = v;
                        //Session["UserName"] = kh.TaiKhoan.ToString();

                        TempData["message"] = "Bạn đã đăng nhập thành công";
                        //truyền usernam tới Index/Home
                        return RedirectToAction("Index", "Home", new { username = v.TaiKhoan.ToString() });
                    }
                    else
                    {

                    }
                }

            }
            //KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);
           
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