using MedicalStorageSystem.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicalStorageSystem.Controllers
{
    public class SearchController : Controller
    {
        MedicalStoreEntities4 db = new MedicalStoreEntities4();

        public ActionResult Index(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return View();
            }

            // Full-Text Search SQL sorgusu
            string query = @"
                SELECT 'Ilaclar' AS TabloAdi, ilac_id AS ID, isim AS ArananAlan
                FROM Ilaclar
                WHERE CONTAINS((isim, uretici), @p0)
                UNION ALL
                SELECT 'Musteri' AS TabloAdi, musteri_id AS ID, ad AS ArananAlan
                FROM Musteri
                WHERE CONTAINS((ad, soyad), @p0);
            ";

            var results = db.Database.SqlQuery<SearchResult>(
                query,
                new SqlParameter("@p0", $"\"{searchTerm}*\"")
            ).ToList();

            return View(results);
        }
    }
}