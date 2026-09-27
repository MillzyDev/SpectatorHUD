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


using Il2CppSLZ.Marrow;

namespace SpectatorHUD
{
    public static class HudEvents
    {
        public delegate void HealthChangedHandler(float value, float previous);

        public delegate void AmmoChangedHandler(int value, int previous);

        public delegate void HeldItemChangedHandler(bool objectHeld, Gun? gunHeld, bool isTriggerGrip);

        public static event HealthChangedHandler? HealthChanged;
        public static event AmmoChangedHandler? LeftAmmoChanged;
        public static event AmmoChangedHandler? RightAmmoChanged;
        public static event HeldItemChangedHandler? LeftHeldItemChanged;
        public static event HeldItemChangedHandler? RightHeldItemChanged;

        public static void OnHealthChanged(float value, float previous) => HealthChanged?.Invoke(value, previous);
        public static void OnLeftAmmoChanged(int value, int previous) => LeftAmmoChanged?.Invoke(value, previous);
        public static void OnRightAmmoChanged(int value, int previous) => RightAmmoChanged?.Invoke(value, previous);

        public static void OnLeftHeldItemChanged(bool objectHeld, Gun? gunHeld, bool isTriggerGrip) =>
            LeftHeldItemChanged?.Invoke(objectHeld, gunHeld, isTriggerGrip);
        
        public static void OnRightHeldItemChanged(bool objectHeld, Gun? gunHeld, bool isTriggerGrip) =>
            RightHeldItemChanged?.Invoke(objectHeld, gunHeld, isTriggerGrip);

    }
}