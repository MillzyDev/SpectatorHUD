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
    public static int? HUDCompatibility;
    public static string? HUDName;
    public static string? HUDAuthor;
    public static int? HUDVersion;

    public static bool HasLeftReserveCounter;
    public static bool HasLeftAmmoCounter;
    public static bool HasTotalLeftAmmoCounter;
    public static bool HasRightReserveCounter;
    public static bool HasRightAmmoCounter;
    public static bool HasTotalRightAmmoCounter;
    public static bool HasTotalLightAmmoCounter;
    public static bool HasTotalMediumAmmoCounter;
    public static bool HasTotalHeavyAmmoCounter;
    public static bool HasHealthCounter;
    public static bool HasMaxHealthCounter;
    public static bool HasHealthPercentageCounter;

    public static int ActiveWithGunInLeftHand;
    public static int ActiveWithGunInRightHand;

    public static int AnimationLeftGunHeld;
    public static int AnimationRightGunHeld;
    public static int AnimationLeftGunHeldChanged;
    public static int AnimationRightGunHeldChanged;

    public static int AnimationLeftAmmo;
    public static int AnimationRightAmmo;
    public static int AnimationLeftAmmoChanged;
    public static int AnimationRightAmmoChanged;

    public static int AnimationLeftReserve;
    public static int AnimationRightReserve;
    public static int AnimationLeftReserveChanged;
    public static int AnimationRightReserveChanged;

    public static int AnimationHealth;
    public static int AnimationMaxHealth;
    public static int AnimationHealthPercentage;
    public static int AnimationHealthChanged;
    public static int AnimationMaxHealthChanged;

    public static int ShaderLeftAmmo;
    public static int ShaderRightAmmo;
    public static int ShaderLeftReserve;
    public static int ShaderRightReserve;
    public static int ShaderTotalLeftAmmo;
    public static int ShaderTotalRightAmmo;
    public static int ShaderTotalLightAmmo;
    public static int ShaderTotalMediumAmmo;
    public static int ShaderTotalHeavyAmmo;
    public static int ShaderHealth;
    public static int ShaderMaxHealth;
    public static int ShaderHealthPercentage;
}