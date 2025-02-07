using MedicalStorageSystem.Models;
using MedicalStorageSystem.Models.EntityFramework;
using MedicalStorageSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.WebPages;

namespace MedicalStorageSystem.Controllers
{
    public class AnaSayfaController : Controller
    {
        MedicalStoreEntities4 db = new MedicalStoreEntities4();

        [Authorize]
        public ActionResult Index(string searchTerm)
        {
            var model = db.vw_SatisDetay.ToList();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var results = db.Database.SqlQuery<SearchResult>(
                    "EXEC SearchSatisByColumns @searchTerm",
                    new SqlParameter("@searchTerm", searchTerm)
                ).ToList();

                var vw_satisDetay = new List<vw_SatisDetay>();

                foreach (var result in results)
                {
                    if (result.TabloAdi == "SatisDetay")
                    {
                        var satisDetayTemp = db.vw_SatisDetay.Find(result.ID);

                        if (satisDetayTemp != null)
                        {
                            var satisDetay = db.vw_SatisDetay.Where(s => s.SatisID == satisDetayTemp.SatisID).ToList();
                            vw_satisDetay.AddRange(satisDetay);
                        }
                    }
                }
                model = vw_satisDetay;
            }
            return View(model);

        }

    }
}