﻿namespace RaspberryPi
{
    public class CpuSensorsStatus
    {
        /// <summary>
        /// Gets or sets the CPU temperature in °C.
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// Gets or sets the CPU voltage in volts.
        /// </summary>
        public double Voltage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the CPU is in under voltage.
        /// </summary>
        public bool UnderVoltageDetected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the CPU frequency is capped.
        /// </summary>
        public bool ArmFrequencyCapped { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the CPU is throttled.
        /// </summary>
        public bool CurrentlyThrottled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the CPU is over the soft temperature limit.
        /// </summary>
        public bool SoftTemperatureLimitActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether an under voltage occurred.
        /// </summary>
        public bool UnderVoltageOccurred { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a frequency capping occurred.
        /// </summary>
        public bool ArmFrequencyCappingOccurred { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a throttling occurred.
        /// </summary>
        public bool ThrottlingOccurred { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a soft temperature limit occurred.
        /// </summary>
        public bool SoftTemperatureLimitOccurred { get; set; }
    }
}