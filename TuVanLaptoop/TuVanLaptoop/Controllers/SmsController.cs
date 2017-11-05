using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//sử dụng twilio
using System.Configuration;
using Twilio;
using Twilio.AspNet.Mvc;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using TuVanLaptoop.Models;
using System.Web.Helpers;
using TuVanLaptoop.Filter;

namespace TuVanLaptoop.Controllers
{
    public class SmsController : TwilioController
    {
        //cần đăng nhập email để gửi yêu cầu tới email admin
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(Registration reg)
        {
            if (ModelState.IsValid)
            {
                WebMail.Send("votuanbk232@gmail.com", reg.Name + "-gửi yêu cầu tới!", reg.Message
                    , null, null, null, true, null, null, null, null, null, reg.Email

                    );
                Session["Message"] = true;
                return RedirectToAction("Thankyou");
            }
            return View();
        }
        [MessageFilter]
        public ActionResult Thankyou()
        {
            return View();
        }
        public ActionResult MessageSend(string message)
        {
            if (message == "")
            {
                return RedirectToAction("Index", "Home");

            }
            WebMail.Send("votuanbk232@gmail.com", "Yêu cầu từ khách hàng", message
                    , null, null, null, true, null, null, null, null, null, "votuanbk232@gmail.com");
            TempData["message"] = "Yêu cầu đã được gửi.Cảm ơn sự đóng góp của bạn!";
            return RedirectToAction("Index", "Home");
        }
        public ActionResult SendSms()
        {
            var accountSid = ConfigurationManager.AppSettings["AC9a755c3d403cadc2adf2d60e0578d547"];
            var authToken = ConfigurationManager.AppSettings["81da4c98182745f881de6bc893853b49"];
            TwilioClient.Init(accountSid, authToken);
            var to = new PhoneNumber(ConfigurationManager.AppSettings["+84 01626356708"]);
            var from = new PhoneNumber("+1 424-233-4439 ");
            var message = MessageResource.Create(
                to: to,
                from: from,
                body: "Heelo"
                );
            return Content(message.Sid);
            //(424) 233 - 4439
            //    + 14242334439
        }
    }
}

