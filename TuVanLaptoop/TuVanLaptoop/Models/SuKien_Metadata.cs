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

    }
    public class SuKien_Metadata
    {
        [Display(Name="ID")]
        public int Id { get; set; }
        [Display(Name="Tên sự kiện")]
        public string Name { get; set; }
    }
}