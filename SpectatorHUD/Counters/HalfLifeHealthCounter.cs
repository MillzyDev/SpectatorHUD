using System;
using MelonLoader;

namespace SpectatorHUD.Counters;

[RegisterTypeInIl2Cpp]
public class HalfLifeHealthCounter : HealthCounterBase
{
    protected override void UpdateCounter()
    {
        var value = (int)Math.Round((decimal)Value*10, 0);
        text!.SetText(value.ToString());
    }
}