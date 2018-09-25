using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Gpio.Adapter
{
    public static class PackageFinder
    {
        public static string FindPackageDirectory()
        {
            string directory = string.Empty;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                try
                {
                    var envPath = Environment.GetEnvironmentVariable("LOCALAPPDATA");
                    string localAppData = envPath.Replace("Administrator", "DefaultAccount");
                    var directories = Directory.GetDirectories(localAppData);
                    var packageFolder = new DirectoryInfo(localAppData);
                    packageFolder = packageFolder.GetDirectories().Single(m => m.Name.StartsWith("RpiGpioAdapter"));
                    directory = packageFolder.GetDirectories().Single(m => m.Name.StartsWith("LocalState"))?.FullName;
                }
                catch(Exception ex)
                {
                    throw new Exception("Could not find device path for Windows application", ex);
                }
            }
            else
            {
                directory = @"/sys/class/gpio";
            }
            return directory;
        }
    }
}
