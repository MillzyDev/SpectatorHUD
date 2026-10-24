using HarmonyLib;
using Il2CppSLZ.Marrow;

namespace SpectatorHUD.HarmonyPatches;

[HarmonyPatch(typeof(Player_Health._CoRegenHealth_d__78))]
[HarmonyPatch(nameof(Player_Health._CoRegenHealth_d__78.MoveNext))]
public static class PlayerHealth_CoRegenHealth
{
    [HarmonyPostfix]
    private static void Postfix(Player_Health._CoRegenHealth_d__78 __instance)
    {
        if (__instance._timer_5__2 > 0.0)
        {
            return;
        } 
        
        HudState.Health = __instance.__4__this.curr_Health;
    }
}