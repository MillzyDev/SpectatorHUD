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
using SpectatorHUD.Extensions;
using SpectatorHUD.Util;

namespace SpectatorHUD.HarmonyPatches;

[HarmonyPatch(typeof(Gun))]
[HarmonyPatch(nameof(Gun.CompleteSlidePull))]
public static class Gun_CompleteSlidePull
{
    [HarmonyPostfix]
    private static void Postfix(Gun __instance)
    {
        if (__instance == HudState.RightGun)
        {
            HudState.RightTotalAmmo = HudState.AmmoInventory.GetTotalAmmoForGun(__instance);
            HudState.RightAmmo = HudState.RightGun.GetAmmoCount();

            switch (HudState.RightDefaultCartridge?.ammoCategory)
            {
                case AmmoCategory.Light:
                    HudState.TotalLightAmmo = HudState.RightTotalAmmo;
                    break;
                case AmmoCategory.Medium:
                    HudState.TotalMediumAmmo = HudState.RightTotalAmmo;
                    break;
                case AmmoCategory.Heavy:
                    HudState.TotalHeavyAmmo = HudState.RightTotalAmmo;
                    break;
                case AmmoCategory.Unknown:
                case null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        else if (__instance == HudState.LeftGun)
        {
            HudState.LeftTotalAmmo = HudState.AmmoInventory.GetTotalAmmoForGun(__instance);
            HudState.LeftAmmo = HudState.LeftGun.GetAmmoCount();
            
            switch (HudState.LeftDefaultCartridge?.ammoCategory)
            {
                case AmmoCategory.Light:
                    HudState.TotalLightAmmo = HudState.LeftTotalAmmo;
                    break;
                case AmmoCategory.Medium:
                    HudState.TotalMediumAmmo = HudState.LeftTotalAmmo;
                    break;
                case AmmoCategory.Heavy:
                    HudState.TotalHeavyAmmo = HudState.LeftTotalAmmo;
                    break;
                case AmmoCategory.Unknown:
                case null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}