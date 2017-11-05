using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuVanLaptoop.Models;

namespace TuVanLaptoop.ViewModels
{
    public class SuKienViewModel
    {
        public IEnumerable<SelectListItem> GioiTinhs { get; set; }
        public IEnumerable<SelectListItem> HangLaptops { get; set; }
        public IEnumerable<SelectListItem> YeuCauGiaTiens { get; set; }
        public IEnumerable<SelectListItem> HeDieuHanhs { get; set; }
        public IEnumerable<SelectListItem> MucDichs { get; set; }
        public IEnumerable<SelectListItem> NgheNghieps { get; set; }
    }
}