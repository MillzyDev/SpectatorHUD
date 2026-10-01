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

namespace SpectatorHUD.HarmonyPatches
{
    [HarmonyPatch(typeof(AmmoSocket))]
    [HarmonyPatch(nameof(AmmoSocket.OnPlugUnlocked))]
    public class AmmoSocket_OnPlugUnlocked
    {
        [HarmonyPostfix]
        private static void Postfix(AmmoSocket __instance)
        {
            if (__instance.gun == HudState.RightGun)
            {
                HudState.OnMagazineRemovedRight();
                Logger.Debug("Right magazine removed");
            }
            else if (__instance.gun == HudState.LeftGun)
            {
                HudState.OnMagazineRemovedLeft();
                Logger.Debug("Left magazine removed");
            }
        }
    }
}