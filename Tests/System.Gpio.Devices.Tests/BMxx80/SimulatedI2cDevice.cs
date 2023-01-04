// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Device.I2c;

namespace System.Gpio.Devices.Tests.BMxx80
{
    public class SimulatedI2cDevice : I2cDevice
    {
        private readonly byte[] registers;
        private byte currentRegister;

        public SimulatedI2cDevice(I2cConnectionSettings connectionSettings)
        {
            this.registers = new byte[256];
            this.currentRegister = 0;
            this.ConnectionSettings = connectionSettings;
        }

        public SimulatedI2cDevice()
            : this(new I2cConnectionSettings(1, 1))
        {
        }

        public override I2cConnectionSettings ConnectionSettings { get; }

        public override byte ReadByte()
        {
            return this.registers[this.currentRegister];
        }

        public override void Read(Span<byte> buffer)
        {
            int idx = this.currentRegister;
            for (var i = 0; i < buffer.Length; i++)
            {
                buffer[i] = this.registers[idx];
                idx++;
            }
        }

        public override void WriteByte(byte value)
        {
            this.currentRegister = value;
        }

        public override void Write(ReadOnlySpan<byte> buffer)
        {
            this.currentRegister = buffer[0];
            int idx = this.currentRegister;

            for (var i = 1; i < buffer.Length; i++)
            {
                this.registers[idx] = buffer[i];
                idx++;
            }
        }

        public override void WriteRead(ReadOnlySpan<byte> writeBuffer, Span<byte> readBuffer)
        {
            this.Write(writeBuffer);
            this.Read(readBuffer);
        }

        public void SetRegister(int register, byte value)
        {
            this.registers[register] = value;
        }

        public void SetRegister(int register, short value)
        {
            this.registers[register] = (byte)(value & 0xFF);
            this.registers[register + 1] = (byte)(value >> 8);
        }

        public void SetRegister(int register, ushort value)
        {
            this.registers[register] = (byte)(value & 0xFF);
            this.registers[register + 1] = (byte)(value >> 8);
        }
    }
}
