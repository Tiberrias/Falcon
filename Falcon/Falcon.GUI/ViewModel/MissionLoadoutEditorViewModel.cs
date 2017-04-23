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

        public ObservableCollection<ManEquipment> MissionLoadouts { get; set; }

        public ManEquipment CurrentLoadout { get; set; }

        public RelayCommand DoStuff { get; private set; }

        public MissionLoadoutEditorViewModel(IVirtualArsenalLoadoutService arsenalLoadoutService)
        {
            _arsenalLoadoutService = arsenalLoadoutService;
        }
    }
}