using System.Collections.ObjectModel;
using Falcon.Core.Model.Loadouts;
using Falcon.GUI.Messages;
using GalaSoft.MvvmLight;
using PropertyChanged;

namespace Falcon.GUI.ViewModel
{
    [ImplementPropertyChanged]
    public class FalconViewModel : ViewModelBase
    {
        public ImportLoadoutsViewModel ImportLoadoutsViewModel { get; private set; }

        public MissionLoadoutEditorViewModel MissionLoadoutEditorViewModel { get; private set; }

        public SelectLoadoutsViewModel SelectLoadoutsViewModel { get; private set; }
        
        public ViewModelBase CurrentViewModel { get; set; }

        public FalconViewModel(ImportLoadoutsViewModel importLoadoutsViewModel,  SelectLoadoutsViewModel selectLoadoutsViewModel, MissionLoadoutEditorViewModel missionLoadoutEditorViewModel)
        {
            ImportLoadoutsViewModel = importLoadoutsViewModel;
            SelectLoadoutsViewModel = selectLoadoutsViewModel;
            MissionLoadoutEditorViewModel = missionLoadoutEditorViewModel;

            CurrentViewModel = ImportLoadoutsViewModel;

            MessengerInstance.Register<ShowMessage>(this, FalconMessageToken.ShowImportView, message => ShowImportView());
            MessengerInstance.Register<LoadoutsCollectionMessage>(this, FalconMessageToken.ShowSelectionView, message => ShowSelectionView(message.Loadouts));
            MessengerInstance.Register<LoadoutsCollectionMessage>(this, FalconMessageToken.ShowEditorView, message => ShowEditorView(message.Loadouts));
        }

        private void ShowImportView()
        {
            CurrentViewModel = ImportLoadoutsViewModel;
        }

        private void ShowSelectionView(ObservableCollection<Loadout> importedLoadouts)
        {
            SelectLoadoutsViewModel.ArsenalLoadouts = importedLoadouts;
            SelectLoadoutsViewModel.MissionLoadouts.Clear();

            CurrentViewModel = SelectLoadoutsViewModel;
        }

        private void ShowEditorView(ObservableCollection<Loadout> selectedLoadouts)
        {
            MissionLoadoutEditorViewModel.MissionLoadouts = selectedLoadouts;

            CurrentViewModel = MissionLoadoutEditorViewModel;
        }

    }
}