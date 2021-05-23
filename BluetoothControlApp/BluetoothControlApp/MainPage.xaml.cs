using MvvmCross.Forms.Presenters.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BluetoothControlApp.Views
{
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Root, WrapInNavigationPage = false, Title = "Main")]

    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }
}