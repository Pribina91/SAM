using System;
using System.Collections.Generic;
using System.Linq;

namespace VisualAnalytics.Controllers.Analytics
{
    public class Outlier
    {
        public int seriesNumber;

        public List<WeatherColumns> weatherDependency;

        public List<double> outlierness;

        public List<double> tStats;

        public Outlier()
        {
            this.weatherDependency = new List<WeatherColumns>();
            this.outlierness = new List<double>();
            this.tStats = new List<double>();
            this.seriesNumber = 0;
        }
    }
}