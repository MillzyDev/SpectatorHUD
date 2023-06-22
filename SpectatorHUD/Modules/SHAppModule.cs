using Boneject.Ninject.Extensions;
using Ninject.Modules;

namespace SpectatorHUD.Modules
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class SHAppModule : NinjectModule
    {
        private readonly Config _config;

        public SHAppModule(Config config)
        {
            _config = config;
        }

        public override void Load()
        {
            this.BindConstant(_config).InSingletonScope();
        }
    }
}
