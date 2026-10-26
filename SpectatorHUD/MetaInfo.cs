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

namespace SpectatorHUD;

public static class MetaInfo
{
    public static int? HUDCompatibility { get; set; }
    public static string? HUDName { get; set; }
    public static string? HUDAuthor { get; set; }
    public static int? HUDVersion { get; set; }

    public static bool HasLeftReserveCounter { get; set; }
    public static bool HasLeftAmmoCounter { get; set; }
    public static bool HasTotalLeftAmmoCounter { get; set; }
    public static bool HasRightReserveCounter { get; set; }
    public static bool HasRightAmmoCounter { get; set; }
    public static bool HasTotalRightAmmoCounter { get; set; }
    public static bool HasTotalLightAmmoCounter { get; set; }
    public static bool HasTotalMediumAmmoCounter { get; set; }
    public static bool HasTotalHeavyAmmoCounter { get; set; }
    public static bool HasHealthCounter { get; set; }
    public static bool HasMaxHealthCounter { get; set; }
    public static bool HasHealthPercentageCounter { get; set; }

    public static int ActiveWithGunInLeftHand { get; set; }
    public static int ActiveWithGunInRightHand { get; set; }

    public static int AnimationLeftGunHeld { get; set; }
    public static int AnimationRightGunHeld { get; set; }
    public static int AnimationLeftGunHeldChanged { get; set; }
    public static int AnimationRightGunHeldChanged { get; set; }

    public static int AnimationLeftAmmo { get; set; }
    public static int AnimationRightAmmo { get; set; }
    public static int AnimationLeftAmmoChanged { get; set; }
    public static int AnimationRightAmmoChanged { get; set; }

    public static int AnimationLeftReserve { get; set; }
    public static int AnimationRightReserve { get; set; }
    public static int AnimationLeftReserveChanged { get; set; }
    public static int AnimationRightReserveChanged { get; set; }
    
    public static int AnimationTotalLeftAmmo { get; set; }
    public static int AnimationTotalRightAmmo { get; set; }
    public static int AnimationTotalLightAmmo { get; set; }
    public static int AnimationTotalMediumAmmo { get; set; }
    public static int AnimationTotalHeavyAmmo { get; set; }
    public static int AnimationTotalLeftAmmoChanged { get; set; }
    public static int AnimationTotalRightAmmoChanged { get; set; }
    public static int AnimationTotalLightAmmoChanged { get; set; }
    public static int AnimationTotalMediumAmmoChanged { get; set; }
    public static int AnimationTotalHeavyAmmoChanged { get; set; }

    public static int AnimationHealth { get; set; }
    public static int AnimationMaxHealth { get; set; }
    public static int AnimationHealthPercentage { get; set; }
    public static int AnimationHealthChanged { get; set; }
    public static int AnimationMaxHealthChanged{ get; set; }

    public static int ShaderLeftAmmo { get; set; }
    public static int ShaderRightAmmo { get; set; }
    public static int ShaderLeftReserve { get; set; }
    public static int ShaderRightReserve { get; set; }
    public static int ShaderTotalLeftAmmo { get; set; }
    public static int ShaderTotalRightAmmo { get; set; }
    public static int ShaderTotalLightAmmo { get; set; }
    public static int ShaderTotalMediumAmmo { get; set; }
    public static int ShaderTotalHeavyAmmo { get; set; }
    public static int ShaderHealth { get; set; }
    public static int ShaderMaxHealth { get; set; }
    public static int ShaderHealthPercentage { get; set; }
}