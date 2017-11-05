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