namespace SpectatorHUD.Util;

public static class AmmoCategoryUtil
{
    public static AmmoCategory GetCategoryFromGroupKey(string groupKey)
    {
        if (groupKey == HudState.LightAmmoKey)
        {
            return AmmoCategory.Light;
        }

        if (groupKey == HudState.MediumAmmoKey)
        {
            return AmmoCategory.Medium;
        }

        if (groupKey == HudState.HeavyAmmoKey)
        {
            return AmmoCategory.Heavy;
        }

        return AmmoCategory.Unknown;
    }
}