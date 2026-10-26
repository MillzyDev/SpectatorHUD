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