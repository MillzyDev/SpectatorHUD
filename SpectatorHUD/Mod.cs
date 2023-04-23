using System;
using Boneject;
using MelonLoader;
using SpectatorHUD.Managers;
using SpectatorHUD.Modules;
using UnityEngine;

namespace SpectatorHUD;

public class Mod : MelonMod
{
    public override void OnInitializeMelon()
    {
        if (Application.platform != RuntimePlatform.WindowsPlayer)
            throw new PlatformNotSupportedException("SpectatorHUD only works on PC.");

        MelonLogger.Msg("Registering modules...");
        
        var bonejector = Bonejector.Instance;
        bonejector.InstallModule<SHGameModule>(InstallLocation.Game);
        
        MelonLogger.Msg("Finished melon initialization.");
    }

    public override void OnLateInitializeMelon()
    {
        GlobalDependencies.AddDependency(
            new AssetManager<GameObject>(
                "SpectatorHUD.Resources.spectatorhud.bundle",
                "SpectatorHUD"
            ));
    }
}