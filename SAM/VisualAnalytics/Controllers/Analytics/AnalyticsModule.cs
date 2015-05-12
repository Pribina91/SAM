using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;
using RDotNet;
using VisualAnalytics.Controllers.Helpers;
using VisualAnalytics.Controllers.Helpers.Arima;
using VisualAnalytics.Models;

namespace VisualAnalytics.Controllers.Analytics
{
    public class AnalyticsModule
    {
        private const string WEATHERMATRIX = "weatherMatrix";
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private WeatherColumns modelWeatherColumns;

        private REngine rEngine;

        public void modelChange(string JSONmodelWithWeather, string modelName, WeatherColumns wc)
        {
            this.Init();
            //
            string modelData = "modelData";
            string evaluate = string.Format("{0} <-fromJSON(\'{1}\')", modelData, JSONmodelWithWeather);

            //log.Debug("evaluateString:" + evaluate);
            rEngine.Evaluate(evaluate);

            string weatherModel = "modelData";
            //evaluate = string.Format("{0} <-fromJSON(\'{1}\')", weatherModel, JSONweather);
            //log.Debug("evaluateString:" + evaluate);
            //rEngine.Evaluate(evaluate);

            CreateWeatherMatrix(modelData, wc);

            //log.Debug(engine.Evaluate("data"));
            //engine.Evaluate("data <- predata[,1]");
            evaluate = string.Format(modelName + "<- auto.arima({0}$Amount,xreg={1})", modelData, WEATHERMATRIX);
            SymbolicExpression model;
            try
            {
                model = rEngine.Evaluate(evaluate);
            }
            catch (Exception ex)
            {
                throw new Exception("ARIMA error");
            }

            //foreach (var item in model.AsList())
            //{
            //    log.Debug(item.ToString());
            //}

            var coef = model.AsList()["coef"].AsList();

            int lengthData = rEngine.Evaluate(modelData + "$Amount").AsNumeric().Length;
            double[] dataSeries = new double[lengthData];
            double[] errorSeries = new double[lengthData];

            model.AsList()["x"].AsNumeric().CopyTo(dataSeries, lengthData);
            model.AsList()["residuals"].AsNumeric().CopyTo(errorSeries, lengthData);
            modelWeatherColumns = wc;
            //residuals
        }

        public void helloworld()
        {
            REngine.SetEnvironmentVariables(); // <-- May be omitted; the next line would call it.

            REngine engine = REngine.GetInstance();

            // A somewhat contrived but customary Hello World:
            CharacterVector charVec = engine.CreateCharacterVector(new[] { "Hello, R world!, .NET speaking" });
            engine.SetSymbol("greetings", charVec);
            engine.Evaluate("str(greetings)"); // print out in the console
            string[] a = engine.Evaluate("'Hi there .NET, from the R engine'").AsCharacter().ToArray();

            log.Debug("R answered: '" + a[0].ToString() + "'");
            log.Debug("Press any key to exit the program");

            engine.Dispose();
        }

        public void Test(HomeController controller)
        {
            //require R 2.15, package forecast on R
            //var envPath = Environment.GetEnvironmentVariable("PATH");
            //var rBinPath = GetRPath(); //C:\Program Files\R\R-2.15.1\bin\i386

            //var BinPath = GetWinRegistryPath();
            //var envPath = Environment.GetEnvironmentVariable("PATH");
            // this statement not work under iis
            //Environment.SetEnvironmentVariable("PATH", envPath + Path.PathSeparator + BinPath);

            REngine engine = REngine.GetInstance();
            engine.Initialize();

            string currentPath = @"C:\Workspace\SAM\SAM\VisualAnalytics\bin"; //Directory.GetCurrentDirectory();
            string dataPath = currentPath + @"\data\paper.dat";
            string readDataCommand = string.Format("predata <- read.table(\"{0}\", header=FALSE)", dataPath).Replace('\\', '/');

            //engine.Evaluate("install.packages(\"forecast\")");
            engine.Evaluate("library(jsonlite)");
            engine.Evaluate("library(forecast)");
            String JsonData = @"http://localhost:25910/Home/getBigTable";//controller.getBigTable().Content;

            log.Debug("JSON:" + JsonData);
            string evaluate = string.Format("data <-fromJSON(\"{0}\")", JsonData);
            log.Debug("evaluateString:" + evaluate);
            engine.Evaluate(evaluate);
            //log.Debug(engine.Evaluate("data"));
            //engine.Evaluate("data <- predata[,1]");
            var model = engine.Evaluate("fit <- auto.arima(data$Amount)").AsList();
            foreach (var item in model)
            {
                log.Debug(item.ToString());
            }

            var coef = model["coef"].AsList();

            int lengthData = engine.Evaluate("data$Amount").AsNumeric().Length;
            double[] dataSeries = new double[lengthData];
            double[] errorSeries = new double[lengthData];

            engine.Evaluate("data$Amount").AsNumeric().CopyTo(dataSeries, lengthData);
            model["residuals"].AsNumeric().CopyTo(errorSeries, lengthData);
            //residuals

            int arOrder = model["arma"].AsInteger().ElementAt(0);
            int maOrder = model["arma"].AsInteger().ElementAt(1);
            int arSeasonOrder = model["arma"].AsInteger().ElementAt(2);
            int maSeasonOrder = model["arma"].AsInteger().ElementAt(3);
            int seasonOrder = model["arma"].AsInteger().ElementAt(4);
            int diffOrder = model["arma"].AsInteger().ElementAt(5);
            int diffSeasonOrder = model["arma"].AsInteger().ElementAt(6);

            double[] arCoef = new double[arOrder];
            double[] maCoef = new double[maOrder];
            double[] arSeasonCoef = new double[arSeasonOrder];
            double[] maSeasonCoef = new double[maSeasonOrder];
            double intercept = 0;

            int n = model["coef"].AsNumeric().Length;
            int start = 0;
            model["coef"].AsNumeric().CopyTo(arCoef, arOrder, start, 0);
            start += arOrder;
            model["coef"].AsNumeric().CopyTo(maCoef, maOrder, start, 0);
            start += maOrder;
            model["coef"].AsNumeric().CopyTo(arSeasonCoef, arSeasonOrder, start, 0);
            start += arSeasonOrder;
            model["coef"].AsNumeric().CopyTo(maSeasonCoef, maSeasonOrder, start, 0);
            start += maSeasonOrder;
            if (n > start)
            {
                intercept = model["coef"].AsNumeric().ElementAt(start);
            }

            ArimaModel arimaModel = new ArimaModel(arCoef, maCoef, arSeasonCoef, maSeasonCoef, intercept, (uint)seasonOrder, (uint)diffOrder, (uint)diffSeasonOrder);
            Polynomial arModel = arimaModel.ComputeARModel();
            Polynomial maModel = arimaModel.ComputeMAModel();
            double interceptModel = arimaModel.ComputeIntercept();

            double test = arimaModel.ComputeValue(dataSeries, errorSeries, dataSeries.Length);

            log.Info("Forecast");
            log.Info(test);
            log.Info("Model");
            log.Info(interceptModel);
            log.Info("Ar");
            log.Info(arModel.ToString());
            log.Info("Ma");
            log.Info(maModel.ToString());
            //Console.ReadLine();
        }

        public List<Consuption> fitSeriesToModel(string JSONdataWithWeather, string modelName, string fittedDataName, WeatherColumns weatherColumns)
        {
            string evaluate;
            const string fittingDataName = "dataName";

            evaluate = string.Format("{0} <-fromJSON(\'{1}\')", fittingDataName, JSONdataWithWeather);
            var sourceData = rEngine.Evaluate(evaluate);

            CreateWeatherMatrix(fittingDataName, weatherColumns);

            evaluate = string.Format("{0} <- forecast::Arima({1}$Amount,xreg = {2},model={3})", fittedDataName, fittingDataName, WEATHERMATRIX, modelName);
            //evaluate = string.Format("{0} <- forecast::Arima({1}$Amount,xreg = {2})", fittedDataName, fittingDataName, WEATHERMATRIX, modelName);
            var result = rEngine.Evaluate(evaluate);

            GenericVector resultList = result.AsList();
            var fittedValuesList = resultList["residuals"].AsNumeric().ToList();

            var sourceDates = rEngine.Evaluate("sourceDates <- " + fittingDataName + "$IDDate").AsList();
            var sourceAmounts =
                rEngine.Evaluate("sourceDates <- " + fittingDataName + "$Amount").AsList();
            List<Consuption> retList = new List<Consuption>();
            for (int i = 0; i < fittedValuesList.Count; i++)
            {
                Consuption c = new Consuption();
                c.Amount = (float)fittedValuesList[i] + (float)sourceAmounts.AsNumeric().ElementAt(i);
                c.IDDate = sourceDates.AsInteger().ElementAt(i);
                retList.Add(c);
            }
            return retList;
            //List<double> returnList = result.AsList();
        }

        public List<Outlier> findOutliers(string fittedDataName, WeatherColumns wc)
        {
            string evaluate;

            evaluate = string.Format("resid <- residuals({0})", fittedDataName);
            var resid = rEngine.Evaluate(evaluate);

            evaluate = string.Format("pars <- coefs2poly({0})", fittedDataName);
            var pars = rEngine.Evaluate(evaluate);

            evaluate = string.Format("outliers <- locate.outliers(resid, pars)");
            var outliers = rEngine.Evaluate(evaluate).AsList();

            List<Outlier> retList = new List<Outlier>();

            for (int i = 0; i < outliers[0].AsList().Length; i++)
            {
                Outlier o = new Outlier();
                o.seriesNumber = (int)outliers[1].AsVector()[i];
                var date = rEngine.Evaluate(fittedDataName).AsList();
                // o.IDDate = date.
                o.weatherDependency.Add(wc);
                o.outlierness.Add((double)outliers[2].AsVector()[i]);
                o.tStats.Add((double)outliers[3].AsVector()[i]);

                retList.Add(o);
                //outliers[0].AsVector().ToArray()[i];//type
            }

            //foreach (var outlierColumn in outliers)
            //{
            //    var outVector = outlierColumn.AsDataFrame();
            //    var outVector2 = outlierColumn.AsList();
            //    var outVector3 = outlierColumn.AsVector();
            //    var outVector4 = outlierColumn.AsVector();
            //}

            return retList;
        }

        public List<Outlier> FindLocalProperties(string JSONdataWithWeather, string modelName, WeatherColumns weatherColumns, List<Outlier> outlierList)
        {
            if (outlierList == null)
            {
                throw new Exception("outlierList not defined");
            }

            if (weatherColumns.temperature == true)
            {
                WeatherColumns wc = new WeatherColumns() { temperature = true };
                outlierList = ExploreDefinedProperty(JSONdataWithWeather, modelName, outlierList, wc);
            }
            if (weatherColumns.solar == true)
            {
                WeatherColumns wc = new WeatherColumns() { solar = true };
                outlierList = ExploreDefinedProperty(JSONdataWithWeather, modelName, outlierList, wc);
            }
            if (weatherColumns.pressure == true)
            {
                WeatherColumns wc = new WeatherColumns() { pressure = true };
                outlierList = ExploreDefinedProperty(JSONdataWithWeather, modelName, outlierList, wc);
            }
            if (weatherColumns.rain == true)
            {
                WeatherColumns wc = new WeatherColumns() { rain = true };
                outlierList = ExploreDefinedProperty(JSONdataWithWeather, modelName, outlierList, wc);
            }
            if (weatherColumns.windSpeed == true)
            {
                WeatherColumns wc = new WeatherColumns() { windSpeed = true };
                outlierList = ExploreDefinedProperty(JSONdataWithWeather, modelName, outlierList, wc);
            }
            if (weatherColumns.humitdity == true)
            {
                WeatherColumns wc = new WeatherColumns() { humitdity = true };
                outlierList = ExploreDefinedProperty(JSONdataWithWeather, modelName, outlierList, wc);
            }
            //noneWeatherDependency
            {
                WeatherColumns wc = new WeatherColumns();
                if (!outlierList.Exists(o => o.weatherDependency.Contains(wc)))
                {
                    outlierList = ExploreDefinedProperty(JSONdataWithWeather, modelName, outlierList, wc);
                }
            }

            return outlierList;
        }

        public List<double> makeForecast(string fittedDataName, string JSONWeatherForecastShort, int forecastedNumber = 5, string forecasteListName = "forecastedValues")
        {
            //var sourceAmounts = rEngine.Evaluate("sourceDates <- " + fittingDataName + "$Amount").AsList();

            //List<Consuption> retList = new List<Consuption>();
            //for (int i = 0; i < fittedValuesList.Count; i++)
            //{
            //    Consuption c = new Consuption();
            //    c.Amount = (float)fittedValuesList[i] + (float)sourceAmounts.AsNumeric().ElementAt(i);
            //    c.IDDate = sourceDates.AsInteger().ElementAt(i);
            //    retList.Add(c);
            //}
            //return retList;
            const string fittingDataName = "dataName_short";
            string evaluate;

            //evaluate = string.Format("{0} <-fromJSON(\'{1}\')", weatherModel, JSONweather);
            //log.Debug("evaluateString:" + evaluate);
            //rEngine.Evaluate(evaluate);

            //evaluate = string.Format("{0} <-fromJSON(\'{1}\')", fittingDataName, JSONplaceDataShort);

            if (modelWeatherColumns.AllFalse())
            {
                evaluate = string.Format("{0} <- forecast.Arima({1}, h={2})", forecasteListName, fittedDataName, forecastedNumber);
            }
            else
            {
                const string weatherDataShort = "weather_Data_short";
                evaluate = string.Format("{0} <-fromJSON(\'{1}\')", weatherDataShort, JSONWeatherForecastShort);

                log.Debug(evaluate);
                rEngine.Evaluate(evaluate);
                CreateWeatherMatrix(weatherDataShort, modelWeatherColumns);
                evaluate = string.Format("{0} <- forecast.Arima({1}, xreg={2})", forecasteListName, fittedDataName, WEATHERMATRIX);
            }

            //forecast
            //log.Debug(evaluate);
            var result = rEngine.Evaluate(evaluate).AsList();

            var means = result["mean"].AsNumeric().ToList();

            List<double> forecastList = means;

            return forecastList;
        }

        public AccuracyResult compareResults(string JSONmeasuredValues, string forecastResultName)
        {
            const string MEASURED_DATA = "measured_data";

            string evaluate;
            evaluate = string.Format("{0} <-fromJSON(\'{1}\')", MEASURED_DATA, JSONmeasuredValues);
            //log.Debug(evaluate);
            rEngine.Evaluate(evaluate);

            evaluate = string.Format("a <- accuracy(f={0}, x={1}$Amount)", forecastResultName, MEASURED_DATA);
            //log.Debug(evaluate);
            var accuracyResult = rEngine.Evaluate(evaluate).AsVector();
            AccuracyResult ar = new AccuracyResult();

            var train = accuracyResult;
            int i = 0;
            ar.ME = train.AsNumeric().ElementAt(i++);
            ar.RMSE = train.AsNumeric().ElementAt(i++);
            ar.MAE = train.AsNumeric().ElementAt(i++);
            ar.MPE = train.AsNumeric().ElementAt(i++);
            ar.MAPE = train.AsNumeric().ElementAt(i++);
            ar.MASE = train.AsNumeric().ElementAt(i++);
            ar.ACF1 = train.AsNumeric().ElementAt(i++);

            ar.testME = train.AsNumeric().ElementAt(i++);
            ar.testRMSE = train.AsNumeric().ElementAt(i++);
            ar.testMAE = train.AsNumeric().ElementAt(i++);
            ar.testMPE = train.AsNumeric().ElementAt(i++);
            ar.testMAPE = train.AsNumeric().ElementAt(i++);
            ar.testMASE = train.AsNumeric().ElementAt(i++);
            double t = train.AsNumeric().ElementAt(i++);
            //ME     RMSE      MAE       MPE     MAPE     MASE       ACF1

            return ar;
        }

        private static string GetRPath()
        {
            RegistryKey rCore = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\R-core");
            if (rCore == null)
            {
                return string.Empty;
            }
            bool is64Bit = IntPtr.Size == 8;
            RegistryKey r = rCore.OpenSubKey(is64Bit ? "R64" : "R");
            if (r == null)
            {
                return string.Empty;
            }
            Version currentVersion = new Version((string)r.GetValue("Current Version"));
            return (string)r.GetValue("InstallPath") + @"\bin\i386";
        }

        private List<Outlier> ExploreDefinedProperty(string JSONdataWithWeather, string JSONmodelWithWeather, List<Outlier> outlierList, WeatherColumns wc)
        {
            string fittedDataName = "feature_data_" + wc.ToString(); ;
            string fittingModelName = "feature_model_" + wc.ToString();

            modelChange(JSONmodelWithWeather, fittingModelName, wc);

            fitSeriesToModel(JSONdataWithWeather, fittingModelName, fittedDataName, wc);

            //HashSet<int> oulierSet = new HashSet<int>();

            List<Outlier> returnList = findOutliers(fittedDataName, wc);
            foreach (Outlier o in returnList)
            {
                if (!outlierList.Exists(x => x.seriesNumber.Equals(o.seriesNumber)))
                {
                    continue;
                }
                outlierList.Single(x => x.seriesNumber.Equals(o.seriesNumber))
                    .Add(o.weatherDependency.First(), o.outlierness.First(), o.tStats.First());
            }
            return outlierList;
        }

        /// <summary>
        /// Creates the weather matrix. One bool neccessary!
        /// </summary>
        /// <param name="weatherSource">The weather source evaluated in Engine</param>
        /// <param name="wc">The wc.</param>
        /// <exception cref="System.Exception">No column chosen</exception>
        private void CreateWeatherMatrix(string weatherSource, WeatherColumns wc)
        {
            string weatherMatrixString = WEATHERMATRIX + " <- matrix(c(";
            int cols = 0;
            if (wc.pressure)
            {
                weatherMatrixString += weatherSource + "$Pressure,";
                cols++;
            }
            if (wc.rain)
            {
                weatherMatrixString += weatherSource + "$Rain,";
                cols++;
            }
            if (wc.windSpeed)
            {
                weatherMatrixString += weatherSource + "$WindSpeed,";
                cols++;
            }
            if (wc.temperature)
            {
                weatherMatrixString += weatherSource + "$Temperature,";
                cols++;
            }
            if (wc.solar)
            {
                weatherMatrixString += weatherSource + "$Solar,";
                cols++;
            }
            if (wc.humitdity)
            {
                weatherMatrixString += weatherSource + "$Humidity,";
                cols++;
            }
            if (cols == 0)
            {
                log.Debug("weatherMatrixString: NULL");
                rEngine.Evaluate(WEATHERMATRIX + "<-NULL");
                return;
            }

            weatherMatrixString = weatherMatrixString.Remove(weatherMatrixString.Length - 1, 1);
            weatherMatrixString += "),";//c end

            weatherMatrixString += "ncol=" + cols.ToString() + ")";//matrix end

            log.Debug("weatherMatrixString:" + weatherMatrixString);
            rEngine.Evaluate(weatherMatrixString);
        }

        /// <summary>
        /// First run of REngine
        /// </summary>
        private void Init()
        {
            //var path = System.Environment.GetEnvironmentVariable("Path");
            //path = @"c:\apps\R\bin;c:\apps\R\bin\x64;c:\Users\Andrej\Documents\R\win-library\3.1" + path;

            //System.Environment.SetEnvironmentVariable("Path", path);
            //var path = System.Environment.GetEnvironmentVariable("Path");
            //path = @"c:\apps\R\bin;c:\apps\R\bin\x64;" + path;

            //System.Environment.SetEnvironmentVariable("Path", path);

            //REngine.(@"C:\APPS\R\bin\x64");
            //using (REngine engine = REngine.CreateInstance("RDotNet", new[] { @"R_HOME=c:\APPS\R", @"R_USER=c:\APPS\R" }))

            rEngine = REngine.GetInstance();
            rEngine.Initialize();
            //rEngine.Evaluate("install.packages(\"forecast\",dependencies = TRUE)");
            //rEngine.Evaluate("install.packages(\"jsonlite\",dependencies = TRUE)");
            //rEngine.Evaluate("install.packages('httr')");
            //rEngine.Dispose();
            rEngine.Evaluate("library(forecast)");

            //rEngine.Evaluate("install.packages(\"jsonlite\")");

            rEngine.Evaluate("library(jsonlite)");
            rEngine.Evaluate("library(tsoutliers)");
        }
    }
}