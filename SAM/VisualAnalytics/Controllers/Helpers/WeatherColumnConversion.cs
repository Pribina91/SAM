using System;
using System.Linq;
using VisualAnalytics.Controllers.Analytics;

namespace VisualAnalytics.Controllers.Helpers
{
    public static class WeatherColumnConversion
    {
        public static WeatherColumns stringToWeatherColumn(string weatherDependency)
        {
            byte[] bytes = new byte[6];
            var chArray = weatherDependency.ToCharArray();
            for (int i = 0; i < 6; i++)
            {
                bytes[i] = BitConverter.GetBytes(int.Parse(chArray[i].ToString()))[0];
            }
            return new WeatherColumns(bytes);
        }
    }
}