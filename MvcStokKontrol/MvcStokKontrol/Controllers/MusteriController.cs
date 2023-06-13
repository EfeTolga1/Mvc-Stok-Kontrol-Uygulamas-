using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStokKontrol.Models.Entity;

namespace MvcStokKontrol.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(string p)
        {
            //var degerler = db.TblMusteriler.ToList();
            //return View(degerler);
            var degerler = from d in db.TblMusteriler select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.MusteriAd.Contains(p));
            }
            return View(degerler.ToList());
        }
        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMusteri(TblMusteriler p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.TblMusteriler.Add(p1);
            db.SaveChanges();
            return View();
        }
        public ActionResult Sil(int id)
        {
            var mst = db.TblMusteriler.Find(id);
            db.TblMusteriler.Remove(mst);
            db.SaveChanges();
             return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(int id)
        {
            var mus = db.TblMusteriler.Find(id);
            return View("MusteriGetir", mus);
        }
        public ActionResult Guncelle(TblMusteriler p1)
        {
            var musteri = db.TblMusteriler.Find(p1.MusteriID);
            musteri.MusteriAd = p1.MusteriAd;
            musteri.MusteriSoyad = p1.MusteriSoyad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}