using System.Collections.ObjectModel;
using System.Linq;
using Falcon.Core.Model;
using Falcon.Core.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Falcon.GUI.ViewModel
{
    public class ImportLoadoutsViewModel : ViewModelBase
    {
        private readonly IVirtualArsenalLoadoutService _arsenalLoadoutService;

        public RelayCommand ImportCommand { get; private set; }

        public ImportLoadoutsViewModel(IVirtualArsenalLoadoutService arsenalLoadoutService)
        {
            _arsenalLoadoutService = arsenalLoadoutService;
            
            ImportCommand = new RelayCommand(Import);
        }

        private void Import()
        {
            var arsenalFilePaths = _arsenalLoadoutService.GetPossibleConfigVarsFilepaths();
            if (arsenalFilePaths.Count == 1)
            {
                var importedLoadouts = _arsenalLoadoutService.ImportLoadouts(arsenalFilePaths.First());
                
            }
        }
    }
}