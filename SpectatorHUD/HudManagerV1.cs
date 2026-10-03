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
        
        public List<GameObject> activeWithGunHeldInLeftHand;
        public List<GameObject> activeWithGunHeldInRightHand;
        
        public List<AnimationBool> setBoolOnLeftGunHeld;
        public List<AnimationBool> setBoolOnRightGunHeld;
        public List<AnimationBool> setBoolOnLeftGunDropped;
        public List<AnimationBool> setBoolOnRightGunDropped;
        
        public HudManagerV1(IntPtr ptr) : base(ptr)
        {
        }

        private void OnEnable()
        {
            HudState.OnLeftGunChanged += this.OnLeftGunChanged;
            HudState.OnRightGunChanged += this.OnRightGunChanged;
            HudState.OnLeftAmmoChanged += this.UpdateLeftAmmoCounter;
            HudState.OnRightAmmoChanged += this.UpdateRightAmmoCounter;
            HudState.OnLeftReserveChanged += this.UpdateLeftReserveCounter;
            HudState.OnRightReserveChanged += this.UpdateRightReserveCounter;
            HudState.OnHealthChanged += this.UpdateHealth;
        }

        private void OnDisable()
        {
            HudState.OnLeftGunChanged -= this.OnLeftGunChanged;
            HudState.OnRightGunChanged -= this.OnRightGunChanged;
            HudState.OnLeftAmmoChanged -= this.UpdateLeftAmmoCounter;
            HudState.OnRightAmmoChanged -= this.UpdateRightAmmoCounter;
            HudState.OnLeftReserveChanged -= this.UpdateLeftReserveCounter;
            HudState.OnRightReserveChanged -= this.UpdateRightReserveCounter;
            HudState.OnHealthChanged -= this.UpdateHealth;
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

        private void UpdateHealth(float? health)
        {
            this.healthCounter.text = $"{health * 10:0.}";
        }

        private void OnLeftGunChanged(Gun? gun)
        {
            Logger.Debug("HudManagerV1: Left gun changed");
            
            if (gun == null) // no gun in hand
            {
                foreach (GameObject go in this.activeWithGunHeldInLeftHand)
                {
                    go.active = false;
                    Logger.Debug("HudManagerV1: disabled " + go.name);
                }

                foreach (AnimationBool animBool in this.setBoolOnLeftGunDropped)
                {
                    animBool.animator.SetBool(animBool.parameterName, animBool.value);
                    Logger.Debug("HudManagerV1: AnimationBool " + animBool.parameterName + " set to " + animBool.value);
                }

                return;
            }
            
            foreach (GameObject go in this.activeWithGunHeldInLeftHand)
            {
                go.active = true;
                Logger.Debug("HudManagerV1: enabled " + go.name);
            }

            foreach (AnimationBool animBool in this.setBoolOnLeftGunHeld)
            {
                animBool.animator.SetBool(animBool.parameterName, animBool.value);
                Logger.Debug("HudManagerV1: AnimationBool " + animBool.parameterName + " set to " + animBool.value);
                
            }
        }
        
        private void OnRightGunChanged(Gun? gun)
        {
            Logger.Debug("HudManagerV1: Right gun changed");
            
            if (gun == null) // no gun in hand
            {
                foreach (GameObject go in this.activeWithGunHeldInRightHand)
                {
                    Logger.Debug("HudManagerV1: disabled " + go.name);
                    go.active = false;
                }

                foreach (AnimationBool animBool in this.setBoolOnRightGunDropped)
                {
                    animBool.animator.SetBool(animBool.parameterName, animBool.value);
                    Logger.Debug("HudManagerV1: AnimationBool " + animBool.parameterName + " set to " + animBool.value);
                }

                return;
            }
            
            foreach (GameObject go in this.activeWithGunHeldInRightHand)
            {
                Logger.Debug("HudManagerV1: enabled " + go.name);
                go.active = true;
            }

            foreach (AnimationBool animBool in this.setBoolOnRightGunHeld)
            {
                animBool.animator.SetBool(animBool.parameterName, animBool.value);
                Logger.Debug("HudManagerV1: AnimationBool " + animBool.parameterName + " set to " + animBool.value);
            }
        }
    }
}