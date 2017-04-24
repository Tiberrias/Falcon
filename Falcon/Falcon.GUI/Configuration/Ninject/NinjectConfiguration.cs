using ArmaConfigParser.Modules;
using Falcon.Core.Modules;
using Falcon.Utilities.Modules;
using Ninject;

namespace Falcon.GUI.Configuration.Ninject
{
    public static class NinjectConfiguration
    {
        public static IKernel LoadKernel()
        {
            return new StandardKernel(
                new UtilitiesNinjectModule(),
                new ArmaConfigParserNinjectModule(),
                new FalconCoreNinjectModule(),
                new FalconGuiNinjectModule());
        }
    }
}