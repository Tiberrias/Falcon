using Ninject.Modules;
using Ninject.Extensions.Conventions;

namespace ArmaConfigParser.Modules
{
    public class ArmaConfigParserNinjectModule : NinjectModule
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