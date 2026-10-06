using HarmonyLib;
using Il2CppSLZ.Marrow.SceneStreaming;
using Il2CppSLZ.Marrow.Zones;
using UnityEngine;

namespace SpectatorHUD.HarmonyPatches
{
    [HarmonyPatch(typeof(PlayerMarker))]
    [HarmonyPatch(nameof(PlayerMarker.OnPlayerSpawned))]
    public static class PlayerMarker_OnPlayerSpawned
    {
        [HarmonyPostfix]
        private static void Postfix(PlayerMarker __instance, GameObject go)
        {
            Logger.Debug("Player Spawned: " + go.name);
            go.AddComponent<HudBootstrap>();
        }
    }
}