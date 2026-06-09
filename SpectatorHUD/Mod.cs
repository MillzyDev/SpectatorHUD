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

using MelonLoader;
using SpectatorHUD;
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
            
            Logger.Msg("Patching methods");
            this.InstallPatch(typeof(RigManager_Start));
            
            
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

        private void InstallPatch(Type patchType)
        {
            this.HarmonyInstance.PatchAll(patchType);
        }
    }
}