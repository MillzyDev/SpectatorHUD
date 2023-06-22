using System;
using MelonLoader;
using Ninject;
using SLZ.Rig;
using UnityEngine;

namespace SpectatorHUD.Managers
{
    [RegisterTypeInIl2Cpp]
    public class HudValueManager : MonoBehaviour
    {
        private RigManager _rigManager = null!;
        private float _lastHealth = -1f;

        public event Action<float>? HealthChanged;

        public HudValueManager(IntPtr ptr) : base(ptr)
        {
        }

        private float Health
        {
            get => _rigManager.health.curr_Health;
        }

        [Inject]
        public void Inject(RigManager rigManager)
        {
            _rigManager = rigManager;
            
#if DEBUG
            HealthChanged += health => MelonLogger.Msg($"Health changed to: {health}");
#endif
        }

        private void LateUpdate()
        {
            float currentHealth = Health;
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (!currentHealth.Equals(_lastHealth))
            {
                HealthChanged?.Invoke(Health);
                _lastHealth = currentHealth;
            }
        }
    }
}
