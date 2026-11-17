/*
 *      SpectatorHUD
 *      Copyright (C) 2026  Millzy
 *
 *      This software is licensed under the terms of the Functional Source License,
 *      Version 1.1, ALv2 Future License.  You are permitted to use, copy, modify,
 *      create derivative works, publicly display, and redistribute it under the the
 *      terms of the license.
 *
 *      THE SOFTWARE IS PROVIDED "AS IS" AND WITHOUT WARRANTIES OF ANY KIND, EXPRESS OR
 *      IMPLIED, INCLUDING WITHOUT LIMITATION WARRANTIES OF FITNESS FOR A PARTICULAR
 *      PURPOSE, MERCHANTABILITY, TITLE OR NON-INFRINGEMENT.
 *
 *      You should have received a copy of the Functional Source License, Version 1.1,
 *      ALv2 Future License along with this program.  If not, see https://fsl.software/.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using FieldInjector;
using HarmonyLib;
using MelonLoader;
using MelonLoader.Utils;
using SpectatorHUD.Animation;
using SpectatorHUD.Expressions;
using SpectatorHUD.HarmonyPatches;
using SpectatorHUD.Logging;
using SpectatorHUD.Shading;
using SpectatorHUD.UI;

namespace SpectatorHUD;

public sealed class Mod : MelonMod
{
    public static bool DebugEnabled
    {
        get
        {
#if DEBUG
            return true;
#else // DEBUG
                return MelonDebug.IsEnabled();
#endif // DEBUG
        }
    }

    public override void OnInitializeMelon()
    {
        Logger logger = LogService.Logger;
        
        logger.Info("Injecting types");
        this.InjectType<LoadingSceneActivator>();
        // Animation
        this.InjectType<AnimationBoolReference>();
        this.InjectType<AnimationFloatReference>();
        this.InjectType<AnimationIntReference>();
        this.InjectType<AnimationTriggerReference>();
        this.InjectType<RandomAnimationParameter>();
        this.InjectType<TMPMaterialPropertyOverrideFloat>();
        this.InjectType<TMPMaterialPropertyOverrideColor>();
        this.InjectType<TMPMaterialPropertyOverrideInt>();
        this.InjectType<TMPMaterialPropertyOverrideVector>();
        this.InjectType<AnimationController>();
        // Shading
        this.InjectType<ShaderFloatReference>();
        this.InjectType<ShaderIntReference>();
        this.InjectType<ShaderController>();
        // Hud
        this.InjectType<DebugGUI>();
        this.InjectType<HudVersion>();
        this.InjectType<HudV1>();
        this.InjectType<LinearParameters>();
        this.InjectType<HudController>();
        this.InjectType<HudBootstrap>();
        this.InjectType<PlayerEvents>();

        logger.Info("Patching methods");
        this.InstallPatch(typeof(AmmoInventory_AddCartridge));
        this.InstallPatch(typeof(Gashapon_InsertMagazine));
        this.InstallPatch(typeof(Gashapon_SetupAmmo));
        this.InstallPatch(typeof(Gun_CompleteSlidePull));
        this.InstallPatch(typeof(Gun_OnMagazineInserted));
        this.InstallPatch(typeof(Gun_OnMagazineRemoved));
        this.InstallPatch(typeof(Health_SetAvatar));
        this.InstallPatch(typeof(HealthSetterMachine_SET_HEALTH));
        this.InstallPatch(typeof(MonoMatNew_InsertMagazine));
        this.InstallPatch(typeof(PlayerHealth_CoRegenHealth));
        this.InstallPatch(typeof(PlayerHealth_Dying));
        this.InstallPatch(typeof(PlayerHealth_LifeSavingDamgeDelt));
        this.InstallPatch(typeof(PlayerHealth_SetFullHealth));
        this.InstallPatch(typeof(PlayerHealth_STARTDATA));
        this.InstallPatch(typeof(PlayerHealth_TAKEDAMAGE));
        this.InstallPatch(typeof(PlayerHealth_ToggleInstantDeathMode));
        this.InstallPatch(typeof(PlayerHealth_UpdateHealth));

        this.InstallPatch(typeof(LoadingScene_Start));
        this.InstallPatch(typeof(PlayerMarker_OnPlayerSpawned));

        logger.Info("Creating HUDs directory");
        this.CreateHudDirectory();

        logger.Info("Registering BoneMenu");
        SettingsMenu.Register();
    }

    public override void OnDeinitializeMelon()
    {
        Logger logger = LogService.Logger;
        
        logger.Info("Unpatching methods");
        this.HarmonyInstance.UnpatchSelf();

        logger.Info("Saving config");
        Config.Instance.Save();
    }

    private void CreateHudDirectory()
    {
        Logger logger = LogService.Logger;
        
        string path = Path.Join(MelonEnvironment.UserDataDirectory, "SpectatorHUD", "HUDs");
        try
        {
            Directory.CreateDirectory(path);
        }
        catch (Exception ex)
        {
            logger.Error("Failed to create HUDs directory.", ex);
        }
    }

    private void InstallPatch(Type patchType)
    {
        Logger logger = LogService.Logger;
        
        PatchClassProcessor processor = this.HarmonyInstance.CreateClassProcessor(patchType, true);
        try
        {
            List<MethodInfo> patchedMethods = processor.Patch();

            patchedMethods.ForEach(method =>
                logger.Debug(
                    "Patched {0}::{1}",
                    method.DeclaringType?.FullName ?? "",
                    method.Name
                )
            );
        }
        catch (HarmonyException ex)
        {
            logger.Error("Failed to patch method.", ex);
        }
    }

    private void InjectType<T>()
    {
#if DEBUG
        const int logLevel = 4;
#else // DEBUG
        int logLevel = MelonDebug.IsEnabled() ? 4 : 0;
#endif // DEBUG
        
        Logger logger = LogService.Logger;
        logger.Debug("Injecting type {0}", typeof(T).FullName ?? "");
        
        SerialisationHandler.Inject<T>(logLevel);
    }
}