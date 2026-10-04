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

using System.Collections;
using Il2CppSLZ.Marrow;
using Il2CppTMPro;
using MelonLoader;
using SpectatorHUD.Animation;
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
        
        #region Animation Events
        public List<AnimationBoolReference> animationLeftGunHeld;
        public List<AnimationBoolReference> animationRightGunHeld;
        public List<AnimationTriggerReference> animationLeftGunHeldChanged;
        public List<AnimationTriggerReference> animationRightGunHeldChanged;

        public List<AnimationIntReference> animationLeftAmmo;
        public List<AnimationIntReference> animationRightAmmo;
        public List<AnimationTriggerReference> animationLeftAmmoChanged;
        public List<AnimationTriggerReference> animationRightAmmoChanged;
        
        public List<AnimationIntReference> animationLeftReserve;
        public List<AnimationIntReference> animationRightReserve;
        public List<AnimationTriggerReference> animationLeftReserveChanged;
        public List<AnimationTriggerReference> animationRightReserveChanged;

        public List<AnimationFloatReference> animationHealth;
        public List<AnimationFloatReference> animationMaxHealth;
        public List<AnimationFloatReference> animationHealthPercentage;
        public List<AnimationTriggerReference> animationHealthChanged;
        public List<AnimationTriggerReference> animationMaxHealthChanged;
        #endregion
        
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
            this.SetAnimationInts(this.animationLeftAmmo, ammo ?? 0);
            this.FireAnimationTriggers(this.animationLeftAmmoChanged);
            
            this.leftHandAmmoCounter.text = ammo.ToString();
        }
        
        private void UpdateRightAmmoCounter(int? ammo)
        {
            this.SetAnimationInts(this.animationRightAmmo, ammo ?? 0);
            this.FireAnimationTriggers(this.animationRightAmmoChanged);
            
            this.rightHandAmmoCounter.text = ammo.ToString();
        }

        private void UpdateLeftReserveCounter(int? ammo)
        {
            this.SetAnimationInts(this.animationLeftReserve, ammo ?? 0);
            this.FireAnimationTriggers(this.animationLeftReserveChanged);
            
            this.leftHandReserveCounter.text = ammo.ToString();
        }

        private void UpdateRightReserveCounter(int? ammo)
        {
            this.SetAnimationInts(this.animationRightReserve, ammo ?? 0);
            this.FireAnimationTriggers(this.animationRightReserveChanged);
            
            this.rightHandReserveCounter.text = ammo.ToString();
        }

        private void UpdateHealth(float? health)
        {
            this.SetAnimationFloats(this.animationHealth, health ?? 0f);
            this.FireAnimationTriggers(this.animationHealthChanged);
            
            this.healthCounter.text = $"{health * 10:0.}";
        }
        
        // TODO: Max Health
        // TODO: Percentage Health

        private void OnLeftGunChanged(Gun? gun)
        {
            Logger.Debug("HudManagerV1: Left gun changed");
            this.FireAnimationTriggers(this.animationLeftGunHeldChanged);
            
            if (gun == null) // no gun in hand
            {
                foreach (GameObject go in this.activeWithGunHeldInLeftHand)
                {
                    go.active = false;
                    Logger.Debug("HudManagerV1: disabled " + go.name);
                }
                
                this.SetAnimationBools(this.animationLeftGunHeld, false);
                return;
            }
            
            foreach (GameObject go in this.activeWithGunHeldInLeftHand)
            {
                go.active = true;
                Logger.Debug("HudManagerV1: enabled " + go.name);
            }
            
            this.SetAnimationBools(this.animationLeftGunHeld, true);
        }
        
        private void OnRightGunChanged(Gun? gun)
        {
            Logger.Debug("HudManagerV1: Right gun changed");
            this.FireAnimationTriggers(this.animationRightGunHeldChanged);
            
            if (gun == null) // no gun in hand
            {
                foreach (GameObject go in this.activeWithGunHeldInRightHand)
                {
                    Logger.Debug("HudManagerV1: disabled " + go.name);
                    go.active = false;
                }

                this.SetAnimationBools(this.animationRightGunHeld, false);
                return;
            }
            
            foreach (GameObject go in this.activeWithGunHeldInRightHand)
            {
                Logger.Debug("HudManagerV1: enabled " + go.name);
                go.active = true;
            }
            
            this.SetAnimationBools(this.animationRightGunHeld, true);
        }

        private void FireAnimationTriggers(List<AnimationTriggerReference> triggers)
        {
            foreach (AnimationTriggerReference trigger in triggers)
            {
                MelonCoroutines.Start(this.SingleFrameAnimationTrigger(trigger));
            }
        }

        private void SetAnimationBools(List<AnimationBoolReference> bools, bool value)
        {
            foreach (AnimationBoolReference @bool in bools)
            {
                @bool.animator.SetBool(@bool.parameterName, value);
            }
        }

        private void SetAnimationFloats(List<AnimationFloatReference> floats, float value)
        {
            foreach (AnimationFloatReference @float in floats)
            {
                @float.animator.SetFloat(@float.parameterName, value);
            }
        }

        private void SetAnimationInts(List<AnimationIntReference> ints, int value)
        {
            foreach (AnimationIntReference @int in ints)
            {
                @int.animator.SetInteger(@int.parameterName, value);
            }
        }

        private IEnumerator SingleFrameAnimationTrigger(AnimationTriggerReference trigger)
        {
            trigger.animator.SetTrigger(trigger.parameterName);
            yield return null;
            yield return null;
            trigger.animator.ResetTrigger(trigger.parameterName);
        }
    }
}