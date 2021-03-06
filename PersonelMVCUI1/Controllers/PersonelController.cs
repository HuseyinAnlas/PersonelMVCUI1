using PersonelMVCUI1.Models.Entity_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PersonelMVCUI1.ViewModels;

namespace PersonelMVCUI1.Controllers
{
    public class PersonelController : Controller
    {
        PersonelDbEntities db = new PersonelDbEntities();
       
        public ActionResult Index()
        {

            var model = db.Personel.Include(x=>x.Departman).ToList();
            return View(model);
        }
        
        public ActionResult Yeni()
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.Departman.ToList(),
                Personel=new Personel()
            };
            return View("PersonelForm",model);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(Personel personel)
        {
            if (!ModelState.IsValid)
            {
                var model = new PersonelFormViewModel() 
                {
                    Departmanlar = db.Departman.ToList(),
                    Personel=personel 
                };
                return View("PersonelForm",model);
            }

            MesajViewModel model1 = new MesajViewModel();
            if (personel.Id==0)
            {
                model1.Mesaj = personel.Ad + " başarı ile eklendi.";
                db.Personel.Add(personel);
            }
            else
            {
                db.Entry(personel).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();

            model1.Status = true;
            model1.LinkText = "Personel Listesi";
            model1.Url = "/Personel";

            return View("_Mesaj", model1);

            
        }

        public ActionResult Guncelle(int id){

            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.Departman.ToList(),
                Personel=db.Personel.Find(id)
            };

            
            return View("PersonelForm", model);
        }

        public ActionResult Sil(int id)
        {
            var silinecekPersonel = db.Personel.Find(id);
            if (silinecekPersonel==null)
            {
                return HttpNotFound();
            }
            db.Personel.Remove(silinecekPersonel);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult PersonelleriListele(int id)
        {
            var model = db.Personel.Where(x => x.DepartmanId == id).ToList();
            return PartialView(model);
        }

        public ActionResult ToplamMaas()
        {   
            ViewBag.Maas= db.Personel.Sum(x => x.Maas);
            return PartialView();
            
        }

    }
}