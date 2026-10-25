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