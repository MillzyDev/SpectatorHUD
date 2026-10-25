using HarmonyLib;
using Il2CppSLZ.Marrow;

namespace SpectatorHUD.HarmonyPatches;

[HarmonyPatch(typeof(Health))]
[HarmonyPatch(nameof(Health.SetAvatar))]
public class Health_SetAvatar
{
    [HarmonyPostfix]
    private static void Postfix(Health __instance)
    {
        if (__instance._rigManager != HudState.RigManager)
        {
            return;
        }
        
        HudState.Health = __instance.curr_Health;
        HudState.MaxHealth = __instance.max_Health;
    }
}