using Ninject.Extensions.Conventions;
using Ninject.Modules;

namespace Falcon.Utilities.Modules
{
    public class UtilitiesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(x => x.FromThisAssembly()
                .SelectAllClasses()
                .BindAllInterfaces()
                .Configure(opt => opt.InTransientScope()));
        }
    }
}