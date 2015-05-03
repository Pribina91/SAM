using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;
using RDotNet;
using VisualAnalytics.Controllers.Helpers;
using VisualAnalytics.Controllers.Helpers.Arima;

namespace VisualAnalytics.Controllers.Analytics
{
    public class AnalyticsModule
    {
        private const string WEATHERMATRIX = "weatherMatrix";
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private REngine rEngine;

        public void modelChange(string JSONmodelWithWeather, string modelName)
        {
            this.Init();
            //
            string modelData = "modelData";
            string evaluate = string.Format("{0} <-fromJSON(\'{1}\')", modelData, JSONmodelWithWeather);

            log.Debug("evaluateString:" + evaluate);
            rEngine.Evaluate(evaluate);

            string weatherModel = "modelData";
            //evaluate = string.Format("{0} <-fromJSON(\'{1}\')", weatherModel, JSONweather);
            //log.Debug("evaluateString:" + evaluate);
            //rEngine.Evaluate(evaluate);

            CreateWeatherMatrix(modelData, true, true, true, true, true, true);

            //log.Debug(engine.Evaluate("data"));
            //engine.Evaluate("data <- predata[,1]");
            evaluate = string.Format(modelName + "<- auto.arima({0}$Amount,xreg={1})", modelData, WEATHERMATRIX);

            var model = rEngine.Evaluate(evaluate);

            foreach (var item in model.AsList())
            {
                log.Debug(item.ToString());
            }

            var coef = model.AsList()["coef"].AsList();

            int lengthData = rEngine.Evaluate(modelData + "$Amount").AsNumeric().Length;
            double[] dataSeries = new double[lengthData];
            double[] errorSeries = new double[lengthData];

            model.AsList()["x"].AsNumeric().CopyTo(dataSeries, lengthData);
            model.AsList()["residuals"].AsNumeric().CopyTo(errorSeries, lengthData);
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

        public void fitSeriesToModel(string JSONdataWithWeather, string modelName, string fittedDataName)
        {
            string evaluate;
            const string fittingDataName = "dataName";

            evaluate = string.Format("{0} <-fromJSON(\'{1}\')", fittingDataName, JSONdataWithWeather);
            rEngine.Evaluate(evaluate);

            CreateWeatherMatrix(fittingDataName, true, true, true, true, true, true);

            evaluate = string.Format("{0} <- Arima({1}$Amount,model={2},xreg = {3})", fittedDataName, fittingDataName, modelName, WEATHERMATRIX);
            rEngine.Evaluate(evaluate);
        }

        public List<> findOutliers(string fittedDataName)
        {
            string evaluate;

            evaluate = string.Format("resid <- residuals({0})", fittedDataName);
            var resid = rEngine.Evaluate(evaluate);

            evaluate = string.Format("pars <- coefs2poly({0})", fittedDataName);
            var pars = rEngine.Evaluate(evaluate);

            evaluate = string.Format("outliers <- locate.outliers(resid, pars)");
            var outliers = rEngine.Evaluate(evaluate);
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

        /// <summary>
        /// Creates the weather matrix. One bool neccessary!
        /// </summary>
        /// <param name="weatherSource">The weather source evaluated in Engine</param>
        /// <param name="presure">if set to <c>true</c> [presure].</param>
        /// <param name="rain">if set to <c>true</c> [rain].</param>
        /// <param name="windSpeed">if set to <c>true</c> [wind speed].</param>
        /// <param name="temperature">if set to <c>true</c> [temperature].</param>
        /// <param name="solar">if set to <c>true</c> [solar].</param>
        /// <param name="humitdity">if set to <c>true</c> [humitdity].</param>
        private void CreateWeatherMatrix(string weatherSource, bool presure, bool rain, bool windSpeed, bool temperature, bool solar, bool humitdity)
        {
            string weatherMatrixString = WEATHERMATRIX + " <- matrix(c(";
            int cols = 0;
            if (presure)
            {
                weatherMatrixString += weatherSource + "$Pressure,";
                cols++;
            }
            if (rain)
            {
                weatherMatrixString += weatherSource + "$Rain,";
                cols++;
            }
            if (windSpeed)
            {
                weatherMatrixString += weatherSource + "$WindSpeed,";
                cols++;
            }
            if (temperature)
            {
                weatherMatrixString += weatherSource + "$Temperature,";
                cols++;
            }
            if (solar)
            {
                weatherMatrixString += weatherSource + "$Solar,";
                cols++;
            }
            if (humitdity)
            {
                weatherMatrixString += weatherSource + "$Humidity,";
                cols++;
            }
            if (cols == 0)
            {
                throw new Exception("No column chosen");
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
        }
    }
}