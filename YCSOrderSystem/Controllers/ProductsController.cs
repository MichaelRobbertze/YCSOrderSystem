﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YCSOrderSystem.Models;

namespace YCSOrderSystem.Controllers
{
    [Authorize(Roles ="Admin,Manager,Employee")]
    public class ProductsController : Controller
    {
        private YCSDatabaseEntities db = new YCSDatabaseEntities();

        // GET: Products
        public ActionResult Index()
        {
            if (SUserRole() != "Customer" && SUserRole() != null)
            {
                ViewBag.displayMenu = "Yes";
            }
            //ViewBag.displayMenu = "yes";
            var products = db.Products.Include(p => p.Supplier);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (SUserRole() != "Customer" && SUserRole() != null)
            {
                ViewBag.displayMenu = "Yes";
            }
            ViewBag.displayMenu = "yes";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            if (SUserRole() != "Customer" && SUserRole() != null)
            {
                ViewBag.displayMenu = "Yes";
            }
            ViewBag.displayMenu = "yes";
            ViewBag.SuppNum = new SelectList(db.Suppliers, "SuppNum", "SuppName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProdNum,ProdName,ProdDesc,Price,QtyOnHand,SuppNum")] Product product)
        {
            if (SUserRole() != "Customer" && SUserRole() != null)
            {
                ViewBag.displayMenu = "Yes";
            }
            ViewBag.displayMenu = "yes";
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SuppNum = new SelectList(db.Suppliers, "SuppNum", "SuppName", product.SuppNum);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (SUserRole() != "Customer" && SUserRole() != null)
            {
                ViewBag.displayMenu = "Yes";
            }
            ViewBag.displayMenu = "yes";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.SuppNum = new SelectList(db.Suppliers, "SuppNum", "SuppName", product.SuppNum);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProdNum,ProdName,ProdDesc,Price,QtyOnHand,SuppNum")] Product product)
        {
            if (SUserRole() != "Customer" && SUserRole() != null)
            {
                ViewBag.displayMenu = "Yes";
            }
            ViewBag.displayMenu = "yes";
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SuppNum = new SelectList(db.Suppliers, "SuppNum", "SuppName", product.SuppNum);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (SUserRole() != "Customer" && SUserRole() != null)
            {
                ViewBag.displayMenu = "Yes";
            }
            ViewBag.displayMenu = "yes";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (SUserRole() != "Customer" && SUserRole() != null)
            {
                ViewBag.displayMenu = "Yes";
            }
            ViewBag.displayMenu = "yes";
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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

        public String SUserRole()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin")
                {
                    return "Admin";
                }
                else if (s[0].ToString() == "Manager")
                {
                    return "Manager";
                }
                else if (s[0].ToString() == "Employee")
                {
                    return "Employee";
                }
                else
                {
                    return "Customer";
                }
            }
            return "Customer";
        }
    }
}
