using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuVanLaptoop.Models
{
    public class GioHang
    {
        public int iMaSanPham { get; set; }
        public string sTenSanPham { get; set; }
        public string sAnhBia { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double ThanhTien { get { return iSoLuong * dDonGia; } }

        TuVanLaptopEntities db = new TuVanLaptopEntities();
        public GioHang(int MaSanPham)
        {
            iMaSanPham = MaSanPham;
            Laptop laptop = db.Laptops.Single(n => n.Id == MaSanPham);
            sTenSanPham = laptop.Name;
            sAnhBia = laptop.AnhBia;
            dDonGia = double.Parse(laptop.gia.ToString());
            iSoLuong = 1;
        }
    }
}