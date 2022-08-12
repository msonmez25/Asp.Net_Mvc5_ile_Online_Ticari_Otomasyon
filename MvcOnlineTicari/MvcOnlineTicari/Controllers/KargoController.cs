using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicari.Models.Siniflar;

namespace MvcOnlineTicari.Controllers
{
    [Authorize(Roles = "A")]
    public class KargoController : Controller
    {
        // GET: Kargo

        Context c = new Context();

        //Kargoları Listelemek
        public ActionResult Index()
        {
            var kargolar = c.KargoDetays.ToList();
            return View(kargolar);
        }



        //Yeni Kargo Girişi
        [HttpGet]
        public ActionResult KargoEkle()
        {
            Random rnd = new Random();
            string[] karakterler = { "A", "B", "C", "D", "E", "F", "G", "H", "K", "L", "M", "S", "X", "Y", "Z" };

            int k1, k2, k3;
            k1 = rnd.Next(0, karakterler.Length); // 1 karakter buradan
            k2 = rnd.Next(0, karakterler.Length); // 1 karakter buradan
            k3 = rnd.Next(0, karakterler.Length); // 1 karakter buradan

            int s1, s2, s3;
            s1 = rnd.Next(100, 1000); // 10 karaktere ihriyacımız var 3 sayı buradan
            s2 = rnd.Next(10, 99); // 2 sayı buradan
            s3 = rnd.Next(10, 99); // 2 sayı buradan

            string kod = s1.ToString() + karakterler[k1] + s2.ToString() + karakterler[k2] + s3.ToString() + karakterler[k3];

            ViewBag.takipkod = kod;

            return View();
        }

        [HttpPost]
        public ActionResult KargoEkle(KargoDetay k)
        {
            c.KargoDetays.Add(k);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        //Kargo Detay Görüntüleme
        public ActionResult KargoTakip(string id)
        {
            var degerler = c.KargoTakips.Where(x => x.TakipKodu == id).OrderBy(y=>y.KargoTakipid).ToList();

            var kargotakino = c.KargoTakips.Where(x => x.TakipKodu == id).Select(y=>y.TakipKodu).FirstOrDefault();
            ViewBag.kargo = kargotakino;

            return View(degerler);
        }

        //Kargo Hareket Ekleme
        [HttpGet]
        public ActionResult KargoTakipEkle(string id)
        {
            var kargotakino = c.KargoTakips.Where(x => x.TakipKodu == id).Select(y => y.TakipKodu).FirstOrDefault();
            ViewBag.kargo = kargotakino;

            return View();
        }

        [HttpPost]
        public ActionResult KargoTakipEkle(KargoTakip k)
        {
            c.KargoTakips.Add(k);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}