using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicari.Models.Siniflar;

namespace MvcOnlineTicari.Controllers
{
    [Authorize(Roles="A")]
    public class SatisController : Controller
    {
        // GET: Satis

        Context c = new Context();

        //Satışları Listeleme
        public ActionResult Index()
        {
            var satislar = c.SatisHarekets.ToList();
            return View(satislar);
        }


        //Yeni Satış Yapmak
        [HttpGet]
        public ActionResult YeniSatis()
        {
            //Ürünleri getirmek için
            List<SelectListItem> deger1 = (from i in c.Uruns.OrderBy(x=>x.UrunAd).ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.UrunAd +" - MARKASI: "+ i.Marka,
                                               Value = i.UrunID.ToString(),
                                           }).ToList();

            //Markaları getirmek için
            List<SelectListItem> deger4 = (from i in c.Uruns.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Marka,
                                               Value = i.UrunID.ToString(),
                                           }).ToList();


            //Carileri getirmek için
            List<SelectListItem> deger2 = (from i in c.Carilers.OrderBy(x=>x.CariAd).ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.CariAd +" "+ i.CariSoyad,
                                               Value = i.CariID.ToString(),
                                           }).ToList();
            

            //Personel getirmek için
            List<SelectListItem> deger3 = (from i in c.Personels.Where(x=>x.Departmanid==1).OrderBy(x => x.PersonelAd).ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.PersonelAd + " " + i.PersonelSoyad,
                                               Value = i.PersonelID.ToString(),
                                           }).ToList();
           

            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            ViewBag.dgr4 = deger4;

            return View();
        }

        [HttpPost]
        public ActionResult YeniSatis(SatisHareket s)
        {
            s.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(s);
            c.SaveChanges();
            return RedirectToAction("Index");
        }


        //Satış Güncellemek
        public ActionResult SatisGetir(int id)
        {
            //Ürünleri getirmek için
            List<SelectListItem> deger1 = (from i in c.Uruns.OrderBy(x => x.UrunAd).ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.UrunAd + " - MARKASI: " + i.Marka,
                                               Value = i.UrunID.ToString(),
                                           }).ToList();

            //Markaları getirmek için
            List<SelectListItem> deger4 = (from i in c.Uruns.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Marka,
                                               Value = i.UrunID.ToString(),
                                           }).ToList();


            //Carileri getirmek için
            List<SelectListItem> deger2 = (from i in c.Carilers.OrderBy(x => x.CariAd).ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.CariAd + " " + i.CariSoyad,
                                               Value = i.CariID.ToString(),
                                           }).ToList();


            //Personel getirmek için
            List<SelectListItem> deger3 = (from i in c.Personels.Where(x => x.Departmanid == 1).OrderBy(x => x.PersonelAd).ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.PersonelAd + " " + i.PersonelSoyad,
                                               Value = i.PersonelID.ToString(),
                                           }).ToList();


            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            ViewBag.dgr4 = deger4;

            var deger = c.SatisHarekets.Find(id);
            return View("SatisGetir", deger);
        }

        public ActionResult SatisGuncelle(SatisHareket s)
        {
            var deger = c.SatisHarekets.Find(s.SatisID);
            
            deger.Cariid = s.Cariid;
            deger.Adet = s.Adet;
            deger.Fiyat = s.Fiyat;
            deger.ToplamTutar = s.ToplamTutar;
            deger.Personelid = s.Personelid;
            deger.Urunid = s.Urunid;
            deger.Tarih = s.Tarih;
            c.SaveChanges();
            return RedirectToAction("Index");
        }


        //Satış Detay Görüntüleme
        public ActionResult SatisDetay(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.SatisID == id).ToList();
            return View(degerler);
        }


        //pdf-excell
        public ActionResult SatisDetayPDF(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.SatisID == id).ToList();
            return View(degerler);
        }



    }
}