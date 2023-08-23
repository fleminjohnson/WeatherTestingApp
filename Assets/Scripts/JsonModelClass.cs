using System;
using System.Collections.Generic;

namespace WeatherForeCasting
{
    public abstract class JsonModelClass
    {
        public class WeatherData
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public double GenerationTimeMs { get; set; }
            public int UtcOffsetSeconds { get; set; }
            public string Timezone { get; set; }
            public string TimezoneAbbreviation { get; set; }
            public double Elevation { get; set; }
            public DailyUnits daily_units { get; set; }
            public DailyData Daily { get; set; }
        }

        public class ConfigData
        {
            public int RequestTimeout { get; set; }
        }

        public class DailyUnits
        {
            public string Time { get; set; }
            public string temperature_2m_max { get; set; }
        }

        public class DailyData
        {
            public List<string> Time { get; set; }
            public List<double> temperature_2m_max { get; set; }
        }

    }
}