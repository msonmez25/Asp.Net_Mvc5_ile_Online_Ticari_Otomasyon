using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicari.Models.Siniflar;

namespace MvcOnlineTicari.Controllers
{
    
    public class DuyurularController : Controller
    {
        // GET: Duyurular

        Context c = new Context();

        //Duyuru Listeleme
        public ActionResult Index()
        {
            var tumduyurular = c.Duyurus.ToList();
            return View(tumduyurular);
        }


        //Yeni Duyuru Ekleme
        [HttpGet]
        public ActionResult DuyuruEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DuyuruEkle(Duyuru d)
        {
            c.Duyurus.Add(d);
            c.SaveChanges();
            return RedirectToAction("Index");
        }


        // Duyuru Güncelleme
        public ActionResult DuyuruGetir(int id)
        {
            var dyr = c.Duyurus.Find(id);
            return View("DuyuruGetir", dyr);
        }

        public ActionResult DuyuruGuncelle(Duyuru d)
        {
            var dyr = c.Duyurus.Find(d.DuyuruID);
            dyr.Kategori = d.Kategori;
            dyr.icerik = d.icerik;
            dyr.Tarih = d.Tarih;            
            c.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}