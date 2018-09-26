using System;
using Windows.ApplicationModel.Background;
using Windows.Storage;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace RpiGpioAdapter
{
    public sealed class StartupTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var packageFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var sampleFile = await packageFolder.GetFileAsync("PinSettingLocation.txt");
            var folder = await FileIO.ReadTextAsync(sampleFile);
            GpioHandler gpioHandler = new GpioHandler(folder);
            gpioHandler.InitPins();
            while (true) { }
        }
    }
}
