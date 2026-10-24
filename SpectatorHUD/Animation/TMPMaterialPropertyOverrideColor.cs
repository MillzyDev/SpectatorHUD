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

using Il2CppTMPro;
using UnityEngine;

namespace SpectatorHUD.Animation;

public class TMPMaterialPropertyOverrideColor : MonoBehaviour
{
    public TMP_FontAsset fontAsset;
    public string propertyName;
    public Color value;

    private readonly Il2CppFieldAccessor<Color> _valueAccessor;

    private Material _material;
    private int _propertyId;

    public TMPMaterialPropertyOverrideColor(IntPtr ptr) : base(ptr)
    {
        this._valueAccessor = new Il2CppFieldAccessor<Color>(this, nameof(this.value));
    }

    private void Awake()
    {
        this._material = this.fontAsset.material;
        this._propertyId = Shader.PropertyToID(this.propertyName);
    }

    private void Update()
    {
        // The animator can't actually set our value field on the managed side, so we need to get the IL2CPP field ourselves
        this.value = this._valueAccessor.Value;
        this._material.SetColor(this._propertyId, this.value);
    }
}