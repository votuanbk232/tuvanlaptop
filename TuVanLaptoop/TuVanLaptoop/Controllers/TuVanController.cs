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
        public ActionResult Index(FormCollection model,string submit)
        {
            StringBuilder value = new StringBuilder();
            //mảng sự kiện vế trải (int) của Luật
            List<String> list = new List<String>();
            //danh sách yêu cầu
            List<String> yeucau = new List<String>();

            //kiểm tra submit có đc clicked chưa
            if (string.IsNullOrEmpty(submit))
            {
                return RedirectToAction("Index", "Home");
            }

                //lấy id của item selected
                if (model["Gioitinhs"].ToString() != "")
                    {
                        list.Add(getSuKienId(model["Gioitinhs"].ToString()));
                        yeucau.Add(model["Gioitinhs"].ToString());
            }

                    if (model["NgheNghieps"].ToString() != "")
                    {
                        yeucau.Add(model["NgheNghieps"].ToString());
                        list.Add(getSuKienId(model["NgheNghieps"].ToString()));
            }
                    if (model["MucDichs"].ToString() != "")
                    {
                        yeucau.Add(model["MucDichs"].ToString());
                        list.Add(getSuKienId(model["MucDichs"].ToString()));
            }
                    if (model["YeuCauGiaTiens"].ToString() != "")
                    {
                        yeucau.Add(model["YeuCauGiaTiens"].ToString());
                        list.Add(getSuKienId(model["YeuCauGiaTiens"].ToString()));
            }
                    if (model["HangLaptops"].ToString() != "")
                    {
                        yeucau.Add(model["HangLaptops"].ToString());
                        list.Add(getSuKienId(model["HangLaptops"].ToString()));
            }
                
                    if (model["HeDieuHanhs"].ToString() != "")
                    {
                        yeucau.Add(model["HeDieuHanhs"].ToString());
                        list.Add(getSuKienId(model["HeDieuHanhs"].ToString()));
            }
                    //khi ko có yêu cầu, trả về trang chủ
            if (list.Count == 0)
            {
                TempData["message"] = "Không có yêu cầu đưa ra!";
                return RedirectToAction("Index", "Home");
            }
            //có yêu cầu
            string mangYeuCau = String.Join(",", yeucau.ToArray());

            //khi ko có những sự kiện có thể chứa luật(giới tính, nghề nghiệp, mục đích sở dụng)
            if (model["Gioitinhs"].ToString() == "" && model["NgheNghieps"].ToString() == "" && model["MucDichs"].ToString() == "")
            {
                string mingia="";
                string maxgia="";
                string hangsanxuat= model["HangLaptops"].ToString();
                string hedieuhanh= model["HeDieuHanhs"].ToString();
                if(model["YeuCauGiaTiens"].ToString()== "Trên 20 triệu")
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
                List<Laptop> laptops_child =getLaptopSimple(mingia,maxgia,hangsanxuat,hedieuhanh);
                //khi ko có sản phẩm đc gợi ý
                if (laptops_child.Count == 0)
                {
                    TempData["message"] = "Sử dụng query-Có " + laptops_child.Count() + " sản phẩm được gợi ý!" + "\nYêu cầu:" + mangYeuCau;
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.ThongBao = "Sử dụng query-Có " + laptops_child.Count() + " sản phẩm được gợi ý!" + "\nYêu cầu:" + mangYeuCau;
                return View(laptops_child);
            }
            //Khi sự kiện có thể chứa luật(giới tính, nghề nghiệp, mục đích sở dụng)
            string vetrai = String.Join(",", list.ToArray());
            string vephai = getVePhaiByVeTrai(vetrai);
            if (vephai == null)
            {
                TempData["message"] = "Luật không tồn tại\nYêu cầu: "+ mangYeuCau;
                return RedirectToAction("Index", "Home");
            }
            string sukien = getSuKienById(Convert.ToInt16(vephai));
            //if (sukien == null)
            //{
            //    ViewBag.ThongBao = "Chưa có sản phẩm được gợi ý-Yêu cầu: ";
            //    return RedirectToAction("Index", "Home");
            //}
            List<Laptop> laptops = getLaptopBySuKien(sukien);
            if (laptops.Count==0)
            {
               
                    TempData["message"] = "Luật tồn tại-Chưa có sản phẩm gợi ý\nYêu cầu: " + mangYeuCau
                        + "\nĐộ tin cậy:" + getDoTinCay(vetrai); ;
                

                return RedirectToAction("Index", "Home");
            }
            ViewBag.ThongBao = "Luật tồn tại-Có "+laptops.Count()+" sản phẩm được gợi ý!"+"\nYêu cầu:"+mangYeuCau;

            return View(laptops);
            }
        //lấy danh sách laptop dựa vào các sự kiện(giá tiền,hệ điều hành,hãng laptop)
        public List<Laptop> getLaptopSimple(string mingia, string maxgia, string hangsanxuat, string hedieuhanh)
        {
            string mingiaString = (mingia != "") ? "gia>=" + mingia : "";
            string maxgiaString = (maxgia != "") ? "gia<" + maxgia : "";
            string hanglaptopIdString = (hangsanxuat != "") ? " HangLaptopId=" + getHangSanXuatId(hangsanxuat) : "";
            string hedieuhanhIdString = (hedieuhanh != "") ? " HeDieuHanhId=" + getHeDieuHanhId(hedieuhanh) : "";
            //tạo query dùng list:(kết hợp và tạo thêm ' and ' cho query)
            List<String> list = new List<string>();
            if (mingiaString != "")
            {
                list.Add(mingiaString);
            }
            if (maxgiaString != "")
            {
                list.Add(maxgiaString);
            }
            if (hanglaptopIdString != "")
            {
                list.Add(hanglaptopIdString);
            }
            if (hedieuhanhIdString != "")
            {
                list.Add(hedieuhanhIdString);
            }
            String query = String.Join(" and ",list.ToArray());
            var laptops = db.Laptops.SqlQuery("SELECT * FROM dbo.Laptop where " + query) ;
            return laptops.ToList();
        }
        //lấy hãng sản xuất id dựa vào name
        public String getHangSanXuatId(string name)
        {
            HangLapTop rs = db.HangLapTops.SingleOrDefault(n => n.Name == name);
            if (rs == null) { return null; }
            return (rs.Id).ToString();
        }
        //lấy hệ điều hành id dựa vào name
        public String getHeDieuHanhId(string name)
        {
            HeDieuHanh rs = db.HeDieuHanhs.SingleOrDefault(n => n.Name == name);
            if (rs == null) { return null; }
            return (rs.Id).ToString();
        }
        //lấy danh sách laptop dựa vào tên sự kiện từ vế phải Id
        public List<Laptop> getLaptopBySuKien(string sukien)
        {
            var laptops = db.Laptops.SqlQuery("SELECT * FROM dbo.Laptop where " + sukien);
            return laptops.ToList();
        }
        //lấy Id của sự kiện dựa vào Name(Bảng sự kiện)
        public String getSuKienId(string name)
        {
            SuKien sk= db.SuKiens.SingleOrDefault(n => n.Name == name);
            if (sk == null) { return null; }
            return (sk.Id).ToString();
        }
        //Lấy Id của sự kiện vế phải dựa vào dãy sự kiện vế trái
        // cần xét thêm độ tin cậy( lấy luật có độ tin cậy cao nhất)
        public String getVePhaiByVeTrai(string vetrai)
        {
            //Luat sk = db.Luats.SingleOrDefault(n => n.SuKienVT == vetrai);
            var query= "SELECT TOP 1 * FROM dbo.Luat WHERE SuKienVT = '"+vetrai+"'" + " ORDER BY  DoTinCay DESC ";
            Luat sk = db.Luats.SqlQuery(query).FirstOrDefault();
            if (sk == null) { return null; }
            return sk.SukienVP;
        }
        //lấy độ tin cậy(max trong các bản ghi) dựa vào vế trái của luật(tương tự lấy sự kiện vế phải)
        public int getDoTinCay(string vetrai)
        {
            //Luat sk = db.Luats.SingleOrDefault(n => n.SuKienVT == vetrai);
            var query = "SELECT TOP 1 * FROM dbo.Luat WHERE SuKienVT = '" + vetrai + "'" + " ORDER BY  DoTinCay DESC ";
            Luat sk = db.Luats.SqlQuery(query).FirstOrDefault();

            if (sk == null) { return 0; }
            return (int)sk.DoTinCay;
        }
        //Lấy giá trị sự kiện dựa vào ID(bảng sự kiện)
        public String getSuKienById(int id)
        {
            SuKien sk = db.SuKiens.Single(n => n.Id == id);
            return (sk.Name).ToString();
        }
        //public ActionResult Index()
        //{

        //    var dk = "mausac Like N'%Hồng%'";
        //    var query = db.Laptops.SqlQuery("SELECT * FROM dbo.Laptop where " + dk);


        //    return View(query.ToList());
        //}
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    string query = "SELECT * FROM Department WHERE DepartmentID = {0}";
        //    var department = await _context.Departments
        //        .FromSql(query, id)
        //        .Include(d => d.Administrator)
        //        .AsNoTracking()
        //        .SingleOrDefaultAsync();

        //    if (department == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(department);
        //}
        //public IEnumerable<Laptop> GetSanPham()
        //{
        //    List<Laptop> laptops = new List<Laptop>();
        //    string query = "SELECT * from dbo.Laptop where mausac Like N'%Hồng%'";
        //    string constr = ConfigurationManager.ConnectionStrings["TuVanLaptopEntities"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(query))
        //        {
        //            cmd.Connection = con;
        //            con.Open();
        //            using (SqlDataReader sdr = cmd.ExecuteReader())
        //            {
        //                while (sdr.Read())
        //                {
        //                    laptops.Add(new Laptop
        //                    {
        //                        Id = (int)sdr["Id"],
        //                        NgayCapNhat = (DateTime)sdr["NgayCapNhat"],
        //                        mausac = sdr["mausac"].ToString(),
        //                        gia = (decimal)sdr["gia"],
        //                        Name = sdr["Name"].ToString(),
        //                        MoTa = sdr["MoTa"].ToString(),
        //                        AnhBia = sdr["AnhBia"].ToString(),
        //                        cardroi = (bool)sdr["cardroi"],
        //                        dophangiai = (Boolean)sdr["dophangiai"],
        //                        HangLaptopId = (int)sdr["HangLaptopId"],
        //                        HeDieuHanhId = (int)sdr["HeDieuHanhId"],
        //                        cpu = sdr["cpu"].ToString(),
        //                        khoiluong = (float)sdr["khoiluong"],
        //                        manhinh = (float)sdr["manhinh"],
        //                        ocung = (int)sdr["ocung"],
        //                        pin = (float)sdr["pin"],
        //                        ram = (int)sdr["ram"]
        //                    });
        //                }
        //            }
        //            con.Close();
        //            return laptops;
        //        }
        //    }
        //}
    }
}