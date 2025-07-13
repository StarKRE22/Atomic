using System;
using System.Collections.Generic;
using System.Reflection;
using Atomic.Entities;
using UnityEngine;

// ReSharper disable ForCanBeConvertedToForeach

namespace Atomic.Contexts
{
    public static class InjectCases
    {
        private static readonly Type OBJECT_TYPE = typeof(object);
        private static readonly Type MONO_BEHAVIOUR_TYPE = typeof(MonoBehaviour);

        public static void Construct(this IEntity context) => context.InjectAll(context.GetBehaviours());

        public static void InjectAll(this IEntity context, IEnumerable<object> targets)
        {
            foreach (object target in targets) 
                context.Inject(target);
        }

        public static void Inject(this IEntity context, object target)
        {
            Type type = target.GetType();

            while (true)
            {
                if (type == null || type == OBJECT_TYPE || type == MONO_BEHAVIOUR_TYPE)
                    break;

                context.InjectByFields(target, type);
                context.InjectByProperties(target, type);
                context.InjectByMethods(target, type);

                type = type.BaseType;
            }
        }

        public static void InjectByFields(this IEntity context, object target, Type targetType)
        {
            FieldInfo[] fields = targetType.GetFields(BindingFlags.Instance |
                                                      BindingFlags.Public |
                                                      BindingFlags.NonPublic |
                                                      BindingFlags.DeclaredOnly);

            for (int i = 0, count = fields.Length; i < count; i++)
            {
                FieldInfo field = fields[i];
                InjectAttribute attribute = field.GetCustomAttribute<InjectAttribute>();
                if (attribute == null)
                {
                    continue;
                }

                int valueId = attribute.key;
                object value = context.GetValue(valueId);

                if (value == null)
                    Debug.LogError($"Can't resolve field with key {EntityUtils.IdToName(valueId)}");
                else
                    field.SetValue(target, value);
            }
        }

        public static void InjectByProperties(this IEntity context, object target, Type targetType)
        {
            PropertyInfo[] properties = targetType.GetProperties(BindingFlags.Instance |
                                                                 BindingFlags.Public |
                                                                 BindingFlags.NonPublic |
                                                                 BindingFlags.DeclaredOnly);

            for (int i = 0, count = properties.Length; i < count; i++)
            {
                PropertyInfo property = properties[i];
                InjectAttribute attribute = property.GetCustomAttribute<InjectAttribute>();
                if (attribute == null)
                    continue;

                int valueId = attribute.key;
                object value = context.GetValue(valueId);

                if (value == null)
                    Debug.LogError($"Can't resolve property with key {EntityUtils.IdToName(valueId)}");
                else
                    property.SetValue(target, value);
            }
        }


        public static void InjectByMethods(this IEntity context, object target, Type targetType)
        {
            MethodInfo[] methods = targetType.GetMethods(BindingFlags.Instance |
                                                         BindingFlags.Public |
                                                         BindingFlags.NonPublic |
                                                         BindingFlags.DeclaredOnly);

            for (int i = 0, count = methods.Length; i < count; i++)
            {
                MethodInfo method = methods[i];
                if (method.IsDefined(typeof(InjectAttribute))) 
                    context.InjectByMethod(target, method);
            }
        }
        
        public static void InjectByMethod(this IEntity context, object target, MethodInfo method)
        {
            ParameterInfo[] parameters = method.GetParameters();
            int count = parameters.Length;

            object[] args = new object[count];
            for (var i = 0; i < count; i++)
            {
                ParameterInfo parameter = parameters[i];
                InjectAttribute attribute = parameter.GetCustomAttribute<InjectAttribute>();
                if (attribute == null)
                    throw new Exception($"Exprected parameter {parameter.Name} with [Inject] attribute!");

                int valueId = attribute.key;
                object value = context.GetValue(valueId);

                if (value == null)
                    Debug.LogError($"Can't resolve parameter with key {EntityUtils.IdToName(valueId)}");
                else
                    args[i] = value;
            }

            method.Invoke(target, args);
        }
    }
}