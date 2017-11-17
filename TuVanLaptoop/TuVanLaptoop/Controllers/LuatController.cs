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
        [AdminFilter]
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
                    luat.SukienVP = db.SuKiens.Single(n => n.Id.ToString() == luat.SukienVP).Name;
                    ViewBag.ThongBao = "Sửa luật";
                }
                else
                {
                    ViewBag.ThongBao = "Thêm luật";
                }
                //luật vế trái chỉ lấy các sự kiện ở giao diện
                luat.SuKienCollection = db.SuKiens.Take(28).ToList();
                ViewBag.IdLuat = id;
                return View(luat);
            }
        }

        //cách truyền Url bằng Request.Url.ToString(), nếu như view có error thông báo=>mất ViewBag.
        [AdminFilter]
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

                    Luat luat1 = db.Luats.Where(x => x.Id == id).FirstOrDefault(); ;
                    luat.SuKienSelectedIDArray = luat1.SuKienVT.Split(',').ToArray();

                    luat.SuKienCollection = db.SuKiens.Take(28).ToList();
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
                        string giatrisau = "N" + "'%" + giatri[i] + "%'";
                        vephai += thuoctinh[i] + " " + toantu[i] + " " + giatrisau;

                    }
                    else
                    {
                        vephai += thuoctinh[i] + " " + toantu[i] + " " + giatri[i];
                    }

                }

                //thêm sự kiện vế phải vào bảng sự kiện
                SuKien sk = new SuKien();
                //sk.Name = luat.SukienVP; // trước
                sk.Name = vephai; // sau 14/11
                                  //kiểm tra sự kiện vế phải  đã tồn tại hay chưa
                SuKien skCheck = db.SuKiens.SingleOrDefault(n => n.Name == vephai);
                //chưa tồn tại, thêm mới sự kiện
                if (skCheck == null)
                {
                    db.SuKiens.Add(sk);
                    db.SaveChanges();
                    //gán Id của sự kiện vế phải vừa thêm vào csdl bằng với SukienVP của luật
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
                //return Redirect(stringUrl);
                //return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
                return RedirectToAction("QuanLiLuat", "Admin");
            }
        }

        //xóa luật
        [HttpDelete]
        [AdminFilter]

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