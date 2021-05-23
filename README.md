# BluetoothControlApp
Xamarin Forms simple bluetooth messenger. Based on https://github.com/marcboeker/esp32-ble-ios-demo and migrated to Xamarin from swift.

Uses MVVMCross Nuget package and my c# shared projects c# base classes at https://github.com/devintivie/SharedProjects.git

Hardware is ESP32 DEVKITV1 and iPhone XR.

Next version will make application work on Android.

Known bugs:
* Bluetooth does not attempt to reconnect after disconnect
