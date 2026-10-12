using UnityEngine;

namespace SpectatorHUD.Shading
{
    [Serializable]
    public struct ShaderIntReference
    {
        public Material material;
        public string propertyName;

        [NonSerialized] public int propertyId;
    }
}