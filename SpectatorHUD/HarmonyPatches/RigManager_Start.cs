using HarmonyLib;
using MelonLoader;

namespace SpectatorHUD.HarmonyPatches
{
    [HarmonyPatch(typeof(Il2CppSLZ.Marrow.RigManager))]
    [HarmonyPatch(nameof(Il2CppSLZ.Marrow.RigManager.Start))]
    public static class RigManager_Start
    {
        [HarmonyPostfix]
        // ReSharper disable once UnusedMember.Local
        private static void Postfix()
        {
            MelonLogger.Msg("My shit works");
        }
    }
}