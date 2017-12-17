using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Web;
using System.Web.Mvc;
using TuVanLaptoop.Models;

namespace TuVanLaptoop.Controllers
{
    public class SpeechController : Controller
    {
        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + String.Join("", input.Skip(1));
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Result(FormCollection model)
        {

            string speech = model["text"].ToString();
            //TempData["message"] = "Nội dung yêu cầu : " + speech;

            using (TuVanLaptopEntities db = new TuVanLaptopEntities())
            {

                List<String> SuKien = (from m in db.SuKiens
                                       select m.Name.ToLower()).ToList();
                //mảng các sự kiện lấy đc
                List<String> Yeucau = new List<string>(); 
                //string[] word_key = speech.Split(' ');
                //foreach (var x in word_key)
                //{
                //    if (SuKien.Contains(x.ToUpper()))
                //    {
                //        Yeucau.Add(x);
                //    }
                //}
                foreach(var x in SuKien)
                {
                    if (speech.ToLower().Contains(x))
                    {
                        Yeucau.Add(x);
                    }

                }
                List<string> yeucau_int = new List<string>();
                //List<String> yc = Yeucau; //bắt được nam, lập trình viên
                foreach (var x in Yeucau)
                {
                    yeucau_int.Add(Models.SuKien.getSuKienId(FirstCharToUpper(x))); //lấy đc id sk
                }
                // List<String> yc = yeucau_int; //bắt đc sự kiện vế trái, 1 và 7
                string mangYeuCau_int = String.Join(",", yeucau_int.ToArray());

                Luat luat = Luat.getLuatByVeTrai(mangYeuCau_int);

                if (luat != null)
                {
                    var laptopList = Laptop.getLaptopByVePhai(Luat.getVePhaiByVeTrai(mangYeuCau_int));


                    // hiện section đánh giá độ tin cậy
                    TempData["CheckLuatTonTai"] = "Luật tồn tại";
                    //lấy id của luật đó:
                    int idold = luat.Id;
                    TempData["LuatId"] = idold;
                    //mô tả luật
                    TempData["MoTaLuat"] = Luat.GetMoTaLuat(idold);
                    //lấy độ tin cậy của luật đó
                    TempData["DoTinCay"] = Luat.GetDoTinCay(idold);

                    if (laptopList == null)
                    {
                        TempData["message"] = "Luật tồn tại!Chưa có sản phẩm gợi ý!";
                        return RedirectToAction("NotFoundSanPham", "Home");
                    }
                    else
                    {
                        TempData["message"] = "Có " + laptopList.Count() + " sản phẩm phù hợp!";
                        //return RedirectToAction("NotFoundSanPham", "Home",laptopList);
                        //return RedirectToAction("Index","TuVan",laptopList);
                        return PartialView("SpeechResult", laptopList);

                    }
                }
                TempData["message"] = "Luật không tồn tại";
                return PartialView("NotFound");
               

                




               

            }
        }
        //[HttpPost]
        //public ActionResult Result(FormCollection model)
        //{

        //    string speech = model["text"].ToString();
        //    TempData["message"] = "Nội dung yêu cầu : " + speech;


        //    using (TuVanLaptopEntities db = new TuVanLaptopEntities())
        //    {
        //        List<Laptop> laptops = new List<Laptop>();
        //        if (speech.Contains("Lenovo"))
        //        {
        //            laptops = db.Laptops.Where(x=>x.HangLapTop.Name=="Lenovo").ToList();

        //        }
        //        if (speech.Contains("student"))
        //        {
        //            laptops= (from laptop in db.Laptops
        //                      where laptop.gia < 15000000
        //                      orderby laptop.NgayCapNhat descending
        //                     select laptop).ToList();

        //        }
        //        TempData["message"] = ".Có " + laptops.Count() + " sản phẩm phù hợp!";
        //        return PartialView("SpeechResult", laptops);

        //    }
        //}
        //[HttpPost]
        //public string Result(FormCollection model)
        //{

        //    string speech = model["text"].ToString();
        //    TempData["message"] = "Nội dung yêu cầu" + speech;

        //    return "Xinchao";
        //}

        public ActionResult GetView(string search)
        {

            return PartialView("ResultPartialView");
        }
        //show kết quả
        public ActionResult ShowResult(string id)
        {
            using(TuVanLaptopEntities db=new TuVanLaptopEntities())
            {
                if (id.Contains("a")){
                    List<Laptop> laptops = db.Laptops.Where(x => x.HeDieuHanh.Name == "Dell").ToList();
                    return PartialView(laptops);

                }
                return null;

            }
        }
        //SpeechSynthesizer ss = new SpeechSynthesizer();
        //PromptBuilder pb = new PromptBuilder();
        //SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        //Choices clist;
        //[HttpPost]
        //public ActionResult Index()
        //{
        //    return View();
        //}
        //public ActionResult BatDau(string start)
        //{
        //    string threadMessage = null;
        //    bool returnValue = true;

        //    var t = new System.Threading.Thread(() =>
        //    {
        //        try
        //        {
        //            SpeechEngine.SetOutputToWaveFile(wavFilePath);
        //            SpeechEngine.Speak(text);
        //            SpeechEngine.SetOutputToNull();
        //        }
        //        catch (Exception exception)
        //        {
        //            threadMessage = "Error doing text to speech to file: " + exception.Message;
        //            returnValue = false;
        //        }
        //    });
        //    t.Start();
        //    t.Join();

        //    if (!returnValue)
        //    {
        //        message = threadMessage;
        //        return returnValue;
        //    }
        //    return View();
        //}
    }
}