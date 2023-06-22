using System;
using MelonLoader;

namespace SpectatorHUD.Counters
{
    [RegisterTypeInIl2Cpp]
    public class HealthCounterBase : FloatCounter
    {
        // ReSharper disable once PublicConstructorInAbstractClass
        public HealthCounterBase(IntPtr ptr) : base(ptr)
        {
        }
    }
}
