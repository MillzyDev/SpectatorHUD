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
using Il2CppSLZ.Marrow.Data;
using SpectatorHUD.Util;

namespace SpectatorHUD.HarmonyPatches;

[HarmonyPatch(typeof(Control_MonoMatNew))]
[HarmonyPatch(nameof(Control_MonoMatNew.InsertMagazine))]
public static class MonoMatNew_InsertMagazine
{
    [HarmonyPostfix]
    private static void Postfix(Magazine magazine)
    {
        CartridgeData cartridge = magazine.magazineState.cartridgeData;
        int ammoTotal = AmmoInventory.Instance.GetCartridgeCount(cartridge);
        
        if (cartridge == HudState.LeftDefaultCartridge?.cartridge)
        {
            HudState.LeftTotalAmmo = ammoTotal;
            HudState.LeftReserve = ammoTotal - HudState.LeftAmmo;

            switch (HudState.LeftDefaultCartridge.ammoCategory)
            {
                case AmmoCategory.Unknown:
                    break;
                case AmmoCategory.Light:
                    HudState.TotalLightAmmo = ammoTotal;
                    break;
                case AmmoCategory.Medium:
                    HudState.TotalMediumAmmo = ammoTotal;
                    break;
                case AmmoCategory.Heavy:
                    HudState.TotalHeavyAmmo = ammoTotal;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return;
        }
        
        if (cartridge == HudState.RightDefaultCartridge?.cartridge)
        {
            HudState.RightTotalAmmo = ammoTotal;
            HudState.RightReserve = ammoTotal - HudState.RightAmmo;

            switch (HudState.RightDefaultCartridge.ammoCategory)
            {
                case AmmoCategory.Unknown:
                    break;
                case AmmoCategory.Light:
                    HudState.TotalLightAmmo = ammoTotal;
                    break;
                case AmmoCategory.Medium:
                    HudState.TotalMediumAmmo = ammoTotal;
                    break;
                case AmmoCategory.Heavy:
                    HudState.TotalHeavyAmmo = ammoTotal;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            return;
        }

        // Ammo category not computed ahead of time, so just do string comparison here
        string groupKey = HudState.AmmoInventory.GetGroupByCartridge(cartridge);
        if (groupKey == HudState.LightAmmoKey)
        {
            HudState.TotalLightAmmo = ammoTotal;
        }
        else if (groupKey == HudState.MediumAmmoKey)
        {
            HudState.TotalMediumAmmo = ammoTotal;
        }
        else if (groupKey == HudState.HeavyAmmoKey)
        {
            HudState.TotalHeavyAmmo = ammoTotal;
        }
    }
}