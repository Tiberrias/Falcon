using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Falcon.Core.Model;
using Falcon.Core.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PropertyChanged;

namespace Falcon.GUI.ViewModel
{
    [ImplementPropertyChanged]
    public class MissionLoadoutEditorViewModel : ViewModelBase
    {
        private readonly IVirtualArsenalLoadoutService _arsenalLoadoutService;

        public ObservableCollection<Loadout> MissionLoadouts { get; set; }

        public Loadout CurrentLoadout { get; set; }

        public RelayCommand DoStuffCommand { get; private set; }

        public MissionLoadoutEditorViewModel(IVirtualArsenalLoadoutService arsenalLoadoutService)
        {
            _arsenalLoadoutService = arsenalLoadoutService;

            MissionLoadouts = new ObservableCollection<Loadout>();
            DoStuffCommand = new RelayCommand(DoStuff);
        }

        private void DoStuff()
        {
            var arsenalFilePaths = _arsenalLoadoutService.GetPossibleConfigVarsFilepaths();
            if (arsenalFilePaths.Count == 1)
            {
                var importedLoadouts = _arsenalLoadoutService.ImportLoadouts(arsenalFilePaths.First());
                MissionLoadouts = new ObservableCollection<Loadout>(importedLoadouts);
            }
        }
    }
}