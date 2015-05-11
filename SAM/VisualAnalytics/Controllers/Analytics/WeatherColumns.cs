using System;
using System.Linq;

namespace VisualAnalytics.Controllers.Analytics
{
    public class WeatherColumns : IEquatable<WeatherColumns>
    {
        public bool rain;
        public bool windSpeed;
        public bool temperature;
        public bool solar;
        public bool humitdity;
        public bool pressure;

        public WeatherColumns(byte[] bitArray)
        {
            rain = Convert.ToBoolean(bitArray[0]);
            windSpeed = Convert.ToBoolean(bitArray[1]);
            temperature = Convert.ToBoolean(bitArray[2]);
            solar = Convert.ToBoolean(bitArray[3]);
            humitdity = Convert.ToBoolean(bitArray[4]);
            pressure = Convert.ToBoolean(bitArray[5]);
        }

        public WeatherColumns()
        {
            //throw new NotImplementedException();
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}", Convert.ToInt32(rain), Convert.ToInt32(windSpeed), Convert.ToInt32(temperature), Convert.ToInt32(solar), Convert.ToInt32(humitdity), Convert.ToInt32(pressure));
            //return base.ToString();
        }

        public bool Equals(WeatherColumns other)
        {
            if (other == null) return false;

            return (this.rain.Equals(other.rain)
                    && this.windSpeed.Equals(other.windSpeed)
                    && this.temperature.Equals(other.temperature)
                    && this.solar.Equals(other.solar)
                    && this.humitdity.Equals(other.humitdity)
                    && this.pressure.Equals(other.pressure)
                    );
        }

        public bool AllFalse()
        {
            return !(this.rain
                    || this.windSpeed
                    || this.temperature
                    || this.solar
                    || this.humitdity
                    || this.pressure
                );
        }
    }
}