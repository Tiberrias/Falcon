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

        public ShellViewModel ShellViewModel
        {
            get { return _kernel.Get<ShellViewModel>(); }
        }

        public MissionLoadoutEditorViewModel MissionLoadoutEditorViewModel
        {
            get { return _kernel.Get<MissionLoadoutEditorViewModel>(); }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}