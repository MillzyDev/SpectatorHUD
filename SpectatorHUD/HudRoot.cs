using System;
using MelonLoader;
using Ninject;
using SpectatorHUD.Counters;
using SpectatorHUD.Managers;
using UnhollowerBaseLib;
using UnhollowerBaseLib.Attributes;
using UnityEngine;

namespace SpectatorHUD
{
    [RegisterTypeInIl2Cpp]
    public class HudRoot : MonoBehaviour
    {
        private HudValueManager _hudValueManager = null!;

        public HudRoot(IntPtr ptr) : base(ptr)
        {
        }

        [HideFromIl2Cpp]
        [Inject]
        public void Inject(HudValueManager hudValueManager)
        {
            _hudValueManager = hudValueManager;
        }

        private void Start()
        {
            Il2CppArrayBase<HalfLifeHealthCounter>? halfLifeHealthCounters =
                GetComponentsInChildren<HalfLifeHealthCounter>();
            Il2CppArrayBase<HealthCounter>? healthCounters = 
                GetComponentsInChildren<HealthCounter>();

            foreach (HalfLifeHealthCounter? healthCounter in halfLifeHealthCounters)
                _hudValueManager.HealthChanged += healthCounter.HealthUpdated;
            foreach (HealthCounter? healthCounter in healthCounters)
                _hudValueManager.HealthChanged += healthCounter.HealthUpdated;
        }
    }
}
