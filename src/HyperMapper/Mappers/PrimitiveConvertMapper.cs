using HyperMapper.Internal.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace HyperMapper.Mappers
{
#if DEBUG && (NET45 || NET47)
    public

#else
    internal
#endif
        static class PrimitiveConvertMapper
    {
        const string ModuleName = "HyperMapper.PrimitiveConvertMappers";
        internal static readonly DynamicAssembly assembly;

        static PrimitiveConvertMapper()
        {
            assembly = new DynamicAssembly(ModuleName);
        }

#if DEBUG && (NET45 || NET47)
        public static AssemblyBuilder Save()
        {
            return assembly.Save();
        }
#endif

        static readonly Regex SubtractFullNameRegex = new Regex(@", Version=\d+.\d+.\d+.\d+, Culture=\w+, PublicKeyToken=\w+", RegexOptions.Compiled);
        static int nameSequence = 0;

        static readonly HashSet<Type> primitiveTypes = new HashSet<Type>
        {
            {typeof(short)},
            {typeof(int)},
            {typeof(long)},
            {typeof(ushort)},
            {typeof(uint)},
            {typeof(ulong)},
            {typeof(float)},
            {typeof(double)},
            // {typeof(bool)},
            {typeof(byte)},
            {typeof(sbyte)},
            // {typeof(char)},
            // {typeof(decimal)},
            // {typeof(string)},
        };

        public static bool IsPrimitive(Type type)
        {
            return primitiveTypes.Contains(type);
        }

        internal static object Build(Type fromType, Type toType)
        {
            if (!primitiveTypes.Contains(fromType))
            {
                throw new ArgumentException("Type must be primitive. Type:" + fromType.Name);
            }
            if (!primitiveTypes.Contains(toType))
            {
                throw new ArgumentException("Type must be primitive. Type:" + toType.Name);
            }

            var typeBuilder = assembly.DefineType($"HyperMapper.Formatters.{{{SubtractFullNameRegex.Replace(fromType.FullName, "").Replace(".", "_")}}}-{{{SubtractFullNameRegex.Replace(toType.FullName, "").Replace(".", "_")}}}Formatter{Interlocked.Increment(ref nameSequence)}",
                TypeAttributes.Public | TypeAttributes.Sealed, null, new[] { typeof(IObjectMapper<,>).MakeGenericType(fromType, toType) });

            // TTo Map(TFrom from, IObjectMapperResolver resolver);
            {
                var method = typeBuilder.DefineMethod("Map", MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual,
                    toType,
                    new Type[] { fromType, typeof(IObjectMapperResolver) });
                var il = method.GetILGenerator();

                var fromPrimitiveType = fromType;
                var toPrimitiveType = toType;

                if (fromPrimitiveType == toPrimitiveType)
                {
                    il.EmitLdarg(1); // simply load and return
                    il.EmitRet();
                }
                else
                {
                    // require convert
                    il.EmitLdarg(1);
                    if (toPrimitiveType == typeof(byte))
                    {
                        il.Emit(OpCodes.Conv_U1);
                    }
                    else if (toPrimitiveType == typeof(sbyte))
                    {
                        il.Emit(OpCodes.Conv_I1);
                    }
                    else if (toPrimitiveType == typeof(short))
                    {
                        il.Emit(OpCodes.Conv_I2);
                    }
                    else if (toPrimitiveType == typeof(ushort))
                    {
                        il.Emit(OpCodes.Conv_U2);
                    }
                    else if (toPrimitiveType == typeof(int))
                    {
                        il.Emit(OpCodes.Conv_I4);
                    }
                    else if (toPrimitiveType == typeof(uint))
                    {
                        il.Emit(OpCodes.Conv_U4);
                    }
                    else if (toPrimitiveType == typeof(long))
                    {
                        il.Emit(OpCodes.Conv_I8);
                    }
                    else if (toPrimitiveType == typeof(ulong))
                    {
                        il.Emit(OpCodes.Conv_U8);
                    }
                    else if (toPrimitiveType == typeof(float))
                    {
                        il.Emit(OpCodes.Conv_R4);
                    }
                    else if (toPrimitiveType == typeof(double))
                    {
                        il.Emit(OpCodes.Conv_R8);
                    }
                    il.EmitRet();
                }
            }

            var typeInfo = typeBuilder.CreateTypeInfo();
            return Activator.CreateInstance(typeInfo);
        }
    }
}
