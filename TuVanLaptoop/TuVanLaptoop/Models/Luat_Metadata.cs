using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TuVanLaptoop.Models
{
    [MetadataTypeAttribute(typeof(Luat_Metadata))]
    public partial class Luat
    {
        public string[] SuKienSelectedIDArray { get; set; }
        public List<SuKien> SuKienCollection { get; set; }

        //kiểm tra luật đã tồn tại hay chưa dựa vào vế trái và vế phải
        public static bool CheckLuatTonTai(string vetrai,string vephai)
        {
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                var query = "SELECT  * FROM dbo.Luat WHERE SuKienVT = '" + vetrai +"'"+ " AND SukienVP ='" + vephai+"'";
                Luat sk = db.Luats.SqlQuery(query).SingleOrDefault();
                if (sk == null)
                {
                    return false;
                }
                return true;
            }
           
        }

        //lấy id của luật dựa vào vế trái và vế phải
        public static int? GetId(string vetrai, string vephai)
        {
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                var query = "SELECT  * FROM dbo.Luat WHERE SuKienVT = '" + vetrai + "'" + " AND SukienVP ='" + vephai + "'";
                Luat sk = db.Luats.SqlQuery(query).SingleOrDefault();
                if (sk == null)
                {
                    return null;
                }
                return sk.Id;
            }

        }
        //lấy độ tin cậy dựa vào id của luật
        public static int? GetDoTinCay(int id)
        {
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                var query = "SELECT  * FROM dbo.Luat WHERE Id="+id;
                Luat sk = db.Luats.SqlQuery(query).SingleOrDefault();
                if (sk == null)
                {
                    return null;
                }
                return sk.DoTinCay;
            }

        }
        //lấy mô tả dựa vào id của luật
        public static string GetMoTaLuat(int id)
        {
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                var query = "SELECT  * FROM dbo.Luat WHERE Id=" + id;
                Luat sk = db.Luats.SqlQuery(query).SingleOrDefault();
                if (sk == null)
                {
                    return null;
                }
                return sk.GiaiThich;
            }

        }

       
    }
    public class Luat_Metadata
    {
        [Display(Name ="ID")]
        public int Id { get; set; }
        [Display(Name ="Sự kiện vế trái")]
        public string SuKienVT { get; set; }
        [Display(Name ="Sự kiện vế phải")]
        public string SukienVP { get; set; }
        [Display(Name ="Giải thích")]
        public string GiaiThich { get; set; }
        [Display(Name ="Độ tin cậy")]
        public Nullable<int> DoTinCay { get; set; }

        
    }

   

   
}