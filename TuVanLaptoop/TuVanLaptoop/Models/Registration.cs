using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TuVanLaptoop.Models
{
    public class Registration
    {
        [Required(ErrorMessage ="Bắt buộc có tên")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Bắt buộc có email")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
        ErrorMessage = "Please enter correct email address")]
        public string Email { get; set; }
        //[Required(ErrorMessage = "Bắt buộc có số điện thoại")]
        //public string Mobile { get; set; }
        [Required(ErrorMessage = "Bắt buộc có tin nhắn")]
        public string Message { get; set; }
    }
}