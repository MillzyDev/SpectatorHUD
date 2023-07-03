using System;
using MelonLoader;
using Ninject;
using SLZ.Interaction;
using SLZ.Props.Weapons;
using SLZ.Rig;
using UnhollowerBaseLib.Attributes;
using UnityEngine;

namespace SpectatorHUD.Managers
{
    [RegisterTypeInIl2Cpp]
    public class HudValueManager : MonoBehaviour, IInitializable
    {
        private RigManager _rigManager = null!;
        private Health _health = null!;
        private Hand _leftHand = null!;
        private Hand _rightHand = null!;
        
        private Gun? _leftGun;
        private Gun? _rightGun;
        
        private float _lastHealth;
        private int _lastLeftAmmo;
        private int _lastRightAmmo;

        public event Action<float>? HealthChanged;
        public event Action<int, int>? AmmoChanged;

        public HudValueManager(IntPtr ptr) : base(ptr)
        {
        }

        private float Health
        {
            get => _health.curr_Health;
        }

        private int? LeftAmmo
        {
            get => _leftGun ? _leftGun!.AmmoCount() : null;
        }

        private int? RightAmmo
        {
            get => _rightGun ? _rightGun!.AmmoCount() : null;
        }

        [HideFromIl2Cpp]
        [Inject]
        public void Inject(RigManager rigManager)
        {
            _rigManager = rigManager;
            _health = _rigManager.health;
            _leftHand = _rigManager.physicsRig.leftHand;
            _rightHand = _rigManager.physicsRig.rightHand;

#if DEBUG
            HealthChanged += health => MelonLogger.Msg($"Health changed to: {health}");
#endif
        }
        
        [HideFromIl2Cpp]
        public void Initialize() // called after all injection is done
        {
            _leftHand.onRecieverAttached += new Action<HandReciever>(AttachToLeftHand);
            _leftHand.onRecieverDetached += new Action<HandReciever>(DetachFromLeftHand);
            _rightHand.onRecieverAttached += new Action<HandReciever>(AttachToRightHand);
            _rightHand.onRecieverDetached += new Action<HandReciever>(DetachFromRightHand);
        }

        #region Held Gun Update

        private void AttachToLeftHand(HandReciever handReciever)
        {
            var gun = _leftHand.GetComponentInParent<Gun>();
            if (!gun)
                gun = _leftHand.GetComponentInChildren<Gun>();
            _leftGun = gun == _rightGun ? null : gun;
        }

        private void DetachFromLeftHand(HandReciever handReciever)
        {
            _leftGun = null;
        }

        private void AttachToRightHand(HandReciever handReciever)
        {
            var gun = _rightHand.GetComponentInParent<Gun>();
            if (!gun)
                gun = _rightHand.GetComponentInChildren<Gun>();
            _rightGun = gun == _leftGun ? null : gun;
        }

        private void DetachFromRightHand(HandReciever handReciever)
        {
            _rightGun = null;
        }
        
        #endregion

        private void LateUpdate()
        {
            float currentHealth = Health;
            int? currentLeftAmmo = LeftAmmo;
            int? currentRightAmmo = RightAmmo;
            
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (!currentHealth.Equals(_lastHealth))
            {
                HealthChanged?.Invoke(Health);
                _lastHealth = currentHealth;
            }

            if (currentLeftAmmo != _lastLeftAmmo)
            {
                AmmoChanged?.Invoke(LeftAmmo ?? 0, RightAmmo ?? 0);
                _lastLeftAmmo = currentLeftAmmo ?? 0;
                _lastRightAmmo = currentRightAmmo ?? 0;
            } 
            else if (currentRightAmmo != _lastRightAmmo) // if the ammo changed above, we dont need to update it again.
            {
                AmmoChanged?.Invoke(LeftAmmo ?? 0, RightAmmo ?? 0);
                _lastRightAmmo = currentRightAmmo ?? 0;
            }
        }
    }
}
