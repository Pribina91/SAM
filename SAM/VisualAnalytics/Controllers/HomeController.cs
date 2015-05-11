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
        private const string MODEL_FITTED = "model_fitted";

        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SAMcontext db = new SAMcontext();
        private AnalyticsModule am = new AnalyticsModule();

        private IOrderedEnumerable<Outlier> outliersList;
        private string modelJSON
        {
            get { return (string)ViewData[MODEL_JSON]; }
            set { ViewData[MODEL_JSON] = value; }
        }
        private string placeJSON
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
            //string modelJson = getDailyAvgModelTable().Content;
            //am.modelChange(modelJson, "fit_avg", allTrue);
            //string placeJson = getDailyPlaceTable().Content;
            //am.fitSeriesToModel(placeJson, "fit_avg", "dailyDataPlace", allTrue);
            //List<Outlier> outliers = new List<Outlier>();
            ////WeatherColumns nonerue = new WeatherColumns(new byte[] { 0, 0, 0, 0, 1, 1 });
            //outliers = am.findOutliers("dailyDataPlace", allTrue);
            //outliers = am.FindLocalProperties(placeJson, modelJson, allTrue, outliers);
            //outliersList = outliers;
            ////
            //WeatherColumns nonerue = new WeatherColumns(new byte[] { 0, 0, 0, 0, 0, 1 });
            //am.FindLocalProperties(place, "fit", allTrue, outliers);

            this.getForecast();
            return View();
        }

        public ContentResult markPoint(int IDDate)
        {
            log.Debug("Point selected:" + IDDate);
            return new ContentResult { Content = MODEL_JSON.ToJSON(), ContentType = "application/json" }; ;
        }

        public ActionResult getBiggestOutliers(string model = "SUM", string weatherDependency = "000000", int IDConsuptionPlace = 1622)
        {
            const string MODEL_FIT = "model_fit";
            const string DATA_PLACE = "data_place";

            WeatherColumns wd = WeatherColumnConversion.stringToWeatherColumn(weatherDependency);

            string modelJson = getDailyModelTable(model).Content;

            am.modelChange(modelJson, MODEL_FIT, wd);
            string placeJson = getDailyPlaceTable(IDConsuptionPlace).Content;

            am.fitSeriesToModel(placeJson, MODEL_FIT, DATA_PLACE, wd);
            List<Outlier> outliers = new List<Outlier>();
            //WeatherColumns nonerue = new WeatherColumns(new byte[] { 0, 0, 0, 0, 1, 1 });
            outliers = am.findOutliers(DATA_PLACE, wd);
            outliers = am.FindLocalProperties(placeJson, modelJson, wd, outliers);
            //outliersList = outliers.OrderBy(o => o.outlierness.Max());
            Outlier emptyOutlier = new Outlier() { IDDate = 20130101, outlierness = new List<double>(4), seriesNumber = -1, tStats = new List<double>(4) };
            for (int i = 0; i < 8; i++)
            {
                emptyOutlier.outlierness.Add(0);
                emptyOutlier.tStats.Add(0);
            }
            foreach (Outlier ou in outliers)
            {
                log.Info(ou.seriesNumber + " :" + ou.outlierness[0]);
            }
            outliers.Insert(0, emptyOutlier);
            return new ContentResult { Content = outliers.ToJSON(), ContentType = "application/json" };
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

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
                    importance = int.Parse(x.eventType)
                })
                    .OrderBy(x => x.IDDate);

            return new ContentResult { Content = ev.ToJSON(), ContentType = "application/json" };
        }

        public ContentResult getForecast(string Type = "SUM", string weatherDependency = "000000", long IDConsuptionPlace = 1622, int lenght = 5, int startIDDate = 20141210)
        {
            //const string MODEL_FIT = "model_fit";
            const string MODEL_FITTED_SHORTER = "model_fit_short";
            const string DATA_SHORT = "data_short";
            WeatherColumns wd = WeatherColumnConversion.stringToWeatherColumn(weatherDependency);
            //WeatherColumns wd = WeatherColumnConversion.stringToWeatherColumn(weatherDependency);

            //string modelJson = getDailyModelTable(Type, endIDDate: startIDDate).Content; // start date of forcast is end date of series
            //am.modelChange(modelJson, MODEL_FIT, wd);

            //string placeJson = getDailyPlaceTable(IDConsuptionPlace, endIDDate: startIDDate).Content;

            //am.fitSeriesToModel(placeJson, MODEL_FIT, DATA_PLACE, wd);

            var consPlace = db.ConsuptionPlaces
                .Join(db.PlaceWeathers
                    , cp => cp.DistrictName, pw => pw.DistrictName, (cp, pw) => new { pw.IDLocation, pw.IDDistrict, cp.IDConsuptionPlace })
                .Single(cp => cp.IDConsuptionPlace.Equals(IDConsuptionPlace));

            string wetherLocationForecastShort = getWeather(consPlace.IDLocation, startIDDate, startIDDate + lenght).Content;
            string modelJsonShort = getDailyModelTable(Type, consPlace.IDDistrict, endIDDate: startIDDate).Content;
            am.modelChange(modelJsonShort, MODEL_FITTED_SHORTER, wd);
            string placeJsonShort = getDailyPlaceTable(IDConsuptionPlace, startIDDate, startIDDate + lenght).Content;
            am.fitSeriesToModel(placeJsonShort, MODEL_FITTED_SHORTER, DATA_SHORT, wd);

            List<double> forecatsList = am.makeForecast(MODEL_FITTED_SHORTER, wetherLocationForecastShort, lenght);

            return new ContentResult { Content = forecatsList.ToJSON(), ContentType = "application/json" };
        }

        //public ContentResult getConsuptions()
        //{
        //    var consuptions = db.Consuptions.Where(x => x.source == 2);

        //    var ev = //consuptions.First();
        //        consuptions
        //        //.Where(x => x.source == 2 && x.MeasurementSequence == 10)
        //        //.Where(x => x.source == 2 )
        //            .GroupBy(ce => ce.IDDate)
        //            .Select(a => new
        //            {
        //                amount = a.Sum(c => c.Amount),
        //                dt = a.Key
        //                //,measureOffset = a.MeasurementSequence
        //            })
        //            .OrderBy(x => x.dt)
        //        //.ThenBy(x => x.measureOffset)
        //        ;

        //    //var jsonData = Json(ev, JsonRequestBehavior.AllowGet);
        //    var json2 = ev.ToList().ToJSON();
        //    return new ContentResult { Content = json2, ContentType = "application/json" };
        //    ;
        //}

        public ContentResult getWeather(int idLocation, int startIDDate, int endIDDate = 20141231)
        {
            var weathers = db.Weathers
                .Where(x => x.IDLocation.Equals(idLocation))
                .Where(x => x.IDDate >= startIDDate && x.IDDate <= endIDDate)
                //.Select(a => new Weather()
                //{
                //    IDLocation = a.IDLocation,
                //    IDDate = a.IDDate,
                //    MeasurementTime = a.MeasurementTime,
                //    surfaceTemperature = a.surfaceTemperature,
                //    rainfall = a.rainfall,
                //    windSpeed = a.windSpeed,
                //    relativeHumidity = a.relativeHumidity,
                //    solarShine = a.solarShine,
                //    atmosphericPressure = a.atmosphericPressure
                //})
                ;

            return new ContentResult { Content = weathers.ToJSON(), ContentType = "application/json" };
        }

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

        public ContentResult getDailyPlaceTable(long IDConsuptionPlace = 1622, int startIDDate = 20140101, int endIDDate = 20150101)
        {
            var bigTable =
                from c in db.ConsuptionsDailies
                join cp in db.ConsuptionPlaces on new { c.IDConsuptionPlace } equals new { cp.IDConsuptionPlace }
                join pw in db.PlaceWeathers on new { cp.DistrictName } equals new { pw.DistrictName }
                join w in db.WeathersDailies on new { n1 = c.IDDate, n3 = pw.IDLocation } equals
                    new { n1 = w.IDDate, n3 = w.IDLocation }
                //where c.IDConsuptionPlace == IDConsuptionPlace //CP from 62 district
                //&& c.IDDate >= startIDDate
                //&& c.IDDate <= endIDDate
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

            bigTable = bigTable
                //.Where(c => c.IDDate >= startIDDate && c.IDDate <= endIDDate)
                .Where(c => c.IDConsuptionPlace.Equals(IDConsuptionPlace))
                .OrderBy(a => a.IDConsuptionPlace).ThenBy(a => a.IDDate);
            //int x = bigTable.Count();
            try
            {
                placeJSON = bigTable.ToJSON();
            }
            catch (Exception EX_NAME)
            {
                Console.WriteLine(EX_NAME);
                log.Error(EX_NAME.Message);
            }

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
            WeatherColumns wd = WeatherColumnConversion.stringToWeatherColumn(weatherDependency);

            ModelType modelTp = (ModelType)Enum.Parse(
                                          typeof(ModelType), Type, true);
            switch (modelTp)
            {
                case ModelType.AVERAGE:
                    {
                        am.modelChange(getDailyModelTable(Type, idDistrict).Content, MODEL_FITTED, wd);
                        var returnValue = am.fitSeriesToModel(getDailyPlaceTable().Content, MODEL_FITTED, "dailyDataPlace", wd);

                        return new ContentResult { Content = returnValue.ToJSON(), ContentType = "application/json" };
                    }
                    break;

                case ModelType.SUM:
                    {
                        am.modelChange(getDailyModelTable(Type, idDistrict).Content, MODEL_FITTED, wd);
                        var returnValue = am.fitSeriesToModel(getDailyPlaceTable().Content, MODEL_FITTED, "dailyDataPlace", wd);

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

        public ContentResult getDailyModelTable(string Type = "SUM", int idDistrict = 62, int startIDDate = 20140101, int endIDDate = 20150101)
        {
            ModelType modelTp = (ModelType)Enum.Parse(
                                          typeof(ModelType), Type, true);
            switch (modelTp)
            {
                case ModelType.AVERAGE:
                    return getDailyAvgModelTable(startIDDate, endIDDate, idDistrict);
                    break;

                case ModelType.SUM:
                    return getDailySumModelTable(startIDDate, endIDDate, idDistrict);
                    break;

                default:
                    throw new Exception("Bad type");
            }
        }

        public ContentResult getDailyAvgModelTable(int startIDDate, int endIDDate, int idDistrict = 62)
        {
            var bigTable = BigDailyModelTable(idDistrict, "A", startIDDate, endIDDate);
            modelJSON = bigTable.ToJSON();

            return new ContentResult { Content = bigTable.ToJSON(), ContentType = "application/json" };
        }

        public ContentResult getDailySumModelTable(int startIDDate, int endIDDate, int idDistrict = 62)
        {
            var bigTable = BigDailyModelTable(idDistrict, "S", startIDDate, endIDDate);
            modelJSON = bigTable.ToJSON();

            return new ContentResult { Content = bigTable.ToJSON(), ContentType = "application/json" };
        }

        private IQueryable BigDailyModelTable(int idDistrict, string type, int startIDDate, int endIDDate)
        {
            var ret = from c in db.ConsuptionModelDailies
                      join pw in db.PlaceWeathers on new { c.IDDistrict } equals new { pw.IDDistrict }
                      join w in db.WeathersDailies on new { n1 = c.IDDate, n3 = pw.IDLocation } equals
                          new { n1 = w.IDDate, n3 = w.IDLocation }
                      where c.Type == type
                        && c.IDDistrict.Equals(idDistrict)
                      //&& c.IDDate >= startIDDate
                      //&& c.IDDate <= endIDDate
                      select new
                      {
                          Amount = c.Amount,
                          pw.DistrictName,
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
            ret = ret
                //.Where(c => c.IDDate >= startIDDate && c.IDDate <= endIDDate)
                .OrderBy(a => a.IDDistrict).ThenBy(a => a.IDDate);
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