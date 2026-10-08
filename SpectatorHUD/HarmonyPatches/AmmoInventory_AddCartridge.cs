using System.Reflection;
using HarmonyLib;
using Il2CppSLZ.Marrow;
using Il2CppSLZ.Marrow.Data;

namespace SpectatorHUD.HarmonyPatches
{
    [HarmonyPatch(typeof(AmmoInventory))]
    [HarmonyPatch(nameof(AmmoInventory.AddCartridge))]
    public static class AmmoInventory_AddCartridge
    {
        [HarmonyPostfix]
        [HarmonyPatch([typeof(AmmoGroup), typeof(int)])]
        public static void Postfix()
        {
            HudState.OnCartridgeAdded();
        }

        [HarmonyPostfix]
        [HarmonyPatch([typeof(CartridgeData), typeof(int)])]
        private static void PostfixCartridgeData()
        {
            HudState.OnCartridgeAdded();
        }
    }
}