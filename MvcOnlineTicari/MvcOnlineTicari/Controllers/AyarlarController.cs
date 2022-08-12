using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicari.Models.Siniflar;

namespace MvcOnlineTicari.Controllers
{
    public class AyarlarController : Controller
    {
        // GET: Ayarlar

        Context c = new Context();

        //Admin Listelem
        public ActionResult Index()
        {
            var kullanici = c.Admins.ToList();
            return View(kullanici);
        }

        //Yeni Admin Eklemek
        [HttpGet]
        public ActionResult YeniAdmin()
        {
            return View();
        }


        [HttpPost]
        public ActionResult YeniAdmin(Admin p)
        {
            c.Admins.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }


        //Admin Silme
        public ActionResult AdminSil(int id)
        {
            var kullanici = c.Admins.Find(id);
            kullanici.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }


        //Admin Güncellemek

        public ActionResult AdminGetir(int id)
        {
            var admin = c.Admins.Find(id);
            return View("AdminGetir", admin);
        }



        public ActionResult AdminGuncelle(Admin p)
        {
            var admin = c.Admins.Find(p.AdminID);

            admin.KullaniciAd = p.KullaniciAd;
            admin.Sifre = p.Sifre;
            admin.Yetki = p.Yetki;
            admin.Durum = true;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}