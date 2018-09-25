using Gpio.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gpio.Adapter
{
    public class PinEventArgs : EventArgs
    {
        public IPin Pin { get; private set; }

        public PinEventArgs(IPin pin)
        {
            Pin = pin;
        }
    }
}
