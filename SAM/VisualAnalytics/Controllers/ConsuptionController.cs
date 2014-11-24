using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VisualAnalytics.Models;

namespace VisualAnalytics.Controllers
{
    public class ConsuptionController : Controller
    {
        private SAMcontext db = new SAMcontext();

        // GET: /Consumption/
        public ActionResult Index()
        {
            var consuptions = db.Consuptions.Include(c => c.ConsuptionPlace);
            return View(consuptions.ToList());
        }

        // GET: /Consumption/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consuption consuption = db.Consuptions.Find(id);
            if (consuption == null)
            {
                return HttpNotFound();
            }
            return View(consuption);
        }

        // GET: /Consumption/Create
        public ActionResult Create()
        {
            ViewBag.IDConsuptionPlace = new SelectList(db.ConsuptionPlaces, "IDConsuptionPlace", "ZipCode");
            return View();
        }

        // POST: /Consumption/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="IDConsuption,Amount,DayOffset,MeasurementSequence,MeasurementTime,IDDate,IDConsuptionPlace")] Consuption consuption)
        {
            if (ModelState.IsValid)
            {
                db.Consuptions.Add(consuption);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDConsuptionPlace = new SelectList(db.ConsuptionPlaces, "IDConsuptionPlace", "ZipCode", consuption.IDConsuptionPlace);
            return View(consuption);
        }

        // GET: /Consumption/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consuption consuption = db.Consuptions.Find(id);
            if (consuption == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDConsuptionPlace = new SelectList(db.ConsuptionPlaces, "IDConsuptionPlace", "ZipCode", consuption.IDConsuptionPlace);
            return View(consuption);
        }

        // POST: /Consumption/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="IDConsuption,Amount,DayOffset,MeasurementSequence,MeasurementTime,IDDate,IDConsuptionPlace")] Consuption consuption)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consuption).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDConsuptionPlace = new SelectList(db.ConsuptionPlaces, "IDConsuptionPlace", "ZipCode", consuption.IDConsuptionPlace);
            return View(consuption);
        }

        // GET: /Consumption/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consuption consuption = db.Consuptions.Find(id);
            if (consuption == null)
            {
                return HttpNotFound();
            }
            return View(consuption);
        }

        // POST: /Consumption/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Consuption consuption = db.Consuptions.Find(id);
            db.Consuptions.Remove(consuption);
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
