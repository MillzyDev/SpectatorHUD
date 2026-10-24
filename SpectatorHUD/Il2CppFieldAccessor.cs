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
using Il2CppInterop.Runtime.InteropTypes;

// "unmanaged" is unsupported unfortunately
#pragma warning disable CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type

namespace SpectatorHUD;

public class Il2CppFieldAccessor<T> where T : struct
{
    private readonly IntPtr _fieldInfo;
    private readonly Il2CppObjectBase _objectBase;

    public Il2CppFieldAccessor(Il2CppObjectBase obj, string fieldName)
    {
        this._objectBase = obj;
        this._fieldInfo = IL2CPP.GetIl2CppField(obj.ObjectClass, fieldName);
        IntPtr fieldType = IL2CPP.il2cpp_field_get_type(this._fieldInfo);
        IntPtr klass = IL2CPP.il2cpp_class_from_type(fieldType);
        bool isValueType = IL2CPP.il2cpp_class_is_valuetype(klass);

        if (!isValueType) throw new ArgumentException("T must be a value type");
    }

    public T Value
    {
        get => this.Get();
        set => this.Set(value);
    }

    public unsafe T Get()
    {
        return *(T*)(IL2CPP.Il2CppObjectBaseToPtrNotNull(this._objectBase) +
                     (int)IL2CPP.il2cpp_field_get_offset(this._fieldInfo));
    }

    public unsafe void Set(T value)
    {
        *(T*)(IL2CPP.Il2CppObjectBaseToPtrNotNull(this._objectBase) +
              (int)IL2CPP.il2cpp_field_get_offset(this._fieldInfo)) = value;
    }
}