# RaspberryGPIOAdapter
Adapter for .net core applications to access the GPIO pins on a Raspberry Pi

This Repo is to allow .net core applications to talk to and control the GPIO pins on the Raspberry Pi

How to Use- Window IoT:
Find a Directory on the Raspberry Pi that will share data. (eg. C:\Data\SharedData)
Deploy RpiGpioAdapter to Raspberry Pi.
Update PinSettingLocation.txt file to point to the shared data folder.
Run the setup_iot.ps1 file with the following params RPiServerIP, RpiUserNm, RpiPassword, RpiSharedDirectory

Find a new directory where the .net core application will live.
Run the publish_deploy_iot.ps1 file with the following params .netCoreProjectDir, RpiDeployDir

Start the background task that we first deployed.
Start the .net core application.
