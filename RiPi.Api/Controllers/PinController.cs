using System;
using Microsoft.AspNetCore.Mvc;
using Gpio.Adapter;
using Gpio.Implemention;
using Gpio.Abstract;
using Microsoft.Extensions.Options;
using RiPi.Api.Models;
using System.Collections.Generic;

namespace RasPiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PinsController : ControllerBase
    {
        private GpioAdapter _gpioAdapter;

        public PinsController(IOptions<AppSettings> settings)
        {
            _gpioAdapter = new GpioAdapter(settings.Value.PinFileDirectory);
        }

        [HttpGet]
        [Route("Get/{id}")]
        public string Get(int id)
        {
            Console.WriteLine("About to get pin status.");
            var pin = _gpioAdapter.GetPin((short)id);
            return pin.Value.ToString();
        }

        [HttpGet]
        [Route("SetValue/{id}/{value}")]
        public bool SetValue(int id, PinValue value)
        {
            Console.WriteLine("About to change pin value.");
            var pin = _gpioAdapter.GetPin((short)id);
            pin.Value = value;
            _gpioAdapter.UpdatePins(new List<IPin>() { pin });
            return true;
        }

        [HttpGet]
        [Route("SetDirection/{id}/{direction}")]
        public bool SetDirection(int id, PinDirection direction)
        {
            Console.WriteLine("About to change pin direction.");
            var pin = _gpioAdapter.GetPin((short)id);
            pin.Direction = direction;
            _gpioAdapter.UpdatePins(new List<IPin>() { pin });
            return true;
        }

        [HttpPost]
        [Route("SetPins")]
        public bool SetPins(List<Pin> pins)
        {
            Console.WriteLine("About to update pins.");
            _gpioAdapter.UpdatePins(pins);
            return true;
        }
    }
}
