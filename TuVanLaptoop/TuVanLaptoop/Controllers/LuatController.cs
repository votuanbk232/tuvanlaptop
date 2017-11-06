using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuVanLaptoop.Filter;
using TuVanLaptoop.Models;

namespace TuVanLaptoop.Controllers
{
    public class LuatController : Controller
    {
        TuVanLaptopEntities db = new TuVanLaptopEntities();
        [HttpGet]
        public ActionResult ThemLuatOrSuaLuat(int id = 0)
        {
            Luat luat = new Luat();
            //khi sửa luật, tức có id truyền vào
            if (id != 0)
            {
                luat = db.Luats.Where(x => x.Id == id).FirstOrDefault();
                luat.SuKienSelectedIDArray = luat.SuKienVT.Split(',').ToArray();
                luat.SukienVP = db.SuKiens.Single(n => n.Id.ToString() == luat.SukienVP).Name;
                ViewBag.ThongBao = "Sửa luật";
            }
            else
            {
                ViewBag.ThongBao = "Thêm luật";
            }
            //luật vế trái chỉ lấy các sự kiện ở giao diện
            luat.SuKienCollection = db.SuKiens.Take(28).ToList();
            return View(luat);
        }
        [AdminFilter]
        [HttpPost]
        public ActionResult ThemLuatOrSuaLuat(Luat luat)
        {
            //thêm sự kiện vế phải vào bảng sự kiện
            SuKien sk = new SuKien();
            sk.Name = luat.SukienVP;
            //kiểm tra sự kiện vế phải  đã tồn tại hay chưa
            SuKien skCheck = db.SuKiens.SingleOrDefault(n => n.Name == luat.SukienVP);
            //chưa tồn tại, thêm mới sự kiện
            if (skCheck == null)
            {
                db.SuKiens.Add(sk);
                db.SaveChanges();
                luat.SukienVP = sk.Id.ToString();
            }
            else
            {
                //nếu sự kiện vế phải đã tồn tại, ko cần add nữa
                luat.SukienVP = skCheck.Id.ToString();
            }
            //chuyển sự kiện vế trái về dạng string[]
            luat.SuKienVT = string.Join(",", luat.SuKienSelectedIDArray);

            //lúc này đã có cả vế trái và vế phải của luật

            //khi chưa có Id, tức đang tạo luật mới
            if (luat.Id == 0)
            {
                //khi thêm luật, kiểm tra xem luật này đã tồn tại hay chưa
                //nếu false: tức ko tồn tại, thêm đc luật
                if (!Luat.CheckLuatTonTai(luat.SuKienVT, luat.SukienVP))
                {
                    //thêm luật vào bảng luật
                    db.Luats.Add(luat);
                }
                //nếu ko , sẽ ko thêm đc luật
                //cần truyền các giá trị bắt buộc của View
                else
                {
                    luat.SuKienCollection = db.SuKiens.Take(28).ToList();
                    ViewBag.CheckLuat = "Luật đã tồn tại";
                    return View(luat);
                }
            }


            //khi có Id tức sửa luật
            else
            {
                db.Entry(luat).State = EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("QuanLiLuat","Admin");
        }
        
        //Xử lí độ tin cậy: Người dùng đánh giá
        public ActionResult TangDoTinCay(int MaLuat, string strUrl)
        {
            using (TuVanLaptopEntities db=new TuVanLaptopEntities())
            {
                Luat luat = db.Luats.SingleOrDefault(n => n.Id == MaLuat);
                if (luat == null)
                {
                    return null;
                }
                luat.DoTinCay++;
                db.Entry(luat).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Redirect(strUrl);
        }
        public ActionResult GiamDoTinCay(int MaLuat, string strUrl)
        {
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                Luat luat = db.Luats.SingleOrDefault(n => n.Id == MaLuat);
                if (luat == null)
                {
                    return null;
                }
                luat.DoTinCay--;
                db.Entry(luat).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Redirect(strUrl);
        }



    }
}