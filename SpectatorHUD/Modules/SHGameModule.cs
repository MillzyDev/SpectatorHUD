using Boneject;
using Ninject.Modules;
using SpectatorHUD.Managers;

namespace SpectatorHUD.Modules;

// ReSharper disable once ClassNeverInstantiated.Global
public class SHGameModule : NinjectModule
{
    public override void Load()
    {
        this.BindComponentOnNewGameObject<HudManager>().InSingletonScope();
    }
}