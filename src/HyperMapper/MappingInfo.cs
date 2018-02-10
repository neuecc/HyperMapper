using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using HyperMapper.Internal;
using HyperMapper.Internal.Emit;

namespace HyperMapper
{
    public static class MappingInfo
    {
        public static void Create<TFrom, TTo>()
        {



        }

        static void GetAll<T>()
        {
            var type = typeof(T);

            foreach (var item in type.GetRuntimeProperties())
            {
                if (item.GetIndexParameters().Length > 0) continue; // skip indexer
                if (item.GetCustomAttribute<IgnoreDataMemberAttribute>(true) != null) continue;

                var member = new MetaMember<T>(item);
                if (!member.IsReadable && !member.IsWritable) continue;

            }

            foreach (var item in type.GetRuntimeFields())
            {
                if (item.GetCustomAttribute<IgnoreDataMemberAttribute>(true) != null) continue;
                if (item.GetCustomAttribute<System.Runtime.CompilerServices.CompilerGeneratedAttribute>(true) != null) continue;
                if (item.IsStatic) continue;
                if (item.Name.StartsWith("<")) continue; // compiler generated field(anonymous type, etc...)

                var member = new MetaMember<T>(item);
                if (!member.IsReadable && !member.IsWritable) continue;
            }


        }
    }


    public class MappingInfo<TFrom, TTo>
    {
        public MetaMemberPair<TFrom, TTo>[] TargetMembers { get; set; }
        public MetaConstructorInfo<TFrom, TTo> TargetConstructor { get; set; }
        public Action<TFrom> BeforeMap { get; }
        public Action<TTo> AfterMap { get; }

        public MetaMember<TFrom>[] FromAllMembers { get; }
        public MetaMember<TTo>[] ToAllMembers { get; }
        public MetaConstructorInfo<TFrom, TTo>[] MappingConstructors { get; }

        internal MappingInfo()
        {

        }

        public MappingInfo<TFrom, TTo> Ignore<TMember>(Expression<Func<TTo, TMember>> memberSelector)
        {
            var memberExpression = memberSelector.Body as MemberExpression;
            var member = memberExpression.Member;

            TargetMembers = TargetMembers.Where(x => x.To.MemberInfo != member).ToArray();
            return this;
        }

        public MappingInfo<TFrom, TTo> AddMap<TFromMember, TToMember>(Expression<Func<TFrom, TFromMember>> from, Expression<Func<TTo, TToMember>> to)
        {
            // var memberExpression = memberSelector.Body as MemberExpression;
            //var member = memberExpression.Member;

            //TargetMembers = TargetMembers.Where(x => x.To.MemberInfo != member).ToArray();
            //return this;
            throw new NotImplementedException();
        }

        public MappingInfo<TFrom, TTo> AddMap<TFromMember, TToMember>(Expression<Func<TFrom, TFromMember>> from, Expression<Func<TTo, TToMember>> to, Func<TFromMember, TToMember> convertAction)
        {
            // var memberExpression = memberSelector.Body as MemberExpression;
            //var member = memberExpression.Member;

            //TargetMembers = TargetMembers.Where(x => x.To.MemberInfo != member).ToArray();
            //return this;
            throw new NotImplementedException();
        }

        public MappingInfo<TFrom, TTo> AddMap<TToMember>(Expression<Func<TTo, TToMember>> to, Func<TToMember> convertAction)
        {
            // var memberExpression = memberSelector.Body as MemberExpression;
            //var member = memberExpression.Member;

            //TargetMembers = TargetMembers.Where(x => x.To.MemberInfo != member).ToArray();
            //return this;
            throw new NotImplementedException();
        }

        // same as AddMap:)
        public MappingInfo<TFrom, TTo> WithConvertAction<TFromMember, TToMember>(Expression<Func<TFrom, TFromMember>> from, Expression<Func<TTo, TToMember>> to, Func<TFromMember, TToMember> convertAction)
        {
            // var memberExpression = memberSelector.Body as MemberExpression;
            //var member = memberExpression.Member;

            //TargetMembers = TargetMembers.Where(x => x.To.MemberInfo != member).ToArray();
            //return this;
            throw new NotImplementedException();
        }
    }

    public class MetaConstructorInfo<TFrom, TTo>
    {
        public ConstructorInfo ConstructorInfo { get; }
        public MetaMember<TFrom>[] Arguments { get; set; }
    }

    public class MetaMemberPair<TFrom, TTo>
    {
        public MetaMember<TFrom> From { get; }
        public MetaMember<TTo> To { get; }
        public Func<TFrom, TTo> ConvertAction { get; }
    }

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
