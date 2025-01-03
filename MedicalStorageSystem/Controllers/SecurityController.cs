using MedicalStorageSystem.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MedicalStorageSystem.Controllers
{
    public class SecurityController : Controller
    {
        MedicalStoreEntities4 db = new MedicalStoreEntities4(); 
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Kullanici kullanici)
        {
            var kullaniciInDb = db.Kullanici.FirstOrDefault(x => x.Ad == kullanici.Ad && x.Sifre == kullanici.Sifre);
            if (kullaniciInDb != null)
            {
                FormsAuthentication.SetAuthCookie(kullaniciInDb.Ad, false);
                return RedirectToAction("Index","Anasayfa");
            }
            else
            {
                ViewBag.Mesaj = "Geçersiz Kullanici Adı veya Şifre";
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View("Login");
        }
    }
}