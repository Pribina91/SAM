using System;
using System.Collections.Generic;
using System.Linq;

namespace VisualAnalytics.Controllers.Analytics
{
    public enum Feature
    { Temperature, Rain, WindSpeed, Humidity, Solar, Pressure }

    public class Outlier
    {
        public int seriesNumber;
        public int IDDate;
        public List<WeatherColumns> weatherDependency;

        public List<double> outlierness;

        public List<double> tStats;

        public Feature outliernessFeature;

        public Outlier()
        {
            this.weatherDependency = new List<WeatherColumns>();
            this.outlierness = new List<double>();
            this.tStats = new List<double>();
            this.seriesNumber = 0;
            this.IDDate = 0;
        }

        public void Add(WeatherColumns wc, double o, double ts)
        {
            this.weatherDependency.Add(wc);
            this.outlierness.Add(o);
            this.tStats.Add(ts);
        }
    }
}