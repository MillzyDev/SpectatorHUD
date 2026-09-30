using HarmonyLib;
using Il2CppSLZ.Marrow;
using UnityEngine;

namespace SpectatorHUD.HarmonyPatches
{
    [HarmonyPatch(typeof(RigManager))]
    [HarmonyPatch(nameof(RigManager.Start))]
    public static class RigManager_Start
    {
        [HarmonyPostfix]
        private static void Postfix()
        {
            // FIXME: TEMPORARY SOLUTION FOR BOOTSTRAPPING HUD REMOVE FOR STABLE
            var dispatcherGo = new GameObject("SpectatorHUD Dispatcher");
            dispatcherGo.AddComponent<HudBootstrap>();
        }
    }
}