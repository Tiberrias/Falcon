using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Falcon.Core.Model.Loadouts;
using Falcon.Core.Model.Profiles;
using Falcon.Core.Services.Interfaces;
using Falcon.GUI.Messages;
using Falcon.Utilities.Dialogs.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PropertyChanged;

namespace Falcon.GUI.ViewModel
{
    [ImplementPropertyChanged]
    public class ImportLoadoutsViewModel : ViewModelBase
    {
        private readonly IVirtualArsenalLoadoutService _arsenalLoadoutService;
        private readonly IVirtualArsenalFilesLocatorService _arsenalFilesLocatorService;
        private readonly IFileDialogService _fileDialogService;

        public bool ShowSelectFromMultipleProfiles { get; set; }

        public bool ShowImport { get; set; }

        public ObservableCollection<ProfileInfo> Profiles { get; set; }

        public RelayCommand ImportCommand { get; private set; }

        public RelayCommand SelectFileCommand { get; private set; }

        public ImportLoadoutsViewModel(
            IVirtualArsenalLoadoutService arsenalLoadoutService,
            IVirtualArsenalFilesLocatorService arsenalFilesLocatorService, IFileDialogService fileDialogService)
        {
            _arsenalLoadoutService = arsenalLoadoutService;
            _arsenalFilesLocatorService = arsenalFilesLocatorService;
            _fileDialogService = fileDialogService;

            ImportCommand = new RelayCommand(Import);
            SelectFileCommand = new RelayCommand(SelectFile);
            ShowImport = true;
        }

        public void ClearViewModel()
        {
            ShowSelectFromMultipleProfiles = false;
            ShowImport = true;

            Profiles?.Clear();
        }

        private void SelectFile()
        {
            var filename = _fileDialogService.GetFileNameDialog();
            if (String.IsNullOrEmpty(filename))
            {
                return;
            }
            var importedLoadouts = _arsenalLoadoutService.ImportLoadouts(filename);
            ProceedToNextView(importedLoadouts);
        }

        private void Import()
        {
            var arsenalFilePaths = _arsenalFilesLocatorService.GetPossibleConfigVarsFilepaths();
            switch (arsenalFilePaths.Count)
            {
                case 1:
                    var importedLoadouts = _arsenalLoadoutService.ImportLoadouts(arsenalFilePaths.First());
                    ProceedToNextView(importedLoadouts);
                    break;
                case 0:

                    break;
                default:
                    ShowSelectFromMultipleProfiles = true;
                    Profiles = GetProfileInfosCollectionFromPossiblePaths(arsenalFilePaths);
                    break;
            }
        }

        private ObservableCollection<ProfileInfo> GetProfileInfosCollectionFromPossiblePaths(IEnumerable<string> arsenalFilePaths)
        {
            return new ObservableCollection<ProfileInfo>(
                    arsenalFilePaths.Select(
                        path =>
                            new ProfileInfo
                            {
                                Path = path,
                                Name = path.Split('\\').Last()
                                    .Replace(".vars.Arma3Profile", String.Empty)
                            }));
        }

        private void ProceedToNextView(List<Loadout> importedLoadouts)
        {
            var message = new LoadoutsCollectionMessage()
            {
                Loadouts = new ObservableCollection<Loadout>(importedLoadouts)
            };

            MessengerInstance.Send(message, FalconMessageToken.ShowSelectionView);
        }
    }
}