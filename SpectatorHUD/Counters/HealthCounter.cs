using System;
using System.Globalization;
using MelonLoader;

namespace SpectatorHUD.Counters;

[RegisterTypeInIl2Cpp]
public class HealthCounter : HealthCounterBase
{
    public HealthCounter(IntPtr ptr) : base(ptr)
    {
    }
    
    protected override void UpdateCounter()
    {
        text!.SetText(Value.ToString(CultureInfo.CurrentCulture));
    }
}