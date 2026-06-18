/*
 *      SpectatorHUD
 *      Copyright (C) 2026  Millzy
 *
 *      This program is free software: you can redistribute it and/or modify
 *      it under the terms of the GNU Lesser General Public License as published by
 *      the Free Software Foundation, either version 3 of the License, or
 *      (at your option) any later version.
 *
 *      This program is distributed in the hope that it will be useful,
 *      but WITHOUT ANY WARRANTY; without even the implied warranty of
 *      MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *      GNU General Public License for more details.
 *
 *      You should have received a copy of the GNU Lesser General Public License
 *      along with this program.  If not, see https://www.gnu.org/licenses/.
 */

using System.Reflection;
using FieldInjector;
using HarmonyLib;
using MelonLoader;
using MelonLoader.Utils;
using SpectatorHUD;
using SpectatorHUD.Counters;
using SpectatorHUD.HarmonyPatches;
using BuildInfo = SpectatorHUD.BuildInfo;

[assembly: MelonInfo(typeof(Mod), BuildInfo.Name, BuildInfo.Version, BuildInfo.Author)]
[assembly: MelonGame("Stress Level Zero", "BONELAB")]
[assembly: MelonID(BuildInfo.Id)]
[assembly: HarmonyDontPatchAll]

namespace SpectatorHUD
{
    public sealed class Mod : MelonMod
    {
        public override void OnInitializeMelon()
        {
            this.InitLogger();
            Logger.Msg("Logger initialised");
            
            Logger.Msg("Injecting types");
            this.InjectType<HealthCounter>();
            this.InjectType<AmmoCounter>();
            this.InjectType<ReserveCounter>();
            this.InjectType<HudVersion>();
            this.InjectType<HudV1>();
            this.InjectType<HudManagerV1>();
            this.InjectType<HudBootstrap>();
            
            Logger.Msg("Patching methods");
            this.InstallPatch(typeof(Gun_AmmoCount));
            this.InstallPatch(typeof(RigManager_Start));
            
            Logger.Msg("Creating HUDs directory");
            this.CreateHudDirectory();
        }

        public override void OnDeinitializeMelon()
        {
            Logger.Msg("Unpatching methods");
            this.HarmonyInstance.UnpatchSelf();
            
            Logger.Msg("Saving config");
            Config.Instance.Save();
        }

        private void InitLogger()
        {
            Logger.Instance = this.LoggerInstance;
            
#if DEBUG
            Logger.EnableDebug(true);
#else // DEBUG
            Logger.EnableDebug(MelonDebug.IsEnabled())
#endif // DEBUG
        }

        private void CreateHudDirectory()
        {
            string path = Path.Join(MelonEnvironment.UserDataDirectory, "SpectatorHUD", "HUDs");
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to create HUDs directory.", ex);
            }
        }

        private void InstallPatch(Type patchType)
        {
            PatchClassProcessor processor = this.HarmonyInstance.CreateClassProcessor(patchType, true);
            try
            {
                List<MethodInfo> patchedMethods = processor.Patch();

                patchedMethods.ForEach(method =>
                    Logger.Debug(
                        "Patched {0}::{1}",
                        method.DeclaringType?.FullName ?? "",
                        method.Name
                    )
                );
            }
            catch (HarmonyException ex)
            {
                Logger.Error("Failed to patch method.", ex);
            }
        }

        private void InjectType<T>()
        {
#if DEBUG
            const int logLevel = 4;
#else // DEBUG
            const int logLevel = MelonDebug.IsEnabled() ? 4 : 0
#endif // DEBUG
            Logger.Debug("Injecting type {0}", typeof(T).FullName ?? "");
            SerialisationHandler.Inject<T>(logLevel);
        }
    }
}