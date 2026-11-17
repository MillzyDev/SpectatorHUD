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
using Il2CppSLZ.Marrow.Data;

namespace SpectatorHUD.Util;

public static class AmmoUpdateUtil
{
    public static void UpdateReservesTargeted(CartridgeData cartridge)
    {
        var hudState = HudState.Instance;
        int ammoTotal = hudState.AmmoInventory.GetCartridgeCount(cartridge);
        
        if (cartridge == hudState.LeftDefaultCartridge?.cartridge)
        {
            hudState.LeftTotalAmmo = ammoTotal;
            hudState.LeftReserve = ammoTotal - hudState.LeftAmmo;

            switch (hudState.LeftDefaultCartridge.ammoCategory)
            {
                case AmmoCategory.Unknown:
                    break;
                case AmmoCategory.Light:
                    hudState.TotalLightAmmo = ammoTotal;
                    break;
                case AmmoCategory.Medium:
                    hudState.TotalMediumAmmo = ammoTotal;
                    break;
                case AmmoCategory.Heavy:
                    hudState.TotalHeavyAmmo = ammoTotal;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return;
        }
        
        if (cartridge == hudState.RightDefaultCartridge?.cartridge)
        {
            hudState.RightTotalAmmo = ammoTotal;
            hudState.RightReserve = ammoTotal - hudState.RightAmmo;

            switch (hudState.RightDefaultCartridge.ammoCategory)
            {
                case AmmoCategory.Unknown:
                    break;
                case AmmoCategory.Light:
                    hudState.TotalLightAmmo = ammoTotal;
                    break;
                case AmmoCategory.Medium:
                    hudState.TotalMediumAmmo = ammoTotal;
                    break;
                case AmmoCategory.Heavy:
                    hudState.TotalHeavyAmmo = ammoTotal;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            return;
        }

        // Ammo category not computed ahead of time, so just do string comparison here
        string groupKey = hudState.AmmoInventory.GetGroupByCartridge(cartridge);
        if (groupKey == hudState.LightAmmoKey)
        {
            hudState.TotalLightAmmo = ammoTotal;
        }
        else if (groupKey == hudState.MediumAmmoKey)
        {
            hudState.TotalMediumAmmo = ammoTotal;
        }
        else if (groupKey == hudState.HeavyAmmoKey)
        {
            hudState.TotalHeavyAmmo = ammoTotal;
        }
    }

    public static void UpdateReservesTargeted(string groupKey)
    {
        var hudState = HudState.Instance;
        int ammoTotal = hudState.AmmoInventory.GetCartridgeCount(groupKey);
        
        if (groupKey == hudState.LeftDefaultCartridge?.groupKey)
        {
            hudState.LeftTotalAmmo = ammoTotal;
            hudState.LeftReserve = ammoTotal - hudState.LeftAmmo;

            switch (hudState.LeftDefaultCartridge.ammoCategory)
            {
                case AmmoCategory.Unknown:
                    break;
                case AmmoCategory.Light:
                    hudState.TotalLightAmmo = ammoTotal;
                    break;
                case AmmoCategory.Medium:
                    hudState.TotalMediumAmmo = ammoTotal;
                    break;
                case AmmoCategory.Heavy:
                    hudState.TotalHeavyAmmo = ammoTotal;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return;
        }
        
        if (groupKey == hudState.RightDefaultCartridge?.groupKey)
        {
            hudState.RightTotalAmmo = ammoTotal;
            hudState.RightReserve = ammoTotal - hudState.RightAmmo;

            switch (hudState.RightDefaultCartridge?.ammoCategory)
            {
                case AmmoCategory.Unknown:
                    break;
                case AmmoCategory.Light:
                    hudState.TotalLightAmmo = ammoTotal;
                    break;
                case AmmoCategory.Medium:
                    hudState.TotalMediumAmmo = ammoTotal;
                    break;
                case AmmoCategory.Heavy:
                    hudState.TotalHeavyAmmo = ammoTotal;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            return;
        }

        // Ammo category not computed ahead of time, so just do string comparison here
        if (groupKey == hudState.LightAmmoKey)
        {
            hudState.TotalLightAmmo = ammoTotal;
        }
        else if (groupKey == hudState.MediumAmmoKey)
        {
            hudState.TotalMediumAmmo = ammoTotal;
        }
        else if (groupKey == hudState.HeavyAmmoKey)
        {
            hudState.TotalHeavyAmmo = ammoTotal;
        }
    }
}