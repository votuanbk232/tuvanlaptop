using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TuVanLaptoop.Models
{
    [MetadataTypeAttribute(typeof(SuKien_Metadata))]
    public partial class SuKien
    {
        //lưu sự kiện vào database
        public static bool SaveSuKien(SuKien sukien)
        {
            using (TuVanLaptopEntities db=new TuVanLaptopEntities())
            {
                //tìm xem có tồn tại sk này chưa
                SuKien sk = db.SuKiens.SingleOrDefault(n=>n.Name== sukien.Name);
                //nếu tồn tại trả về null
                if (sk != null)
                    {
                        return false;
                    }
                db.SuKiens.Add(sukien);
                db.SaveChanges();
                return true;
            }
          
        }
        //Lấy giá trị sự kiện(string) dựa vào ID(int,bảng sự kiện)
        public static String getSuKienById(int id)
        {
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                SuKien sk = db.SuKiens.Single(n => n.Id == id);
                return (sk.Name).ToString();
            }
        }
        //lấy Id của sự kiện dựa vào Name(string)(Bảng sự kiện)
        //tên sự kiện ko đc trùng nhau và là duy nhất
        public static String getSuKienId(string name)
        {
            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {
                SuKien sk = db.SuKiens.SingleOrDefault(n => n.Name == name);
                if (sk == null) { return null; }
                return (sk.Id).ToString();
            }
        }
    }
    public class SuKien_Metadata
    {
        [Display(Name="ID")]
        public int Id { get; set; }

        [Display(Name="Tên sự kiện")]
        [SuKienNameValidation]
        public string Name { get; set; }

        
    }
}