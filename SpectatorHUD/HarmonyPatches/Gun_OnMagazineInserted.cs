using HarmonyLib;
using Il2CppSLZ.Marrow;

namespace SpectatorHUD.HarmonyPatches
{
    [HarmonyPatch(typeof(Gun))]
    [HarmonyPatch(nameof(Gun.OnMagazineInserted))]
    public static class Gun_OnMagazineInserted
    {
        [HarmonyPostfix]
        private static void Postfix(Gun __instance)
        {
            if (__instance == HudState.LeftGun)
            {
                HudState.OnMagazineInsertedLeft();
                Logger.Debug("Left magazine inserted");
                
            }
            else if (__instance == HudState.RightGun)
            {
                HudState.OnMagazineInsertedRight();
                Logger.Debug("Right magazine removed");
            }
        }
    }
}