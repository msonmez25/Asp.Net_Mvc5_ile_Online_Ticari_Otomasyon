using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicari.Models.Siniflar;
using System.Web.Security;


namespace MvcOnlineTicari.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login

        Context c = new Context();

        public ActionResult Index()
        {
            return View();
        }

        //Cari Kayıt
        [HttpGet]
        public PartialViewResult Partial1()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult Partial1(Cariler p)
        {
            c.Carilers.Add(p);
            p.Durum = true;
            c.SaveChanges();

            return PartialView();
        }


        //Cari Giriş
       [HttpGet]
        public ActionResult CariLogin2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CariLogin2(Cariler p)
        {
            var bilgiler = c.Carilers.FirstOrDefault(x => x.CariMail == p.CariMail &&  x.CariSifre == p.CariSifre);
            if(bilgiler!= null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.CariMail, false);
                Session["CariMail"] = bilgiler.CariMail.ToString();
                Session["CariAd"] = bilgiler.CariAd.ToString();
                Session["CariSoyad"] = bilgiler.CariSoyad.ToString();
                Session["CariGorsel"] = bilgiler.CariGorsel.ToString();
                return RedirectToAction("Index", "CariPanel");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }            
        }



        //Admin Girişi
        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(Admin p)
        {
            var bilgiler = c.Admins.FirstOrDefault(x => x.KullaniciAd == p.KullaniciAd && x.Sifre == p.Sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.KullaniciAd, false);
                Session["KullaniciAd"] = bilgiler.KullaniciAd.ToString();
                return RedirectToAction("Index", "istatistik");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        //Admin Çıkış
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap", "Login");
        }
    }
}