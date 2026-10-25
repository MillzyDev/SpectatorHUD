using HarmonyLib;
using Il2CppSLZ.Marrow;
using SpectatorHUD.Extensions;

namespace SpectatorHUD.HarmonyPatches;

[HarmonyPatch(typeof(Gun))]
[HarmonyPatch(nameof(Gun.OnMagazineRemoved))]
public static class Gun_OnMagazineRemoved
{
    [HarmonyPostfix]
    private static void Postfix(Gun __instance)
    {
        if (__instance == HudState.RightGun)
        {
            HudState.RightAmmo = HudState.RightGun.GetAmmoCount();
            HudState.RightReserve = AmmoInventory.Instance.GetTotalAmmoForGun(__instance) - HudState.RightAmmo;
            Logger.Debug("Right magazine removed");
        }
        else if (__instance == HudState.LeftGun)
        {
            HudState.LeftAmmo = HudState.LeftGun.GetAmmoCount();
            HudState.LeftReserve = AmmoInventory.Instance.GetTotalAmmoForGun(__instance) - HudState.LeftAmmo;
            Logger.Debug("Left magazine removed");
        }
    }
}