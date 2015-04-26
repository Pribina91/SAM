using System;
using RDotNet;

using Microsoft.Win32;
using System.Linq;
using VisualAnalytics.Controllers.Helpers.Arima;

namespace VisualAnalytics.Controllers.Helpers
{
	public class Prediction
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public void arimaForecast(){

			REngine.SetEnvironmentVariables(); // <-- May be omitted; the next line would call it.

			REngine engine = REngine.GetInstance();
				
			// A somewhat contrived but customary Hello World:
			CharacterVector charVec = engine.CreateCharacterVector(new[] { "Hello, R world!, .NET speaking" });
			engine.SetSymbol("greetings", charVec);
			engine.Evaluate("str(greetings)"); // print out in the console
			string[] a = engine.Evaluate("'Hi there .NET, from the R engine'").AsCharacter().ToArray();
				
			log.Debug("R answered: '"+ a[0].ToString()+"'");
			log.Debug("Press any key to exit the program");


			engine.Dispose();

		}
		/// <summary>
		/// First run of REngine
		/// </summary>
		void init()
		{
			REngine engine = REngine.GetInstance();
			engine.Initialize();
			engine.Evaluate("install.packages(\"forecast\")");
			engine.Evaluate("install.packages(\"jsonlite\")");
			engine.Evaluate("install.packages('httr')"); 
			
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

			string currentPath = @"C:\Workspace\SAM\SAM\VisualAnalytics\bin" ; //Directory.GetCurrentDirectory();
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

		static string GetRPath()
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

	}
}