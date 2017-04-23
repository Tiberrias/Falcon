using System;
using System.Collections.ObjectModel;
using Falcon.Core.Model;
using Falcon.Core.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Falcon.GUI.ViewModel
{
    public class MissionLoadoutEditorViewModel : ViewModelBase
    {
        private readonly IVirtualArsenalLoadoutService _arsenalLoadoutService;

        public ObservableCollection<Loadout> MissionLoadouts { get; set; }

        public Loadout CurrentLoadout { get; set; }

        public RelayCommand DoStuffCommand { get; private set; }

        public MissionLoadoutEditorViewModel(IVirtualArsenalLoadoutService arsenalLoadoutService)
        {
            _arsenalLoadoutService = arsenalLoadoutService;

            DoStuffCommand = new RelayCommand(DoStuff);
        }

        private void DoStuff()
        {
            CurrentLoadout = new Loadout()
            {
                Name = Guid.NewGuid().ToString()
            };
        }
    }
}