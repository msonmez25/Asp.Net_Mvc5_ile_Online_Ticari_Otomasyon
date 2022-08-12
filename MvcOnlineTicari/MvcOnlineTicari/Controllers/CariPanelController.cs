using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicari.Models.Siniflar;
using System.Web.Security;

namespace MvcOnlineTicari.Controllers
{
    public class CariPanelController : Controller
    {
        // GET: CariPanel

        Context c = new Context();

        [Authorize]
        public ActionResult Index()
        {
            var carimailadresi = (string)Session["CariMail"];
            var degerler = c.Carilers.FirstOrDefault(x => x.CariMail == carimailadresi);
            var adsoyad = c.Carilers.Where(x => x.CariMail == carimailadresi).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adsoyad1 = adsoyad;
            var fotograf = c.Carilers.Where(x => x.CariMail == carimailadresi).Select(y => y.CariGorsel).FirstOrDefault();
            ViewBag.fotograf3 = fotograf;
            var telefon = c.Carilers.Where(x => x.CariMail == carimailadresi).Select(y => y.CariTelefon).FirstOrDefault();
            ViewBag.telefon4 = telefon;
            var sehir = c.Carilers.Where(x => x.CariMail == carimailadresi).Select(y => y.CariSehir).FirstOrDefault();
            ViewBag.sehir5 = sehir;            
            var mail = c.Carilers.Where(x => x.CariMail == carimailadresi).Select(y => y.CariMail).FirstOrDefault();
            ViewBag.mail7 = mail;
            var meslek = c.Carilers.Where(x => x.CariMail == carimailadresi).Select(y => y.CariMeslek).FirstOrDefault();
            ViewBag.meslek12 = meslek;

            var cariid = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariID).FirstOrDefault();
            var siparis = c.SatisHarekets.Where(x => x.Cariid == cariid).Count();
            ViewBag.siparis8 = siparis;

            var tutar = c.SatisHarekets.Where(x => x.Cariid == cariid).Sum(y => y.ToplamTutar).ToString();
            ViewBag.tutar9 = tutar;


            return View(degerler);
        }


        //Cari Bilgi Güncellem
        public ActionResult Index2(Cariler k)
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.CariID).FirstOrDefault();
            var cari = c.Carilers.Find(id);
            cari.CariAd = k.CariAd;
            cari.CariSoyad = k.CariSoyad;
            cari.CariSehir = k.CariSehir;
            cari.CariSifre = k.CariSifre;
            cari.CariMeslek = k.CariMeslek;
            c.SaveChanges();
            return RedirectToAction("Index");
        }


        //Cari Sipariş
        public ActionResult CariSiparislarim()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.CariID).FirstOrDefault();
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            return View(degerler);
        }


        //Kargo takibi
        public ActionResult KargoTakip(string p)
        {
            var k = from x in c.KargoDetays select x;
            k = k.Where(y => y.TakipKodu.Contains(p));
            return View(k.ToList());
        }

        //Kargo Detay Görüntüleme
        public ActionResult CariKargoTakip(string id)
        {
            var degerler = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();

            var kargotakino = c.KargoTakips.Where(x => x.TakipKodu == id).Select(y => y.TakipKodu).FirstOrDefault();

            ViewBag.kargo = kargotakino;

            return View(degerler);
        }


        //Cari Panelden çıkış yapmak
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }


        //Cari Profil Mesajlar
        public PartialViewResult Partial1()
        {
            var carimail = (string)Session["CariMail"];            
            var mesajlar = c.Mesajlars.Where(x => x.Alici == carimail.ToString()).OrderByDescending(y=>y.MesajID).ToList();


            return PartialView("Partial1", mesajlar);
            
        }

        //Cari Profil Bilgilerini Güncellemek
        public PartialViewResult Partial2()
        {
            var carimail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == carimail.ToString()).Select(z => z.CariID).FirstOrDefault();
            var caribul = c.Carilers.Find(id);
            return PartialView("Partial2", caribul);
        }


        //Duyuru 
        public PartialViewResult Partial3()
        {
            var duyurular = c.Duyurus.ToList();
            return PartialView(duyurular);
        }
    }
}