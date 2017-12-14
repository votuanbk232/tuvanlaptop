using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuVanLaptoop.Filter;
using TuVanLaptoop.Models;

namespace TuVanLaptoop.Controllers
{
    public class SanPhamController : Controller
    {
        TuVanLaptopEntities db = new TuVanLaptopEntities();

        // GET: ChiTietLapTop
        public ActionResult SanphamMoiPartial()
        {
            //List<Laptop> ctlt = db.Laptops.OrderBy(n => n.NgayCapNhat).Take(3).ToList();
            List<Laptop> ctlt = db.Laptops.OrderBy(n => n.NgayCapNhat).Take(2).ToList();
            return View(ctlt);
        }
        public ActionResult XemChiTiet(int MaLaptop = 0)
        {
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                Laptop laptop = db.Laptops.SingleOrDefault(n => n.Id == MaLaptop);
                if (laptop == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                ViewBag.TenHangSanXuat = db.HangLapTops.Single(n => n.Id == laptop.HangLaptopId).Name;
                ViewBag.TenHeDieuHanh = db.HeDieuHanhs.Single(n => n.Id == laptop.HeDieuHanhId).Name;
                return View(laptop);
            }
        }
        [AdminFilter]
        [HttpGet]
        public ActionResult ThemSanPham()
        {
            //Đưa dữ liệu vào dropdownlist
            ViewBag.HangLaptopId = new SelectList(db.HangLapTops.ToList(), "Id", "Name");
            ViewBag.HeDieuHanhId = new SelectList(db.HeDieuHanhs.ToList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        [AdminFilter]
        [ValidateInput(false)]
        public ActionResult ThemSanPham(Laptop laptop, HttpPostedFileBase fileUpload)
        {
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                //Đưa dữ liệu vào dropdownlist
                ViewBag.HangLaptopId = new SelectList(db.HangLapTops.ToList(), "Id", "Name");
                ViewBag.HeDieuHanhId = new SelectList(db.HeDieuHanhs.ToList(), "Id", "Name");
                //kiểm tra đường dẫn ảnh bìa
                if (fileUpload == null)
                {
                    ViewBag.ThongBao = "Chọn hình ảnh";
                    return View();
                }
                //Thêm vào cơ sở dữ liệu
                if (ModelState.IsValid)
                {
                    //Lưu tên file
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    //Lưu đường dẫn của file
                    var path = Path.Combine(Server.MapPath("~/HinhAnhSanPham"), fileName);
                    //Kiểm tra hình ảnh đã tồn tại chưa
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                        return View();
                    }
                    else
                    {
                        fileUpload.SaveAs(path);
                    }
                    laptop.AnhBia = fileUpload.FileName;
                    db.Laptops.Add(laptop);
                    db.SaveChanges();
                }
                return RedirectToAction("QuanLiSanPham", "Admin");
            }
        }

        [AdminFilter]
        [HttpGet]
        public ActionResult ChinhSuaSanPham(int MaSanPham)
        {
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                Laptop laptop = db.Laptops.SingleOrDefault(n => n.Id == MaSanPham);
                if (laptop == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                ViewBag.HangLaptopId = new SelectList(db.HangLapTops.ToList(), "Id", "Name", laptop.HangLaptopId);
                ViewBag.HeDieuHanhId = new SelectList(db.HeDieuHanhs.ToList(), "Id", "Name", laptop.HeDieuHanhId);
                return View(laptop);

            }
        }
        [HttpPost]
        [AdminFilter]
        [ValidateInput(false)]
        public ActionResult ChinhSuaSanPham(Laptop laptop)
        {
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                //Thêm vào cơ sở dữ liệu
                if (ModelState.IsValid)
                {

                    db.Entry(laptop).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("QuanLiSanPham", "Admin");
            }
        }

        //xóa sản phẩm
        [HttpDelete]
        [AdminFilter]

        public ActionResult Delete(int id)
        {
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                Laptop laptop = db.Laptops.SingleOrDefault(n => n.Id == id);
                if (laptop == null)
                {
                    return null;
                }
                db.Laptops.Remove(laptop);
                db.SaveChanges();
                return RedirectToAction("QuanLiSanPham", "Admin");
            }
        }
    }
}