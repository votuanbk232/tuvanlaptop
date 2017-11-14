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
       


        //mảng sự kiện vế trái(Id) 
        public string[] SuKienSelectedIDArray { get; set; }
        //Danh sách sự kiện ( lấy các sự kiện thuộc giao diện)
        public List<SuKien> SuKienCollection { get; set; }
        //chuyển string([int]) sang string([string])
        //public string sukienvetrais = ConvertIntArrayToStringArray();
        public  string sukienvetrais;
        public  string sukienvephai;
        //public Luat(string vetrai,string vephai)
        //{
        //    this.SuKienVT = vetrai;
        //    sukienvetrais = Luat.ConvertIntArrayToStringArray(vetrai);
        //}



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
        //lấy độ tin cậy(max trong các bản ghi) dựa vào vế trái([]int) của luật(tương tự lấy sự kiện vế phải)
        public static int getDoTinCay(string vetrai)
        {
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                //Luat sk = db.Luats.SingleOrDefault(n => n.SuKienVT == vetrai);
                var query = "SELECT TOP 1 * FROM dbo.Luat WHERE SuKienVT = '" + vetrai + "'" + " ORDER BY  DoTinCay DESC ";
                Luat sk = db.Luats.SqlQuery(query).FirstOrDefault();

                if (sk == null) { return 0; }
                return (int)sk.DoTinCay;
            }
        }
        //Lấy Id của sự kiện vế phải dựa vào dãy sự kiện vế trái([]int)
        // cần xét thêm độ tin cậy( lấy luật có độ tin cậy cao nhất)
        public static String getVePhaiByVeTrai(string vetrai)
        {
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                //Luat sk = db.Luats.SingleOrDefault(n => n.SuKienVT == vetrai);
                var query = "SELECT TOP 1 * FROM dbo.Luat WHERE SuKienVT = '" + vetrai + "'" + " ORDER BY  DoTinCay DESC ";
                Luat sk = db.Luats.SqlQuery(query).FirstOrDefault();
                if (sk == null) { return null; }
                return sk.SukienVP;
            }
        }

        //chuyển luât vế trái string[int] sang string[string]
        public  static string ConvertIntArrayToStringArray(string vetrai)
        {
            //chuyển vế trái của luật sang list string[string]
            List<string> result = vetrai.Split(new char[] { ',' }).ToList();
            List<String> vetraisau=new List<string>();
            for(int i = 0; i < result.Count; i++)
            {
                //chuyển list string(id) tới list string(name)
                vetraisau.Add(SuKien.getSuKienById(Convert.ToInt16(result[i])));
            }
            return String.Join(", ", vetraisau.ToArray()); ;
        }
        

    }
    public class Luat_Metadata
    {
        [Display(Name ="ID")]
        public int Id { get; set; }
        [Display(Name ="Sự kiện vế trái")]
        public  string SuKienVT { get; set; }
        [Display(Name ="Sự kiện vế phải")]
        public string SukienVP { get; set; }
        [Display(Name ="Giải thích")]
        public string GiaiThich { get; set; }
        [Display(Name ="Độ tin cậy")]
        public Nullable<int> DoTinCay { get; set; }



    }




}