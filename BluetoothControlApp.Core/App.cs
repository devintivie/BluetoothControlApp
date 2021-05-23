using BaseClasses;
using BaseClasses.StateManagers;
using BluetoothControlApp.Core.ViewModels;
using BluetoothControlApp.StateManagers;
using MvvmCross;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BluetoothControlApp.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();

            Mvx.IoCProvider.RegisterSingleton<IConfigManager>(() => new BluetoothControlStateManager());
            Mvx.IoCProvider.RegisterSingleton<IMessenger>(() => new Messenger());
            RegisterAppStart<MainViewModel>();
        }
    }
}
