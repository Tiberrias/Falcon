using ArmaConfigParser.Configuration;
using GalaSoft.MvvmLight;
using Ninject.Extensions.Conventions;
using Ninject.Modules;

namespace Falcon.GUI.Configuration.Ninject
{
    public class FalconGuiNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(x => x.FromThisAssembly()
                .SelectAllClasses()
                .InheritedFrom<ViewModelBase>()
                .BindToSelf()
                .Configure(opt => opt.InTransientScope()));

            Bind<IConfigurationService>().To<ConfigurationService>();
        }
    }
}