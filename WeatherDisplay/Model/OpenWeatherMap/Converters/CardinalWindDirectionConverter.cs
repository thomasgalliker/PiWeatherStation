namespace WeatherDisplay.Model.OpenWeatherMap.Converters
{
    internal static class WindHelper
    {
        /// <summary>
        ///     http://snowfence.umn.edu/Components/winddirectionanddegrees.htm
        /// </summary>
        internal static CardinalWindDirection GetCardinalWindDirection(double windDegrees)
        {
            if (windDegrees > 11.25 && windDegrees <= 33.75)
            {
                return CardinalWindDirection.NNE;
            }

            if (windDegrees > 33.75 && windDegrees <= 56.25)
            {
                return CardinalWindDirection.NE;
            }

            if (windDegrees > 56.25 && windDegrees <= 78.75)
            {
                return CardinalWindDirection.ENE;
            }

            if (windDegrees > 78.75 && windDegrees <= 101.25)
            {
                return CardinalWindDirection.E;
            }

            if (windDegrees > 101.25 && windDegrees <= 123.75)
            {
                return CardinalWindDirection.ESE;
            }

            if (windDegrees > 123.75 && windDegrees <= 146.25)
            {
                return CardinalWindDirection.SE;
            }

            if (windDegrees > 146.25 && windDegrees <= 168.75)
            {
                return CardinalWindDirection.SSE;
            }

            if (windDegrees > 168.75 && windDegrees <= 191.25)
            {
                return CardinalWindDirection.S;
            }

            if (windDegrees > 191.25 && windDegrees <= 213.75)
            {
                return CardinalWindDirection.SSW;
            }

            if (windDegrees > 213.75 && windDegrees <= 236.25)
            {
                return CardinalWindDirection.SW;
            }

            if (windDegrees > 236.25 && windDegrees <= 258.75)
            {
                return CardinalWindDirection.WSW;
            }

            if (windDegrees > 258.75 && windDegrees <= 281.25)
            {
                return CardinalWindDirection.W;
            }

            if (windDegrees > 281.25 && windDegrees <= 303.75)
            {
                return CardinalWindDirection.WNW;
            }

            if (windDegrees > 303.75 && windDegrees <= 326.25)
            {
                return CardinalWindDirection.NW;
            }

            if (windDegrees > 326.25 && windDegrees <= 348.75)
            {
                return CardinalWindDirection.NNW;
            }

            return CardinalWindDirection.N;
        }
    }
}