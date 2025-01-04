using MedicalStorageSystem.Models.EntityFramework;
using System;
using System.Collections.Generic;
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
        public ActionResult Index()
        {
            var model = db.Kullanici.ToList();
            return View(model);
        }
        public ActionResult Yeni()
        {
            return View("KullanicilarForm", new Kullanici());
        }
        public ActionResult Kaydet(Kullanici kullanici)
        {
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