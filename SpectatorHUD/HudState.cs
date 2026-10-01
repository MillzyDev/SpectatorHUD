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
using Il2CppSLZ.Marrow;
using Il2CppSLZ.Marrow.Data;
using SpectatorHUD.Watchers;

namespace SpectatorHUD
{
    public static class HudState
    {
        public static Gun? LeftGun { get; private set; } = null;
        public static Gun? RightGun { get; private set; } = null;
        public static CartridgeData? LeftDefaultCartridge { get; private set; }
        public static CartridgeData? RightDefaultCartridge { get; private set; }

        public static event Action<Gun?>? OnLeftGunChanged = null;
        public static event Action<Gun?>? OnRightGunChanged = null;
        public static event Action<int?>? OnLeftAmmoChanged = null;
        public static event Action<int?>? OnRightAmmoChanged = null;
        public static event Action<int?>? OnLeftReserveChanged = null;
        public static event Action<int?>? OnRightReserveChanged = null;
        public static event Action<float?>? OnHealthChanged = null;
        
        private static RigManager? _rigManager = null;
        private static Hand? _leftHand = null;
        private static Hand? _rightHand = null;

        private static bool _delegatesConverted = false;
        private static Il2CppSystem.Action<HandReciever>? _onReceiverAttachedLeft;
        private static Il2CppSystem.Action<HandReciever>? _onReceiverDetachedLeft;
        private static Il2CppSystem.Action<HandReciever>? _onReceiverAttachedRight;
        private static Il2CppSystem.Action<HandReciever>? _onReceiverDetachedRight;
        private static Il2CppSystem.Action<Gun>? _onGunFireLeft;
        private static Il2CppSystem.Action<Gun>? _onGunFireRight;
        
        
        /// <summary>
        /// Configures members for player load and invokes all callbacks to trigger initial values.
        /// Should be called after the HUD has been loaded.
        /// </summary>
        /// <param name="rigManager">Target player's RigManager</param>
        public static void OpenState(RigManager rigManager)
        {
            _rigManager = rigManager;

            // Hands (no way)
            PhysicsRig physicsRig = rigManager.physicsRig;
            _leftHand = physicsRig.leftHand; // <--- so this gets the left hand
            _rightHand = physicsRig.rightHand; // that's right! this one gets the right hand! aren't you clever!

            if (!_delegatesConverted)
            {
                ConvertDelegates();
                _delegatesConverted = true;
            }

            _leftHand.onRecieverAttached += _onReceiverAttachedLeft;
            _leftHand.onRecieverDetached += _onReceiverDetachedLeft;
            _rightHand.onRecieverAttached += _onReceiverAttachedRight;
            _rightHand.onRecieverDetached += _onReceiverDetachedRight;

            _rigManager.gameObject.AddComponent<PlayerHealthWatcher>();
            
            UpdateLeftAmmoCount();
            UpdateRightAmmoCount();
            UpdateLeftReserveCount();
            UpdateRightReserveCount();
            UpdateHealth();
        }

        public static void CloseState()
        {
            _leftHand?.onRecieverAttached -= _onReceiverAttachedLeft;
            _leftHand?.onRecieverDetached -= _onReceiverDetachedLeft;
            _rightHand?.onRecieverAttached -= _onReceiverAttachedRight;
            _rightHand?.onRecieverDetached -= _onReceiverDetachedRight;
            
            _rigManager = null;
            _leftHand = null;
            _rightHand = null;
        }

        private static void ConvertDelegates()
        {
            _onReceiverAttachedLeft =
                DelegateSupport.ConvertDelegate<Il2CppSystem.Action<HandReciever>>(OnReceiverAttachedLeft);
            _onReceiverDetachedLeft =
                DelegateSupport.ConvertDelegate<Il2CppSystem.Action<HandReciever>>(OnReceiverDetachedLeft);
            _onReceiverAttachedRight =
                DelegateSupport.ConvertDelegate<Il2CppSystem.Action<HandReciever>>(OnReceiverAttachedRight);
            _onReceiverDetachedRight =
                DelegateSupport.ConvertDelegate<Il2CppSystem.Action<HandReciever>>(OnReceiverDetachedRight);
            _onGunFireLeft = DelegateSupport.ConvertDelegate<Il2CppSystem.Action<Gun>>(OnGunFireLeft);
            _onGunFireRight = DelegateSupport.ConvertDelegate<Il2CppSystem.Action<Gun>>(OnGunFireRight);
        }
        
        private static void OnReceiverAttachedLeft(HandReciever handReciever) // learn to spell SLZ
        {
            if (!TryFindGunFromReceiver(handReciever, out Gun? gun))
            {
                return;
            }

            gun?.onFireDelegate += _onGunFireLeft;
            LeftGun = gun;
            LeftDefaultCartridge = gun?.defaultCartridge;
            OnLeftGunChanged?.Invoke(gun);
            
            UpdateLeftAmmoCount();
            UpdateLeftReserveCount();
        }

        private static void OnReceiverDetachedLeft(HandReciever handReciever)
        {
            if (!TryFindGunFromReceiver(handReciever, out Gun? gun))
            {
                return;
            }

            gun?.onFireDelegate -= _onGunFireLeft;
            LeftGun = null;
            LeftDefaultCartridge = null;
            OnLeftGunChanged?.Invoke(null);
            
            UpdateLeftAmmoCount();
            UpdateLeftReserveCount();
        }
        
        private static void OnReceiverAttachedRight(HandReciever handReciever)
        {
            if (!TryFindGunFromReceiver(handReciever, out Gun? gun))
            {
                return;
            }

            gun?.onFireDelegate += _onGunFireRight;
            RightGun = gun;
            RightDefaultCartridge = gun?.defaultCartridge;
            OnRightGunChanged?.Invoke(gun);
            
            UpdateRightAmmoCount();
            UpdateRightReserveCount();
        }

        private static void OnReceiverDetachedRight(HandReciever handReciever)
        {
            if (!TryFindGunFromReceiver(handReciever, out Gun? gun))
            {
                return;
            }

            gun?.onFireDelegate -= _onGunFireRight;
            RightGun = null;
            RightDefaultCartridge = null;
            OnRightGunChanged?.Invoke(null);
            
            UpdateRightAmmoCount();
            UpdateRightReserveCount();
        }

        private static bool TryFindGunFromReceiver(HandReciever handReciever, out Gun? gun)
        {
            gun = handReciever.GetComponentInParent<Gun>();
            if (gun == null)
            {
                return false;
            }
            
            return gun.triggerGrip == handReciever;
        }

        public static void OnMagazineInsertedLeft()
        {
            UpdateLeftAmmoCount();
            UpdateLeftReserveCount();
        }

        public static void OnMagazineInsertedRight()
        {
            UpdateRightAmmoCount();
            UpdateRightReserveCount();
        }
        
        public static void OnMagazineRemovedLeft()
        {
            UpdateLeftAmmoCount();
            UpdateLeftReserveCount();
        }

        public static void OnMagazineRemovedRight()
        {
            UpdateRightAmmoCount();
            UpdateRightReserveCount();
        }

        public static void OnGachaMagazineInserted()
        {
            UpdateLeftReserveCount();
            UpdateRightReserveCount();
        }

        public static void OnHealthUpdated(float? value)
        {
            OnHealthChanged?.Invoke(value);
        }

        private static void UpdateLeftAmmoCount()
        {
            OnLeftAmmoChanged?.Invoke(GetLeftAmmoCount());
        }

        private static void UpdateRightAmmoCount()
        {
            OnRightAmmoChanged?.Invoke(GetRightAmmoCount());
        }

        private static void UpdateLeftReserveCount()
        {
            if (LeftDefaultCartridge == null)
            {
                OnLeftReserveChanged?.Invoke(null);
                return;
            }
            OnLeftReserveChanged?.Invoke(AmmoInventory.Instance.GetCartridgeCount(LeftDefaultCartridge) - GetLeftAmmoCount());
        }

        private static void UpdateRightReserveCount()
        {
            if (RightDefaultCartridge == null)
            {
                OnRightReserveChanged?.Invoke(null);
                return;
            }
            OnRightReserveChanged?.Invoke(AmmoInventory.Instance.GetCartridgeCount(RightDefaultCartridge) - GetRightAmmoCount());
        }

        private static void UpdateHealth()
        {
            OnHealthUpdated(_rigManager?.health.curr_Health);
        }

        public static int? GetLeftAmmoCount()
        {
            return GetAmmoCount(LeftGun);
        }

        public static int? GetRightAmmoCount()
        {
            return GetAmmoCount(RightGun);
        }

        public static int? GetAmmoCount(Gun? gun)
        {
            if (gun == null)
            {
                return null;
            }
            
            if (gun?._magState != null)
            {
                return gun.chamberedCartridge != null ? gun._magState.AmmoCount + 1 : gun._magState.AmmoCount;
            }

            if (gun?.chamberedCartridge != null)
            {
                return 1;
            }

            return 0;
        }

        public static void OnGunFireLeft(Gun gun)
        {
            UpdateLeftAmmoCount();
            UpdateLeftReserveCount();
        }

        public static void OnGunFireRight(Gun gun)
        {
            UpdateRightAmmoCount();
            UpdateLeftReserveCount();
        }

        public static void OnSlidePullLeft(Gun gun)
        {
            UpdateLeftAmmoCount();
            UpdateLeftReserveCount();
        }
        
        public static void OnSlidePullRight(Gun gun)
        {
            UpdateRightAmmoCount();
            UpdateLeftReserveCount();
        }
    }
}