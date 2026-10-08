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
        public TMP_Text maxHealthCounter;
        public TMP_Text percentageHealthCounter;
        
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

        private void Start()
        {
            MetaInfo.HasLeftReserveCounter = this.leftHandReserveCounter != null;
            MetaInfo.HasLeftAmmoCounter = this.leftHandAmmoCounter != null;
            MetaInfo.HasRightReserveCounter = this.rightHandReserveCounter != null;
            MetaInfo.HasRightAmmoCounter = this.rightHandAmmoCounter != null;
            MetaInfo.HasHealthCounter = this.healthCounter != null;
            MetaInfo.HasMaxHealthCounter = this.maxHealthCounter != null;
            MetaInfo.HasHealthPercentageCounter = this.percentageHealthCounter != null;

            MetaInfo.ActiveWithGunInLeftHand = this.activeWithGunHeldInLeftHand.Count;
            MetaInfo.ActiveWithGunInRightHand = this.activeWithGunHeldInRightHand.Count;

            MetaInfo.AnimationLeftGunHeld = this.animationLeftGunHeld.Count;
            MetaInfo.AnimationRightGunHeld = this.animationRightGunHeld.Count;
            MetaInfo.AnimationLeftGunHeldChanged = this.animationLeftGunHeldChanged.Count;
            MetaInfo.AnimationRightGunHeldChanged = this.animationRightGunHeldChanged.Count;

            MetaInfo.AnimationLeftAmmo = this.animationLeftAmmo.Count;
            MetaInfo.AnimationRightAmmo = this.animationRightAmmo.Count;
            MetaInfo.AnimationLeftAmmoChanged = this.animationLeftAmmoChanged.Count;
            MetaInfo.AnimationRightAmmoChanged = this.animationRightAmmoChanged.Count;

            MetaInfo.AnimationLeftReserve = this.animationLeftReserve.Count;
            MetaInfo.AnimationRightReserve = this.animationRightReserve.Count;
            MetaInfo.AnimationLeftReserveChanged = this.animationLeftReserveChanged.Count;
            MetaInfo.AnimationRightReserveChanged = this.animationRightReserveChanged.Count;

            MetaInfo.AnimationHealth = this.animationHealth.Count;
            MetaInfo.AnimationMaxHealth = this.animationMaxHealth.Count;
            MetaInfo.AnimationHealthPercentage = this.animationHealthPercentage.Count;
            MetaInfo.AnimationHealthChanged = this.animationHealthChanged.Count;
            MetaInfo.AnimationMaxHealthChanged = this.animationMaxHealthChanged.Count;
        }

        private void OnEnable()
        {
            HudState.OnLeftGunChanged += this.OnLeftGunChanged;
            HudState.OnRightGunChanged += this.OnRightGunChanged;

            if (this.leftHandAmmoCounter != null)
            {
                HudState.OnLeftAmmoChanged += this.UpdateLeftAmmoCounter;
            }

            if (this.rightHandAmmoCounter != null)
            {
                HudState.OnRightAmmoChanged += this.UpdateRightAmmoCounter;
            }

            if (this.leftHandReserveCounter != null)
            {
                HudState.OnLeftReserveChanged += this.UpdateLeftReserveCounter;
            }

            if (this.rightHandReserveCounter != null)
            {
                HudState.OnRightReserveChanged += this.UpdateRightReserveCounter;
            }

            if (this.healthCounter != null)
            {
                HudState.OnHealthChanged += this.UpdateHealthCounter;
            }

            if (this.percentageHealthCounter)
            {
                HudState.OnHealthChanged += this.UpdateHealthPercentageCounter;
            }

            if (this.maxHealthCounter)
            {
                HudState.OnMaxHealthChanged += this.UpdateMaxHealthCounter;
            }
            
            // Animation events
            HudState.OnLeftAmmoChanged += this.UpdateLeftAmmoAnimation;
            HudState.OnRightAmmoChanged += this.UpdateRightAmmoAnimation;
            HudState.OnLeftReserveChanged += this.UpdateLeftReserveAnimation;
            HudState.OnRightReserveChanged += this.UpdateRightReserveAnimation;
            HudState.OnHealthChanged += this.UpdateHealthAnimation;
            HudState.OnMaxHealthChanged += this.UpdateMaxHealthAnimation;
        }

        private void OnDisable()
        {
            HudState.OnLeftGunChanged -= this.OnLeftGunChanged;
            HudState.OnRightGunChanged -= this.OnRightGunChanged;
            
            // Null checks not needed here; no errors if callbacks not present
            HudState.OnLeftAmmoChanged -= this.UpdateLeftAmmoCounter;
            HudState.OnRightAmmoChanged -= this.UpdateRightAmmoCounter;
            HudState.OnLeftReserveChanged -= this.UpdateLeftReserveCounter;
            HudState.OnRightReserveChanged -= this.UpdateRightReserveCounter;
            HudState.OnHealthChanged -= this.UpdateHealthCounter;
            HudState.OnHealthChanged -= this.UpdateHealthPercentageCounter;
            
            // Animation events
            HudState.OnLeftAmmoChanged -= this.UpdateLeftAmmoAnimation;
            HudState.OnRightAmmoChanged -= this.UpdateRightAmmoAnimation;
            HudState.OnLeftReserveChanged -= this.UpdateLeftReserveAnimation;
            HudState.OnRightReserveChanged -= this.UpdateRightReserveAnimation;
            HudState.OnHealthChanged -= this.UpdateHealthAnimation;
            HudState.OnMaxHealthChanged -= this.UpdateMaxHealthAnimation;
        }

        private void UpdateLeftAmmoCounter(int? ammo)
        {
            this.leftHandAmmoCounter.text = ammo.ToString();
        }

        private void UpdateLeftAmmoAnimation(int? ammo)
        {
            this.SetAnimationInts(this.animationLeftAmmo, ammo ?? 0);
            this.FireAnimationTriggers(this.animationLeftAmmoChanged);
        }
        
        private void UpdateRightAmmoCounter(int? ammo)
        {
            this.rightHandAmmoCounter.text = ammo.ToString();
        }

        private void UpdateRightAmmoAnimation(int? ammo)
        {
            this.SetAnimationInts(this.animationRightAmmo, ammo ?? 0);
            this.FireAnimationTriggers(this.animationRightAmmoChanged);
        }

        private void UpdateLeftReserveCounter(int? ammo)
        {
            this.leftHandReserveCounter.text = ammo.ToString();
        }

        private void UpdateLeftReserveAnimation(int? ammo)
        {
            this.SetAnimationInts(this.animationLeftReserve, ammo ?? 0);
            this.FireAnimationTriggers(this.animationLeftReserveChanged);
        }

        private void UpdateRightReserveCounter(int? ammo)
        {
            this.rightHandReserveCounter.text = ammo.ToString();
        }

        private void UpdateRightReserveAnimation(int? ammo)
        {
            this.SetAnimationInts(this.animationRightReserve, ammo ?? 0);
            this.FireAnimationTriggers(this.animationRightReserveChanged);
        }

        private void UpdateHealthCounter(float? health, float? percentage)
        {
            this.healthCounter.text = $"{health * 10:0.}";
        }

        private void UpdateHealthPercentageCounter(float? health, float? percentage)
        {
            this.percentageHealthCounter.text = $"{percentage * 100:0.}";
        }

        private void UpdateHealthAnimation(float? health, float? percentage)
        {
            this.SetAnimationFloats(this.animationHealth, health ?? 0f); 
            this.SetAnimationFloats(this.animationHealthPercentage, percentage ?? 0);
            this.FireAnimationTriggers(this.animationHealthChanged);
        }

        private void UpdateMaxHealthCounter(float? maxHealth)
        {
            this.maxHealthCounter.text = $"{maxHealth * 10:0.}";
        }

        private void UpdateMaxHealthAnimation(float? maxHealth)
        {
            this.SetAnimationFloats(this.animationMaxHealth, maxHealth ?? 0f);
            this.FireAnimationTriggers(this.animationMaxHealthChanged);
        }

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
                trigger.animator.ResetTrigger(trigger.parameterName);
                trigger.animator.SetTrigger(trigger.parameterName);
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
    }
}