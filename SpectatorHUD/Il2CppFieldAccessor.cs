using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.InteropTypes;

namespace SpectatorHUD
{
    public class Il2CppFieldAccessor<T> where T : struct
    {
        private readonly Il2CppObjectBase _objectBase;
        private readonly IntPtr _fieldInfo;

        public T Value
        {
            get => this.Get();
            set => this.Set(value);
        }
        
        public Il2CppFieldAccessor(Il2CppObjectBase obj, string fieldName)
        {
            this._objectBase = obj;
            this._fieldInfo = IL2CPP.GetIl2CppField(obj.ObjectClass, fieldName);
            IntPtr fieldType = IL2CPP.il2cpp_field_get_type(this._fieldInfo);
            IntPtr klass = IL2CPP.il2cpp_class_from_type(fieldType);
            bool isValueType = IL2CPP.il2cpp_class_is_valuetype(klass);

            if (!isValueType)
            {
                throw new ArgumentException("T must be a value type");
            }
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
}