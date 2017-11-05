using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Web;
using System.Web.Mvc;

namespace TuVanLaptoop.Controllers
{
    public class SpeechController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection model)
        {
            string speech=model["text"].ToString();
            TempData["message"] = "Nội dung yêu cầu" + speech;
            return RedirectToAction("Index","Home");
        }

        //show kết quả
        public ActionResult ShowResult(string speech_required)
        {
            return PartialView("SpeechResultView");
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