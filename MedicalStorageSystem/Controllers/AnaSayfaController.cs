using MedicalStorageSystem.Models;
using MedicalStorageSystem.Models.EntityFramework;
using MedicalStorageSystem.Services;
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
        public ActionResult Index(string searchTerm, int? pageSize, bool? pageSizeBool, int? pageNumber)
        {
            var _tableService = new TableService();
            (int mevcutSayfa,int finalPageSize,int offset) = _tableService.getDataFromTable(pageSize, pageSizeBool, pageNumber);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var results = db.Database.SqlQuery<SearchResult>(
                    "EXEC SearchSatisPagingByColumns @searchTerm, @offset, @pageSize",
                    new SqlParameter("@searchTerm", searchTerm),
                    new SqlParameter("@offset", offset),
                    new SqlParameter("@pageSize", finalPageSize)
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
                var model = vw_satisDetay;
                ViewBag.Page = mevcutSayfa;
                return View(model);
            }
            else
            {
                var results = db.Database.SqlQuery<vw_SatisDetay>(
                    "SELECT * FROM dbo.vw_SatisDetay\r\nORDER BY Tarih DESC OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY",
                    new SqlParameter("@offset", offset),
                    new SqlParameter("@pageSize", finalPageSize)
                    ).ToList();

                var model = results;
                ViewBag.Page = mevcutSayfa;
                return View(model);

            }
        }
    }
}