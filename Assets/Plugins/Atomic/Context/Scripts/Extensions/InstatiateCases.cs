using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Atomic.Contexts
{
    public static class InstatiateCases
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Instantiate<T>(this IContext context)
        {
            return (T) context.Instantiate(typeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object Instantiate(this IContext context, Type type)
        {
            ConstructorInfo[] constructors = type
                .GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

            int count = constructors.Length;
            if (count == 0)
            {
                ConstructorInfo defaultCtor = type.GetConstructor(Type.EmptyTypes);
                if (defaultCtor != null)
                {
                    object[] parameters = Array.Empty<object>();
                    return defaultCtor.Invoke(parameters);
                }
                
                throw new Exception("Constructor is not found!");
            }
            
            if (count > 1)
            {
                throw new Exception($"There are multiple constructors for type {type.Name}! Can't instatiate");
            }

            ConstructorInfo constructor = constructors[0];
            return context.Instantiate(constructor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object Instantiate(this IContext context, ConstructorInfo constructor)
        {
            ParameterInfo[] parameters = constructor.GetParameters();
            int count = parameters.Length;

            object[] args = new object[count];

            for (int i = 0; i < count; i++)
            {
                ParameterInfo parameter = parameters[i];
                InjectAttribute attribute = parameter.GetCustomAttribute<InjectAttribute>();
                if (attribute == null)
                {
                    throw new Exception($"Exprected parameter {parameter.Name} with [Inject] attribute!");
                }
                
                int valueId = attribute.key;
                object value = context.ResolveValue(valueId);

                if (value == null)
                {
                    Debug.LogError($"Can't resolve parameter with key {debugUtils.ConvertToName(valueId)}");
                }
                else
                {
                    args[i] = value;
                }
            }

            return constructor.Invoke(args);
        }
    }
}



// for (var i = 0; i < count; i++)
// {
//
// }
//
// // if (count == 0)
// // {
// //     ConstructorInfo defaultCtor = type.GetConstructor(Type.EmptyTypes);
// //     if (defaultCtor != null)
// //     {
// //         return defaultCtor.Invoke(Array.Empty<object>());
// //     }
// // }
//
//
// throw new Exception("Constructor is not found!");