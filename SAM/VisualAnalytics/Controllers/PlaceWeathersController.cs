using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using VisualAnalytics.Models;

namespace VisualAnalytics.Controllers
{
    public class PlaceWeathersController : Controller
    {
        private SAMcontext db = new SAMcontext();

        // GET: PlaceWeathers
        public ActionResult Index()
        {
            var weatherDistinct = db.WeathersDailies.Select(daily => new { IDLocation = daily.IDLocation, locationName = daily.locationName }).Distinct();
            var result = from w in weatherDistinct
                         join pw in db.PlaceWeathers on new { w.IDLocation } equals new { pw.IDLocation }
                         select
            new PlaceWeather()
            {
                IDLocation = pw.IDLocation,
                //w.locationName,//TODO add
                DistrictName = pw.DistrictName,
                IDDistrict = pw.IDDistrict,
            };

            return View(result.ToList());
        }

        // GET: PlaceWeathers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceWeather placeWeather = db.PlaceWeathers.Find(id);
            if (placeWeather == null)
            {
                return HttpNotFound();
            }
            return View(placeWeather);
        }

        // GET: PlaceWeathers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlaceWeathers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DistrictName,IDDistrict,IDLocation")] PlaceWeather placeWeather)
        {
            if (ModelState.IsValid)
            {
                db.PlaceWeathers.Add(placeWeather);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(placeWeather);
        }

        // GET: PlaceWeathers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceWeather placeWeather = db.PlaceWeathers.Find(id);
            if (placeWeather == null)
            {
                return HttpNotFound();
            }
            return View(placeWeather);
        }

        // POST: PlaceWeathers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DistrictName,IDDistrict,IDLocation")] PlaceWeather placeWeather)
        {
            if (ModelState.IsValid)
            {
                db.Entry(placeWeather).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(placeWeather);
        }

        // GET: PlaceWeathers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceWeather placeWeather = db.PlaceWeathers.Find(id);
            if (placeWeather == null)
            {
                return HttpNotFound();
            }
            return View(placeWeather);
        }

        // POST: PlaceWeathers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlaceWeather placeWeather = db.PlaceWeathers.Find(id);
            db.PlaceWeathers.Remove(placeWeather);
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