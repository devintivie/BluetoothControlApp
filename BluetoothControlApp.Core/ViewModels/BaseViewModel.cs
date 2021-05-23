using MvvmCross.ViewModels;

namespace BluetoothControlApp.Core.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public BaseViewModel()
        {

        }
        #endregion

        #region Methods

        #endregion
            
    }

    public abstract class BaseViewModel<TParameter> : BaseViewModel, IMvxViewModel<TParameter>, IMvxViewModel
    {
        public abstract void Prepare(TParameter parameter);
    }
}