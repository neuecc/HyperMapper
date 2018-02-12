using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Runtime.Serialization;
using HyperMapper.Internal.Emit;

namespace HyperMapper
{
    public class MetaMember<T>
    {
        readonly MethodInfo getMethod;
        readonly MethodInfo setMethod;

        public string MemberName { get; }
        public bool IsWritable { get; }
        public bool IsReadable { get; }
        public Type Type { get; }
        public FieldInfo FieldInfo { get; }
        public PropertyInfo PropertyInfo { get; }

        public bool IsProperty => PropertyInfo != null;
        public bool IsField => FieldInfo != null;
        public MemberInfo MemberInfo => IsProperty ? (MemberInfo)PropertyInfo : (MemberInfo)FieldInfo;

        public MetaMember(FieldInfo info)
        {
            this.MemberName = info.Name;
            this.FieldInfo = info;
            this.Type = info.FieldType;
            this.IsReadable = info.IsPublic;
            this.IsWritable = (info.IsPublic && !info.IsInitOnly);
        }

        public MetaMember(PropertyInfo info)
        {
            this.getMethod = info.GetGetMethod(true);
            this.setMethod = info.GetSetMethod(true);

            this.MemberName = info.Name;
            this.PropertyInfo = info;
            this.Type = info.PropertyType;
            this.IsReadable = (getMethod != null) && (getMethod.IsPublic) && !getMethod.IsStatic;
            this.IsWritable = (setMethod != null) && (setMethod.IsPublic) && !setMethod.IsStatic;
        }

        internal void EmitLoadValue(ILGenerator il)
        {
            if (IsProperty)
            {
                il.EmitCall(getMethod);
            }
            else
            {
                il.Emit(OpCodes.Ldfld, FieldInfo);
            }
        }

        internal void EmitStoreValue(ILGenerator il)
        {
            if (IsProperty)
            {
                il.EmitCall(setMethod);
            }
            else
            {
                il.Emit(OpCodes.Stfld, FieldInfo);
            }
        }
    }
}