using BluetoothControlApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace BluetoothControlApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}