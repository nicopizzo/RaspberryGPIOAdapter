using System;

namespace Gpio.Abstract
{
    public interface IPin
    {
        short PinNo { get; set; }
        PinDirection Direction { get; set; }
        PinValue Value { get; set; }
    }
}
