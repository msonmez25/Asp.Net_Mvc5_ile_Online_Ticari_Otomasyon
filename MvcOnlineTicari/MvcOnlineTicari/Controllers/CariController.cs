using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicari.Models.Siniflar;


namespace MvcOnlineTicari.Controllers
{
    public class CariController : Controller
    {
        // GET: Cari

        Context c = new Context();

        //Cari Listeleme
        public ActionResult Index()
        {
            var cariler = c.Carilers.ToList();
            return View(cariler);
        }


        //Yeni Cari Ekleme
        [HttpGet]
        public ActionResult CariEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CariEkle(Cariler k)
        {
            k.Durum = true;
            c.Carilers.Add(k);
            c.SaveChanges();
            return RedirectToAction("Index");
        }



        // Cari Silme
        public ActionResult CariSil(int id)
        {
            var cr = c.Carilers.Find(id);
            cr.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }


        // Cari Güncelleme
        public ActionResult CariGetir(int id)
        {
            var cr = c.Carilers.Find(id);
            return View("CariGetir", cr);
        }

        public ActionResult CariGuncelle(Cariler k)
        {
            if (!ModelState.IsValid) 
            {
                return View("CariGetir");
            }
            var cr = c.Carilers.Find(k.CariID);
            cr.CariAd = k.CariAd;
            cr.CariSoyad = k.CariSoyad;
            cr.CariGorsel = k.CariGorsel;
            cr.CariSehir = k.CariSehir;
            cr.CariMail = k.CariMail;
            cr.CariTelefon = k.CariTelefon;
            cr.CariMeslek = k.CariMeslek;
            cr.Durum = true;
            c.SaveChanges();
            return RedirectToAction("Index");
        }


        //Cari Satış Listeleme
        public ActionResult CariDetay(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            var cariadsoyad = c.Carilers.Where(x => x.CariID == id).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();

            ViewBag.cr = cariadsoyad;


            return View(degerler);
        }
    }
}