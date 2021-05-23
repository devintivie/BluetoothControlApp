using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BluetoothControlApp.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Fields
        readonly IMvxNavigationService _navService;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public MainViewModel(IMvxNavigationService navService)
        {
            _navService = navService;
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            _ = NavigateInitial();
        }
        #endregion

        #region Methods
        private async Task NavigateInitial()
        {
            await _navService.Navigate<TestViewModel>();
        }
        #endregion

    }
}
