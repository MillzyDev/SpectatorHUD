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
using Il2CppSLZ.Bonelab;
using Il2CppSLZ.Marrow;
using SpectatorHUD.Util;

namespace SpectatorHUD.HarmonyPatches;

[HarmonyPatch(typeof(Control_Gashapon))]
[HarmonyPatch(nameof(Control_Gashapon.SetupAmmo))]
public static class Gashapon_SetupAmmo
{
    [HarmonyPostfix]
    private static void Postfix()
    {
        // this patch could totally be nothing;
        // since the original method doesn't seem to be called or referenced from anywhere in the game
        AmmoInventory ammoInventory = HudState.AmmoInventory;
        int lightAmmo = ammoInventory.GetCartridgeCount(HudState.LightAmmoKey);
        int mediumAmmo = ammoInventory.GetCartridgeCount(HudState.MediumAmmoKey);
        int heavyAmmo = ammoInventory.GetCartridgeCount(HudState.HeavyAmmoKey);

        HudState.TotalLightAmmo = lightAmmo;
        HudState.TotalMediumAmmo = mediumAmmo;
        HudState.TotalMediumAmmo = heavyAmmo;

        if (HudState.RightDefaultCartridge != null)
        {
            switch (HudState.RightDefaultCartridge.ammoCategory)
            {
                case AmmoCategory.Unknown:
                    HudState.RightTotalAmmo = ammoInventory.GetCartridgeCount(HudState.RightDefaultCartridge.cartridge);
                    HudState.RightReserve = HudState.RightTotalAmmo - HudState.RightAmmo;
                    break;
                case AmmoCategory.Light:
                    HudState.RightTotalAmmo = lightAmmo;
                    HudState.RightReserve = lightAmmo - HudState.RightAmmo;
                    break;
                case AmmoCategory.Medium:
                    HudState.RightTotalAmmo = mediumAmmo;
                    HudState.RightReserve = mediumAmmo - HudState.RightAmmo;
                    break;
                case AmmoCategory.Heavy:
                    HudState.RightTotalAmmo = heavyAmmo;
                    HudState.RightReserve = heavyAmmo - HudState.RightAmmo;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        if (HudState.LeftDefaultCartridge != null)
        {
            switch (HudState.LeftDefaultCartridge.ammoCategory)
            {
                case AmmoCategory.Unknown:
                    HudState.LeftTotalAmmo = ammoInventory.GetCartridgeCount(HudState.LeftDefaultCartridge.cartridge);
                    HudState.LeftReserve = HudState.LeftTotalAmmo - HudState.LeftAmmo;
                    break;
                case AmmoCategory.Light:
                    HudState.LeftTotalAmmo = lightAmmo;
                    HudState.LeftReserve = lightAmmo - HudState.LeftAmmo;
                    break;
                case AmmoCategory.Medium:
                    HudState.LeftTotalAmmo = mediumAmmo;
                    HudState.LeftReserve = mediumAmmo - HudState.LeftAmmo;
                    break;
                case AmmoCategory.Heavy:
                    HudState.LeftTotalAmmo = heavyAmmo;
                    HudState.LeftReserve = heavyAmmo - HudState.LeftAmmo;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}