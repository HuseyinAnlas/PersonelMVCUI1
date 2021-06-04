using PersonelMVCUI1.Models.Entity_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PersonelMVCUI1.Controllers
{
   
    public class SecurityController : Controller
    {
        PersonelDbEntities db = new PersonelDbEntities();
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Kullanici kullanici)
        {
            var kullaniciInDb = db.Kullanici.FirstOrDefault(x => x.Email == kullanici.Email && x.Sifre == kullanici.Sifre);
            if (kullaniciInDb!=null)
            {
                FormsAuthentication.SetAuthCookie(kullanici.Email, false);
                return RedirectToAction("Index","Departman");
            }
            else
            {
                ViewBag.Mesaj = "Geçersiz kullanıcı Adı ya da şifresi";
                 return View();    
            }           
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}