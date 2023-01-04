// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using UnitsNet;

namespace System.Gpio.Devices.BMxx80
{
    /// <summary>
    /// The heater profile configuration saved on the device.
    /// </summary>
    public class Bme680HeaterProfileConfig
    {
        /// <summary>
        /// The chosen heater profile slot, ranging from 0-9.
        /// </summary>
        public Bme680HeaterProfile HeaterProfile { get; set; }

        /// <summary>
        /// The heater resistance.
        /// </summary>
        public ushort HeaterResistance { get; set; }

        /// <summary>
        /// The heater duration
        /// </summary>
        public Duration HeaterDuration { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="Bme680HeaterProfileConfig"/>.
        /// </summary>
        /// <param name="profile">The used heater profile.</param>
        /// <param name="heaterResistance">The heater resistance in Ohm.</param>
        /// <param name="heaterDuration">The heating duration.</param>
        /// <exception cref="ArgumentOutOfRangeException">Unknown profile setting used</exception>
        public Bme680HeaterProfileConfig(Bme680HeaterProfile profile, ushort heaterResistance, Duration heaterDuration)
        {
            if (!Enum.IsDefined(typeof(Bme680HeaterProfile), profile))
            {
                throw new ArgumentOutOfRangeException(nameof(profile));
            }

            this.HeaterProfile = profile;
            this.HeaterResistance = heaterResistance;
            this.HeaterDuration = heaterDuration;
        }
    }
}
