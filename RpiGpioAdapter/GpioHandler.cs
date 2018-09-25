using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using System.IO;
using Gpio.Adapter;
using Gpio.Abstract;

namespace RpiGpioAdapter
{
    internal class GpioHandler : IDisposable
    {
        private GpioAdapter _adapter;
        private GpioController _gpioController;
        private Dictionary<short, GpioPin> _openPins = new Dictionary<short, GpioPin>();

        public GpioHandler(string directory)
        {
            _gpioController = GpioController.GetDefault();
            if (_gpioController == null) throw new NotSupportedException("Not Gpio Pins found.");

            _adapter = new GpioAdapter(directory);
            _adapter.PinSet += _adapter_PinSet;
            _adapter.InitFileWatcher();
        }

        public void InitPins()
        {
            foreach(var id in _adapter.AvailiblePins)
            {
                UpdatePin(_adapter.GetPin(id));
            }
        }

        private void _adapter_PinSet(object sender, PinEventArgs e)
        {
            UpdatePin(e.Pin);
        }

        private void UpdatePin(IPin pin)
        {
            GpioPin gpin;
            if (_openPins.ContainsKey(pin.PinNo))
            {
                gpin = _openPins[pin.PinNo];
            }
            else
            {
                gpin = _gpioController.OpenPin(pin.PinNo);
                _openPins.Add(pin.PinNo, gpin);
            }
            gpin.SetDriveMode((GpioPinDriveMode)pin.Direction);
            gpin.Write((GpioPinValue)pin.Value);
        }

        public void Dispose()
        {
            foreach(var pin in _openPins)
            {
                pin.Value.Dispose();
            }
        }
    }
}
