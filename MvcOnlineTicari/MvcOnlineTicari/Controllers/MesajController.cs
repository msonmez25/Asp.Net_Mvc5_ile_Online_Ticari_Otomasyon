using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicari.Models.Siniflar;

namespace MvcOnlineTicari.Controllers
{
    public class MesajController : Controller
    {
        // GET: Mesaj

        Context c = new Context();

        //gelen mesajları listeleme
        public ActionResult Index()
        {
            var carimail = (string)Session["CariMail"].ToString();
            var mesajlar = c.Mesajlars.Where(x => x.Alici == carimail.ToString()).OrderByDescending(x=>x.MesajID).ToList();
            
            return View(mesajlar);
        }

        //giden mesajları listeleme
        public ActionResult Giden()
        {
            var carimail = (string)Session["CariMail"].ToString();
            var mesajlar = c.Mesajlars.Where(x => x.Gonderen == carimail.ToString()).ToList();
            return View(mesajlar);
        }


        // partial ile mesajlar sol satırları oluşturduk
        public PartialViewResult Partial1()
        {
            var carimail = (string)Session["CariMail"].ToString();

            var gelensayisi = c.Mesajlars.Where(x => x.Alici == carimail).Count();
            ViewBag.d1 = gelensayisi;
            var gidensayisi = c.Mesajlars.Where(x => x.Gonderen == carimail).Count();
            ViewBag.d2 = gidensayisi;

            return PartialView();
        }

        // mesajların detayını görmek 
        public ActionResult MesajDetay(int id)
        {
            var degerler = c.Mesajlars.Where(x => x.MesajID == id).ToList();

            var uyemail = (string)Session["CariMail"].ToString();
            var gelensayisi = c.Mesajlars.Where(x => x.Alici == uyemail).ToString();
            ViewBag.d1 = gelensayisi;
            var gidensayisi = c.Mesajlars.Where(x => x.Gonderen == uyemail).ToString();
            ViewBag.d2 = gidensayisi;
            return View(degerler);
        }

        // Yeni mesaj atmak için
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar t)
        {
            var carimail = (string)Session["CariMail"].ToString();
            t.Gonderen = carimail.ToString();
            t.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.Mesajlars.Add(t);
            c.SaveChanges();
            return RedirectToAction("Giden", "Mesaj");
        }

    }
}