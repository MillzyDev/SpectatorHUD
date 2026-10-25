using HarmonyLib;
using Il2CppSLZ.Marrow;

namespace SpectatorHUD.HarmonyPatches;

[HarmonyPatch(typeof(Player_Health))]
[HarmonyPatch(nameof(Player_Health.STARTDATA))]
public static class PlayerHealth_STARTDATA
{
    [HarmonyPostfix]
    private static void Postfix(Player_Health __instance)
    {
        HudState.Health = __instance.curr_Health;
    }
}