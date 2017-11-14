using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuVanLaptoop.Models
{
    public partial class HangLapTop
    {
        //lấy hãng sản xuất (id) dựa vào tên (name)
        public static String getHangSanXuatId(string name)
        {
            using(TuVanLaptopEntities db=new TuVanLaptopEntities())
            {
                HangLapTop rs = db.HangLapTops.SingleOrDefault(n => n.Name == name);
                if (rs == null) { return null; }
                return (rs.Id).ToString();
            }
           
        }
    }
    public class Hangsanxuat_metatdata
    {
        
    }
}