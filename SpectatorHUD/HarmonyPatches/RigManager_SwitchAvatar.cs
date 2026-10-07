using HarmonyLib;
using Il2CppSLZ.Marrow;

namespace SpectatorHUD.HarmonyPatches
{
    [HarmonyPatch(typeof(RigManager))]
    [HarmonyPatch(nameof(RigManager.SwitchAvatar))]
    public static class RigManager_SwitchAvatar
    {
        [HarmonyPostfix]
        private static void Postfix(RigManager __instance)
        {
            if (__instance != HudState.RigManager)
            {
                return;
            }
            
            HudState.OnMaxHealthUpdated(__instance.health.max_Health);
        }
    }
}