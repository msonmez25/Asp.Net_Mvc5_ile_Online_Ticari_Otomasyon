using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicari.Models.Siniflar;

namespace MvcOnlineTicari.Controllers
{
    [Authorize(Roles = "A")]
    public class GaleriController : Controller
    {
        // GET: Galeri

        Context c = new Context();

        //Ürün Görsellerini Listeleme
        public ActionResult Index()
        {
            var degerler = c.Uruns.ToList();
            return View(degerler);
        }
    }
}