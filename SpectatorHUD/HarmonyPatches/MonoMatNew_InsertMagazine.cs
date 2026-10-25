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