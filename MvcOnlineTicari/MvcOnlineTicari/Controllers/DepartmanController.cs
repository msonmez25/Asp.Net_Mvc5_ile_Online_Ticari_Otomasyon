using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicari.Models.Siniflar;

namespace MvcOnlineTicari.Controllers
{
    public class DepartmanController : Controller
    {
        // GET: Departman

        Context c = new Context();

        //Departman Listeleme
        public ActionResult Index()
        {
            var departmanlar = c.Departmans.ToList();
            return View(departmanlar);
        }


        //Yeni Departman Ekleme
        [HttpGet]
        public ActionResult DepartmanEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DepartmanEkle(Departman d)
        {
            c.Departmans.Add(d);
            d.Durum = true;
            c.SaveChanges();
            return RedirectToAction("Index");
        }



        // Departman Silme
        public ActionResult DepartmanSil(int id)
        {
            var dprtmn = c.Departmans.Find(id);
            dprtmn.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }


        // Departman Güncelleme
        public ActionResult DepartmanGetir(int id)
        {
            var dprtmn = c.Departmans.Find(id);
            return View("DepartmanGetir", dprtmn);
        }

        public ActionResult DepartmanGuncelle(Departman d)
        {
            var dprtmn = c.Departmans.Find(d.DepartmanID);
            dprtmn.DepartmanAd = d.DepartmanAd;
            dprtmn.Durum = true;
            c.SaveChanges();
            return RedirectToAction("Index");
        }


        //Departman Detay Görüntüleme
        public ActionResult DepartmanDetay(int id)
        {
            var degerler = c.Personels.Where(x => x.Departmanid == id).ToList();
            var dprtm = c.Personels.Where(x => x.Departmanid == id).Select(y=>y.Departman.DepartmanAd).FirstOrDefault();

            ViewBag.d = dprtm;
            return View(degerler);
        }


        //Personel Satış Detay Görüntüleme
        public ActionResult DepartmanPersonelSatis(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.Personelid == id).ToList();
            var personeladsoyad = c.Personels.Where(x => x.PersonelID == id).Select(y => y.PersonelAd +" "+ y.PersonelSoyad).FirstOrDefault();

            ViewBag.pers = personeladsoyad;

            return View(degerler);
        }
    }
}