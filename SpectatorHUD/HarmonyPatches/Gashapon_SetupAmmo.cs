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