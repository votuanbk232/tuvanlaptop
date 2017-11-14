using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuVanLaptoop.Models;

namespace TuVanLaptoop.Models
{
    [MetadataTypeAttribute(typeof(Laptop_Metadata))]
    public partial class Laptop
    {
        //TuVanLaptopEntities db = new TuVanLaptopEntities();

        //public IEnumerable<SelectListItem> AllFields { get; set; }

        //lấy danh sách laptop dựa vào tên sự kiện từ vế phải Id(string)
        public static List<Laptop> getLaptopBySuKien(string sukien)
        {
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                var laptops = db.Laptops.SqlQuery("SELECT * FROM dbo.Laptop where " + sukien);

                return laptops.ToList();
            }

        }
        //lấy danh sách laptop dựa vào các sự kiện(giá tiền,hệ điều hành,hãng laptop)
        public static List<Laptop> getLaptopSimple(string mingia, string maxgia, string hangsanxuat, string hedieuhanh)
        {
            string mingiaString = (mingia != "") ? "gia>=" + mingia : "";
            string maxgiaString = (maxgia != "") ? "gia<" + maxgia : "";
            string hanglaptopIdString = (hangsanxuat != "") ? " HangLaptopId=" + HangLapTop.getHangSanXuatId(hangsanxuat) : "";
            string hedieuhanhIdString = (hedieuhanh != "") ? " HeDieuHanhId=" + HeDieuHanh.getHeDieuHanhId(hedieuhanh) : "";
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
            String query = String.Join(" and ", list.ToArray());
            using(TuVanLaptopEntities db=new TuVanLaptopEntities())
            {
                var laptops = db.Laptops.SqlQuery("SELECT * FROM dbo.Laptop where " + query);
                return laptops.ToList();
            }
         
        }



    }
    public class Laptop_Metadata
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }
        [Display(Name = "Ảnh bìa")]
        public string AnhBia { get; set; }
        [Display(Name = "Ngày cập nhật")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> NgayCapNhat { get; set; }
        [Display(Name = "Hãng sản xuất")]
        public Nullable<int> HangLaptopId { get; set; }
        [Display(Name = "Hệ điều hành")]
        public Nullable<int> HeDieuHanhId { get; set; }
        [Display(Name = "Màu sắc")]
        public string mausac { get; set; }
        [Display(Name = "Giá")]
        public Nullable<decimal> gia { get; set; }
        [Display(Name = "Màn hình")]
        public Nullable<double> manhinh { get; set; }
        [Display(Name = "Độ phân giải")]
        public Nullable<bool> dophangiai { get; set; }
        [Display(Name = "CPU")]
        public string cpu { get; set; }
        [Display(Name = "RAM")]
        public Nullable<int> ram { get; set; }
        [Display(Name = "Ổ cứng")]
        public Nullable<int> ocung { get; set; }
        [Display(Name = "Khối lượng")]
        public Nullable<double> khoiluong { get; set; }
        [Display(Name = "Pin")]
        public Nullable<double> pin { get; set; }
        [Display(Name = "Card rời")]
        public Nullable<bool> cardroi { get; set; }
    }
}