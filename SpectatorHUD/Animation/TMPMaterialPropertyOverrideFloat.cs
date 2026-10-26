/*
 *      SpectatorHUD
 *      Copyright (C) 2026  Millzy
 *
 *      This software is licensed under the terms of the Functional Source License,
 *      Version 1.1, ALv2 Future License.  You are permitted to use, copy, modify,
 *      create derivative works, publicly display, and redistribute it under the the
 *      terms of the license.
 *
 *      THE SOFTWARE IS PROVIDED "AS IS" AND WITHOUT WARRANTIES OF ANY KIND, EXPRESS OR
 *      IMPLIED, INCLUDING WITHOUT LIMITATION WARRANTIES OF FITNESS FOR A PARTICULAR
 *      PURPOSE, MERCHANTABILITY, TITLE OR NON-INFRINGEMENT.
 *
 *      You should have received a copy of the Functional Source License, Version 1.1,
 *      ALv2 Future License along with this program.  If not, see https://fsl.software/.
 */

using Il2CppTMPro;
using SpectatorHUD.Util;
using UnityEngine;

namespace SpectatorHUD.Animation;

public class TMPMaterialPropertyOverrideFloat : MonoBehaviour
{
    public TMP_FontAsset fontAsset;
    public string propertyName;
    public float value;

    private readonly Il2CppFieldAccessor<float> _valueAccessor;

    private Material _material;
    private int _propertyId;

    public TMPMaterialPropertyOverrideFloat(IntPtr ptr) : base(ptr)
    {
        this._valueAccessor = new Il2CppFieldAccessor<float>(this, nameof(this.value));
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
        this._material.SetFloat(this._propertyId, this.value);
    }
}