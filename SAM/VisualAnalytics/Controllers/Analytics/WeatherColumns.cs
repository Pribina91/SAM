using System;
using System.Linq;

namespace VisualAnalytics.Controllers.Analytics
{
    public class WeatherColumns
    {
        public bool presure;
        public bool rain;
        public bool windSpeed;
        public bool temperature;
        public bool solar;
        public bool humitdity;

        private bool[] boolArray;

        public bool[] BoolArray
        {
            get
            {
                bool[] a = new bool[6];
                a[0] = presure;
                a[1] = rain;

                return a;
            }
        }
    }
}