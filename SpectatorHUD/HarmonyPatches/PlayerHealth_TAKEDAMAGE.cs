using HarmonyLib;
using Il2CppSLZ.Marrow;

namespace SpectatorHUD.HarmonyPatches;

[HarmonyPatch(typeof(Player_Health))]
[HarmonyPatch(nameof(Player_Health.TAKEDAMAGE))]
public class PlayerHealth_TAKEDAMAGE
{
    [HarmonyPostfix]
    private static void Postfix(Player_Health __instance)
    {
        HudState.Health = __instance.curr_Health;
    }
}