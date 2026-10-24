/*
 *      SpectatorHUD
 *      Copyright (C) 2026  Millzy
 *
 *      This program is free software: you can redistribute it and/or modify
 *      it under the terms of the GNU Lesser General Public License as published by
 *      the Free Software Foundation, either version 3 of the License, or
 *      (at your option) any later version.
 *
 *      This program is distributed in the hope that it will be useful,
 *      but WITHOUT ANY WARRANTY; without even the implied warranty of
 *      MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *      GNU General Public License for more details.
 *
 *      You should have received a copy of the GNU Lesser General Public License
 *      along with this program.  If not, see https://www.gnu.org/licenses/.
 */

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
        [HarmonyPatch(new[]{typeof(AmmoGroup), typeof(int)})]
        public static void PostfixAmmoGroup()
        {
            HudState.OnCartridgeAdded();
        }

        [HarmonyPostfix]
        [HarmonyPatch(new[] {typeof(CartridgeData), typeof(int)})]
        private static void PostfixCartridgeData()
        {
            HudState.OnCartridgeAdded();
        }
    }
}