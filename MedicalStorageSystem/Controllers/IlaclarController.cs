using MedicalStorageSystem.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicalStorageSystem.Controllers
{
    public class IlaclarController : Controller
    {
        MedicalStoreEntities4 db = new MedicalStoreEntities4();
        public ActionResult Index()
        {
            var model = db.Ilac.ToList();
            return View(model);
        }

        [Authorize(Roles = "A,B")]
        public ActionResult Yeni()
        {
            return View("IlaclarForm", new Ilac());
        }

        public ActionResult Kaydet(Ilac Ilac)
        {
            if (!ModelState.IsValid)
            {
                return View("IlaclarForm");
            }
            if (Ilac.ilac_id == 0)
            {
                db.Ilac.Add(Ilac);
            }
            else
            {
                var GuncellenecekIlac = db.Ilac.Find(Ilac.ilac_id);
                if(GuncellenecekIlac == null)
                    return HttpNotFound();
                GuncellenecekIlac.medicine_code = Ilac.medicine_code;
                GuncellenecekIlac.isim = Ilac.isim;
                GuncellenecekIlac.uretici = Ilac.uretici;
                GuncellenecekIlac.fiyat = Ilac.fiyat;
                GuncellenecekIlac.stok_miktari = Ilac.stok_miktari;                
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Ilaclar");
        }

        [Authorize(Roles = "A,B")]
        public ActionResult Guncelle(int id)
        {
            var model = db.Ilac.Find(id);
            if(model == null)
                return HttpNotFound();
            return View("IlaclarForm",model);
        }

        [Authorize(Roles = "A,B")]
        public ActionResult Sil(int id)
        {
            var SilinecekIlac = db.Ilac.Find(id);
            if(SilinecekIlac == null)
                return HttpNotFound();
            else
            {
                db.Ilac.Remove(SilinecekIlac);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Ilaclar");
        }
    }
}