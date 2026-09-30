using HarmonyLib;
using Il2CppSLZ.Marrow;

namespace SpectatorHUD.HarmonyPatches
{
    [HarmonyPatch(typeof(Gun))]
    [HarmonyPatch(nameof(Gun.CompleteSlidePull))]
    public class Gun_CompleteSlidePull
    {
        [HarmonyPostfix]
        private static void Postfix(Gun __instance)
        {
            if (__instance == HudState.RightGun)
            {
                HudState.OnSlidePullRight(__instance);
                Logger.Debug("Right slide pulled");
                
            }
            else if (__instance == HudState.LeftGun)
            {
                HudState.OnSlidePullLeft(__instance);
                Logger.Debug("Left slide pulled");
            }
        }
    }
}