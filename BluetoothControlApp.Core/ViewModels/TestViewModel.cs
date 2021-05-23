using BaseClasses;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using Plugin.BLE.Abstractions.Exceptions;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace BluetoothControlApp.Core.ViewModels
{
    public class TestViewModel : BaseViewModel
    {
        #region Fields
        private IMvxNavigationService _navService;
        private IMessenger _messenger;

        private IBluetoothLE _ble;
        private IAdapter _bleAdapter;

        private IDevice _testDevice;

        private string serviceUUID = "ab0828b1-198e-4351-b779-901fa0e0371e";
        private string periphealUUID = "4ac8a682-9736-4e5d-932b-e9b31405049c";
        #endregion

        #region Properties

        #endregion

        #region Commands
        public IMvxCommand StartScanCommand { get; set; }
        public IMvxCommand StopScanCommand { get; set; }
        public IMvxCommand ConnectCommand { get; set; }
        public IMvxCommand SendHelloCommand { get; set; }
        public IMvxCommand SendStuffCommand { get; set; }
        public IMvxCommand DisconnectCommand { get; set; }
        #endregion

        #region Constructors
        public TestViewModel(IMvxNavigationService navService, IMessenger messenger)
        {
            _navService = navService;
            _messenger = messenger;

            //var ble = Mvx.IoCProvider.Resolve<IBluetoothLE>();
            //var adapter = Mvx.IoCProvider.Resolve<IAdapter>();
            _ble = CrossBluetoothLE.Current;
            _bleAdapter = CrossBluetoothLE.Current.Adapter;

            _ble.StateChanged += _ble_StateChanged;

            _bleAdapter.DeviceDiscovered += _bleAdapter_DeviceDiscovered;

            ConnectCommand = new MvxAsyncCommand(OnConnect);
            DisconnectCommand = new MvxAsyncCommand(OnDisconnect);
            StartScanCommand = new MvxAsyncCommand(OnStartScan);
            StopScanCommand = new MvxAsyncCommand(OnStopScan);
            SendHelloCommand = new MvxAsyncCommand(OnSendHello);
        }

        private async Task OnSendHello()
        {
            var sGuid = new Guid(serviceUUID);
            var pGuid = new Guid(periphealUUID);
            var service = await _testDevice.GetServiceAsync(sGuid);
            var idk = await service.GetCharacteristicAsync(pGuid);

            var msg = "hello";
            var b = Encoding.UTF8.GetBytes(msg);
            await idk.WriteAsync(b);

            var resp = await idk.ReadAsync();
            var respStr = Encoding.UTF8.GetString(resp);
            Debug.WriteLine($"received = {respStr}");
        }

        private async Task OnStartScan()
        {
            var state = _ble.State;
            Debug.WriteLine(state.ToString());

            
            await _bleAdapter.StartScanningForDevicesAsync();
        }

        private async Task OnStopScan()
        {
            //await _bleAdapter.ConnectedDevices[0].
        }

        private void _bleAdapter_DeviceDiscovered(object sender, DeviceEventArgs e)
        {
            //Debug.WriteLine($"{e.Device.Id} : {e.Device.Name}");
            if (e.Device.Name != null)
            {
                if (e.Device.Name.Equals("BLETestIvie"))
                {

                    _testDevice = e.Device;
                    Debug.WriteLine("Test ESP32 found");
                    //_ = Connect
                    //foreach (var item in e.Device.)
                    //{

                    //}
                    //_ = PrintServices(e.Device);
                }
            }
            

            
            

            //throw new NotImplementedException();
        }

        private async Task PrintServices(IDevice device)
        {
            try
            {
                await Task.Delay(1000);
                var services = await device.GetServicesAsync();
                foreach (var service in services)
                {
                    Debug.WriteLine($"{service.Device} : {service.Name}= {service.Id}");
                    var idk = await service.GetCharacteristicsAsync();
                    foreach (var charac in idk)
                    {
                        await Task.Delay(200);
                        var desc = await charac.GetDescriptorAsync(charac.Id);
                        Debug.WriteLine($"\t{charac.Id}: {charac.Name} {desc}");
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
            }   

            
        }

        private void _ble_StateChanged(object sender, BluetoothStateChangedArgs e)
        {
            Debug.WriteLine($"The bluetooth state changed to {e.NewState}");
        }

        private async Task OnConnect()
        {
            if (_testDevice != null)
            {
                try
                {
                    await _bleAdapter.ConnectToDeviceAsync(_testDevice);
                    await PrintServices(_testDevice);
                }
                catch(Exception ex)
                {
                    Debug.WriteLine($"Connect error : {ex.Message}");
                }
            }
        }

        private async Task OnDisconnect()
        {
            await _bleAdapter.DisconnectDeviceAsync(_testDevice);
        }

        //private async Task OnConnect()
        //{
        //    var guid = new Guid("ab0828b1-198e-4351-b779-901fa0e0371e"); 
        //    var guids = new Guid[] { guid };
        //    try
        //    {
        //        var systemDevices = _bleAdapter.GetSystemConnectedOrPairedDevices(tyjgjgj);

        //        foreach (var device in systemDevices)
        //        {
        //            Debug.WriteLine(device.Id);
        //        }
        //        //await _bleAdapter.ConnectToDeviceAsync(device);
        //    }
        //    catch (DeviceConnectionException ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //        //specific
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //        //generic
        //    }
        //}
        #endregion

        #region Methods

        #endregion

    }
}