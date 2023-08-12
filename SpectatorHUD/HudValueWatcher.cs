using System;
using System.Linq;
using BoneLib;
using MelonLoader;
using SLZ.Interaction;
using SLZ.Props.Weapons;
using SLZ.Rig;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace SpectatorHUD
{
    [RegisterTypeInIl2Cpp]
    public class HudValueWatcher : MonoBehaviour
    {
        private static readonly Lazy<HudValueWatcher> s_lazy = new(Construct);

        private RigManager _rigManager = null!;
        private Hand _leftHand = null!;
        private Hand _rightHand = null!;

        private readonly Il2CppSystem.Action<HandReciever> _onLeftHandRecieverAttached;
        private readonly Il2CppSystem.Action<HandReciever> _onLeftHandRecieverDetached;
        private readonly Il2CppSystem.Action<HandReciever> _onRightHandRecieverAttached;
        private readonly Il2CppSystem.Action<HandReciever> _onRightHandRecieverDetached;

        private Action? _onValueCheck;
        public Action<float, float>? OnHealth;
        public Action<int, int, bool>? OnAmmo;

        private Health _health = null!;

        private Gun? _leftGun;
        private Gun? _rightGun;

        private float _lastHealth;
        private float _lastMaxHealth;
        private int _lastLeftAmmo;
        private int _lastRightAmmo;
        private bool _lastSameGun;

        public HudValueWatcher(IntPtr ptr) : base(ptr)
        {
            _onLeftHandRecieverAttached =
                DelegateSupport.ConvertDelegate<Il2CppSystem.Action<HandReciever>>(LeftHandAttach);
            _onLeftHandRecieverDetached =
                DelegateSupport.ConvertDelegate<Il2CppSystem.Action<HandReciever>>(LeftHandDetach);
            _onRightHandRecieverAttached =
                DelegateSupport.ConvertDelegate<Il2CppSystem.Action<HandReciever>>(RightHandAttach);
            _onRightHandRecieverDetached =
                DelegateSupport.ConvertDelegate<Il2CppSystem.Action<HandReciever>>(RightHandDetach);
        }

        public static HudValueWatcher Instance
        {
            get => s_lazy.Value;
        }
        
        private static HudValueWatcher Construct()
        {
            HudValueWatcher? any = Resources.FindObjectsOfTypeAll<HudValueWatcher>().FirstOrDefault();
            if (any) return any!; // should realistically never happen.

            var gameObject = new GameObject(typeof(HudValueWatcher).FullName);
            var hudValueWatcher = gameObject.AddComponent<HudValueWatcher>();
            DontDestroyOnLoad(hudValueWatcher);

            return hudValueWatcher;
        }

        public void BeginWatch(RigManager rigManager)
        {
            _rigManager = rigManager;
            
            _leftHand = rigManager.physicsRig.leftHand;
            _leftHand.onRecieverAttached += _onLeftHandRecieverAttached;
            _leftHand.onRecieverDetached += _onLeftHandRecieverDetached;
            
            _rightHand = rigManager.physicsRig.rightHand;
            _rightHand.onRecieverAttached += _onRightHandRecieverAttached;
            _rightHand.onRecieverDetached += _onRightHandRecieverDetached;
            
            _health = rigManager.health;
            
            _onValueCheck += CheckHealth;
            _onValueCheck += CheckAmmo;
        }

        public void EndWatch()
        {
            _leftHand.onRecieverAttached -= (Action<HandReciever>)LeftHandAttach;
            _leftHand.onRecieverDetached -= (Action<HandReciever>)LeftHandDetach;
            _rightHand.onRecieverAttached -= (Action<HandReciever>)RightHandAttach;
            _rightHand.onRecieverDetached -= (Action<HandReciever>)RightHandDetach;
            
            _rigManager = null!;
            _leftHand = null!;
            _rightHand = null!;
            _health = null!;
            
            _onValueCheck = null;
        }

        private void Update()
        {
            _onValueCheck?.Invoke();
        }

        private void CheckHealth()
        {
            float health = _health.curr_Health;
            float maxHealth = _health.max_Health;
            
            if (!health.Equals(_lastHealth) || !maxHealth.Equals(_lastMaxHealth))
                OnHealth?.Invoke(health, maxHealth);
            
            _lastHealth = health;
            _lastMaxHealth = maxHealth;
        }

        private void CheckAmmo()
        {
            int leftAmmo = _leftGun 
                ? _leftGun!.AmmoCount() + (_leftGun.isCharged ? 1 : 0) 
                : 0;
            int rightAmmo = _rightGun 
                ? _rightGun!.AmmoCount() + (_rightGun.isCharged ? 1 : 0) 
                : 0;
            bool sameGun = _leftGun == _rightGun;

            if (leftAmmo != _lastLeftAmmo || rightAmmo != _lastRightAmmo || sameGun != _lastSameGun)
                OnAmmo?.Invoke(leftAmmo, rightAmmo, sameGun);
            
            _lastLeftAmmo = leftAmmo;
            _lastRightAmmo = rightAmmo;
            _lastSameGun = sameGun;
        }

        private void LeftHandAttach(HandReciever _)
        {
            _leftGun = Player.GetGunInHand(_leftHand) ?? null;
        }

        private void LeftHandDetach(HandReciever _)
        {
            _leftGun = null;
        }

        private void RightHandAttach(HandReciever _)
        {
            _rightGun = Player.GetGunInHand(_rightHand) ?? null;
        }

        private void RightHandDetach(HandReciever _)
        {
            _rightGun = null;
        }
    }
}
