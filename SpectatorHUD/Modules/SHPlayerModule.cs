using Boneject.Ninject.Extensions;
using Ninject;
using Ninject.Modules;
using SpectatorHUD.Managers;

namespace SpectatorHUD.Modules
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class SHPlayerModule : NinjectModule
    {
        public override void Load()
        {
            this.BindMonoBehaviourOnNewGameObject<HudValueManager>().InSingletonScope();
            Bind<HudManager>().ToSelf().InSingletonScope();
            
            _ = Kernel!.Get<HudManager>(); // force resolve
        }
    }
}
