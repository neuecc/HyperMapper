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
        public static MappingInfo<TFrom, TTo> Create<TFrom, TTo>()
        {
            var fromMembers = GetMembers<TFrom>();
            var toMembers = GetMembers<TTo>();

            var ctorInfos = GetConstructorInfos<TFrom, TTo>(fromMembers);
            var matchCtor = ctorInfos.Where(x => x.Arguments != null).OrderByDescending(x => x.Arguments.Length).FirstOrDefault();

            var pairs = BuildMemberPair(fromMembers, toMembers);

            return new MappingInfo<TFrom, TTo>(fromMembers, toMembers, ctorInfos)
            {
                TargetConstructor = matchCtor,
                TargetMembers = pairs
            };
        }

        static MetaMember<T>[] GetMembers<T>()
        {
            var type = typeof(T);

            var array = new ArrayBuffer<MetaMember<T>>(4);

            foreach (var item in type.GetRuntimeProperties())
            {
                if (item.GetIndexParameters().Length > 0) continue; // skip indexer
                if (item.GetCustomAttribute<IgnoreDataMemberAttribute>(true) != null) continue;

                var member = new MetaMember<T>(item);
                if (!member.IsReadable && !member.IsWritable) continue;

                array.Add(member);
            }

            foreach (var item in type.GetRuntimeFields())
            {
                if (item.GetCustomAttribute<IgnoreDataMemberAttribute>(true) != null) continue;
                if (item.GetCustomAttribute<System.Runtime.CompilerServices.CompilerGeneratedAttribute>(true) != null) continue;
                if (item.IsStatic) continue;
                if (item.Name.StartsWith("<")) continue; // compiler generated field(anonymous type, etc...)

                var member = new MetaMember<T>(item);
                if (!member.IsReadable && !member.IsWritable) continue;

                array.Add(member);
            }

            return array.ToArray();
        }

        static MetaConstructorInfo<TFrom, TTo>[] GetConstructorInfos<TFrom, TTo>(MetaMember<TFrom>[] arguments)
        {
            var array = new ArrayBuffer<MetaConstructorInfo<TFrom, TTo>>(4);

            var map = arguments.ToDictionary(x => x.MemberName);

            foreach (var ctorInfo in typeof(TTo).GetTypeInfo().DeclaredConstructors)
            {
                var metaCtor = new MetaConstructorInfo<TFrom, TTo>(ctorInfo);
                var parameters = ctorInfo.GetParameters();

                var memberBuffer = new ArrayBuffer<MetaMember<TFrom>>(parameters.Length);

                foreach (var item in parameters)
                {
                    if (map.TryGetValue(item.Name, out var member))
                    {
                        memberBuffer.Add(member);
                    }
                }

                // match all.
                if (parameters.Length == memberBuffer.Size)
                {
                    metaCtor.Arguments = memberBuffer.ToArray();
                }
            }

            return array.ToArray();
        }

        static MetaMemberPair<TFrom, TTo>[] BuildMemberPair<TFrom, TTo>(MetaMember<TFrom>[] from, MetaMember<TTo>[] to)
        {
            var array = new ArrayBuffer<MetaMemberPair<TFrom, TTo>>(to.Length);

            var map = from.ToDictionary(x => x.MemberName);
            foreach (var toMember in to)
            {
                if (map.TryGetValue(toMember.MemberName, out var fromMember))
                {
                    array.Add(new MetaMemberPair<TFrom, TTo>(fromMember, toMember));
                }
            }

            return array.ToArray();
        }
    }


    public class MappingInfo<TFrom, TTo>
    {
        public MetaMemberPair<TFrom, TTo>[] TargetMembers { get; set; }
        public MetaConstructorInfo<TFrom, TTo> TargetConstructor { get; set; }
        public Action<TFrom> BeforeMap { get; set; }
        public Action<TTo> AfterMap { get; set; }

        public MetaMember<TFrom>[] FromAllMembers { get; }
        public MetaMember<TTo>[] ToAllMembers { get; }
        public MetaConstructorInfo<TFrom, TTo>[] MappingConstructors { get; }

        internal MappingInfo(MetaMember<TFrom>[] fromMembers, MetaMember<TTo>[] toMembers, MetaConstructorInfo<TFrom, TTo>[] mappingConstructors)
        {
            this.FromAllMembers = fromMembers;
            this.ToAllMembers = toMembers;
            this.MappingConstructors = mappingConstructors;
        }

        public MappingInfo<TFrom, TTo> Ignore<TMember>(Expression<Func<TTo, TMember>> memberSelector)
        {
            var memberExpression = memberSelector.Body as MemberExpression;
            var member = memberExpression.Member;

            TargetMembers = TargetMembers.Where(x => x.To.MemberInfo != member).ToArray();
            return this;
        }

        // TODO:not yet implemented
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

        public MetaConstructorInfo(ConstructorInfo constructorInfo)
        {
            ConstructorInfo = constructorInfo;
        }
    }

    public class MetaMemberPair<TFrom, TTo>
    {
        public MetaMember<TFrom> From { get; }
        public MetaMember<TTo> To { get; }
        public Func<TFrom, TTo> ConvertAction { get; }

        public MetaMemberPair(MetaMember<TFrom> from, MetaMember<TTo> to)
            : this(from, to, null)
        {
        }

        public MetaMemberPair(MetaMember<TFrom> from, MetaMember<TTo> to, Func<TFrom, TTo> convertAction)
        {
            From = from;
            To = to;
            ConvertAction = convertAction;
        }
    }
}
