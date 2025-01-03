using MedicalStorageSystem.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}