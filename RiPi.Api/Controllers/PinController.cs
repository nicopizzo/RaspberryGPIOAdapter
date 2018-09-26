using System;
using Microsoft.AspNetCore.Mvc;
using Gpio.Adapter;
using Gpio.Implemention;
using Gpio.Abstract;
using Microsoft.Extensions.Options;
using RiPi.Api.Models;

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
        [Route("Set/{id}/{status}")]
        public bool Set(int id, int status)
        {
            Console.WriteLine("About to change pin status.");
            var pin = new Pin()
            {
                PinNo = (short)id,
                Direction = PinDirection.Out,
                Value = (PinValue)status
            };
            _gpioAdapter.UpdatePin(pin);
            return true;
        }
    }
}
