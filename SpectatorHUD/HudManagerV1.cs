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

using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Attributes;
using Il2CppSLZ.Marrow;
using Il2CppTMPro;
using Il2CppUltEvents;
using SpectatorHUD.Counters;
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

        public UltEvent<float> leftHandReserveCounterChanged;
        public UltEvent<float> leftHandAmmoCounterChanged;
        public UltEvent<float> rightHandReserveCounterChanged;
        public UltEvent<float> rightHandAmmoCounterChanged;
        public UltEvent<float> healthCounterChanged;
        public UltEvent<float> maxHealthCounterChanged;

        public GameObject? hud = null;
        public RigManager? rigManager = null;

        private PhysicsRig? _physicsRig;
        private Hand? _leftHand;
        private Hand? _rightHand;

        private HealthCounter? _healthCounter;
        private AmmoCounter? _leftAmmoCounter;
        private AmmoCounter? _rightAmmoCounter;
        private ReserveCounter? _leftReserveCounter;
        private ReserveCounter? _rightReserveCounter;

        private Il2CppSystem.Action<HandReciever>? _onLeftHandAttached;
        private Il2CppSystem.Action<HandReciever>? _onRightHandAttached;
        private Il2CppSystem.Action<HandReciever>? _onLeftHandDetached;
        private Il2CppSystem.Action<HandReciever>? _onRightHandDetached;
        
        public HudManagerV1(IntPtr ptr) : base(ptr)
        {
            this._onLeftHandAttached =
                DelegateSupport.ConvertDelegate<Il2CppSystem.Action<HandReciever>>(this.LeftHandAttached);
            this._onRightHandAttached =
                DelegateSupport.ConvertDelegate<Il2CppSystem.Action<HandReciever>>(this.RightHandAttached);
            this._onLeftHandDetached = 
                DelegateSupport.ConvertDelegate<Il2CppSystem.Action<HandReciever>>(this.LeftHandDetached);
            this._onRightHandDetached =
                DelegateSupport.ConvertDelegate<Il2CppSystem.Action<HandReciever>>(this.RightHandDetached);
        }

        public void Start()
        {
            this._physicsRig = this.rigManager?.physicsRig;
            this._leftHand = this._physicsRig?.leftHand;
            this._rightHand = this._physicsRig?.rightHand;
            
            Gun? leftGun = this.GetGun(this._physicsRig?.leftHand);
            Gun? rightGun = this.GetGun(this._physicsRig?.rightHand);
            
            if (this.healthCounter != null)
            {
                this._healthCounter = this.healthCounter.gameObject.AddComponent<HealthCounter>();
                this._healthCounter.rigManager = this.rigManager;
                this._healthCounter.onChange = this.healthCounterChanged;
            }
            
            this._leftHand?.onRecieverAttached += this._onLeftHandAttached;
            this._leftHand?.onRecieverDetached += this._onLeftHandDetached;
            this._rightHand?.onRecieverAttached += this._onRightHandAttached;
            this._rightHand?.onRecieverDetached += this._onRightHandDetached;

            if (this.leftHandAmmoCounter != null)
            {
                this._leftAmmoCounter = this.leftHandAmmoCounter.gameObject.AddComponent<AmmoCounter>();
                this._leftAmmoCounter.rigManager = this.rigManager;
                this._leftAmmoCounter.heldGun = leftGun;
            }

            if (this.rightHandAmmoCounter != null)
            {
                this._rightAmmoCounter = this.rightHandAmmoCounter.gameObject.AddComponent<AmmoCounter>();
                this._rightAmmoCounter.rigManager = this.rigManager;
                this._rightAmmoCounter.heldGun = rightGun;
                
            }

            if (this.leftHandReserveCounter != null)
            {
                this._leftReserveCounter = this.leftHandReserveCounter.gameObject.AddComponent<ReserveCounter>();
                this._leftReserveCounter.rigManager = this.rigManager;
                this._leftReserveCounter.heldGun = leftGun;
            }

            if (this._rightReserveCounter != null)
            {
                this._rightReserveCounter = this.rightHandReserveCounter.gameObject.AddComponent<ReserveCounter>();
                this._rightReserveCounter.rigManager = this.rigManager;
                this._rightReserveCounter.heldGun = rightGun;
            }
        }
        
        private Gun? GetGun(Hand? hand)
        {
            if (hand == null)
            {
                return null;
            }

            GameObject heldObject = hand.m_CurrentAttachedGO;
            if (heldObject == null)
            {
                return null;
            }

            Logger.Debug("Getting gun component");
            var gun = heldObject.GetComponentInParent<Gun>();
            return gun;
        }

        private void LeftHandAttached(HandReciever _)
        {
            this._leftAmmoCounter?.enabled = true;
            this._leftReserveCounter?.enabled = true;
            
            Gun? heldGun = this.GetGun(this._leftHand);
            this._leftAmmoCounter?.heldGun = heldGun;
            this._leftReserveCounter?.heldGun = heldGun;
            
            Logger.Debug("Gun {0} attached", heldGun?.gameObject.name ?? "NULL");
        }

        private void RightHandAttached(HandReciever _)
        {
            this._rightAmmoCounter?.enabled = true;
            this._rightReserveCounter?.enabled = true;
            
            Gun? heldGun = this.GetGun(this._rightHand);
            this._rightAmmoCounter?.heldGun = heldGun;
            this._rightReserveCounter?.heldGun = heldGun;
            
            Logger.Debug("Gun {0} attached", this._rightAmmoCounter?.heldGun?.gameObject.name ?? "");
            
        }

        private void LeftHandDetached(HandReciever _)
        {
            Logger.Debug("Gun {0} detached", this._leftAmmoCounter?.heldGun?.gameObject.name ?? "");
            
            this._leftAmmoCounter?.heldGun = null;
            this._leftReserveCounter?.heldGun = null;
            
            this._leftAmmoCounter?.enabled = false;
            this._leftReserveCounter?.enabled = false;
        }

        private void RightHandDetached(HandReciever _)
        {
            Logger.Debug("Gun {0} detached", this._rightAmmoCounter?.heldGun?.gameObject.name ?? "");
            
            this._rightAmmoCounter?.heldGun = null;
            this._rightReserveCounter?.heldGun = null;
            
            this._rightAmmoCounter?.enabled = false;
            this._rightReserveCounter?.enabled = false;
        }
    }
}