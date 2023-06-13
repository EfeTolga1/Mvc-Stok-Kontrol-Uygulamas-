using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStokKontrol.Models.Entity;

namespace MvcStokKontrol.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            var degerler = db.TblUrunler.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> degerler = (from i in db.TblKategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.KategoriID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(TblUrunler p1)
        {
            var ktg = db.TblKategoriler.Where(m => m.KategoriID == p1.TblKategoriler.KategoriID).FirstOrDefault();
            p1.TblKategoriler = ktg;
            
            db.TblUrunler.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var urun = db.TblUrunler.Find(id);
            db.TblUrunler.Remove(urun);
            db.SaveChanges(); 
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> degerler = (from i in db.TblKategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.KategoriID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            
            var urun = db.TblUrunler.Find(id);
            return View("UrunGetir", urun);
        }
        public ActionResult Guncelle(TblUrunler p)
        {
            var urun = db.TblUrunler.Find(p.UrunID);
            urun.UrunAdi = p.UrunAdi;
            urun.Marka = p.Marka;
            urun.Stok = p.Stok;
            urun.Fiyat = p.Fiyat;
            //urun.UrunKategori = p.UrunKategori;
            var ktg = db.TblKategoriler.Where(m => m.KategoriID == p.TblKategoriler.KategoriID).FirstOrDefault();
            urun.UrunKategori = ktg.KategoriID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}