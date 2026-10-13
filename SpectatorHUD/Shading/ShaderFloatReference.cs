using Il2CppInterop.Runtime.Attributes;
using SpectatorHUD.Expressions;
using UnityEngine;

namespace SpectatorHUD.Shading
{
    [Serializable]
    public struct ShaderFloatReference
    {
        public Material material;
        public string propertyName;
        public LinearParameters modifier;

        [NonSerialized] public int propertyId;
        [NonSerialized] public LinearExpression<float> modifierExpression;
    }
}