using MedicalStorageSystem.Models.EntityFramework;
using MedicalStorageSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicalStorageSystem.Controllers
{
    public class AnaSayfaController : Controller
    {
        MedicalStoreEntities4 db = new MedicalStoreEntities4();

        [Authorize]
        public ActionResult Index()
        {
            var model = new SatislarFormViewModels()
            {
                Ilaclar = db.Ilac.ToList(),
                Musteriler = db.Musteri.ToList(),
                Satislar = db.Satis.ToList(),
            };
            return View(model);

        }
        
    }
}