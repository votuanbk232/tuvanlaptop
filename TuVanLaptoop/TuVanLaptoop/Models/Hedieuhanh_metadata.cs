using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuVanLaptoop.Models
{
    public partial class HeDieuHanh
    {
        //lấy hệ điều hành id dựa vào tên hệ điều hành
        public static String getHeDieuHanhId(string name)
        {
            using(TuVanLaptopEntities db=new TuVanLaptopEntities())
            {
                HeDieuHanh rs = db.HeDieuHanhs.SingleOrDefault(n => n.Name == name);
                if (rs == null) { return null; }
                return (rs.Id).ToString();
            }
          
        }
    }
    public class Hedieuhanh_metadata
    {
    }
}