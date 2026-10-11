/*
 *      SpectatorHUD
 *      Copyright (C) 2026  Millzy
 *
 *      This program is free software: you can redistribute it and/or modify
 *      it under the terms of the GNU Lesser General Public License as published by
 *      the Free Software Foundation, either version 3 of the License, or
 *      (at your option) any later version.
 *
 *      This program is distributed in the hope that it will be useful,
 *      but WITHOUT ANY WARRANTY; without even the implied warranty of
 *      MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *      GNU General Public License for more details.
 *
 *      You should have received a copy of the GNU Lesser General Public License
 *      along with this program.  If not, see https://www.gnu.org/licenses/.
 */

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

        private readonly IntPtr _valueField;
        
        public TMPMaterialPropertyOverrideFloat(IntPtr ptr) : base(ptr)
        {
            this._valueField = IL2CPP.GetIl2CppField(this.ObjectClass, nameof(this.value));
        }

        private void Awake()
        {
            this._material = this.fontAsset.material;
            this._propertyId = Shader.PropertyToID(this.propertyName);
        }

        private unsafe void Update()
        {
            // The animator can't actually set our value field on the managed side, so we need to get the IL2CPP field ourselves
            this.value = *(float *)(IL2CPP.Il2CppObjectBaseToPtrNotNull(this) + (int) IL2CPP.il2cpp_field_get_offset(this._valueField));
            this._material.SetFloat(this._propertyId, this.value);
        }
    }
}
