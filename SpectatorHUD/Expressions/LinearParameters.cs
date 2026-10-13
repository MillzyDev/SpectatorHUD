using Il2CppInterop.Runtime.Attributes;

namespace SpectatorHUD.Expressions
{
    [Serializable]
    public struct LinearParameters
    {
        public float multiplier;
        public float offset;

        [HideFromIl2Cpp]
        public LinearExpression<float> ToExpression()
        {
            return new LinearExpression<float>(this.multiplier, this.offset);
        }
    }
}