using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicari.Models.Siniflar;

namespace MvcOnlineTicari.Controllers
{
    [Authorize(Roles = "A")]
    public class FaturaController : Controller
    {
        // GET: Fatura

        Context c = new Context();

        //Fatura Listeleme
        public ActionResult Index()
        {
            var faturalar = c.Faturalars.ToList();
            return View(faturalar);
        }


        //Yeni Fatura Ekleme
        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FaturaEkle(Faturalar f)
        {            
            c.Faturalars.Add(f);
            c.SaveChanges();
            return RedirectToAction("Index");
        }



        // Fatura Güncelleme
        public ActionResult FaturaGetir(int id)
        {
            var ftr = c.Faturalars.Find(id);
            return View("FaturaGetir", ftr);
        }

        public ActionResult FaturaGuncelle(Faturalar k)
        {
            var ftr = c.Faturalars.Find(k.FaturaID);
            ftr.FaturaSeriNo = k.FaturaSeriNo;
            ftr.FaturaSiraNo = k.FaturaSiraNo;
            ftr.Tarih = k.Tarih;
            ftr.Saat = k.Saat;
            ftr.VergiDairesi = k.VergiDairesi;
            ftr.TeslimEden = k.TeslimEden;
            ftr.TeslimAlan = k.TeslimAlan;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        //Fatura Detay Görüntüleme
        public ActionResult FaturaDetay(int id)
        {
            var degerler = c.faturaKalems.Where(x => x.Faturaid == id).ToList();

            var ftrID = c.faturaKalems.Where(x => x.Faturaid == id).Select(y => y.Faturaid).FirstOrDefault();
            ViewBag.ftr = ftrID;

            return View(degerler);
        }


        //Yeni Kalem Ekleme
        [HttpGet]
        public ActionResult KalemEkle()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult KalemEkle(FaturaKalem p)
        {
            c.faturaKalems.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }


        //Dinamik şekilde faturalar oluşturmak
        public ActionResult Dinamik()
        {
            Class3 cs = new Class3();
            cs.deger1 = c.Faturalars.ToList();
            cs.deger2 = c.faturaKalems.ToList();
            return View(cs);
        }

        public ActionResult FaturaKaydet(string FaturaSeriNo, string FaturaSiraNo,DateTime Tarih, string VergiDairesi, string Saat, string TeslimEden, string TeslimAlan, string Toplam, FaturaKalem[] kalemler)
        {
            Faturalar f = new Faturalar();
            f.FaturaSeriNo = FaturaSeriNo;
            f.FaturaSiraNo = FaturaSiraNo;
            f.Tarih = Tarih;
            f.VergiDairesi = VergiDairesi;
            f.Saat = Saat;
            f.TeslimEden = TeslimEden;
            f.TeslimAlan = TeslimAlan;
            //f.Toplam =decimal.Parse(Toplam);
            c.Faturalars.Add(f);
            foreach(var x in kalemler)
            {
                FaturaKalem fk = new FaturaKalem();
                fk.Aciklama = x.Aciklama;
                fk.Miktar = x.Miktar;
                fk.BirimFiyat = x.BirimFiyat;
                fk.Faturaid = x.FaturaKalemID;
                fk.Tutar = x.Tutar;
                c.faturaKalems.Add(fk);
            }
            c.SaveChanges();
            return Json("İşlem Başarılı",JsonRequestBehavior.AllowGet);
        }
    }
}