/*
 *      SpectatorHUD
 *      Copyright (C) 2026  Millzy
 *
 *      This software is licensed under the terms of the Functional Source License,
 *      Version 1.1, ALv2 Future License.  You are permitted to use, copy, modify,
 *      create derivative works, publicly display, and redistribute it under the the
 *      terms of the license.
 *
 *      THE SOFTWARE IS PROVIDED "AS IS" AND WITHOUT WARRANTIES OF ANY KIND, EXPRESS OR
 *      IMPLIED, INCLUDING WITHOUT LIMITATION WARRANTIES OF FITNESS FOR A PARTICULAR
 *      PURPOSE, MERCHANTABILITY, TITLE OR NON-INFRINGEMENT.
 *
 *      You should have received a copy of the Functional Source License, Version 1.1,
 *      ALv2 Future License along with this program.  If not, see https://fsl.software/.
 */

using HarmonyLib;
using Il2CppSLZ.Marrow;
using SpectatorHUD.Extensions;

namespace SpectatorHUD.HarmonyPatches;

[HarmonyPatch(typeof(Gun))]
[HarmonyPatch(nameof(Gun.OnMagazineRemoved))]
public static class Gun_OnMagazineRemoved
{
    [HarmonyPostfix]
    private static void Postfix(Gun __instance)
    {
        if (__instance == HudState.RightGun)
        {
            HudState.RightAmmo = HudState.RightGun.GetAmmoCount();
            HudState.RightReserve = AmmoInventory.Instance.GetTotalAmmoForGun(__instance) - HudState.RightAmmo;
            Logger.Debug("Right magazine removed");
        }
        else if (__instance == HudState.LeftGun)
        {
            HudState.LeftAmmo = HudState.LeftGun.GetAmmoCount();
            HudState.LeftReserve = AmmoInventory.Instance.GetTotalAmmoForGun(__instance) - HudState.LeftAmmo;
            Logger.Debug("Left magazine removed");
        }
    }
}