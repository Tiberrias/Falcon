using System.Collections.ObjectModel;
using Falcon.Core.Model.Loadouts;
using Falcon.GUI.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PropertyChanged;

namespace Falcon.GUI.ViewModel
{
    [ImplementPropertyChanged]
    public class MissionLoadoutEditorViewModel : ViewModelBase
    {
        public ObservableCollection<Loadout> MissionLoadouts { get; set; }

        public Loadout CurrentLoadout { get; set; }

        public RelayCommand SaveCommand { get; private set; }

        public MissionLoadoutEditorViewModel()
        {
            MissionLoadouts = new ObservableCollection<Loadout>();
            SaveCommand = new RelayCommand(Save);
        }

        private void Save()
        {
            var message = new ShowMessage();

            MessengerInstance.Send(message, FalconMessageToken.ShowImportView);
        }
    }
}