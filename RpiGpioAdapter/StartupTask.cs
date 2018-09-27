using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Storage;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace RpiGpioAdapter
{
    public sealed class StartupTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            string directory = string.Empty;
            try
            {
                directory = await GetPinFolder();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                Debug.WriteLine("Trying again...");
                directory = await GetPinFolder();
            }
         
            GpioHandler gpioHandler = new GpioHandler(directory);
            gpioHandler.InitPins();
            while (true) { }
        }

        private async Task<string> GetPinFolder()
        {
            var packageFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var sampleFile = await packageFolder.GetFileAsync("PinSettingLocation.txt");
            return await FileIO.ReadTextAsync(sampleFile);
        }
    }
}
