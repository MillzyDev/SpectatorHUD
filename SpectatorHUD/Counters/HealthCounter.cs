using System.Globalization;
using MelonLoader;

namespace SpectatorHUD.Counters;

[RegisterTypeInIl2Cpp]
public class HealthCounter : HealthCounterBase
{
    protected override void UpdateCounter()
    {
        text!.SetText(Value.ToString(CultureInfo.CurrentCulture));
    }
}