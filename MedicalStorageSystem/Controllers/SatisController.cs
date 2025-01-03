using MedicalStorageSystem.Models.EntityFramework;
using MedicalStorageSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicalStorageSystem.Controllers
{
    public class SatisController : Controller
    {
        MedicalStoreEntities4 db = new MedicalStoreEntities4();

        public ActionResult Index()
        {
            return View("SatislarForm", new Satis());
        }
        public ActionResult Kaydet(Satis satis)
        {
            satis.Ilac = db.Ilac.FirstOrDefault(x => x.medicine_code == satis.Ilac.medicine_code);
            satis.Musteri = db.Musteri.Find(satis.musteri_id);

            ModelState.Remove("Ilac.fiyat");
            ModelState.Remove("Ilac.stok_miktari");
            ModelState.Remove("Ilac.uretici");
            ModelState.Remove("Ilac.isim");

            if (ModelState.IsValid)
            {
                satis.ilac_id = satis.Ilac.ilac_id;
                if (satis.Ilac.stok_miktari >= satis.miktar)
                {

                    if (satis.satis_id == 0)
                    {
                        db.Satis.Add(satis);
                        satis.Ilac.stok_miktari -= satis.miktar;
                    }


                }
            }
            else
            {
                return View("SatislarForm");
            }

            
            db.SaveChanges();

            return RedirectToAction("Index", "AnaSayfa");
        }

    }
}