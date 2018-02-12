using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using HyperMapper.Internal;
using HyperMapper.Internal.Emit;
using System.Text.RegularExpressions;
using System.Threading;

namespace HyperMapper.Mappers
{
#if DEBUG && (NET45 || NET47)
    public

#else
    internal
#endif
        static class EnumToEnumMapperBuilder
    {
        const string ModuleName = "HyperMapper.EnumMappers";
        internal static readonly DynamicAssembly assembly;

        static EnumToEnumMapperBuilder()
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
            // {typeof(float)},
            // {typeof(double)},
            // {typeof(bool)},
            {typeof(byte)},
            {typeof(sbyte)},
            {typeof(char)},
            // {typeof(decimal)},
            // {typeof(string)},
        };

        internal static object Build(Type fromType, Type toType)
        {
            if (!fromType.IsEnum && !primitiveTypes.Contains(fromType))
            {
                throw new ArgumentException("Type must be enum or enum primitive. Type:" + fromType.Name);
            }
            if (!toType.IsEnum && !primitiveTypes.Contains(toType))
            {
                throw new ArgumentException("Type must be enum or enum primitive. Type:" + toType.Name);
            }

            var typeBuilder = assembly.DefineType($"HyperMapper.Formatters.{{{SubtractFullNameRegex.Replace(fromType.FullName, "").Replace(".", "_")}}}-{{{SubtractFullNameRegex.Replace(toType.FullName, "").Replace(".", "_")}}}Formatter{Interlocked.Increment(ref nameSequence)}",
                TypeAttributes.Public | TypeAttributes.Sealed, null, new[] { typeof(IObjectMapper<,>).MakeGenericType(fromType, toType) });

            // TTo Map(TFrom from, IObjectMapperResolver resolver);
            {
                var method = typeBuilder.DefineMethod("Map", MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual,
                    toType,
                    new Type[] { fromType, typeof(IObjectMapperResolver) });
                var il = method.GetILGenerator();

                var fromPrimitiveType = ToPrimitiveType(fromType);
                var toPrimitiveType = ToPrimitiveType(toType);

                if (fromPrimitiveType == toPrimitiveType)
                {
                    il.EmitLdarg(1); // simply load and return
                    il.EmitRet();
                }
                else
                {
                    // require convert
                    il.EmitLdarg(1);
                    if (toPrimitiveType == typeof(byte) || toPrimitiveType == typeof(sbyte)) // 8
                    {
                        il.Emit(OpCodes.Conv_I1);
                    }
                    else if (toPrimitiveType == typeof(short) || toPrimitiveType == typeof(ushort) || toPrimitiveType == typeof(char)) // 16
                    {
                        il.Emit(OpCodes.Conv_I2);
                    }
                    else if (toPrimitiveType == typeof(int) || toPrimitiveType == typeof(uint)) // 32
                    {
                        il.Emit(OpCodes.Conv_I4);
                    }
                    else if (toPrimitiveType == typeof(long) || toPrimitiveType == typeof(ulong)) // 64
                    {
                        il.Emit(OpCodes.Conv_I8);
                    }

                    il.EmitRet();
                }
            }

            var typeInfo = typeBuilder.CreateTypeInfo();
            return Activator.CreateInstance(typeInfo);
        }

        static Type ToPrimitiveType(Type type)
        {
            if (type.IsEnum)
            {
                return Enum.GetUnderlyingType(type);
            }
            return type;
        }
    }

    public class EnumToStringMapper<T> : IObjectMapper<T, string>
        where T : struct
    {
        readonly Dictionary<T, string> valueNameMapping;

        public EnumToStringMapper()
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new ArgumentException("T must be Enum Type. T:" + typeof(T).Name);

            var values = (T[])Enum.GetValues(typeof(T));
            var names = Enum.GetNames(typeof(T));

            valueNameMapping = new Dictionary<T, string>(names.Length);

            for (int i = 0; i < names.Length; i++)
            {
                valueNameMapping.Add((T)values[i], names[i]);
            }
        }

        public string Map(T from, IObjectMapperResolver resolver)
        {
            if (valueNameMapping.TryGetValue(from, out var v))
            {
                return v;
            }
            else
            {
                return from.ToString(); // ToString is slightly slow.
            }
        }
    }

    public class StringToEnumMapper<T> : IObjectMapper<string, T>
        where T : struct
    {
        readonly Dictionary<string, T> nameValueMapping;

        public StringToEnumMapper()
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new ArgumentException("T must be Enum Type. T:" + typeof(T).Name);

            var values = (T[])Enum.GetValues(typeof(T));
            var names = Enum.GetNames(typeof(T));

            nameValueMapping = new Dictionary<string, T>(names.Length, StringComparer.Ordinal);

            for (int i = 0; i < names.Length; i++)
            {
                nameValueMapping.Add(names[i], (T)values[i]);
            }
        }


        public T Map(string from, IObjectMapperResolver resolver)
        {
            if (nameValueMapping.TryGetValue(from, out var value))
            {
                return value;
            }
            else
            {
                if (Enum.TryParse<T>(from, out value))
                {
                    return value;
                }
                else
                {
                    throw new ArgumentException($"Can't Parse string to {typeof(T)}, value: {from}");
                }
            }
        }
    }
}
