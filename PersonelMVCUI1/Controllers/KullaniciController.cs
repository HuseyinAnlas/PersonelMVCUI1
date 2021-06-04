using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PersonelMVCUI1.Models.Entity_Framework;
using PersonelMVCUI1.ViewModels;

namespace PersonelMVCUI1.Controllers
{
    [Authorize(Roles = "A")]
    public class KullaniciController : Controller
    {

        PersonelDbEntities db = new PersonelDbEntities();


        public ActionResult Index()
        {
            var model = db.Kullanici.ToList();

            return View(model);
        }

        [HttpGet]
        public ActionResult Yeni()
        {
            var model = new KullaniciFormViewModel()
            {
                Personeller = db.Personel.ToList(),
                Kullanici = new Kullanici()
            };

            return View("KullaniciForm", model);
        }


        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(Kullanici kullanici)
        {
            if (!ModelState.IsValid)
            {
                var model2 = new KullaniciFormViewModel()
                {
                    Personeller = db.Personel.ToList(),
                    Kullanici = kullanici
                };

                return View("KullaniciForm", model2);
            }

            MesajViewModel model = new MesajViewModel();
            if (kullanici.Id == 0)
            {
                model.Mesaj = kullanici.Email + " başarı ile eklendi.";
                db.Kullanici.Add(kullanici);
            }
            else
            {
                db.Entry(kullanici).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();

            model.Status = true;
            model.LinkText = "Kullanici Listesi";
            model.Url = "/Kullanici";

            return View("_Mesaj", model);
        }

        public ActionResult Guncelle(int id)
        {
            var model = new KullaniciFormViewModel()
            {
                Personeller = db.Personel.ToList(),
                Kullanici = db.Kullanici.Find(id)
            };

            return View("KullaniciForm", model);
        }

        public ActionResult Sil(int id)
        {
            var silinecekKullanici = db.Kullanici.Find(id);
            if (silinecekKullanici == null)
            { 
                return HttpNotFound(); 
            }
            db.Kullanici.Remove(silinecekKullanici);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KullaniciDetayi(int id)
        {
            var model = db.Kullanici.Where(x => x.Id == id);
            return PartialView(model);

            //Kullanici kullanici = db.Kullanici.Find(id);
            //if (kullanici == null) return HttpNotFound();

            //return View(kullanici);
        }


        //private PersonelDbEntities db = new PersonelDbEntities();

        //// GET: Kullanici
        //public ActionResult Index()
        //{
        //    return View(db.Kullanici.ToList());
        //}

        //// GET: Kullanici/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Kullanici kullanici = db.Kullanici.Find(id);
        //    if (kullanici == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(kullanici);
        //}

        //// GET: Kullanici/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Kullanici/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Ad,Sifre,Role")] Kullanici kullanici)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Kullanici.Add(kullanici);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(kullanici);
        //}

        //// GET: Kullanici/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Kullanici kullanici = db.Kullanici.Find(id);
        //    if (kullanici == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(kullanici);
        //}

        //// POST: Kullanici/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Ad,Sifre,Role")] Kullanici kullanici)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(kullanici).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(kullanici);
        //}

        //// GET: Kullanici/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Kullanici kullanici = db.Kullanici.Find(id);
        //    if (kullanici == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(kullanici);
        //}

        //// POST: Kullanici/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Kullanici kullanici = db.Kullanici.Find(id);
        //    db.Kullanici.Remove(kullanici);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
