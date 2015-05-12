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

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        #region evaluationTesting

        private string showGlobalWeather;
        private int globalLocation;
        private ConsPlace consPlace;
        private string globalModel;

        /// <summary>
        /// Tests the evaluation.
        /// </summary>
        private void TestEvaluation()
        {
            var cps = db.ConsuptionPlaces.Where(x => x.DistrictName.Equals("Snina"));
            showGlobalWeather = getWeather(1, 20141210, 20141210 + 5).Content;

            foreach (ConsuptionPlace cp in cps)
            {
                consPlace = db.ConsuptionPlaces
                .Join(db.PlaceWeathers
                    , cpx => cpx.DistrictName, pw => pw.DistrictName, (cpx, pw) => new ConsPlace() { IDLocation = pw.IDLocation, IDDistrict = pw.IDDistrict, IDConsuptionPlace = cpx.IDConsuptionPlace })
                .Single(cpx => cpx.IDConsuptionPlace.Equals(cp.IDConsuptionPlace));

                globalModel = getDailyModelTable("SUM", consPlace.IDDistrict, endIDDate: 20141210).Content;

                getForecast("SUM", "000000", cp.IDConsuptionPlace);
                getForecast("SUM", "100000", cp.IDConsuptionPlace);
                getForecast("SUM", "010000", cp.IDConsuptionPlace);
                getForecast("SUM", "001000", cp.IDConsuptionPlace);
                getForecast("SUM", "000100", cp.IDConsuptionPlace);
                getForecast("SUM", "000010", cp.IDConsuptionPlace);
                getForecast("SUM", "000001", cp.IDConsuptionPlace);
                getForecast("SUM", "111111", cp.IDConsuptionPlace);
                globalModel = getDailyModelTable("AVERAGE", consPlace.IDDistrict, endIDDate: 20141210).Content;
                getForecast("AVERAGE", "000000", cp.IDConsuptionPlace);
                getForecast("AVERAGE", "111111", cp.IDConsuptionPlace);
                getForecast("AVERAGE", "100000", cp.IDConsuptionPlace);
                getForecast("AVERAGE", "010000", cp.IDConsuptionPlace);
                getForecast("AVERAGE", "001000", cp.IDConsuptionPlace);
                getForecast("AVERAGE", "000100", cp.IDConsuptionPlace);
                getForecast("AVERAGE", "000010", cp.IDConsuptionPlace);
                getForecast("AVERAGE", "000001", cp.IDConsuptionPlace);
            }

            cps = db.ConsuptionPlaces.Where(x => x.DistrictName.Equals("Bratislava"));
            showGlobalWeather = getWeather(11, 20141210, 20141210 + 5).Content;

            foreach (ConsuptionPlace cp in cps)
            {
                consPlace = db.ConsuptionPlaces
                .Join(db.PlaceWeathers
                    , cpx => cpx.DistrictName, pw => pw.DistrictName, (cpx, pw) => new ConsPlace() { IDLocation = pw.IDLocation, IDDistrict = pw.IDDistrict, IDConsuptionPlace = cpx.IDConsuptionPlace })
                .Single(cpx => cpx.IDConsuptionPlace.Equals(cp.IDConsuptionPlace));

                globalModel = getDailyModelTable("SUM", consPlace.IDDistrict, endIDDate: 20141210).Content;
                getForecast("SUM", "000000", cp.IDConsuptionPlace);
                getForecast("SUM", "100000", cp.IDConsuptionPlace);
                getForecast("SUM", "010000", cp.IDConsuptionPlace);
                getForecast("SUM", "001000", cp.IDConsuptionPlace);
                getForecast("SUM", "000100", cp.IDConsuptionPlace);
                getForecast("SUM", "000010", cp.IDConsuptionPlace);
                getForecast("SUM", "000001", cp.IDConsuptionPlace);
                getForecast("SUM", "111111", cp.IDConsuptionPlace);
                globalModel = getDailyModelTable("AVERAGE", consPlace.IDDistrict, endIDDate: 20141210).Content;
                getForecast("AVERAGE", "000000", cp.IDConsuptionPlace);
                getForecast("AVERAGE", "111111", cp.IDConsuptionPlace);
                getForecast("AVERAGE", "100000", cp.IDConsuptionPlace);
                getForecast("AVERAGE", "010000", cp.IDConsuptionPlace);
                getForecast("AVERAGE", "001000", cp.IDConsuptionPlace);
                getForecast("AVERAGE", "000100", cp.IDConsuptionPlace);
                getForecast("AVERAGE", "000010", cp.IDConsuptionPlace);
                getForecast("AVERAGE", "000001", cp.IDConsuptionPlace);
            }
            cps = db.ConsuptionPlaces.Where(x => x.DistrictName.Equals("Martin"));
            foreach (ConsuptionPlace cp in cps)
            {
                getForecast("SUM", "000000", cp.IDConsuptionPlace);
                getForecast("SUM", "100000", cp.IDConsuptionPlace);
                getForecast("SUM", "010000", cp.IDConsuptionPlace);
                getForecast("SUM", "001000", cp.IDConsuptionPlace);
                getForecast("SUM", "000100", cp.IDConsuptionPlace);
                getForecast("SUM", "000010", cp.IDConsuptionPlace);
                getForecast("SUM", "000001", cp.IDConsuptionPlace);
                getForecast("SUM", "111111", cp.IDConsuptionPlace);
                getForecast("AVERAGE", "000000", cp.IDConsuptionPlace);
                getForecast("AVERAGE", "111111", cp.IDConsuptionPlace);
                getForecast("AVERAGE", "100000", cp.IDConsuptionPlace);
                getForecast("AVERAGE", "010000", cp.IDConsuptionPlace);
                getForecast("AVERAGE", "001000", cp.IDConsuptionPlace);
                getForecast("AVERAGE", "000100", cp.IDConsuptionPlace);
                getForecast("AVERAGE", "000010", cp.IDConsuptionPlace);
                getForecast("AVERAGE", "000001", cp.IDConsuptionPlace);
            }
        }

        #endregion evaluationTesting

        /// <summary>
        /// Marks the point.
        /// </summary>
        /// <param name="IDDate">The identifier date.</param>
        /// <returns></returns>
        public ContentResult markPoint(int IDDate)
        {
            log.Debug("Point selected:" + IDDate);
            return new ContentResult { Content = MODEL_JSON.ToJSON(), ContentType = "application/json" }; ;
        }

        /// <summary>
        /// Gets the biggest outliers.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="weatherDependency">The weather dependency.</param>
        /// <param name="IDConsuptionPlace">The identifier consuption place.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Abouts this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the forecast.
        /// </summary>
        /// <param name="Type">The type.</param>
        /// <param name="weatherDependency">The weather dependency.</param>
        /// <param name="IDConsuptionPlace">The identifier consuption place.</param>
        /// <param name="lenght">The lenght.</param>
        /// <param name="startIDDate">The start identifier date.</param>
        /// <returns></returns>
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
            try
            {
                string wetherLocationForecastShort = showGlobalWeather;
                string modelJsonShort = globalModel;
                am.modelChange(modelJsonShort, MODEL_FITTED_SHORTER, wd);
                string placeJsonShort = getDailyPlaceTable(IDConsuptionPlace, endIDDate: startIDDate).Content;
                am.fitSeriesToModel(placeJsonShort, MODEL_FITTED_SHORTER, DATA_SHORT, wd);

                string FORECAST_RESULT_VARIABLE = "forecast_result_variable";
                List<double> forecatsList = am.makeForecast(DATA_SHORT, wetherLocationForecastShort, lenght, FORECAST_RESULT_VARIABLE);
                //measured values
                string measuredValues = getDailyPlaceTable(consPlace.IDConsuptionPlace, startIDDate, startIDDate + lenght).Content;

                Forecast f = new Forecast();
                f.Means = forecatsList;
                f.Accuracy = am.compareResults(measuredValues, FORECAST_RESULT_VARIABLE);
                log.Info(string.Format("cp:{0}\tidDistrict:{1}\ttype:{3}\tweather:{4}\taccuracy:{2}", IDConsuptionPlace, consPlace.IDDistrict, f.Accuracy, Type, weatherDependency));
                return new ContentResult { Content = f.ToJSON(), ContentType = "application/json" };
            }
            catch (Exception EX_NAME)
            {
                log.Info(string.Format("cp:{0}\tidDistrict:{1}\ttype:{3}\tweather:{4}\taccuracy:Error", IDConsuptionPlace, consPlace.IDDistrict, "", Type, weatherDependency));
                return null;
            }
        }

        /// <summary>
        /// Gets the weather.
        /// </summary>
        /// <param name="idLocation">The identifier location.</param>
        /// <param name="startIDDate">The start identifier date.</param>
        /// <param name="endIDDate">The end identifier date.</param>
        /// <returns></returns>
        public ContentResult getWeather(int idLocation, int startIDDate, int endIDDate = 20141231)
        {
            var weathers = db.WeathersDailies
                .Where(x => x.IDLocation.Equals(idLocation))
                .Where(x => x.IDDate >= startIDDate && x.IDDate <= endIDDate)
                .Select(w => new
                {
                    IDLocation = w.IDLocation,
                    IDDate = w.IDDate,
                    MeasurementTime = w.MeasurementTime,
                    Temperature = w.surfaceTemperature,
                    Rain = w.rainfall,
                    WindSpeed = w.windSpeed,
                    Humidity = w.relativeHumidity,
                    Solar = w.solarShine,
                    Pressure = w.atmosphericPressure
                })
                ;

            return new ContentResult { Content = weathers.ToJSON(), ContentType = "application/json" };
        }

        /// <summary>
        /// Gets the daily place table.
        /// </summary>
        /// <param name="IDConsuptionPlace">The identifier consuption place.</param>
        /// <param name="startIDDate">The start identifier date.</param>
        /// <param name="endIDDate">The end identifier date.</param>
        /// <returns></returns>
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

            return new ContentResult { Content = bigTable.ToJSON(), ContentType = "application/json" };
        }

        /// <summary>
        /// Gets the fitted model table.
        /// </summary>
        /// <param name="Type">The type.</param>
        /// <param name="idDistrict">The identifier district.</param>
        /// <param name="weatherDependency">The weather dependency.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Bad type</exception>
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
        }

        /// <summary>
        /// Gets the daily model table.
        /// </summary>
        /// <param name="Type">The type.</param>
        /// <param name="idDistrict">The identifier district.</param>
        /// <param name="startIDDate">The start identifier date.</param>
        /// <param name="endIDDate">The end identifier date.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Bad type</exception>
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

        /// <summary>
        /// Gets the daily average model table.
        /// </summary>
        /// <param name="startIDDate">The start identifier date.</param>
        /// <param name="endIDDate">The end identifier date.</param>
        /// <param name="idDistrict">The identifier district.</param>
        /// <returns></returns>
        public ContentResult getDailyAvgModelTable(int startIDDate, int endIDDate, int idDistrict = 62)
        {
            var bigTable = BigDailyModelTable(idDistrict, "A", startIDDate, endIDDate);
            modelJSON = bigTable.ToJSON();

            return new ContentResult { Content = bigTable.ToJSON(), ContentType = "application/json" };
        }

        /// <summary>
        /// Gets the daily sum model table.
        /// </summary>
        /// <param name="startIDDate">The start identifier date.</param>
        /// <param name="endIDDate">The end identifier date.</param>
        /// <param name="idDistrict">The identifier district.</param>
        /// <returns></returns>
        public ContentResult getDailySumModelTable(int startIDDate, int endIDDate, int idDistrict = 62)
        {
            var bigTable = BigDailyModelTable(idDistrict, "S", startIDDate, endIDDate);
            modelJSON = bigTable.ToJSON();

            return new ContentResult { Content = bigTable.ToJSON(), ContentType = "application/json" };
        }

        /// <summary>
        /// Bigs the daily model table.
        /// </summary>
        /// <param name="idDistrict">The identifier district.</param>
        /// <param name="type">The type.</param>
        /// <param name="startIDDate">The start identifier date.</param>
        /// <param name="endIDDate">The end identifier date.</param>
        /// <returns></returns>
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

    internal class ConsPlace
    {
        public int IDLocation;
        public int IDDistrict;
        public long IDConsuptionPlace;
    }
}