using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicari.Models.Siniflar;

namespace MvcOnlineTicari.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori

        Context c = new Context();

        //Kategori Listeleme
        public ActionResult Index()
        {
            var kategoriler = c.Kategoris.OrderByDescending(x=>x.Durum).ToList();
            return View(kategoriler);
        }


        //Yeni Kategori Ekleme
        [HttpGet]
        public ActionResult KategoriEkle()
        {                       
            return View();
        }

        [HttpPost]
        public ActionResult KategoriEkle(Kategori k)
        {
            c.Kategoris.Add(k);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        // Kategori Silme
        public ActionResult KategoriSil(int id)
        {
            var ktgr = c.Kategoris.Find(id);
            ktgr.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }


        // Kategori Güncelleme
        public ActionResult KategoriGetir(int id)
        {
            var ktgr = c.Kategoris.Find(id);
            return View("KategoriGetir", ktgr);
        }

        public ActionResult KategoriGuncelle(Kategori k)
        {
            var ktgr = c.Kategoris.Find(k.KategoriID);
            ktgr.KategoriAd = k.KategoriAd;
            ktgr.Durum = true;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        //DropDown şeklinde kategori-ürün seçmek 
        public ActionResult Deneme()
        {
            Class2 cs = new Class2();
            cs.Kategoriler = new SelectList(c.Kategoris, "KategoriID", "KategoriAd");
            cs.Urunler = new SelectList(c.Uruns, "UrunID", "UrunAd");
            return View(cs);
        }

        public JsonResult UrunGetir(int p)
        {
            var urunlistesi = (from x in c.Uruns
                               join y in c.Kategoris
                               on x.Kategori.KategoriID equals y.KategoriID
                               where x.Kategori.KategoriID == p
                               select new
                               {
                                   Text = x.UrunAd + "- (" + x.Marka + ")",
                                   Value = x.UrunID.ToString()
                               }).ToList();
            return Json(urunlistesi, JsonRequestBehavior.AllowGet);
        }
    }
}