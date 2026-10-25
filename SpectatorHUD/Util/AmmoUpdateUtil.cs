using Il2CppSLZ.Marrow.Data;

namespace SpectatorHUD.Util;

public static class AmmoUpdateUtil
{
    public static void UpdateReservesTargeted(CartridgeData cartridge)
    {
        int ammoTotal = HudState.AmmoInventory.GetCartridgeCount(cartridge);
        
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

    public static void UpdateReservesTargeted(string groupKey)
    {
        int ammoTotal = HudState.AmmoInventory.GetCartridgeCount(groupKey);
        
        if (groupKey == HudState.LeftDefaultCartridge?.groupKey)
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
        
        if (groupKey == HudState.RightDefaultCartridge?.groupKey)
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