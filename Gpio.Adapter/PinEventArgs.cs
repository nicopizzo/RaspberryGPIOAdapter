using Gpio.Abstract;
using Gpio.Implemention;
using System;

namespace Gpio.Adapter
{
    public class PinEventArgs : EventArgs
    {
        public IPin Pin { get; private set; }

        public PinEventArgs(IPin pin)
        {
            Pin = pin;
        }

        public PinEventArgs(short pinNo, int pinValue, int pinDirection)
        {
            Pin = new Pin()
            {
                PinNo = pinNo,
                Value = (PinValue)pinValue,
                Direction = (PinDirection)pinDirection
            };
        }
    }
}
