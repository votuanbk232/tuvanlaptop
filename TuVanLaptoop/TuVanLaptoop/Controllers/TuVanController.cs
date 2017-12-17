using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TuVanLaptoop.Models;
using TuVanLaptoop.ViewModels;

namespace TuVanLaptoop.Controllers
{

    public class TuVanController : Controller
    {
        TuVanLaptopEntities db = new TuVanLaptopEntities();
        // GET: TuVan

        //    public ActionResult Index()
        //{
        //    return PartialView();
        //}
        Luat currentLuat=null;
        public ActionResult DoTinCayPartial()
        {
            return PartialView(currentLuat);
        }
        public ActionResult Index(FormCollection model, string submit)
        {
            //tổng độ tin cậy của luật tạo mới
            int dotincay = 0;
            //số lượng luật đã lấy
            int soluongluatbandau = 0;
            //phụ thuộc vào các yếu tố của người dùng(nghề nghiệp,....)
            bool luattontai = true;
            //danh sách câu query thu đc từ các sự kiện
            List<String> queryvephai = new List<String>();
            //lấy yêu cầu từ người dùng string[string]
            //chuyển yêu cầu đó sang string[int]
            //Từ string[int] của sự kiện vế trái, ta lấy đc id của sự kiện vế phải
            //chuyển id của sự kiện vế phải sang string(sql)
            //từ sql ta truy vấn đc sản phẩm
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                StringBuilder value = new StringBuilder();
                //mảng sự kiện vế trải (int) của Luật
                List<String> list = new List<String>();
                //danh sách yêu cầu(string)
                List<String> yeucau = new List<String>();

                //kiểm tra submit có đc clicked chưa
                if (string.IsNullOrEmpty(submit))
                {
                    TempData["message"] = "Vui lòng nhấn submit";
                    return RedirectToAction("NotFoundSanPham", "Home");
                }

                //lấy id của item selected
                if (model["Gioitinhs"].ToString() != "")
                {
                    list.Add(SuKien.getSuKienId(model["Gioitinhs"].ToString())); //lấy đc id sk
                    yeucau.Add(model["Gioitinhs"].ToString());
                    //kiểm tra luật với sự kiện này có tồn tại hay không
                    //nếu không tồn tại=> trả về false
                    if (!Luat.CheckLuatTonTaiWithVeTrai(SuKien.getSuKienId(model["Gioitinhs"].ToString())))
                    {
                        luattontai = false;
                    }
                    //nếu luật có tồn tại, lấy query vế phải của luật theo sự kiện vế trái
                    else
                    {
                        //từ sự kiện ID lấy đc câu query của luật
                        string vp = Luat.getVePhaiByVeTrai(SuKien.getSuKienId(model["Gioitinhs"].ToString()));
                        dotincay += Luat.GetDoTinCayByVetrai(SuKien.getSuKienId(model["Gioitinhs"].ToString()));
                        soluongluatbandau++;
                        queryvephai.Add(vp);
                    }
                    //lưu trạng thái tư vấn của combobox
                    //TempData["GioiTinhs"] = new SelectList(db.GioiTinhs.ToList(), "Name", "Name", model["Gioitinhs"].ToString());

                }

                if (model["NgheNghieps"].ToString() != "")
                {
                    yeucau.Add(model["NgheNghieps"].ToString());
                    list.Add(SuKien.getSuKienId(model["NgheNghieps"].ToString()));

                    if (!Luat.CheckLuatTonTaiWithVeTrai(SuKien.getSuKienId(model["NgheNghieps"].ToString())))
                    {
                        luattontai = false;
                    }
                    else
                    {
                        //từ sự kiện ID lấy đc câu query của luật
                        string vp = Luat.getVePhaiByVeTrai(SuKien.getSuKienId(model["NgheNghieps"].ToString()));
                        dotincay += Luat.GetDoTinCayByVetrai(SuKien.getSuKienId(model["NgheNghieps"].ToString()));
                        soluongluatbandau++;
                        queryvephai.Add(vp);
                    }
                }
                if (model["MucDichs"].ToString() != "")
                {
                    yeucau.Add(model["MucDichs"].ToString());
                    list.Add(SuKien.getSuKienId(model["MucDichs"].ToString()));

                    if (!Luat.CheckLuatTonTaiWithVeTrai(SuKien.getSuKienId(model["MucDichs"].ToString())))
                    {
                        luattontai = false;
                    }
                    else
                    {
                        //từ sự kiện ID lấy đc câu query của luật
                        string vp = Luat.getVePhaiByVeTrai(SuKien.getSuKienId(model["MucDichs"].ToString()));
                        dotincay += Luat.GetDoTinCayByVetrai(SuKien.getSuKienId(model["MucDichs"].ToString()));
                        soluongluatbandau++;
                        queryvephai.Add(vp);
                    }
                }
                if (model["YeuCauGiaTiens"].ToString() != "")
                {
                    yeucau.Add(model["YeuCauGiaTiens"].ToString());
                    list.Add(SuKien.getSuKienId(model["YeuCauGiaTiens"].ToString()));

                    if (Luat.CheckLuatTonTaiWithVeTrai(SuKien.getSuKienId(model["YeuCauGiaTiens"].ToString())))
                    {
                        dotincay += Luat.GetDoTinCayByVetrai(SuKien.getSuKienId(model["YeuCauGiaTiens"].ToString()));
                        soluongluatbandau++;
                    }
                }
                if (model["HangLaptops"].ToString() != "")
                {
                    yeucau.Add(model["HangLaptops"].ToString());
                    list.Add(SuKien.getSuKienId(model["HangLaptops"].ToString()));

                    if (Luat.CheckLuatTonTaiWithVeTrai(SuKien.getSuKienId(model["HangLaptops"].ToString())))
                    {
                        dotincay += Luat.GetDoTinCayByVetrai(SuKien.getSuKienId(model["HangLaptops"].ToString()));
                        soluongluatbandau++;
                    }
                }

                if (model["HeDieuHanhs"].ToString() != "")
                {
                    yeucau.Add(model["HeDieuHanhs"].ToString());
                    list.Add(SuKien.getSuKienId(model["HeDieuHanhs"].ToString()));


                    if (Luat.CheckLuatTonTaiWithVeTrai(SuKien.getSuKienId(model["HeDieuHanhs"].ToString())))
                    {
                        dotincay += Luat.GetDoTinCayByVetrai(SuKien.getSuKienId(model["HeDieuHanhs"].ToString()));
                        soluongluatbandau++;
                    }
                }


                //lấy chuỗi query từ các các sự kiện 100%( giá tiền, hệ điều hành, hãng laptop)
                string mingia = "";
                string maxgia = "";
                string hangsanxuat = model["HangLaptops"].ToString();
                string hedieuhanh = model["HeDieuHanhs"].ToString();
                if (model["YeuCauGiaTiens"].ToString() == "Trên 20 triệu")
                {
                    mingia = 20000000.ToString();
                }
                if (model["YeuCauGiaTiens"].ToString() == "Từ 15 đến 20 triệu")
                {
                    mingia = 15000000.ToString();
                    maxgia = 20000000.ToString();
                }
                if (model["YeuCauGiaTiens"].ToString() == "Từ 10 đến 15 triệu")
                {
                    mingia = 10000000.ToString();
                    maxgia = 15000000.ToString();
                }
                if (model["YeuCauGiaTiens"].ToString() == "Dưới 10 triệu")
                {
                    maxgia = 10000000.ToString();
                }
                //chuỗi thu đc từ các sự kiện 100%
                string simple = Laptop.getLaptopSimple(mingia, maxgia, hangsanxuat, hedieuhanh);
                if (simple != "")
                {
                    queryvephai.Add(simple);

                }
                #region ko có yêu cầu

                //đầu tiền kiểm tra xem có yêu cầu ko ( count của list)
                if (list.Count == 0)
                {
                    TempData["message"] = "Không có yêu cầu được đưa ra.Vui lòng chọn các mục để được tư vấn!";
                    return RedirectToAction("NotFoundSanPham", "Home");
                }
                #endregion
                //Nếu có yêu cầu ( list count!=0)
                //nếu chỉ chứa luật simple
                if (model["Gioitinhs"].ToString() == "" && model["NgheNghieps"].ToString() == "" && model["MucDichs"].ToString() == "")
                {
                    var sql = "SELECT  * FROM dbo.Laptop WHERE " + simple;
                    var laptops = db.Laptops.SqlQuery(sql).ToList();

                    if (laptops == null)
                    {
                        TempData["message"] = "Truy vấn query!Chưa có sản phẩm gợi ý!";
                        return RedirectToAction("NotFoundSanPham", "Home");
                    }
                    ViewBag.ThongBao = "Truy vấn query!Có " + laptops.Count() + " sản phẩm được gợi ý!";
                    return PartialView(laptops);
                }

                //nếu tồn tại luật ko simple
                //khi ấn tư vấn sẽ lấy đc mảng yêu cầu của khách hàng và điều kiện where
                string mangYeuCau = String.Join(",", yeucau.ToArray());
                string query = String.Join(" AND ", queryvephai.ToArray());

                #region luật cũ
                //Xét luật đã tồn tại hay chưa dựa vào vế trái ArrayInt
                string vetraiArrayInt = String.Join(",", list.ToArray());

                //vế trái của luật có thể trùng nhau, vế phải khác nhau.
                //lấy luật có độ tin cậy cao nhất trong số các luật có vế trái==vetraiArrayInt
                string vephaiByvetraiArrayInt = Luat.getVePhaiByVeTrai(vetraiArrayInt);
                //nếu vế phải tồn tại, tức luật tồn tại
                //if (vephaiByvetraiArrayInt != null)
                //{
                //    //lấy luật có độ tin cậy cao nhất dựa vào vế trái([int]) đã xác định
                //    Luat luat = Luat.getLuatByVeTrai(vetraiArrayInt); 

                //    var laptops = Laptop.getLaptopByVePhai(vephaiByvetraiArrayInt);
                //    if (laptops == null)
                //    {
                //        TempData["message"] = "Luật tồn tại-Chưa có sản phẩm gợi ý---\nYêu cầu: " + mangYeuCau;
                //        return RedirectToAction("Index", "Home");
                //    }
                //    //khi có sản phẩm, hiện section đánh giá độ tin cậy
                //    TempData["CheckLuatTonTai"] = "Luật tồn tại";
                //    //lấy id của luật đó:
                //    int id = luat.Id;
                //    TempData["LuatId"] = id;
                //    //mô tả luật
                //    TempData["MoTaLuat"] = Luat.GetMoTaLuat(id);
                //    //lấy độ tin cậy của luật đó
                //    TempData["DoTinCay"] = Luat.GetDoTinCay(id);

                //    ViewBag.ThongBao = "Luật tồn tại-Có: " + laptops.Count() + " sản phẩm được gợi ý!---" + "\nYêu cầu:" + mangYeuCau;
                //    return View(laptops);

                //}
                #endregion
                #region ko đủ sự kiện để tạo luật
                //Nếu luật chưa tồn tại, mới cần xét tự sinh luật
                //nếu luattontai là false tức ko lấy đủ số lượng slq query tự sinh
                //lúc này sẽ ko tự tạo ra đc câu lệnh sqlquery(cần Admin), thông báo người dùng
                if (luattontai == false)
                {
                    TempData["message"] = "Luật không tồn tại---\nYêu cầu: " + mangYeuCau;
                    return RedirectToAction("NotFoundSanPham", "Home");
                }
                #endregion
                #region đủ sự kiện để tạo luật mới
                //nếu luatontai là true, tức sẽ có luật mới tự sinh ra(nếu độ tin cậy cao hơn luật cũ)
                //hoặc sẽ lấy luật đã tồn tại
                else
                {
                    //ta đã có query và sư kiện vế trái=> tạo ra luật mới
                    //luật mới,lúc này chưa tạo mới
                    //Luat newluat = Luat.GetLuat(vetraiArrayInt, query, dotincay / (queryvephai.Count()), query);
                    Luat newluat = Luat.GetLuat(vetraiArrayInt, query, dotincay / soluongluatbandau, query);


                    //nếu tồn tại luật cũ
                    if (vephaiByvetraiArrayInt != null)
                    {
                        //lấy luật có độ tin cậy cao nhất dựa vào vế trái([int]) đã xác định
                        Luat luat = Luat.getLuatByVeTrai(vetraiArrayInt);

                        currentLuat = luat;
                        DoTinCayPartial();

                        //luật cũ có độ tin cậy cao hơn so với luật mới=>lấy luật cũ, ko tạo luật mới
                        if (luat.DoTinCay >= newluat.DoTinCay)
                        {
                            var laptopList = Laptop.getLaptopByVePhai(vephaiByvetraiArrayInt);


                            // hiện section đánh giá độ tin cậy
                            TempData["CheckLuatTonTai"] = "Luật tồn tại";
                            //lấy id của luật đó:
                            int idold = luat.Id;
                            TempData["LuatId"] = idold;
                            //mô tả luật
                            TempData["MoTaLuat"] = Luat.GetMoTaLuat(idold);
                            //lấy độ tin cậy của luật đó
                            TempData["DoTinCay"] = Luat.GetDoTinCay(idold);

                            if (laptopList == null)
                            {
                                TempData["message"] = "Luật tồn tại!Chưa có sản phẩm gợi ý---\nYêu cầu: " + mangYeuCau;
                                return RedirectToAction("NotFoundSanPham", "Home");
                            }
                            ViewBag.ThongBao = "Luật tồn tại!Có " + laptopList.Count() + " sản phẩm được gợi ý!---" + "\nYêu cầu:" + mangYeuCau;
                            return PartialView(laptopList);
                        }
                        else
                        {
                            //nếu luật mới sinh ra có độ tin cậy cao hơn=> tạo luật mới
                            //tạo ở bước sau
                        }
                        
                    }
                    Luat.SaveLuat(newluat);
                    var queryfull = "SELECT  * FROM dbo.Laptop WHERE " + query;
                    List<Laptop> laptops = db.Laptops.SqlQuery(queryfull).ToList();

                    //hiện section đánh giá độ tin cậy
                    TempData["CheckLuatTonTai"] = "Luật tồn tại";
                    //lấy id của luật đó:
                    int id = newluat.Id;
                    TempData["LuatId"] = id;
                    //mô tả luật
                    TempData["MoTaLuat"] = Luat.GetMoTaLuat(id);
                    //lấy độ tin cậy của luật đó
                    TempData["DoTinCay"] = Luat.GetDoTinCay(id);

                    if (laptops.Count == 0)
                    {
                        TempData["message"] = "Luật mới được tạo!Chưa có sản phẩm gợi ý---\nYêu cầu: " + mangYeuCau;
                        return RedirectToAction("NotFoundSanPham", "Home");

                    }

                   
                    ViewBag.ThongBao = "Luật mới được tạo!Có " + laptops.Count() + " sản phẩm được gợi ý!---" + "\nYêu cầu:" + mangYeuCau;
                    return PartialView(laptops);
                }
                #endregion

            }
        }
    }
}