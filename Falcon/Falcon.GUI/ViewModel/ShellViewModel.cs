﻿using GalaSoft.MvvmLight;

namespace Falcon.GUI.ViewModel
{
    public class ShellViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get; set; }

        public ShellViewModel(FalconViewModel viewModel)
        {
            CurrentViewModel = viewModel;
        }
    }
}