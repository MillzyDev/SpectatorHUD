using HarmonyLib;
using Il2CppSLZ.Marrow;

namespace SpectatorHUD.HarmonyPatches;

[HarmonyPatch(typeof(Player_Health))]
[HarmonyPatch(nameof(Player_Health.Dying))]
public static class PlayerHealth_Dying
{
    [HarmonyPostfix]
    private static void Postfix(Player_Health __instance)
    {
        // Health can get set below 0 in this function, clamp the value
        HudState.Health = Math.Max(__instance.curr_Health, 0.0f);
    }
}