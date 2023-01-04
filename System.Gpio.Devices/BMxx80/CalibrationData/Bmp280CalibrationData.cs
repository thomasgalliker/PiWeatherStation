// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Gpio.Devices.BMxx80.Register;

namespace System.Gpio.Devices.BMxx80.CalibrationData
{
    /// <summary>
    /// Calibration data for the BMP280.
    /// </summary>
    internal class Bmp280CalibrationData : Bmxx80CalibrationData
    {
        /// <summary>
        /// Read coefficient data from device.
        /// </summary>
        /// <param name="bmxx80Base">The <see cref="Bmxx80Base"/> to read coefficient data from.</param>
        protected internal override void ReadFromDevice(Bmxx80Base bmxx80Base)
        {
            // Read temperature calibration data
            this.DigT1 = bmxx80Base.Read16BitsFromRegister((byte)Bmx280Register.DIG_T1);
            this.DigT2 = (short)bmxx80Base.Read16BitsFromRegister((byte)Bmx280Register.DIG_T2);
            this.DigT3 = (short)bmxx80Base.Read16BitsFromRegister((byte)Bmx280Register.DIG_T3);

            // Read pressure calibration data
            this.DigP1 = bmxx80Base.Read16BitsFromRegister((byte)Bmx280Register.DIG_P1);
            this.DigP2 = (short)bmxx80Base.Read16BitsFromRegister((byte)Bmx280Register.DIG_P2);
            this.DigP3 = (short)bmxx80Base.Read16BitsFromRegister((byte)Bmx280Register.DIG_P3);
            this.DigP4 = (short)bmxx80Base.Read16BitsFromRegister((byte)Bmx280Register.DIG_P4);
            this.DigP5 = (short)bmxx80Base.Read16BitsFromRegister((byte)Bmx280Register.DIG_P5);
            this.DigP6 = (short)bmxx80Base.Read16BitsFromRegister((byte)Bmx280Register.DIG_P6);
            this.DigP7 = (short)bmxx80Base.Read16BitsFromRegister((byte)Bmx280Register.DIG_P7);
            this.DigP8 = (short)bmxx80Base.Read16BitsFromRegister((byte)Bmx280Register.DIG_P8);
            this.DigP9 = (short)bmxx80Base.Read16BitsFromRegister((byte)Bmx280Register.DIG_P9);
        }
    }
}
