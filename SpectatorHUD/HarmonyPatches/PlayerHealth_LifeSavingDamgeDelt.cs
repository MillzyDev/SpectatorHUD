using HarmonyLib;
using Il2CppSLZ.Marrow;

namespace SpectatorHUD.HarmonyPatches;

[HarmonyPatch(typeof(Player_Health))]
[HarmonyPatch(nameof(Player_Health.LifeSavingDamgeDealt))]
public static class PlayerHealth_LifeSavingDamgeDelt
{
    [HarmonyPostfix]
    private static void Postfix(Player_Health __instance)
    {
        HudState.Health = __instance.curr_Health;
    }
}