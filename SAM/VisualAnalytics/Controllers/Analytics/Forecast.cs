using System;
using System.Collections.Generic;
using System.Linq;

namespace VisualAnalytics.Controllers.Analytics
{
    public class Forecast
    {
        public float OriginalAmount;
        public string Method;
        public List<double> Means;
        public List<double> Residuals;
        public AccuracyResult Accuracy;

        public Forecast()
        {
            Means = new List<double>();
            Residuals = new List<double>();
        }
    }
}