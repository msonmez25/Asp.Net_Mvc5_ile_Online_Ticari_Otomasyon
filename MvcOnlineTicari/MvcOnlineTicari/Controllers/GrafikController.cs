using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MvcOnlineTicari.Models.Siniflar;

namespace MvcOnlineTicari.Controllers
{
    public class GrafikController : Controller
    {
        // GET: Grafik

        Context c = new Context();

        //Index tarafınan manuel grafik oluşturmak
        public ActionResult Index()
        {
            return View();
        }

        //Controller tarafınan manuel grafik oluşturmak
        public ActionResult Index2()
        {
            var grafikciz = new Chart(600, 600);
            grafikciz.AddTitle("Kategori - Ürün Stok Sayısı").
                AddLegend("Stok").
                AddSeries("Değerler", xValue: new[] { "Mobilya", "Ofis Eşyaları", "Laptop" }
                                    , yValues: new[] { 85, 66, 35 }).Write();
            return File(grafikciz.ToWebImage().GetBytes(), "image/jpeg");

        }

        //Veri tabanından çekerek grafik oluşturmak
        public ActionResult Index3()
        {
            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();

            var sonuclar = c.Uruns.ToList();
            sonuclar.ToList().ForEach(x => xvalue.Add(x.UrunAd));
            sonuclar.ToList().ForEach(y => yvalue.Add(y.Stok));

            var grafik = new Chart(width: 1000, height: 1000)
                .AddTitle("Stoklar")
                .AddSeries(chartType: "Pie", name: "Stok", xValue: xvalue, yValues: yvalue);
            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");
        }


        //google chart manuel olarak grafik oluşturmak
        public ActionResult Index4()
        {
            return View();
        }

        public ActionResult VisualizeUrunResult()
        {
            return Json(Urunlistesi(), JsonRequestBehavior.AllowGet);
        }

        public List<sinif1> Urunlistesi()
        {
            List<sinif1> snf = new List<sinif1>();
            snf.Add(new sinif1()
            {
                urunad = "Bilgisayar",
                stok = 120
            });
            snf.Add(new sinif1()
            {
                urunad = "Beyaz Eşya",
                stok = 150
            });
            snf.Add(new sinif1()
            {
                urunad = "Mobilya",
                stok = 70
            });
            snf.Add(new sinif1()
            {
                urunad = "Küçük Ev Aletleri",
                stok = 90
            });
            snf.Add(new sinif1()
            {
                urunad = "Mobil Cihazlar",
                stok = 180
            });

            return snf;
        }



        //google chart dinamik olarak Pie grafik oluşturmak

        public ActionResult Index5()
        {
            return View();
        }

        public ActionResult VisualizeUrunResult2()
        {
            return Json(UrunListesi2(), JsonRequestBehavior.AllowGet);
        }

        public List<sinif2> UrunListesi2()
        {
            List<sinif2> snf = new List<sinif2>();
            using (var c = new Context())
            {
                snf = c.Uruns.Select(x => new sinif2
                {
                    urn = x.UrunAd,
                    stk = x.Stok
                }).ToList();
            }

            return snf;
        }


        //google chart dinamik olarak Line grafik oluşturmak
        public ActionResult Index6()
        {
            return View();
        }


        //google chart dinamik olarak Column grafik oluşturmak
        public ActionResult Index7()
        {
            return View();
        }
    }
}