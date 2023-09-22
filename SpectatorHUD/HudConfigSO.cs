using System;
using UnityEngine;

namespace SpectatorHUD
{
    // ReSharper disable once InconsistentNaming
    public class HudConfigSO : ScriptableObject
    {
        public CombinedAmmoCounterConfig combinedAmmoCounterConfiguration;
        public HealthCounterConfig healthCounterDisplayMode;

        public HudConfigSO(IntPtr ptr)
        {
        }

        public enum CombinedAmmoCounterConfig : ushort
        {
            CombineAlways = 0,
            CombineIfOfSameType = 1,
        }

        public enum HealthCounterConfig : ushort
        {
            Value,
            RawValue,
            Percentage,
            GlobalPercentage
        }
    }
}
