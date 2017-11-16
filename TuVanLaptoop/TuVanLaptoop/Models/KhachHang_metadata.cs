using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TuVanLaptoop.Models
{
    [MetadataTypeAttribute(typeof(KhachHang_metadata))]
    public partial class KhachHang
    {
        public string XacNhanMatKhau { get; set; }

    }
    public class KhachHang_metadata
    {
        public int MaKH { get; set; }

        [Required(ErrorMessage ="Tên bắt buộc")]
        public string HoTen { get; set; }

        public string DienThoai { get; set; }

        [Required(ErrorMessage = "Tên tài khoản bắt buộc")]
        public string TaiKhoan { get; set; }

        [Display(Name ="Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu bắt buộc bắt buộc")]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage ="Mật khẩu có độ dài không nhỏ hơn 6")]
        public string MatKhau { get; set; }

        [Display(Name = "Nhập lại mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("MatKhau",ErrorMessage ="Mật khẩu không trùng nhau")]
        public string XacNhanMatKhau { get; set; }

        public string DiaChi { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> NgaySinh { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", 
            ErrorMessage = "E-mail is not valid")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }
}