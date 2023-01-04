// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Device.I2c;
using System.Gpio.Devices.BMxx80;
using System.Gpio.Devices.Extensions;
using System.Gpio.Devices.Utils;
using System.Threading;
using System.Threading.Tasks;
using UnitsNet;

internal static class Bme680Sample
{
    internal static async Task RunAsync()
    {
        Console.WriteLine("Hello BME680!");

        // The I2C bus ID on the Raspberry Pi 3.
        const int busId = 1;
        // set this to the current sea level pressure in the area for correct altitude readings
        Pressure defaultSeaLevelPressure = WeatherHelper.MeanSeaLevel;

        var i2cSettings = new I2cConnectionSettings(busId, Bme680.SecondaryI2cAddress);
        var i2cDevice = I2cDevice.Create(i2cSettings);

        using var bme680 = new Bme680(i2cDevice, Temperature.FromDegreesCelsius(20.0));

        while (true)
        {
            // reset will change settings back to default
            bme680.Reset();

            // 10 consecutive measurement with default settings
            for (var i = 0; i < 10; i++)
            {
                Console.Clear();

                // Perform a synchronous measurement
                var readResult = bme680.Read();
                var sensorData = readResult.ToSensorData();

                Console.WriteLine($"SensorId: {sensorData.SensorId}");
                Console.WriteLine($"Date: {sensorData.Date:O}");
                foreach (var sensorValue in sensorData.SensorValues)
                {
                    if (sensorValue is Temperature temperature)
                    {
                        Console.WriteLine($"temperature: {temperature.Value:0.0} {temperature.ToString("a")}");
                    }
                    else
                    {
                        Console.WriteLine($"sensorValue: {sensorValue.ToString()} --> {sensorValue.Value} {sensorValue.Unit}");
                    }
                }

                Console.WriteLine();

                // Print out the measured data
                Console.WriteLine($"Gas resistance: {readResult.GasResistance?.Ohms:0.##}Ohm");
                Console.WriteLine($"Temperature: {readResult.Temperature?.DegreesCelsius:0.#}\u00B0C");
                Console.WriteLine($"Pressure: {readResult.Pressure?.Hectopascals:0.##}hPa");
                Console.WriteLine($"Relative humidity: {readResult.Humidity?.Percent:0.#}%");

                if (readResult.Temperature.HasValue && readResult.Pressure.HasValue)
                {
                    var altValue = WeatherHelper.CalculateAltitude(readResult.Pressure.Value, defaultSeaLevelPressure, readResult.Temperature.Value);
                    Console.WriteLine($"Altitude: {altValue.Meters:0.##}m");
                }

                if (readResult.Temperature.HasValue && readResult.Humidity.HasValue)
                {
                    // WeatherHelper supports more calculations, such as saturated vapor pressure, actual vapor pressure and absolute humidity.
                    Console.WriteLine($"Heat index: {WeatherHelper.CalculateHeatIndex(readResult.Temperature.Value, readResult.Humidity.Value).DegreesCelsius:0.#}\u00B0C");
                    Console.WriteLine($"Dew point: {WeatherHelper.CalculateDewPoint(readResult.Temperature.Value, readResult.Humidity.Value).DegreesCelsius:0.#}\u00B0C");
                }

                // when measuring the gas resistance on each cycle it is important to wait a certain interval
                // because a heating plate is activated which will heat up the sensor without sleep, this can
                // falsify all readings coming from the sensor
                Thread.Sleep(5000);
            }

            // change the settings
            bme680.TemperatureSampling = Sampling.HighResolution;
            bme680.HumiditySampling = Sampling.UltraHighResolution;
            bme680.PressureSampling = Sampling.Skipped;

            bme680.ConfigureHeatingProfile(Bme680HeaterProfile.Profile2, Temperature.FromDegreesCelsius(280), Duration.FromMilliseconds(80), Temperature.FromDegreesCelsius(24));
            bme680.HeaterProfile = Bme680HeaterProfile.Profile2;

            // 10 consecutive measurements with custom settings
            for (var i = 0; i < 10; i++)
            {
                // Perform an asynchronous measurement
                var readResult = await bme680.ReadAsync();

                // Print out the measured data
                Console.WriteLine($"Gas resistance: {readResult.GasResistance?.Ohms:0.##}Ohm");
                Console.WriteLine($"Temperature: {readResult.Temperature?.DegreesCelsius:0.#}\u00B0C");
                Console.WriteLine($"Pressure: {readResult.Pressure?.Hectopascals:0.##}hPa");
                Console.WriteLine($"Relative humidity: {readResult.Humidity?.Percent:0.#}%");

                if (readResult.Temperature.HasValue && readResult.Pressure.HasValue)
                {
                    var altValue = WeatherHelper.CalculateAltitude(readResult.Pressure.Value, defaultSeaLevelPressure, readResult.Temperature.Value);
                    Console.WriteLine($"Altitude: {altValue.Meters:0.##}m");
                }

                if (readResult.Temperature.HasValue && readResult.Humidity.HasValue)
                {
                    // WeatherHelper supports more calculations, such as saturated vapor pressure, actual vapor pressure and absolute humidity.
                    Console.WriteLine($"Heat index: {WeatherHelper.CalculateHeatIndex(readResult.Temperature.Value, readResult.Humidity.Value).DegreesCelsius:0.#}\u00B0C");
                    Console.WriteLine($"Dew point: {WeatherHelper.CalculateDewPoint(readResult.Temperature.Value, readResult.Humidity.Value).DegreesCelsius:0.#}\u00B0C");
                }

                Thread.Sleep(1000);
            }
        }
    }
}