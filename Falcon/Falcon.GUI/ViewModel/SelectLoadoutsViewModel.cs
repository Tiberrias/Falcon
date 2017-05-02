using System.Collections.ObjectModel;
using Falcon.Core.Model.Loadouts;
using Falcon.GUI.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PropertyChanged;

namespace Falcon.GUI.ViewModel
{
    [ImplementPropertyChanged]
    public class SelectLoadoutsViewModel : ViewModelBase
    {
        public ObservableCollection<Loadout> ArsenalLoadouts { get; set; }

        public Loadout SelectedArsenalLoadout { get; set; }

        public ObservableCollection<Loadout> MissionLoadouts { get; set; }

        public Loadout SelectedMissionLoadout { get; set; }

        public RelayCommand AddToMissionLoadoutsCommand { get; private set; }
        
        public RelayCommand RemoveFromMissionLoadoutsCommand { get; private set; }

        public RelayCommand ConfirmCommand { get; private set; }

        public SelectLoadoutsViewModel()
        {
            ArsenalLoadouts = new ObservableCollection<Loadout>();
            MissionLoadouts = new ObservableCollection<Loadout>();

            AddToMissionLoadoutsCommand = new RelayCommand(AddToMissionLoadouts);
            RemoveFromMissionLoadoutsCommand = new RelayCommand(RemoveFromMissionLoadouts);

            ConfirmCommand = new RelayCommand(Confirm);
        }
        
        private void AddToMissionLoadouts()
        {
            if (SelectedArsenalLoadout == null)
            {
                return;
            }

            var swappedLoadout = SelectedArsenalLoadout;
            SelectedArsenalLoadout = null;

            ArsenalLoadouts.Remove(swappedLoadout);
            MissionLoadouts.Add(swappedLoadout);
        }

        private void RemoveFromMissionLoadouts()
        {
            if (SelectedMissionLoadout == null)
            {
                return;
            }
            
            var swappedLoadout = SelectedMissionLoadout;
            SelectedMissionLoadout = null;

            MissionLoadouts.Remove(swappedLoadout);
            ArsenalLoadouts.Add(swappedLoadout);
        }

        private void Confirm()
        {
            if (ArsenalLoadouts.Count == 0)
            {
                return;
            }

            var message = new LoadoutsCollectionMessage()
            {
                Loadouts = MissionLoadouts
            };

            MessengerInstance.Send(message, FalconMessageToken.ShowEditorView);
        }
    }
}