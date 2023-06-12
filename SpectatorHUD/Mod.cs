using Boneject.MelonLoader;
using Boneject.MelonLoader.Attributes;
using Boneject.Ninject;
using SpectatorHUD.Modules;

namespace SpectatorHUD;

public class Mod : InjectableMelonMod
{
    [OnInitialize]
    // ReSharper disable once UnusedMember.Global
    public void OnInitialize(Bonejector bonejector)
    {
        bonejector.Load<SHAppModule>(Context.App, Config.Load());
        bonejector.Load<SHPlayerModule>(Context.Player);
    }
}