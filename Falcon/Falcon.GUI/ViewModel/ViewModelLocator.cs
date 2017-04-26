using Falcon.GUI.Configuration.Ninject;
using Ninject;

namespace Falcon.GUI.ViewModel
{
    public class ViewModelLocator
    {
        private readonly IKernel _kernel;

        public ViewModelLocator()
        {
            _kernel = NinjectConfiguration.LoadKernel();
        }

        public ShellViewModel ShellViewModel => _kernel.Get<ShellViewModel>();

        public MissionLoadoutEditorViewModel MissionLoadoutEditorViewModel => _kernel.Get<MissionLoadoutEditorViewModel>();

        public ImportLoadoutsViewModel ImportLoadoutsViewModel => _kernel.Get<ImportLoadoutsViewModel>();

        public SelectLoadoutsViewModel SelectLoadoutsViewModel => _kernel.Get<SelectLoadoutsViewModel>();
        
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}