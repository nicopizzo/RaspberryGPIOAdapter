using System;
using Gpio.Abstract;

namespace Gpio.Implemention
{
    public class Pin : IPin
    {
        public short PinNo { get; set; }
        public PinDirection Direction { get; set; }
        public PinValue Value { get; set; }
    }
}
