using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HyperMapper
{
    public class MappingInfo<TFrom, TTo>
    {
        public MetaMemberPair<TFrom, TTo>[] TargetMembers { get; set; }
        public MetaConstructorInfo<TFrom, TTo> TargetConstructor { get; set; }
        public Action<TFrom> BeforeMap { get; }
        public Action<TTo> AfterMap { get; }

        public MetaMember<TFrom>[] FromAllMembers { get; }
        public MetaMember<TTo>[] ToAllMembers { get; }
        public MetaConstructorInfo<TFrom, TTo>[] MappingConstructors { get; }

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
        public MemberInfo MemberInfo { get; }
    }

}
