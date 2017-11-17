using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuVanLaptoop.Models;

namespace TuVanLaptoop.Controllers
{
    public class GioHangController : Controller
    {
        TuVanLaptopEntities db = new TuVanLaptopEntities();

        // lấy giỏ hàng
        public List<GioHang> layGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        //thêm giỏ hàng
        public ActionResult ThemGioHang(int MaSanPham, string strUrl)
        {
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                //Kiểm tra sản phẩm có tồn tại ko
                Laptop laptop = db.Laptops.SingleOrDefault(n => n.Id == MaSanPham);
                if (laptop == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                List<GioHang> lst = layGioHang();
                //kiểm tra sản phẩm đã tồn tại trong list chưa
                GioHang sp = lst.Find(n => n.iMaSanPham == MaSanPham);
                if (sp == null)
                {
                    sp = new GioHang(MaSanPham);
                    //Thêm sản phẩm vào list
                    lst.Add(sp);
                    return Redirect(strUrl);
                }
                else
                {
                    sp.iSoLuong++;
                    return Redirect(strUrl);
                }

            }
        }
        //xây dựng trang giỏ hàng
        public ActionResult GioHang()
        {
            
            //if (Session["GioHang"] == null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            List<GioHang> lst = layGioHang();
            return View(lst);
        }
        //sửa giỏ hàng
        public ActionResult CapNhatGioHang(int MaSanPham, FormCollection f)
        {
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                //Kiểm tra sản phẩm có tồn tại ko
                Laptop laptop = db.Laptops.SingleOrDefault(n => n.Id == MaSanPham);
                if (laptop == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                List<GioHang> lst = layGioHang();
                GioHang sp = lst.SingleOrDefault(n => n.iMaSanPham == MaSanPham);
                if (sp != null)
                {
                    sp.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
                }
                return RedirectToAction("SuaGioHang");


            }
        }

        //xóa giỏ hàng
        public ActionResult XoaGioHang(int MaSanPham)
        {
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                //Kiểm tra sản phẩm có tồn tại ko
                Laptop sach = db.Laptops.SingleOrDefault(n => n.Id == MaSanPham);
                if (sach == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                List<GioHang> lst = layGioHang();
                GioHang sp = lst.SingleOrDefault(n => n.iMaSanPham == MaSanPham);
                if (sp != null)
                {
                    lst.RemoveAll(n => n.iMaSanPham == MaSanPham);
                }
                if (lst.Count == 0) { return RedirectToAction("Index", "Home"); }
                return RedirectToAction("SuaGioHang");
            }
        }
       
        //tính tổng số lượng và tổng tiền
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        //Tính tổng thành tiền
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.ThanhTien);
            }
            return dTongTien;
        }
        //partial Giỏ hàng
        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        //View Chỉnh sửa giỏ hàng
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lst = layGioHang();
            if (lst.Count==0)
            {
                @TempData["ThongBao"] = "Không có sản phẩm trong giỏ hàng, không thể chỉnh sửa";
                return RedirectToAction("GioHang");
            }
            return View(lst);
        }

        //Đặt Hàng
        [HttpPost]
        public ActionResult DatHang()
        {
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                //Kiểm tra đăng nhập
                if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
                {
                    return RedirectToAction("DangNhap", "NguoiDung");
                }
                //Kiểm tra giỏ hàng
                if (Session["Giohang"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                DonHang dh = new DonHang();
                KhachHang kh = (KhachHang)Session["TaiKhoan"];
                List<GioHang> lst = layGioHang();
                if (lst.Count == 0)
                {
                    TempData["ThongBao"] = "Không có sản phẩm trong giỏ hàng, không thể đặt hàng";
                    return RedirectToAction("GioHang");
                }
                dh.MaKH = kh.MaKH;
                dh.NgayDat = DateTime.Now;
                dh.TinhTrangGiaoHang = false; //false là chưa giao hàng
                db.DonHangs.Add(dh);
                //Thêm chi tiết đơn hàng
                foreach (GioHang item in lst)
                {
                    ChiTietDonHang ctdh = new ChiTietDonHang();
                    ctdh.MaDonHang = dh.MaDonHang;
                    ctdh.MaSanPham = item.iMaSanPham;
                    ctdh.SoLuong = item.iSoLuong;
                    ctdh.DonGia = (decimal)item.dDonGia;
                    db.ChiTietDonHangs.Add(ctdh);
                }
                db.SaveChanges();
                Session["Giohang"] = null;
                return RedirectToAction("Index", "Home");
            }
        }
    }
}