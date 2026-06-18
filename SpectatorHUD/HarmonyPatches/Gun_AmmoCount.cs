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

using HarmonyLib;
using Il2CppSLZ.Marrow;

namespace SpectatorHUD.HarmonyPatches
{
    [HarmonyPatch(typeof(Gun))]
    [HarmonyPatch(nameof(Gun.AmmoCount))]
    public static class Gun_AmmoCount
    {
        [HarmonyPrefix]
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable InconsistentNaming
        private static bool Prefix(Gun __instance, ref int __result)
        {
            // Calls to AmmoCount when _magState is null results in a NullReferenceException on the IL2CPP side
            // This patch ensures this doesn't happen.
            if (__instance._magState != null)
            {
                return true;
            }
            
            __result = 0;
            return false;
        }
    }
}