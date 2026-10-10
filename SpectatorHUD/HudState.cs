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
        public static RigManager? RigManager { get; private set; } = null;
        public static Gun? LeftGun { get; private set; } = null;
        public static Gun? RightGun { get; private set; } = null;
        public static CartridgeData? LeftDefaultCartridge { get; private set; }
        public static CartridgeData? RightDefaultCartridge { get; private set; }
        public static int? LeftReserve { get; private set; }
        public static int? RightReserve { get; private set; }

        public static event Action<Gun?>? OnLeftGunChanged = null;
        public static event Action<Gun?>? OnRightGunChanged = null;
        public static event Action<int?>? OnLeftAmmoChanged = null;
        public static event Action<int?>? OnRightAmmoChanged = null;
        public static event Action<int?>? OnLeftReserveChanged = null;
        public static event Action<int?>? OnRightReserveChanged = null;
        public static event Action<float?, float?>? OnHealthChanged = null;
        public static event Action<float?>? OnMaxHealthChanged = null;
        
        private static Hand? _leftHand = null;
        private static Hand? _rightHand = null;

        private static bool _delegatesConverted = false;
        private static Il2CppSystem.Action<HandReciever>? _onReceiverAttachedLeft;
        private static Il2CppSystem.Action<HandReciever>? _onReceiverDetachedLeft;
        private static Il2CppSystem.Action<HandReciever>? _onReceiverAttachedRight;
        private static Il2CppSystem.Action<HandReciever>? _onReceiverDetachedRight;
        
        
        /// <summary>
        /// Configures members for player load and invokes all callbacks to trigger initial values.
        /// Should be called after the HUD has been loaded.
        /// </summary>
        /// <param name="rigManager">Target player's RigManager</param>
        public static void OpenState(RigManager rigManager)
        {
            RigManager = rigManager;

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

            RigManager.gameObject.AddComponent<PlayerHealthWatcher>();
            
            ForceUpdateCounts();
            
            OnLeftGunChanged?.Invoke(null);
            OnRightGunChanged?.Invoke(null);
        }

        public static void ForceUpdateCounts()
        {
            UpdateLeftAmmoCount();
            UpdateRightAmmoCount();
            UpdateLeftReserveCount();
            UpdateRightReserveCount();
            UpdateHealth();
            UpdateMaxHealth();
        }

        public static void CloseState()
        {
            _leftHand?.onRecieverAttached -= _onReceiverAttachedLeft;
            _leftHand?.onRecieverDetached -= _onReceiverDetachedLeft;
            _rightHand?.onRecieverAttached -= _onReceiverAttachedRight;
            _rightHand?.onRecieverDetached -= _onReceiverDetachedRight;
            
            RigManager = null;
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
        }
        
        private static void OnReceiverAttachedLeft(HandReciever handReciever) // learn to spell SLZ
        {
            Logger.Debug("HudState: OnReceiverAttachedLeft called");
            
            if (!TryFindGunFromReceiver(handReciever, out Gun? gun))
            {
                return;
            }
            
            LeftGun = gun;
            LeftDefaultCartridge = gun?.defaultCartridge;
            OnLeftGunChanged?.Invoke(gun);
            
            UpdateLeftAmmoCount();
            UpdateLeftReserveCount();
        }

        private static void OnReceiverDetachedLeft(HandReciever handReciever)
        {
            Logger.Debug("HudState: OnReceiverDetachedLeft called");
            
            if (!TryFindGunFromReceiver(handReciever, out Gun? gun))
            {
                return;
            }
            
            LeftGun = null;
            LeftDefaultCartridge = null;
            OnLeftGunChanged?.Invoke(null);
            
            UpdateLeftAmmoCount();
            UpdateLeftReserveCount();
        }
        
        private static void OnReceiverAttachedRight(HandReciever handReciever)
        {
            Logger.Debug("HudState: OnReceiverAttachedRight called");
            
            if (!TryFindGunFromReceiver(handReciever, out Gun? gun))
            {
                return;
            }
            
            RightGun = gun;
            Logger.Debug("Gun: " + gun);
            Logger.Debug("Cartridge: " + gun?.defaultCartridge);
            RightDefaultCartridge = gun?.defaultCartridge;
            OnRightGunChanged?.Invoke(gun);
            
            UpdateRightAmmoCount();
            UpdateRightReserveCount();
        }

        private static void OnReceiverDetachedRight(HandReciever handReciever)
        {
            Logger.Debug("HudState: OnReceiverDetachedRight called");
            
            if (!TryFindGunFromReceiver(handReciever, out Gun? gun))
            {
                return;
            }
            
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
            Logger.Debug("HudState: OnMagazineInsertedLeft called");
            UpdateLeftAmmoCount();
            UpdateLeftReserveCount();
        }

        public static void OnMagazineInsertedRight()
        {
            Logger.Debug("HudState: OnMagazineInsertedRight called");
            UpdateRightAmmoCount();
            UpdateRightReserveCount();
        }
        
        public static void OnMagazineRemovedLeft()
        {
            Logger.Debug("HudState: OnMagazineRemovedLeft called");
            UpdateLeftAmmoCount();
            UpdateLeftReserveCount();
        }

        public static void OnMagazineRemovedRight()
        {
            Logger.Debug("HudState: OnMagazineRemoveRight called");
            UpdateRightAmmoCount();
            UpdateRightReserveCount();
        }

        public static void OnGachaMagazineInserted()
        {
            Logger.Debug("HudState: OnGachaMagazineInserted called");
            UpdateLeftReserveCount();
            UpdateRightReserveCount();
        }

        public static void OnCartridgeAdded()
        {
            Logger.Debug("HudState: OnCartridgeAdded called");
            UpdateLeftReserveCount();
            UpdateRightReserveCount();
        }

        // TODO: Standardize these so "On" methods are called externally, and internally call "Update" methods
        public static void OnHealthUpdated(float? current, float? percentage)
        {
            Logger.Debug("HudState: OnHealthUpdated called");
            OnHealthChanged?.Invoke(current, percentage);
        }

        public static void OnMaxHealthUpdated(float? maxHealth)
        {
            Logger.Debug("HudState: OnMaxHealthUpdated called");
            OnMaxHealthChanged?.Invoke(maxHealth);
        }

        private static void UpdateLeftAmmoCount()
        {
            Logger.Debug("HudState: Left ammo updated");
            OnLeftAmmoChanged?.Invoke(GetLeftAmmoCount());
        }

        private static void UpdateRightAmmoCount()
        {
            Logger.Debug("HudState: Right ammo updated");
            OnRightAmmoChanged?.Invoke(GetRightAmmoCount());
        }

        private static void UpdateLeftReserveCount()
        {
            if (LeftDefaultCartridge == null)
            {
                OnLeftReserveChanged?.Invoke(null);
                LeftReserve = null;
                return;
            }

            int? newReserve = AmmoInventory.Instance.GetCartridgeCount(LeftDefaultCartridge) - GetLeftAmmoCount();
            if (LeftReserve == newReserve)
            {
                Logger.Debug("HudState: Left reserve unchanged");
                return;
            }
            
            Logger.Debug("HudState: Left reserve updated");

            LeftReserve = newReserve;
            OnLeftReserveChanged?.Invoke(newReserve);
        }

        private static void UpdateRightReserveCount()
        {
            if (RightDefaultCartridge == null)
            {
                OnRightReserveChanged?.Invoke(null);
                RightReserve = null;
                return;
            }
            
            int? newReserve = AmmoInventory.Instance.GetCartridgeCount(RightDefaultCartridge) - GetRightAmmoCount();
            if (RightReserve == newReserve)
            {
                Logger.Debug("HudState: Right reserve unchanged");
                return;
            }
            
            Logger.Debug("HudState: Right reserve updated");

            RightReserve = newReserve;
            OnRightReserveChanged?.Invoke(newReserve);
        }

        private static void UpdateHealth()
        {
            Logger.Debug("HudState: Health updated");
            float? current = RigManager?.health.curr_Health;
            OnHealthUpdated(current, current / RigManager?.health.max_Health);
        }

        private static void UpdateMaxHealth()
        {
            Logger.Debug("HudState: Right reserve updated");
            OnMaxHealthUpdated(RigManager?.health.max_Health);
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

        public static void OnSlidePullLeft(Gun gun)
        {
            Logger.Debug("HudState: OnSlidePullLeft called");
            UpdateLeftAmmoCount();
        }
        
        public static void OnSlidePullRight(Gun gun)
        {
            Logger.Debug("HudState: OnSlidePullRight called");
            UpdateRightAmmoCount();
        }
    }
}