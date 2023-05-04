using Boneject.Ninject.Extensions;
using Ninject;
using Ninject.Modules;
using SpectatorHUD.Managers;

namespace SpectatorHUD.Modules;

// ReSharper disable once ClassNeverInstantiated.Global
internal class SHPlayerModule : NinjectModule
{
    public override void Load()
    {
        this.BindInterfacesAndSelfTo<HUDManager>().InSingletonScope();
        _ = Kernel!.Get<HUDManager>();
    }
}