using Boneject;
using MelonLoader;
using SpectatorHUD.Modules;
using UnityEngine;

namespace SpectatorHUD;

public class Mod : MelonMod
{
    public override void OnInitializeMelon()
    {
        if (Application.platform != RuntimePlatform.WindowsPlayer)
        {
            MelonLogger.Msg("You are trying to use this on an unsupported version. Will not function.");
            return;
        }
        
        MelonLogger.Msg("Registering modules...");
        var bonejector = Bonejector.Instance;
        bonejector.InstallModule<SHGameModule>(InstallLocation.Game);
        MelonLogger.Msg("Finished melon initialization.");
    }
}