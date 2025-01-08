using MedicalStorageSystem.Models;
using MedicalStorageSystem.Models.EntityFramework;
using MedicalStorageSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicalStorageSystem.Controllers
{
    public class AnaSayfaController : Controller
    {
        MedicalStoreEntities4 db = new MedicalStoreEntities4();

        [Authorize]
        public ActionResult Index(string searchTerm)
        {
            string query = @"
            SELECT 'Ilac' AS TabloAdi, ilac_id AS ID, isim AS ArananAlan
            FROM Ilac
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

            var satislar = new List<Satis>();

            foreach (var result in results)
            {
                if (result.TabloAdi == "Ilac")
                {
                    var ilac = db.Ilac.Find(result.ID);
                    if (ilac != null)
                    {
                        var satis = db.Satis.Where(s => s.ilac_id == ilac.ilac_id).ToList();
                        satislar.AddRange(satis);
                    }

                }
                if (result.TabloAdi == "Musteri")
                {
                    var musteri = db.Musteri.Find(result.ID);
                    if (musteri != null)
                    {
                        var satis = db.Satis.Where(s => s.musteri_id == musteri.musteri_id).ToList();
                        satislar.AddRange(satis);
                    }

                }
            }


            var model = new SearchViewModels()
            {
                SearchResults = results,
                SatislarHepsi = db.Satis.ToList(),
                SatislarElenmis = satislar

            };

            return View(model);
        }

    }
}