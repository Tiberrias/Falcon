using GalaSoft.MvvmLight;
using PropertyChanged;

namespace Falcon.GUI.ViewModel
{
    [ImplementPropertyChanged]
    public class FalconViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get; set; }

        public FalconViewModel(ImportLoadoutsViewModel viewModel)
        {
            CurrentViewModel = viewModel;
        }
    }
}