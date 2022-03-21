using System;
using Newtonsoft.Json;

namespace WeatherDisplay.Model.OpenWeatherMap.Converters
{
    internal static class WindHelper
    {
        /// <summary>
        /// http://snowfence.umn.edu/Components/winddirectionanddegrees.htm
        /// </summary>
        internal static CardinalWindDirection GetCardinalWindDirection(double windDegrees)
        {
            if (windDegrees > 11.25 && windDegrees <= 33.75)
            {
                return CardinalWindDirection.NNE;
            }
            else if (windDegrees > 33.75 && windDegrees <= 56.25)
            {
                return CardinalWindDirection.NE;
            }
            else if (windDegrees > 56.25 && windDegrees <= 78.75)
            {
                return CardinalWindDirection.ENE;
            }
            else if (windDegrees > 78.75 && windDegrees <= 101.25)
            {
                return CardinalWindDirection.E;
            }
            else if (windDegrees > 101.25 && windDegrees <= 123.75)
            {
                return CardinalWindDirection.ESE;
            }
            else if (windDegrees > 123.75 && windDegrees <= 146.25)
            {
                return CardinalWindDirection.SE;
            }
            else if (windDegrees > 146.25 && windDegrees <= 168.75)
            {
                return CardinalWindDirection.SSE;
            }
            else if (windDegrees > 168.75 && windDegrees <= 191.25)
            {
                return CardinalWindDirection.S;
            }
            else if (windDegrees > 191.25 && windDegrees <= 213.75)
            {
                return CardinalWindDirection.SSW;
            }
            else if (windDegrees > 213.75 && windDegrees <= 236.25)
            {
                return CardinalWindDirection.SW;
            }
            else if (windDegrees > 236.25 && windDegrees <= 258.75)
            {
                return CardinalWindDirection.WSW;
            }
            else if (windDegrees > 258.75 && windDegrees <= 281.25)
            {
                return CardinalWindDirection.W;
            }
            else if (windDegrees > 281.25 && windDegrees <= 303.75)
            {
                return CardinalWindDirection.WNW;
            }
            else if (windDegrees > 303.75 && windDegrees <= 326.25)
            {
                return CardinalWindDirection.NW;
            }
            else if (windDegrees > 326.25 && windDegrees <= 348.75)
            {
                return CardinalWindDirection.NNW;
            }
            else
            {
                return CardinalWindDirection.N;
            }
        }
    }
}
