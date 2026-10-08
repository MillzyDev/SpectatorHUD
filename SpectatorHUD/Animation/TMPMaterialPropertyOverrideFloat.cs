using Il2CppInterop.Runtime;
using Il2CppTMPro;
using UnityEngine;

namespace SpectatorHUD.Animation
{
    public class TMPMaterialPropertyOverrideFloat : MonoBehaviour
    {
        public TMP_FontAsset fontAsset;
        public string propertyName;
        public float value;

        private Material _material;
        private int _propertyId;

        private IntPtr _valueField;
        
        public TMPMaterialPropertyOverrideFloat(IntPtr ptr) : base(ptr)
        {
        }

        private void Awake()
        {
            this._material = this.fontAsset.material;
            this._propertyId = Shader.PropertyToID(this.propertyName);
            this._valueField = IL2CPP.GetIl2CppField(this.ObjectClass, "value");
        }

        private unsafe void Update()
        {
            // The animator can't actually set our value field on the managed side, so we need to get the IL2CPP field ourselves
            float il2cppValue;
            IL2CPP.il2cpp_field_get_value(this.Pointer, this._valueField, &il2cppValue);
            this.value = il2cppValue;
            this._material.SetFloat(this._propertyId, this.value);
        }
    }
}

// TODO: Update reserve on ammo pickup
// TODO: Clamp health
// TODO: Put the trigger stuff back in