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
using Il2CppSLZ.Marrow.Data;
using Il2CppTMPro;
using UnityEngine;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace SpectatorHUD
{
    public class HudManagerV1 : MonoBehaviour
    {
        public TMP_Text leftHandReserveCounter;
        public TMP_Text leftHandAmmoCounter;
        public TMP_Text rightHandReserveCounter;
        public TMP_Text rightHandAmmoCounter;
        public TMP_Text healthCounter;
        public TMP_Text maxHealthCounter;
        
        public HudManagerV1(IntPtr ptr) : base(ptr)
        {
            
        }

        private void OnEnable()
        {
            HudState.OnLeftAmmoChanged += this.UpdateLeftAmmoCounter;
            HudState.OnRightAmmoChanged += this.UpdateRightAmmoCounter;
            HudState.OnLeftReserveChanged += this.UpdateLeftReserveCounter;
            HudState.OnRightReserveChanged += this.UpdateRightReserveCounter;
        }

        private void OnDisable()
        {
            HudState.OnLeftAmmoChanged -= this.UpdateLeftAmmoCounter;
            HudState.OnRightAmmoChanged -= this.UpdateRightAmmoCounter;
            HudState.OnLeftReserveChanged -= this.UpdateLeftReserveCounter;
            HudState.OnRightReserveChanged -= this.UpdateRightReserveCounter;
        }

        private void UpdateLeftAmmoCounter(int? ammo)
        {
            this.leftHandAmmoCounter.text = ammo.ToString();
        }
        
        private void UpdateRightAmmoCounter(int? ammo)
        {
            this.rightHandAmmoCounter.text = ammo.ToString();
        }

        private void UpdateLeftReserveCounter(int? ammo)
        {
            this.leftHandReserveCounter.text = ammo.ToString();
        }

        private void UpdateRightReserveCounter(int? ammo)
        {
            this.rightHandReserveCounter.text = ammo.ToString();
        }
    }
}