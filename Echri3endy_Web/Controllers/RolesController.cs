﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace Echri3endy_Web.Controllers
{
    [Authorize(Users = "ahmedou_dah")]
    public class RolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Roles
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }

        // GET: Roles/Details/5
        public ActionResult Details(String id)
        {
            var role = db.Roles.Find(id);
            if (role ==null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        public ActionResult Create(IdentityRole role)
        {
            try
            {
                if (ModelState.IsValid) {
                    db.Roles.Add(role);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                // TODO: Add insert logic here

                return View(role);
            }
            catch
            {
                return View();
            }
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(String id)
        {
            var role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include ="Id,Name")]IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(String id)
        {
            var role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(IdentityRole role)
        {
           
             var  myrole = db.Roles.Find(role.Id);
                db.Roles.Remove(myrole);
                db.SaveChanges();
                // TODO: Add delete logic here
                return RedirectToAction("Index");
           
        }
    }
}
