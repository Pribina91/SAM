using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VisualAnalytics.Controllers.Analytics;
using VisualAnalytics.Controllers.Helpers;
using VisualAnalytics.Models;

namespace VisualAnalytics.Controllers
{
    public enum ModelType
    {
        AVERAGE = 1,
        SUM = 2
    };

    public class HomeController : Controller
    {
        private const string MODEL_JSON = "modelJson";
        private const string WEATHER_JSON = "weatherJson";

        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SAMcontext db = new SAMcontext();
        private AnalyticsModule am = new AnalyticsModule();

        private string modelJSON
        {
            get { return (string)ViewData[MODEL_JSON]; }
            set { ViewData[MODEL_JSON] = value; }
        }

        private string weatherJSON
        {
            get { return (string)ViewData[WEATHER_JSON]; }
            set { ViewData[WEATHER_JSON] = value; }
        }

        public ActionResult Index()
        {
            //new AnalyticsModule().Test(this);

            //new InputParser().parseCSVBelgium(@"D:\Workspace\SamData\20141215_RAJA_ASFEU_OOM_0.txt");

            //getDailyAvgModelTable();

            //WeatherColumns allTrue = new WeatherColumns(new byte[] { 1, 1, 1, 1, 1, 1 });
            //am.modelChange(getDailyAvgModelTable().Content, "fit_avg", allTrue);
            //string place = getDailyPlaceTable().Content;
            //am.fitSeriesToModel(place, "fit_avg", "dailyDataPlace", allTrue);
            //List<Outlier> outliers = new List<Outlier>();
            //outliers = am.findOutliers("dailyDataPlace", allTrue);
            ////
            //WeatherColumns nonerue = new WeatherColumns(new byte[] { 0, 0, 0, 0, 0, 1 });
            //am.FindLocalProperties(place, "fit", allTrue, outliers);
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

        public ContentResult getEvents()
        {
            List<Event> events = db.Events.ToList();

            var ev = events
                //.Where(e=>e.eventType.Equals())
                .Select(x => new
                {
                    IDDate = x.IDDate,
                    importance = 50
                })
                    .OrderBy(x => x.IDDate);

            return new ContentResult { Content = ev.ToJSON(), ContentType = "application/json" };
        }

        public ContentResult getConsuptions()
        {
            var consuptions = db.Consuptions.Where(x => x.source == 2);

            var ev = //consuptions.First();
                consuptions
                //.Where(x => x.source == 2 && x.MeasurementSequence == 10)
                //.Where(x => x.source == 2 )
                    .GroupBy(ce => ce.IDDate)
                    .Select(a => new
                    {
                        amount = a.Sum(c => c.Amount),
                        dt = a.Key
                        //,measureOffset = a.MeasurementSequence
                    })
                    .OrderBy(x => x.dt)
                //.ThenBy(x => x.measureOffset)
                ;

            //var jsonData = Json(ev, JsonRequestBehavior.AllowGet);
            var json2 = ev.ToList().ToJSON();
            return new ContentResult { Content = json2, ContentType = "application/json" };
            ;
        }

        //public ContentResult getWeather()
        //{
        //    List<Weather> weathers = db.Weathers.Where(x => x.IDLocation.Equals(1)).ToList();

        //    var ev = //consuptions.First();
        //        weathers
        //        //.Where(x => x.idweather % 100 == 0)
        //        .Select(a => new { dt = a.dt, temperature = a.surfaceTemperature, humidity = a.relativeHumidity, wind = a.windSpeed, rain = a.rainfall })
        //        .OrderBy(x => x.dt);
        //    //var jsonData = Json(ev, JsonRequestBehavior.AllowGet);
        //    var json2 = ev.ToJSON();
        //    return new ContentResult { Content = json2, ContentType = "application/json" };
        //}

        //public ContentResult getBigTable()
        //{
        //    var bigTable = db.Consuptions
        //            .Where(x => x.source == 2)
        //            .GroupBy(ce => ce.IDDate)
        //            .Select(a => new { dt = a.Key, amount = a.Sum(c => c.Amount) })
        //            .AsEnumerable()
        //            .Where(temp => temp.dt < 20140401)
        //            .Join(db.Weathers
        //                .Where(x => x.dt < new DateTime(2014, 04, 01))
        //                .AsEnumerable().GroupBy(gr => makeDateId(gr.dt))
        //                .Select(grW => new
        //                {
        //                    iddate = grW.Key,
        //                    Temperature = grW.Average(n => n.surfaceTemperature),
        //                    Rain = grW.Average(n => n.rainfall),
        //                    WindSpeed = grW.Average(n => n.windSpeed),
        //                    Humidity = grW.Average(n => n.relativeHumidity),
        //                    Solar = grW.Average(n => n.solarShine)
        //                })
        //                .OrderBy(orderW => orderW.iddate)
        //                , c => c.dt, w => w.iddate, (c, w) => new { Consup = c, weat = w })

        //            .Select(a => new
        //                {
        //                    Amount = a.Consup.amount,
        //                    IDDate = a.Consup.dt,
        //                    Temperature = a.weat.Temperature,
        //                    Rain = a.weat.Rain,
        //                    WindSpeed = a.weat.WindSpeed,
        //                    Humidity = a.weat.Humidity,
        //                    Solar = a.weat.Solar
        //                }
        //            )
        //            .OrderBy(order => order.IDDate);
        //    return new ContentResult { Content = bigTable.ToJSON(), ContentType = "application/json" };
        //}

        public ContentResult getDailyPlaceTable()
        {
            var bigTable =
                from c in db.ConsuptionsDailies
                join cp in db.ConsuptionPlaces on new { c.IDConsuptionPlace } equals new { cp.IDConsuptionPlace }
                join pw in db.PlaceWeathers on new { cp.DistrictName } equals new { pw.DistrictName }
                join w in db.WeathersDailies on new { n1 = c.IDDate, n3 = pw.IDLocation } equals
                    new { n1 = w.IDDate, n3 = w.IDLocation }
                where c.IDConsuptionPlace == 1622 //CP from 62 district
                //&& c.IDDate >= 20140401
                select new
                {
                    Amount = c.Amount,
                    cp.IDConsuptionPlace,
                    CityName = cp.CityName,
                    cp.DistrictName,
                    pw.IDLocation,
                    IDDate = c.IDDate,
                    MeasurementTime = c.MeasurementTime,
                    Temperature = w.surfaceTemperature,
                    Rain = w.rainfall,
                    WindSpeed = w.windSpeed,
                    Humidity = w.relativeHumidity,
                    Solar = w.solarShine,
                    Pressure = w.atmosphericPressure
                };

            bigTable = bigTable.OrderBy(a => a.IDConsuptionPlace).ThenBy(a => a.IDDate);
            int x = bigTable.Count();
            modelJSON = bigTable.ToJSON();

            //    bigTable.Select(a => new
            //{
            //    a.Amount,
            //    a.IDDate,
            //    a.IDLocation,
            //    a.IDConsuptionPlace
            //}
            //    ).ToJSON();

            //weatherJSON = bigTable.Select(a => new
            //{
            //    a.Temperature,
            //    a.Rain,
            //    a.WindSpeed,
            //    a.Humidity,
            //    a.Solar,
            //    a.Pressure,
            //    a.IDDate,
            //    a.IDLocation
            //}).ToJSON();

            return new ContentResult { Content = bigTable.ToJSON(), ContentType = "application/json" };
        }

        public ContentResult getFittedModelTable(string Type = "SUM", int idDistrict = 62, string weatherDependency = "000000")
        {
            byte[] bytes = new byte[6];
            var chArray = weatherDependency.ToCharArray();
            for (int i = 0; i < 6; i++)
            {
                bytes[i] = BitConverter.GetBytes(int.Parse(chArray[i].ToString()))[0];
            }
            //bytes = BitConverter.GetBytes(int.Parse(weatherDependency));
            WeatherColumns wd = new WeatherColumns(bytes);

            ModelType modelTp = (ModelType)Enum.Parse(
                                          typeof(ModelType), Type, true);
            switch (modelTp)
            {
                case ModelType.AVERAGE:
                    {
                        am.modelChange(getDailyAvgModelTable().Content, "fit_avg", wd);
                        var returnValue = am.fitSeriesToModel(getDailyPlaceTable().Content, "fit_avg", "dailyDataPlace", wd);

                        return new ContentResult { Content = returnValue.ToJSON(), ContentType = "application/json" };
                    }
                    break;

                case ModelType.SUM:
                    {
                        am.modelChange(getDailySumModelTable().Content, "fit_sum", wd);
                        var returnValue = am.fitSeriesToModel(getDailyPlaceTable().Content, "fit_sum", "dailyDataPlace", wd);

                        return new ContentResult { Content = returnValue.ToJSON(), ContentType = "application/json" };
                    }
                    break;

                default:
                    throw new Exception("Bad type");
            }

            //string place = getDailyPlaceTable().Content;
            //am.fitSeriesToModel(place, "fit_avg", "dailyDataPlace", allTrue);
            //List<Outlier> outliers = new List<Outlier>();
            //outliers = am.findOutliers("dailyDataPlace", allTrue);
        }

        public ContentResult getDailyModelTable(string Type = "SUM", int idDistrict = 62)
        {
            ModelType modelTp = (ModelType)Enum.Parse(
                                          typeof(ModelType), Type, true);
            switch (modelTp)
            {
                case ModelType.AVERAGE:
                    return getDailyAvgModelTable(idDistrict);
                    break;

                case ModelType.SUM:
                    return getDailySumModelTable(idDistrict);
                    break;

                default:
                    throw new Exception("Bad type");
            }
        }

        public ContentResult getDailyAvgModelTable(int idDistrict = 62)
        {
            var bigTable = BigDailyModelTable(idDistrict, "A");
            modelJSON = bigTable.ToJSON();

            return new ContentResult { Content = bigTable.ToJSON(), ContentType = "application/json" };
        }

        public ContentResult getDailySumModelTable(int idDistrict = 62)
        {
            var bigTable = BigDailyModelTable(idDistrict, "S");
            modelJSON = bigTable.ToJSON();

            return new ContentResult { Content = bigTable.ToJSON(), ContentType = "application/json" };
        }

        private IQueryable BigDailyModelTable(int idDistrict, string type)
        {
            var ret = from c in db.ConsuptionModelDailies
                      join pw in db.PlaceWeathers on new { c.IDDistrict } equals new { pw.IDDistrict }
                      join w in db.WeathersDailies on new { n1 = c.IDDate, n3 = pw.IDLocation } equals
                          new { n1 = w.IDDate, n3 = w.IDLocation }
                      where c.Type == type && c.IDDistrict == idDistrict
                      select new
                      {
                          Amount = c.Amount,
                          pw.IDDistrict,
                          pw.IDLocation,
                          IDDate = c.IDDate,
                          MeasurementTime = c.MeasurementTime,
                          Temperature = w.surfaceTemperature,
                          Rain = w.rainfall,
                          WindSpeed = w.windSpeed,
                          Humidity = w.relativeHumidity,
                          Solar = w.solarShine,
                          Pressure = w.atmosphericPressure
                      };
            ret = ret.OrderBy(a => a.IDDistrict).ThenBy(a => a.IDDate);
            return ret;
        }

        private long makeDateId(DateTime? dt)
        {
            if (dt != null)
                return ((DateTime)(dt)).Day + ((DateTime)(dt)).Month * 100 + ((DateTime)(dt)).Year * 10000;
            else
                return 0;
        }
    }

    internal class JsonEvent
    {
        public DateTime dt;
        public long importance;
    }
}