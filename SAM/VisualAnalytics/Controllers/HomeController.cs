using System;
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
		//new InputParser().parseCSVBelgium(@"C:\Workspace\SAM\SAM\VisualAnalytics\DataBackUp\ELIA_LOAD_2014 (1).csv");
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

			List<weather> weathers = db.weathers.Where(x => x.idweather % 100 == 0).ToList();

			var ev = //consuptions.First();
				weathers
				//.Where(x => x.idweather % 100 == 0)
				.Select(a => new { dtt = a.dtt, temperature = a.surfaceTemperature, humidity = a.relativeHumidity, wind = a.windSpeed, rain = a.rainfall })
				.OrderBy(x => x.dtt );
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
					.Join(db.weathers
						//.Where(x => x.dtt < new DateTime(2014, 04, 01))
						.AsEnumerable().GroupBy(gr => makeDateId(gr.dtt))
						.Select(grW => new
						{
							iddate = grW.Key,
							Temperature = grW.Average(n => n.surfaceTemperature),
							Rain =grW.Average(n => n.rainfall),
							WindSpeed = grW.Average(n => n.windSpeed),
							WindDirection =grW.Average(n => n.windDirection),
							Humidity = grW.Average(n => n.relativeHumidity),
							Solar = grW.Average(n => n.solarFlux)
						})
						.OrderBy(orderW => orderW.iddate)
						, c => c.dt, w => w.iddate, (c, w) => new { Consup = c, weat = w })
					//.Where(temp => temp.Consup.dt < 20140401)
					.Select(a => new
						{
							Amount = a.Consup.amount,
							IdDate = a.Consup.dt,
							Temperature = a.weat.Temperature,
							Rain = a.weat.Rain,
							WindSpeed = a.weat.WindSpeed,
							WindDirection = a.weat.WindDirection,
							Humidity = a.weat.Humidity,
							Solar = a.weat.Solar 
						}
					)
					.OrderBy(order => order.IdDate);
			return new ContentResult { Content = bigTable.ToJSON(), ContentType = "application/json" };				
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