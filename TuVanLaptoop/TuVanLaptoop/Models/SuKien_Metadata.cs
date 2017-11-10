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