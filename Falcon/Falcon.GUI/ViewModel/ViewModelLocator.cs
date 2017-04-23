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

        public MainViewModel Main
        {
            get { return _kernel.Get<MainViewModel>(); }
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