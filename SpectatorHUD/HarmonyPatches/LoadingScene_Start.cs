using HarmonyLib;
using Il2CppSLZ.Bonelab;

namespace SpectatorHUD.HarmonyPatches
{
    [HarmonyPatch(typeof(LoadingScene))]
    [HarmonyPatch(nameof(LoadingScene.Start))]
    public static class LoadingScene_Start
    {
        [HarmonyPostfix]
        public static void Postfix(LoadingScene __instance)
        {
            __instance.gameObject.AddComponent<LoadingSceneActivator>();
        }
    }
}