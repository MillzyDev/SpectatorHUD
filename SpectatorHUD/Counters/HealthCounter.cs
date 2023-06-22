using System;
using System.Globalization;
using MelonLoader;

namespace SpectatorHUD.Counters
{
    [RegisterTypeInIl2Cpp]
    public class HealthCounter : HealthCounterBase
    {
        public HealthCounter(IntPtr ptr) : base(ptr)
        {
        }

        public override void UpdateCounter(float value)
        {
            text!.SetText($"{value}");
        }
    }
}
