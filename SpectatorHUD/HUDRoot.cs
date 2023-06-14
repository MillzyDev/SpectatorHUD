using System;
using System.Collections.Generic;
using MelonLoader;
using SpectatorHUD.Counters;
using UnityEngine;

namespace SpectatorHUD {
  [RegisterTypeInIl2Cpp]
  public class HUDRoot : MonoBehaviour {
    private readonly List<HealthCounterBase> _healthCounters = new();

    public HUDRoot(IntPtr ptr) : base(ptr) { }

    private void Start() {
      _healthCounters.AddRange(GetComponentsInChildren<HealthCounterBase>());
    }

    private void FixedUpdate() {
      foreach (HealthCounterBase? healthCounter in _healthCounters) { }
    }
  }
}
