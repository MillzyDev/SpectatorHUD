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

using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.InteropTypes;

// "unmanaged" keyword isnt in .NET 6 unfortunately
#pragma warning disable CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type

namespace SpectatorHUD.Util;

public class Il2CppFieldAccessor<T> where T : struct
{
    private readonly IntPtr _fieldInfo;
    private readonly uint _fieldOffset;
    private readonly Il2CppObjectBase _objectBase;

    public Il2CppFieldAccessor(Il2CppObjectBase obj, string fieldName)
        : this(obj, IL2CPP.GetIl2CppField(obj.ObjectClass, fieldName))
    {
    }

    public Il2CppFieldAccessor(Il2CppObjectBase obj, IntPtr nativeFieldInfo)
    {
        this._objectBase = obj;
        this._fieldInfo = nativeFieldInfo;
        this._fieldOffset = IL2CPP.il2cpp_field_get_offset(this._fieldInfo);

        IntPtr fieldType = IL2CPP.il2cpp_field_get_type(this._fieldInfo);
        IntPtr klass = IL2CPP.il2cpp_class_from_type(fieldType);
        bool isValueType = IL2CPP.il2cpp_class_is_valuetype(klass);
        if (!isValueType)
        {
            throw new ArgumentException("T must be a value type");
        }
    }

    public T Value
    {
        get => this.Get();
        set => this.Set(value);
    }

    public unsafe T Get()
    {
        return *(T*)(IL2CPP.Il2CppObjectBaseToPtrNotNull(this._objectBase) +
                     (int)this._fieldOffset);
    }

    public unsafe void Set(T value)
    {
        *(T*)(IL2CPP.Il2CppObjectBaseToPtrNotNull(this._objectBase) +
              (int)this._fieldOffset) = value;
    }
}