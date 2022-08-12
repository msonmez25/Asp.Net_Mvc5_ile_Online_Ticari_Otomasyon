using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicari.Models.Siniflar;

namespace MvcOnlineTicari.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel

        Context c = new Context();

        //Personel Listeleme
        public ActionResult Index()
        {
            var personeller = c.Personels.OrderByDescending(x => x.Departman.DepartmanAd).ToList();
            return View(personeller);
        }


        //Yeni Personel Ekleme
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            //Departman getirmek için
            List<SelectListItem> deger1 = (from i in c.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.DepartmanAd,
                                               Value = i.DepartmanID.ToString(),
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            return View();
        }

        [HttpPost]
        public ActionResult PersonelEkle(Personel p)
        {
            if(Request.Files.Count>0)
            {
                string dosyadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                p.PersonelGorsel= "/Image/" + dosyadi + uzanti;
            }
            p.Durum = true;
            c.Personels.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }



        // Personel Güncelleme
        public ActionResult PersonelGetir(int id)
        {
            var prs = c.Personels.Find(id);

            //Departman getirmek için
            List<SelectListItem> deger1 = (from i in c.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.DepartmanAd,
                                               Value = i.DepartmanID.ToString(),
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            return View("PersonelGetir", prs);
        }

        public ActionResult PersonelGuncelle(Personel k)
        {
            var prs = c.Personels.Find(k.PersonelID);
            prs.PersonelAd = k.PersonelAd;
            prs.PersonelSoyad = k.PersonelSoyad;
            prs.PersonelGorsel = k.PersonelGorsel;
            prs.PersonelTelefon = k.PersonelTelefon;
            prs.Departmanid = k.Departmanid;
            prs.PersonelSehir = k.PersonelSehir;
            prs.PersonelMail = k.PersonelMail;
            prs.Durum = true;
            c.SaveChanges();
            return RedirectToAction("Index");
        }


        // Personel Silme
        public ActionResult PersonelSil(int id)
        {
            var prs = c.Personels.Find(id);
            prs.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }


        //Personel Detaylı Gösterim
        public ActionResult PersonelDetayli()
        {
            var sorgu = c.Personels.Where(z=>z.Durum==true).OrderByDescending(x=>x.Departman.DepartmanAd).ToList();
            return View(sorgu);
        }
    }
}