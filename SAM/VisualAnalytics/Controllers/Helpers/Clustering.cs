using RDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisualAnalytics.Controllers.Helpers
{
	public class Clustering
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataFrame cluster()
        {
            REngine engine = REngine.GetInstance();
            engine.Initialize();

            String JsonData = @"http://localhost:25910/Home/getBigTable";//controller.getBigTable().Content;

            log.Debug("JSON:" + JsonData);
            string evaluate = string.Format("data <-fromJSON(\"{0}\")", JsonData);
            log.Debug("evaluateString:" + evaluate);

            engine.Evaluate("library(jsonlite)");
            engine.Evaluate("library(dtw)");
            engine.Evaluate(evaluate);
            engine.Evaluate("distMatrix <- dist(data, method='DTW')");
            engine.Evaluate("hc <- hclust(distMatrix, method='average')");
            engine.Evaluate("memb <- cutree(hc, k=6)");
            var clustered = engine.Evaluate("data.frame(memb,data$IDDate)").AsDataFrame();

            return clustered;


        }

        void init()
		{
			REngine engine = REngine.GetInstance();
			engine.Initialize();

			engine.Dispose();

		}
	}
}