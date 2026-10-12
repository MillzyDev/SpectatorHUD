using SpectatorHUD.Expressions;
using UnityEngine;

namespace SpectatorHUD.Shading
{
    [Serializable]
    public struct ShaderBoolReference
    {
        public Material material;
        public string propertyName;

        [NonSerialized] public int propertyId;
    }
}