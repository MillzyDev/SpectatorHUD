using HarmonyLib;
using Il2CppSLZ.Marrow;

namespace SpectatorHUD.HarmonyPatches;

[HarmonyPatch(typeof(Player_Health))]
[HarmonyPatch(nameof(Player_Health.SetFullHealth))]
public static class PlayerHealth_SetFullHealth
{
    [HarmonyPostfix]
    private static void Postfix(Player_Health __instance)
    {
        HudState.Health = __instance.curr_Health;
    }
}