using MedicalStorageSystem.Models;
using MedicalStorageSystem.Models.EntityFramework;
using MedicalStorageSystem.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicalStorageSystem.Controllers
{
    public class IlaclarController : Controller
    {
        MedicalStoreEntities4 db = new MedicalStoreEntities4();
        public ActionResult Index(string searchTerm, int? pageSize, bool? pageSizeBool, int? pageNumber)
        {
            var _tableService = new TableService();
            (int mevcutSayfa, int finalPageSize, int offset) = _tableService.getDataFromTable(pageSize, pageSizeBool, pageNumber);


            if (!string.IsNullOrEmpty(searchTerm))
            {
                var results = db.Database.SqlQuery<SearchResult>(
                    "EXEC SearchIlacPagingByColumns @searchTerm, @offset, @pageSize",
                    new SqlParameter("@searchTerm", searchTerm),
                    new SqlParameter("@offset", offset),
                    new SqlParameter("@pageSize", finalPageSize)
                ).ToList();

                var ilaclar = new List<Ilac>();

                foreach (var result in results)
                {
                    if (result.TabloAdi == "Ilac")
                    {
                        var ilacDetayTemp = db.Ilac.Find(result.ID);

                        if (ilacDetayTemp != null)
                        {
                            var ilacDetay = db.Ilac.Where(s => s.ilac_id == ilacDetayTemp.ilac_id).ToList();
                            ilaclar.AddRange(ilacDetay);
                        }
                    }
                }
                var model = ilaclar;
                ViewBag.Page = mevcutSayfa;
                return View(model);
            }
            else
            {
                var results = db.Database.SqlQuery<Ilac>(
                    "SELECT * FROM dbo.Ilac\r\nORDER BY ilac_id ASC OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY",
                    new SqlParameter("@offset", offset),
                    new SqlParameter("@pageSize", finalPageSize)
                    ).ToList();

                var model = results;
                ViewBag.Page = mevcutSayfa;
                return View(model);

            }
        }

        [Authorize(Roles = "A,B")]
        public ActionResult Yeni()
        {
            return View("IlaclarForm", new Ilac());
        }

        [HttpPost]
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
                if (GuncellenecekIlac == null)
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
            if (model == null)
                return HttpNotFound();
            return View("IlaclarForm", model);
        }

        [Authorize(Roles = "A,B")]
        public ActionResult Sil(int id)
        {
            var SilinecekIlac = db.Ilac.Find(id);
            if (SilinecekIlac == null)
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