﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisualAnalytics.Controllers.Helpers;
using log4net.Config;
using log4net;
using VisualAnalytics.Models;

namespace VisualAnalytics.Controllers
{
	class JsonEvent
	{
		public DateTime dt;
		public long importance;
	}
	public class HomeController : Controller
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private SAMcontext db = new SAMcontext();

		public ActionResult Index()
		{
			//new Prediction().Test(this);



			//new InputParser().parseCSVBelgium(@"D:\Workspace\SamData\20141215_RAJA_ASFEU_OOM_0.txt");
			//return null;
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		public JsonResult getEvents() 
		{
			List<Event> events = db.Events.ToList();

			var ev = events.Select( x =>
					  new JsonEvent(){ dt =  x.eventDate, importance = 50 }
						 )
						.OrderBy(x => x.dt); ;

			var jsonData = Json(ev,JsonRequestBehavior.AllowGet);
			return jsonData;
		}
		public ContentResult getConsuptions()
		{
			var consuptions = db.Consuptions.Where(x => x.source == 2 );

			var ev = //consuptions.First();
				consuptions
				//.Where(x => x.source == 2 && x.MeasurementSequence == 10)
				//.Where(x => x.source == 2 )
				.GroupBy(ce => ce.IDDate)
				.Select(a => new { amount = a. Sum(c => c.Amount) , dt = a.Key 
					//,measureOffset = a.MeasurementSequence 
				})
				.OrderBy(x => x.dt)
				//.ThenBy(x => x.measureOffset)
				;		


			//var jsonData = Json(ev, JsonRequestBehavior.AllowGet);
			var json2 = ev.ToList().ToJSON();
			return new ContentResult { Content = json2, ContentType = "application/json" }; ;
		}
		public ContentResult getWeather()
		{

			List<Weather> weathers = db.Weathers.Where(x => x.IDLocation.Equals(1)).ToList();

			var ev = //consuptions.First();
				weathers
				//.Where(x => x.idweather % 100 == 0)
				.Select(a => new { dt = a.dt, temperature = a.surfaceTemperature, humidity = a.relativeHumidity, wind = a.windSpeed, rain = a.rainfall })
				.OrderBy(x => x.dt );
			//var jsonData = Json(ev, JsonRequestBehavior.AllowGet);
			var json2 = ev.ToJSON();
			return new ContentResult { Content = json2, ContentType = "application/json" }; 
		}
		public ContentResult getBigTable()
		{
			var bigTable = db.Consuptions
					.Where(x => x.source == 2)
					.GroupBy(ce => ce.IDDate)
					.Select(a => new { dt = a.Key, amount = a.Sum(c => c.Amount) })
					.AsEnumerable()
					.Where(temp => temp.dt < 20140401)
					.Join(db.Weathers
						.Where(x => x.dt < new DateTime(2014, 04, 01))
						.AsEnumerable().GroupBy(gr => makeDateId(gr.dt))
						.Select(grW => new
						{
							iddate = grW.Key,
							Temperature = grW.Average(n => n.surfaceTemperature),
							Rain =grW.Average(n => n.rainfall),
							WindSpeed = grW.Average(n => n.windSpeed),
							Humidity = grW.Average(n => n.relativeHumidity),
							Solar = grW.Average(n => n.solarShine)
						})
						.OrderBy(orderW => orderW.iddate)
						, c => c.dt, w => w.iddate, (c, w) => new { Consup = c, weat = w })
					
					.Select(a => new
						{
							Amount = a.Consup.amount,
							IdDate = a.Consup.dt,
							Temperature = a.weat.Temperature,
							Rain = a.weat.Rain,
							WindSpeed = a.weat.WindSpeed,
							Humidity = a.weat.Humidity,
							Solar = a.weat.Solar 
						}
					)
					.OrderBy(order => order.IdDate);
			return new ContentResult { Content = bigTable.ToJSON(), ContentType = "application/json" };				
		}
        public ContentResult getDailyPlaceTable()
        {
            var bigTable =
                from c in db.ConsuptionsDailies
                join cp in db.ConsuptionPlaces on new {c.IDConsuptionPlace} equals new {cp.IDConsuptionPlace}
                join pw in db.PlaceWeathers on new {cp.DistrictName} equals new {pw.DistrictName}
                join w in db.WeathersDailies on new {n1 = c.IDDate,  n3 = pw.IDLocation} equals
                    new {n1 = w.IDDate, n3 = w.IDLocation}
                where c.IDConsuptionPlace == 1622 //CP from 62 district
                && c.IDDate >= 20140401 
                select new
                {
                    Amount = c.Amount,
                    cp.IDConsuptionPlace,
                    CityName = cp.CityName,
                    cp.DistrictName,
                    pw.IDLocation,
                    IdDate = c.IDDate,
                    MeasurementTime = c.MeasurementTime,
                    Temperature = w.surfaceTemperature,
                    Rain = w.rainfall,
                    WindSpeed = w.windSpeed,
                    Humidity = w.relativeHumidity,
                    Solar = w.solarShine,
                    Pressure = w.atmosphericPressure
                };

            bigTable = bigTable.OrderBy(a => a.IDConsuptionPlace).ThenBy(a => a.IdDate);
                         
                /*db.ConsuptionsDailies
                    .AsEnumerable()
                    .Join(db.ConsuptionPlaces, c => c.IDConsuptionPlace, cp => cp.IDConsuptionPlace, (c, cp) => new { consumption = c, consumptionPlace = cp })
                    .Join(db.PlaceWeathers, consumptionPlace => consumptionPlace. )
                    .Join(db.Weathers
                        .Where(x => x.dt < new DateTime(2014, 04, 01))
                        .AsEnumerable().GroupBy(gr => makeDateId(gr.dt))
                        .Select(grW => new
                        {
                            iddate = grW.Key,
                            Temperature = grW.Average(n => n.surfaceTemperature),
                            Rain = grW.Average(n => n.rainfall),
                            WindSpeed = grW.Average(n => n.windSpeed),
                            Humidity = grW.Average(n => n.relativeHumidity),
                            Solar = grW.Average(n => n.solarShine)
                        })
                        .OrderBy(orderW => orderW.iddate)
                        , c => c.dt, w => w.iddate, (c, w) => new { Consup = c, weat = w })

                    .Select(a => new
                    {
                        Amount = a.Consup.amount,
                        IdDate = a.Consup.dt,
                        Temperature = a.weat.Temperature,
                        Rain = a.weat.Rain,
                        WindSpeed = a.weat.WindSpeed,
                        Humidity = a.weat.Humidity,
                        Solar = a.weat.Solar
                    }
                    )
                    .OrderBy(order => order.IdDate);*/

            return new ContentResult { Content = bigTable.ToJSON(), ContentType = "application/json" };
        }
        public ContentResult getDailyAvgModelTable()
        {
            var bigTable =
                from c in db.ConsuptionModelDailies
                join pw in db.PlaceWeathers on new { c.IDDistrict } equals new { pw.IDDistrict }
                join w in db.WeathersDailies on new { n1 = c.IDDate, n3 = pw.IDLocation } equals
                    new { n1 = w.IDDate, n3 = w.IDLocation }
                where c.Type == "A" && c.IDDate >= 20140401 && c.IDDistrict == 62
                select new
                {
                    Amount = c.Amount,
                    pw.IDDistrict,
                    pw.IDLocation,
                    IdDate = c.IDDate,
                    MeasurementTime = c.MeasurementTime,
                    Temperature = w.surfaceTemperature,
                    Rain = w.rainfall,
                    WindSpeed = w.windSpeed,
                    Humidity = w.relativeHumidity,
                    Solar = w.solarShine,
                    Pressure = w.atmosphericPressure
                };

            bigTable = bigTable.OrderBy(a => a.IDDistrict).ThenBy(a => a.IdDate).Where(a => a.IDDistrict == 62);

            return new ContentResult { Content = bigTable.ToJSON(), ContentType = "application/json" };
        }
	    public ContentResult getDailySumModelTable()
	    {
	        var bigTable =
	            from c in db.ConsuptionModelDailies
	            join pw in db.PlaceWeathers on new {c.IDDistrict} equals new {pw.IDDistrict}
	            join w in db.WeathersDailies on new {n1 = c.IDDate, n3 = pw.IDLocation} equals
	                new {n1 = w.IDDate, n3 = w.IDLocation}
                where c.Type == "S" && c.IDDate >= 20140401 && c.IDDistrict == 62
	            select new
	            {
	                Amount = c.Amount,
	                pw.IDDistrict,
	                pw.IDLocation,
	                IdDate = c.IDDate,
	                MeasurementTime = c.MeasurementTime,
	                Temperature = w.surfaceTemperature,
	                Rain = w.rainfall,
	                WindSpeed = w.windSpeed,
	                Humidity = w.relativeHumidity,
	                Solar = w.solarShine,
                    Pressure = w.atmosphericPressure
	            };

            bigTable = bigTable.OrderBy(a => a.IDDistrict).ThenBy(a => a.IdDate).Where(a => a.IDDistrict == 62);

            return new ContentResult { Content = bigTable.ToJSON(), ContentType = "application/json" };
	    }

	    public ContentResult getClusters()
		{
			//if(ViewData["clusters"])
			var JSONCluster = ViewData["clusters"];

			return new ContentResult { Content = JSONCluster.ToJSON(), ContentType = "application/json" };

		}
		public ActionResult cluster()
		{
			var cl = new Clustering().cluster();
			//var t = cl.Select(a => new {idddate = a[1], cluster = a[0]});
			ViewData.Add("clusters", cl);

		    // ReSharper disable once Mvc.ViewNotResolved
			return View();
		}
		private long makeDateId(DateTime ?dt)
		{
			
			if (dt != null)
				return ((DateTime)(dt)).Day + ((DateTime)(dt)).Month * 100 + ((DateTime)(dt)).Year * 10000;
			else
				return 0;

		}
	}
}