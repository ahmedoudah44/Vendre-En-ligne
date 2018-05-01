using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Echri3endy_Web.Models;
using WebApplication1.Models;
using System.IO;
using Microsoft.AspNet.Identity;

namespace Echri3endy_Web.Controllers
{
    [Authorize]
    public class ProduitsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Produits
        public ActionResult Index()
        {
            var produits = db.Produits.Include(p => p.category);
            return View(produits.ToList());
        }

        // GET: Produits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produit produit = db.Produits.Find(id);
            if (produit == null)
            {
                return HttpNotFound();
            }
            return View(produit);
        }

        // GET: Produits/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "id", "categoryNom");
            return View();
        }

        // POST: Produits/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Produit produit,HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads"),upload.FileName);
                upload.SaveAs(path);
                produit.ProduitImager = upload.FileName;
                produit.UserID = User.Identity.GetUserId();
                db.Produits.Add(produit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "id", "categoryNom", produit.CategoryId);
            return View(produit);
        }

        // GET: Produits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produit produit = db.Produits.Find(id);
            if (produit == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "id", "categoryNom", produit.CategoryId);
            return View(produit);
        }

        // POST: Produits/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Produit produit,HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                String olpath = Path.Combine(Server.MapPath("~/Uploads"),produit.ProduitImager);
                if (upload !=null) {
                    System.IO.File.Delete(olpath);
                    string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                    upload.SaveAs(path);
                    produit.ProduitImager = upload.FileName;

                }
                db.Entry(produit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "id", "categoryNom", produit.CategoryId);
            return View(produit);
        }

        // GET: Produits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produit produit = db.Produits.Find(id);
            if (produit == null)
            {
                return HttpNotFound();
            }
            return View(produit);
        }

        // POST: Produits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produit produit = db.Produits.Find(id);
            db.Produits.Remove(produit);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
