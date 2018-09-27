# RaspberryGPIOAdapter
Adapter for .net core applications to access the GPIO pins on a Raspberry Pi

This Repo is to allow .net core applications to talk to and control the GPIO pins on the Raspberry Pi via UWP application.

To use the universal Gpio.Adapter:

var adapter = GpioAdapter("C:\Data\SharedData");

If the application has access to the Gpio pins (like a UWP application) then set the PinSet event:
adapter += callbackFunction;
then start the file watcher:
adapter.InitFileWatcher();

If you do not have access to the pins (like a .net core api application) then you can call either:
adapter.GetPin(); or
adapter.UpdatePin();

How to Use Premade Apps- Window IoT:
Find a Directory on the Raspberry Pi that will share data. (eg. C:\Data\SharedData)
Deploy RpiGpioAdapter to Raspberry Pi.
Update PinSettingLocation.txt file to point to the shared data folder.
Run the setup_iot.ps1 file with the following params RPiServerIP, RpiUserNm, RpiPassword, RpiSharedDirectory

Find a new directory where the .net core application will live.
Run the publish_deploy_iot.ps1 file with the following params .netCoreProjectDir, RpiDeployDir

Start the background task that we first deployed.
Start the .net core application.
