using BluetoothControlApp.Services;
using BluetoothControlApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BluetoothControlApp
{
    public partial class App : Application
    {

        public App()
        {
            Device.SetFlags(new string[] { "AppTheme_Experimental" });
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            //MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
