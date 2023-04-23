using Boneject;
using MelonLoader;
using SpectatorHUD.Modules;

namespace SpectatorHUD;

public class Mod : MelonMod
{
    public override void OnInitializeMelon()
    {
        MelonLogger.Msg("Registering modules...");
        var bonejector = Bonejector.Instance;
        bonejector.InstallModule<SHGameModule>(InstallLocation.Game);
        MelonLogger.Msg("Finished melon initialization.");
    }
}