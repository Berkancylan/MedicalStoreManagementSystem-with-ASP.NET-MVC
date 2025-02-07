using MedicalStorageSystem.Models;
using MedicalStorageSystem.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;

namespace MedicalStorageSystem.Controllers
{
    [Authorize(Roles = "A")]
    public class KullanicilarController : Controller
    {
        MedicalStoreEntities4 db = new MedicalStoreEntities4();
        public ActionResult Index(string searchTerm)
        {
            var model = db.Kullanici.ToList();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var results = db.Database.SqlQuery<SearchResult>(
                    "EXEC SearchKullaniciByColumns @searchTerm",
                    new SqlParameter("@searchTerm", searchTerm)
                ).ToList();

                var kullanicilar = new List<Kullanici>();

                foreach (var result in results)
                {
                    if (result.TabloAdi == "Kullanici")
                    {
                        var kullaniciTemp = db.Kullanici.Find(result.ID);

                        if (kullaniciTemp != null)
                        {
                            var kullanici = db.Kullanici.Where(s => s.Id == kullaniciTemp.Id).ToList();
                            kullanicilar.AddRange(kullanici);
                        }
                    }
                }
                model = kullanicilar;
            }
            return View(model);
        }
        public ActionResult Yeni()
        {
            return View("KullanicilarForm", new Kullanici());
        }

        [HttpPost]
        public ActionResult Kaydet(Kullanici kullanici)
        {
            if(!ModelState.IsValid)
            {
                return View("KullanicilarForm");
                //return RedirectToAction("Kullanicilar","KullanicilarForm",kullanici); Buna bir bak
            }
            if (kullanici.Id == 0)
            {
                db.Kullanici.Add(kullanici);
            }
            else
            {
                var GuncellenecekKullanici = db.Kullanici.Find(kullanici.Id);
                if (GuncellenecekKullanici == null)
                {
                    return HttpNotFound();
                }
                GuncellenecekKullanici.Ad = kullanici.Ad;
                GuncellenecekKullanici.Sifre = kullanici.Sifre;
                GuncellenecekKullanici.Role = kullanici.Role;

            }
            db.SaveChanges();
            return RedirectToAction("Index", "Kullanicilar");
        }

        public ActionResult Guncelle(int id)
        {
            var model = db.Kullanici.Find(id);
            if(model == null)
                return HttpNotFound();
            return View("KullanicilarForm", model);

        }

        public ActionResult Sil(int id)
        {
            var model = db.Kullanici.Find(id);
            if(model == null) 
                return HttpNotFound();
            else
            {
                db.Kullanici.Remove(model);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Kullanicilar");

        }


    }
}