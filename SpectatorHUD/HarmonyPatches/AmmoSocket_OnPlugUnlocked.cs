using HarmonyLib;
using Il2CppSLZ.Marrow;

namespace SpectatorHUD.HarmonyPatches
{
    [HarmonyPatch(typeof(AmmoSocket))]
    [HarmonyPatch(nameof(AmmoSocket.OnPlugUnlocked))]
    public class AmmoSocket_OnPlugUnlocked
    {
        [HarmonyPostfix]
        private static void Postfix(AmmoSocket __instance)
        {
            if (__instance.gun == HudState.RightGun)
            {
                HudState.OnMagazineRemovedRight();
                Logger.Debug("Right magazine removed");
            }
            else if (__instance.gun == HudState.LeftGun)
            {
                HudState.OnMagazineRemovedLeft();
                Logger.Debug("Left magazine removed");
            }
        }
    }
}