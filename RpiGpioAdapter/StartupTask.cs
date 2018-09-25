using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.Storage;
using Windows.ApplicationModel.Background;
using Gpio.Adapter;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace RpiGpioAdapter
{
    public sealed class StartupTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            //var folder = PackageFinder.FindPackageDirectory();
            var folder = @"C:\Data\SharedData\GPIO";
            GpioHandler gpioHandler = new GpioHandler(folder);
            gpioHandler.InitPins();
            while (true) { }
        }
    }
}
