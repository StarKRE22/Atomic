using System.Collections.Generic;
using System.Linq;
using Atomic.SourceGenerators.Shared;
using EventAPIGenerator.Models;

namespace EventAPIGenerator
{
    /// <summary>
    /// Produces C# source code from <see cref="EventAPIDefinition"/> models.
    /// Generated extension methods use the user-declared key's <c>Id</c> property directly.
    /// </summary>
    internal static class CodeEmitter
    {
        /// <summary>
        /// Generates the full C# source for a given <see cref="EventAPIDefinition"/>.
        /// </summary>
        public static string Emit(EventAPIDefinition def)
        {
            var w = new CodeWriter();

            // Header
            w.Line("/**");
            w.Line(" * Code generation. Don't modify!");
            w.Line(" **/");
            w.Line();

            // Usings
            w.Line("using Atomic.Events;");
            w.Line("using System;");

            // Namespace
            bool hasNamespace = !string.IsNullOrEmpty(def.Namespace);
            if (hasNamespace)
            {
                w.Line();
                w.Line($"namespace {def.Namespace}");
                w.Open();
            }

            // Class declaration
            w.Line($"public static partial class {def.ClassName}");
            w.Open();

            foreach (var evt in def.Events)
                EmitEventRegion(ref w, def.ClassName, evt);

            w.Close(); // class
            if (hasNamespace) w.Close(); // namespace

            return w.Result;
        }

        static void EmitEventRegion(ref CodeWriter w, string className, EventField evt)
        {
            string key = $"{className}.{evt.Name}.Id";
            string busTypeName = evt.BusTypeName;
            int argCount = evt.ArgTypeNames.Count;

            string actionType = GetActionType(evt.ArgTypeNames);
            string subscriptionType = GetSubscriptionType(evt.ArgTypeNames);
            string genericArgs = GetGenericArgs(evt.ArgTypeNames);
            string invokeParams = GetInvokeParams(evt.ArgTypeNames);
            string invokeGenericArgs = string.IsNullOrEmpty(genericArgs) ? string.Empty : $"<{genericArgs}>";
            string subscribeGenericArgs = invokeGenericArgs;

            string subscribeMethod = evt.IsUnsafe ? "SubscribeUnsafe" : "Subscribe";
            string unsubscribeMethod = evt.IsUnsafe ? "UnsubscribeUnsafe" : "Unsubscribe";
            string invokeMethod = evt.IsUnsafe ? "InvokeUnsafe" : "Invoke";

            w.Line();
            w.Line($"#region {evt.Name}");
            w.Line();

            // Subscribe
            w.Line($"public static {subscriptionType} Subscribe{evt.Name}(this {busTypeName} bus, {actionType} action) =>");
            w.Line($"    bus.{subscribeMethod}{subscribeGenericArgs}({key}, action);");
            w.Line();

            // Unsubscribe
            w.Line($"public static void Unsubscribe{evt.Name}(this {busTypeName} bus, {actionType} action) =>");
            w.Line($"    bus.{unsubscribeMethod}{subscribeGenericArgs}({key}, action);");
            w.Line();

            // Invoke
            if (argCount == 0)
            {
                w.Line($"public static void Invoke{evt.Name}(this {busTypeName} bus) =>");
                w.Line($"    bus.{invokeMethod}({key});");
            }
            else
            {
                w.Line($"public static void Invoke{evt.Name}(this {busTypeName} bus, {invokeParams}) =>");
                w.Line($"    bus.{invokeMethod}{invokeGenericArgs}({key}, {GetInvokeArgs(argCount)});");
            }

            w.Line();

            // IsSubscribed
            w.Line($"public static bool IsSubscribed{evt.Name}(this {busTypeName} bus) =>");
            w.Line($"    bus.IsSubscribed({key});");
            w.Line();

            // Dispose
            w.Line($"public static bool Dispose{evt.Name}(this {busTypeName} bus) =>");
            w.Line($"    bus.Dispose({key});");
            w.Line();

            w.Line("#endregion");
        }

        static string GetActionType(IReadOnlyList<string> argTypeNames)
        {
            return argTypeNames.Count == 0
                ? "Action"
                : $"Action<{string.Join(", ", argTypeNames)}>";
        }

        static string GetSubscriptionType(IReadOnlyList<string> argTypeNames)
        {
            return argTypeNames.Count == 0
                ? "Subscription"
                : $"Subscription<{string.Join(", ", argTypeNames)}>";
        }

        static string GetGenericArgs(IReadOnlyList<string> argTypeNames)
        {
            return argTypeNames.Count == 0
                ? string.Empty
                : string.Join(", ", argTypeNames);
        }

        static string GetInvokeParams(IReadOnlyList<string> argTypeNames)
        {
            if (argTypeNames.Count == 0)
                return string.Empty;

            var parts = new List<string>(argTypeNames.Count);
            for (int i = 0; i < argTypeNames.Count; i++)
                parts.Add($"{argTypeNames[i]} arg{i + 1}");

            return string.Join(", ", parts);
        }

        static string GetInvokeArgs(int argCount)
        {
            var parts = new List<string>(argCount);
            for (int i = 0; i < argCount; i++)
                parts.Add($"arg{i + 1}");

            return string.Join(", ", parts);
        }
    }
}
