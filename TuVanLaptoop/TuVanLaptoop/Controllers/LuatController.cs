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
        //Luật vế trái có thể giống nhau, luật vế phải khác nhau
        TuVanLaptopEntities db = new TuVanLaptopEntities();
        [HttpGet]
        //[AdminFilter]
        public ActionResult ThemLuatOrSuaLuat(int id, string stringUrl)
        {
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                //lấy url trang trước đó
                ViewBag.Url = stringUrl;
                Luat luat = new Luat();
                //khi sửa luật, tức có id truyền vào
                if (id != 0)
                {
                    luat = db.Luats.Where(x => x.Id == id).FirstOrDefault();
                    luat.SuKienSelectedIDArray = luat.SuKienVT.Split(',').ToArray();
                    luat.GiaiThich = "tự cập nhật";
                    ViewBag.ThongBao = "Sửa luật";
                }
                else
                {
                    ViewBag.ThongBao = "Thêm luật";
                }
                //luật vế trái chỉ lấy các sự kiện ở giao diện
                luat.SuKienCollection = db.SuKiens.ToList();
                //gán giá trị độ tin cậy mặc định là 100
                luat.DoTinCay = 100;
                luat.GiaiThich = "tự cập nhật";
                ViewBag.IdLuat = id;
                return View(luat);
            }
        }

        //cách truyền Url bằng Request.Url.ToString(), nếu như view có error thông báo=>mất ViewBag.
        //[AdminFilter]
        [HttpPost]
        public ActionResult ThemLuatOrSuaLuat(int id, Luat luat, string[] thuoctinh, string[] toantu, string[] giatri, string stringUrl)
        {
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                //lấy sự kiện vế phải từ thuộc tính, toán tử và giá trị
                //check sửa luật
                string vephai = "";
                //nếu vế phải vẫn là rỗng=> thông báo và ko lưu
                if (thuoctinh == null && toantu == null && giatri == null)
                {
                    //trả về luật ban đầu
                    Luat luat1 = db.Luats.Where(x => x.Id == id).FirstOrDefault(); ;
                    luat.SuKienSelectedIDArray = luat1.SuKienVT.Split(',').ToArray();

                    luat.SuKienCollection = db.SuKiens.ToList();
                    ViewBag.VePhaiRong = "Vui lòng chọn thêm sự kiện vế phải, không được để trống";
                    return View(luat);

                }
                for (int i = 0; i < toantu.Length; i++)
                {
                    if (i != 0)
                    {
                        vephai += " AND ";
                    }
                    if (thuoctinh[i] == "mausac")
                    {
                        string giatrisau = "N" + "'%" + giatri[i].Trim() + "%'";
                        vephai += thuoctinh[i] + " " + toantu[i] + " " + giatrisau;

                    }
                    else
                    {
                        vephai += thuoctinh[i] + " " + toantu[i] + " " + giatri[i];
                    }

                }
                luat.SuKienVT = string.Join(",", luat.SuKienSelectedIDArray);
                luat.SukienVP = vephai;
                if (luat.GiaiThich == "tự cập nhật")
                {
                    luat.GiaiThich = vephai;
                }
                if (luat.DoTinCay == null)
                {
                    luat.DoTinCay = 100;
                }
                //khi chưa có Id, tức đang tạo luật mới
                if (luat.Id == 0)
                {
                        //thêm luật vào bảng luật
                        db.Luats.Add(luat);
                }

                //khi có Id tức sửa luật
                else
                {
                    db.Entry(luat).State = EntityState.Modified;
                }
                db.SaveChanges();
               
                return RedirectToAction("QuanLiLuat", "Admin");
            }
        }

        //xóa luật
        [HttpDelete]
        //[AdminFilter]

        public ActionResult Delete(int id)
        {
            using(TuVanLaptopEntities db=new TuVanLaptopEntities())
            {
                Luat luat = db.Luats.SingleOrDefault(n => n.Id == id);
                if (luat == null)
                {
                    return null;
                }
                db.Luats.Remove(luat);
                db.SaveChanges();
                return RedirectToAction("QuanLiLuat", "Admin");
            }
          
        }


        //Xử lí độ tin cậy: Người dùng đánh giá(click)-sử dụng ajax
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
        public ActionResult ResetDoTinCay(int MaLuat, string strUrl,int dotincay)
        {
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                Luat luat = db.Luats.SingleOrDefault(n => n.Id == MaLuat);
                if (luat == null)
                {
                    return null;
                }
                luat.DoTinCay=dotincay;
                db.Entry(luat).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Redirect(strUrl);
        }



    }
}