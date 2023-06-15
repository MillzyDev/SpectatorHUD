using System;
using System.Collections.Generic;
using MelonLoader;
using SpectatorHUD.Counters;
using SpectatorHUD.Managers;
using UnityEngine;

namespace SpectatorHUD
{
    [RegisterTypeInIl2Cpp]
    public class HudRoot : MonoBehaviour
    {
        private readonly List<HealthCounterBase> _healthCounters = new();
        internal HudValueManager HudValueManager = null!;

        public HudRoot(IntPtr ptr) : base(ptr) { }

        private void Start()
        {
            _healthCounters.AddRange(GetComponentsInChildren<HealthCounterBase>());
        }

        private void FixedUpdate()
        {
            foreach (HealthCounterBase? healthCounter in _healthCounters)
                healthCounter.Value = HudValueManager.Health;
        }
    }
}
