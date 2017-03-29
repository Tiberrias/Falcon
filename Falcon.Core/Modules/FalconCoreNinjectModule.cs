using Ninject.Extensions.Conventions;
using Ninject.Modules;

namespace Falcon.Core.Modules
{
    public class FalconCoreNinjectModule : NinjectModule
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