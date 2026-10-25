using HarmonyLib;
using Il2CppSLZ.Bonelab;

namespace SpectatorHUD.HarmonyPatches;

[HarmonyPatch(typeof(HealthSetterMachine))]
[HarmonyPatch(nameof(HealthSetterMachine.SET_HEALTH))]
public class HealthSetterMachine_SET_HEALTH
{
    [HarmonyPostfix]
    private static void Postfix(HealthSetterMachine __instance)
    {
        // no fucking clue where this is used, but it sets the max health so /shrug
        HudState.MaxHealth = __instance.player_Health.max_Health;
    }
}