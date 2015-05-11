using System;
using System.Collections.Generic;
using System.Linq;

namespace VisualAnalytics.Controllers.Analytics
{
    public class Forecast
    {
        public float OriginalAmount;
        public string Method;
        public List<float> Means;
        public List<float> Residuals;

        public Forecast()
        {
            Means = new List<float>();
            Residuals = new List<float>();
        }
    }
}