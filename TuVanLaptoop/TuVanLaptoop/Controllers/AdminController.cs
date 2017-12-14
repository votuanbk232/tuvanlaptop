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
        public ActionResult QuanLiLuat(int? page,string txtTimKiem,string search,string trash,string all)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 8;

            string message = "";
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                //kiểm tra nút search có đc click hay ko
                if (!string.IsNullOrEmpty(search))
                {
                    if (txtTimKiem != "")
                    {
                        string vetrai = SuKien.getIdByName(txtTimKiem);
                        List<Luat> luatList = db.Luats.Where(x => x.SuKienVT.Contains(vetrai)).ToList();
                        if (luatList.Count>0)
                        {
                            foreach (var item in luatList)
                            {
                                item.sukienvetrais = Luat.ConvertIntArrayToStringArray(item.SuKienVT);
                                //item.sukienvephai = SuKien.getSuKienById(Convert.ToInt16(item.SukienVP));
                            }
                            message = "Có  " + luatList.Count() + " luật thỏa mãn!";
                            ViewBag.ThongBao = message;
                            //db.Luats.ToList().OrderBy(n => n.Id).ToPagedList(pageNumber, pageSize)
                            return View(luatList.OrderBy(n => n.Id).ToPagedList(pageNumber, pageSize));
                        }
                        else
                        {
                            message = "Không có luật phù hợp!";
                        }
                    }
                    else
                    {
                        message = "Hãy nhập giá trị để tìm kiếm luật!";
                    }
                }
                //kiểm tra nút trash có đc click hay không
               if (!string.IsNullOrEmpty(trash))
                {
                    if (txtTimKiem != "")
                    {
                        string vetrai = SuKien.getIdByName(txtTimKiem);
                        //List<Luat> luatList = db.Luats.Where(x => x.SuKienVT.Contains(vetrai)).ToList();
                        //lọc theo txtvetrai đã đc convert tới int
                        List<Luat> listLuat=Luat.DeleteLuatByLuat(vetrai);
                        if (listLuat==null)
                        {
                            message = "Không có luật nào bị xóa!";
                        }
                        else
                        {
                            message = "Có " + listLuat.Count() + " luật bị xóa!";
                        }
                    }
                    else
                    {
                        message = "Hãy nhập giá trị để xóa!";
                    }
                }
                if (!string.IsNullOrEmpty(all))
                {
                    List<Luat> allLuat = db.Luats.ToList();
                    foreach (var item in allLuat)
                    {
                        item.sukienvetrais = Luat.ConvertIntArrayToStringArray(item.SuKienVT);
                        //item.sukienvephai = SuKien.getSuKienById(Convert.ToInt16(item.SukienVP));
                    }
                    message = "Có tất cả " + allLuat.Count() + " luật!";
                    ViewBag.ThongBao = message;
                    return View(allLuat.OrderBy(n => n.Id).ToPagedList(pageNumber, pageSize));
                }

                List<Luat> luats = db.Luats.ToList();
                foreach (var item in luats)
                {
                    item.sukienvetrais = Luat.ConvertIntArrayToStringArray(item.SuKienVT);
                    //item.sukienvephai = SuKien.getSuKienById(Convert.ToInt16(item.SukienVP));
                }
                ViewBag.ThongBao = message;
                //db.Luats.ToList().OrderBy(n => n.Id).ToPagedList(pageNumber, pageSize)
                return View(luats.OrderBy(n => n.Id).ToPagedList(pageNumber, pageSize));

                //if (txtTimKiem ==null)
                //{
                //    List<Luat> luats = db.Luats.ToList();
                //    foreach (var item in luats)
                //    {
                //        item.sukienvetrais = Luat.ConvertIntArrayToStringArray(item.SuKienVT);
                //        //item.sukienvephai = SuKien.getSuKienById(Convert.ToInt16(item.SukienVP));
                //    }
                //    //db.Luats.ToList().OrderBy(n => n.Id).ToPagedList(pageNumber, pageSize)
                //    return View(luats.OrderBy(n => n.Id).ToPagedList(pageNumber, pageSize));
                //}
                //else
                //{
                //    string vetrai = SuKien.getIdByName(txtTimKiem);
                //    List<Luat> luats = db.Luats.Where(x => x.SuKienVT.Contains(vetrai)).ToList();

                //    foreach (var item in luats)
                //    {
                //        item.sukienvetrais = Luat.ConvertIntArrayToStringArray(item.SuKienVT);
                //        //item.sukienvephai = SuKien.getSuKienById(Convert.ToInt16(item.SukienVP));
                //    }
                //    //db.Luats.ToList().OrderBy(n => n.Id).ToPagedList(pageNumber, pageSize)
                //    return View(luats.OrderBy(n => n.Id).ToPagedList(pageNumber, pageSize));
                //}

            }

        }
        
       

    }
}