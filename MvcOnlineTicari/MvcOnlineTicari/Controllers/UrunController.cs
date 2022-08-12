using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicari.Models.Siniflar;

namespace MvcOnlineTicari.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun

        Context c = new Context();

        //Ürünleri Listeleme
        public ActionResult Index()
        {
            var urunler = c.Uruns.ToList();
            return View(urunler);
        }


        //Yeni Ürün Ekleme
        [HttpGet]
        public ActionResult UrunEkle()
        {
            //Kategorileri getirmek için
            List<SelectListItem> deger1 = (from i in c.Kategoris.Where(z => z.Durum == true).OrderByDescending(x => x.KategoriAd).ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.KategoriAd,
                                               Value = i.KategoriID.ToString(),
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }

        [HttpPost]
        public ActionResult UrunEkle(Urun u)
        {
            c.Uruns.Add(u);
            u.Durum = true;
            c.SaveChanges();
            return RedirectToAction("Index");
        }


        // Ürün Silme
        public ActionResult UrunSil(int id)
        {
            var urunler = c.Uruns.Find(id);
            urunler.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }



        // Ürün Güncelleme
        public ActionResult UrunGetir(int id)
        {
            var urunler = c.Uruns.Find(id);

            //Kategorileri getirmek için
            List<SelectListItem> deger1 = (from i in c.Kategoris.Where(z => z.Durum == true).OrderByDescending(x => x.KategoriAd).ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.KategoriAd,
                                               Value = i.KategoriID.ToString(),
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            return View("UrunGetir", urunler);
        }

        public ActionResult UrunGuncelle(Urun u)
        {
            
            var urunler = c.Uruns.Find(u.UrunID);

            urunler.UrunAd = u.UrunAd;
            urunler.Marka = u.Marka;
            urunler.Stok = u.Stok;
            urunler.AlisFiyat = u.AlisFiyat;
            urunler.SatisFiyat = u.SatisFiyat;
            urunler.Durum = true;
            urunler.UrunGorsel = u.UrunGorsel;
            urunler.Kategoriid = u.Kategoriid;

            c.SaveChanges();
            return RedirectToAction("Index");
        }


        //pdf-excell
        public ActionResult UrunListesi()
        {
            var degerler = c.Uruns.ToList();
            return View(degerler);
        }


        //Urun Sayfası Üzerinden Satış Yapmak için
        [HttpGet]
        public ActionResult SatisYap(int id)
        {
            //Personel getirmek için
            List<SelectListItem> deger3 = (from i in c.Personels.Where(x => x.Departmanid == 1).OrderBy(x => x.PersonelAd).ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.PersonelAd + " " + i.PersonelSoyad,
                                               Value = i.PersonelID.ToString(),
                                           }).ToList();

            //Carileri getirmek için
            List<SelectListItem> deger2 = (from i in c.Carilers.OrderBy(x => x.CariAd).ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.CariAd + " " + i.CariSoyad,
                                               Value = i.CariID.ToString(),
                                           }).ToList();

            //ürün Id taşımak için
            var deger1 = c.Uruns.Find(id);

            //ürün fiyatı taşımak için
            var deger4 = c.Uruns.Find(id);

            

            ViewBag.dgr3 = deger3;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr1 = deger1.UrunID;
            ViewBag.dgr4 = deger4.SatisFiyat;
            return View();
        }

        [HttpPost]
        public ActionResult SatisYap(SatisHareket p)
        {
            p.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index","Satis");
        }
    }
}