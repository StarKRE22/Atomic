#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Atomic.Events
{
    internal static class EventAPIGenerator
    {
        private const string NAMESPACE = "Atomic.Events";
        private const string AGRESSIVE_INLINING = "\t\t[MethodImpl(MethodImplOptions.AggressiveInlining)]";

        public static void CreateFile(IEventAPIConfig config)
        {
            string directoryPath = config.Directory;
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            string className = config.ClassName;
            string filePath = $"{directoryPath}/{className}.cs";

            string ns = config.Namespace;
            IReadOnlyCollection<string> imports = config.GetImports();
            IReadOnlyCollection<EventDefinition> events = config.GetEvents();
            bool useInlining = config.AggressiveInlining;
            string entityType = config.EventBusType;

            string content = GenerateContent(ns, className, imports, events, entityType, useInlining);
            using StreamWriter writer = new StreamWriter(filePath);
            writer.Write(content);
        }

        public static void UpdateFile(IEventAPIConfig config)
        {
            string directoryPath = config.Directory;
            if (!Directory.Exists(directoryPath))
                return;

            string className = config.ClassName;
            string filePath = $"{directoryPath}/{className}.cs";
            if (!File.Exists(filePath))
                return;

            string ns = config.Namespace;
            IReadOnlyCollection<string> imports = config.GetImports();
            IReadOnlyCollection<EventDefinition> events = config.GetEvents();

            string busType = config.EventBusType;
            bool useInlining = config.AggressiveInlining;

            string content = GenerateContent(ns, className, imports, events, busType, useInlining);
            using StreamWriter writer = new StreamWriter(filePath);
            writer.Write(content);
        }

        private static string GenerateContent(
            string ns,
            string className,
            IEnumerable<string> imports,
            IReadOnlyCollection<EventDefinition> events,
            string eventBusType,
            bool useInlining
        )
        {
            StringBuilder sb = new StringBuilder();
            int eventCount = events.Count;

            //Generate comments:
            sb.AppendLine("/**");
            sb.AppendLine("* Code generation. Don't modify! ");
            sb.AppendLine("**/");
            sb.AppendLine();

            //Generate imports:
            sb.AppendLine($"using {NAMESPACE};");

            if (useInlining)
                sb.AppendLine("using System.Runtime.CompilerServices;");

            foreach (string import in imports)
                sb.AppendLine($"using {import};");

            //Generate start of class:
            sb.AppendLine();
            sb.AppendLine($"namespace {ns}");
            sb.AppendLine("{");
            sb.AppendLine($"\tpublic static class {className}");
            sb.AppendLine("\t{");

            //Generate event keys:
            if (eventCount > 0)
            {
                sb.AppendLine("\t\t///Events");
                foreach (EventDefinition e in events)
                    GenerateEvent(sb, e);
            }

            //Generate event extensions:
            if (eventCount > 0)
            {
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine("\t\t///Event Extensions");
                foreach (EventDefinition e in events)
                    GenerateEventExtensions(sb, e, eventBusType, useInlining);
            }

            //Generate end of class:
            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private static void GenerateEvent(StringBuilder sb, EventDefinition e)
        {
            sb.AppendLine($"\t\tpublic const int {e.name} = {e.id};");
        }

        private static void GenerateEventExtensions(
            StringBuilder sb,
            EventDefinition e,
            string busType,
            bool useInlining
        )
        {
            string name = e.name;
            string genericParams = GetGenericParams(e);

            sb.AppendLine();

            //IsDefined:
            if (useInlining) sb.AppendLine(AGRESSIVE_INLINING);
            sb.AppendLine($"\t\tpublic static bool Is{name}Declared(this {busType} bus) => bus.IsDefined({name});");
            
            //Undef:
            sb.AppendLine();
            if (useInlining) sb.AppendLine(AGRESSIVE_INLINING);
            sb.AppendLine($"\t\tpublic static bool Undeclare{name}(this {busType} bus) => bus.Undef({name});");

            //Def:
            sb.AppendLine();
            if (useInlining) sb.AppendLine(AGRESSIVE_INLINING);
            sb.AppendLine($"\t\tpublic static void Declare{name}(this {busType} bus) => bus.Def{genericParams}({name});");

            //Subscribe:
            sb.AppendLine();
            if (useInlining) sb.AppendLine(AGRESSIVE_INLINING);
            sb.AppendLine(
                $"\t\tpublic static Action{genericParams} Subscribe{name}(this {busType} bus, Action{genericParams} action) => " +
                $"bus.Subscribe({name}, action);");

            //Unsubscribe:
            sb.AppendLine();
            if (useInlining) sb.AppendLine(AGRESSIVE_INLINING);
            sb.AppendLine(
                $"\t\tpublic static void Unsubscribe{name}(this {busType} bus, Action{genericParams} action) => " +
                $"bus.Unsubscribe({name}, action);");


            //Invoke:
            sb.AppendLine();
            if (useInlining) sb.AppendLine(AGRESSIVE_INLINING);
            sb.AppendLine($"\t\tpublic static void Invoke{name}(this {busType} bus{GetArgs(e)}) => " +
                          $"bus.Invoke({name}{GetArgsNames(e)});");
        }

        private static string GetGenericParams(EventDefinition e)
        {
            var args = e.args;

            return args.Count == 0
                ? string.Empty
                : $"<{string.Join(", ", args.Select(it => it.type))}>";
        }

        private static string GetArgsNames(EventDefinition e)
        {
            var args = e.args;

            return args.Count == 0
                ? string.Empty
                : $", {string.Join(", ", args.Select(it => it.name))}";
        }

        private static string GetArgs(EventDefinition e)
        {
            var args = e.args;

            return args.Count == 0
                ? string.Empty
                : $", {string.Join(", ", args.Select(it => $"{it.type} {it.name}"))}";
        }
    }
}
#endif



	// 	///Event Extensions
 //
	// 	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	// 	public static bool IsHelloDefined(this IEntity bus) => bus.IsDefined(Hello);
 //
	// 	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	// 	public static bool NotHelloDefined(this IEntity bus) => !bus.IsDefined(Hello);
 //
	// 	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	// 	public static bool UndefHello(this IEntity bus) => bus.Undef(Hello);
 //
	// 	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	// 	public static bool DefHello(this IEntity bus) => bus.Def(Hello);
 //
	// 	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	// 	public static Action SubscribeOnHello(this IEntity bus, Action action) => bus.Subscribe(Hello, action);
 //
	// 	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	// 	public static void UnsubscribeFromHello(this IEntity bus, Action action) => bus.Unsubscribe(Hello, action);
 //
	// 	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	// 	public static void InvokeHello(this IEntity bus) => bus.Invoke(Hello);
 //
	// 	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	// 	public static bool IsAttackDefined(this IEntity bus) => bus.IsDefined(Attack);
 //
	// 	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	// 	public static bool NotAttackDefined(this IEntity bus) => !bus.IsDefined(Attack);
 //
	// 	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	// 	public static bool UndefAttack(this IEntity bus) => bus.Undef(Attack);
 //
	// 	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	// 	public static bool DefAttack(this IEntity bus) => bus.Def<GameObject>(Attack);
 //
	// 	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	// 	public static Action<GameObject> SubscribeOnAttack(this IEntity bus, Action<GameObject> action) => bus.Subscribe(Attack, action);
 //
	// 	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	// 	public static void UnsubscribeFromAttack(this IEntity bus, Action<GameObject> action) => bus.Unsubscribe(Attack, action);
 //
	// 	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	// 	public static void InvokeAttack(this IEntity bus,GameObject target) => bus.Invoke(Attack,target);
 //
	// 	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	// 	public static bool IsSpawnDefined(this IEntity bus) => bus.IsDefined(Spawn);
 //
	// 	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	// 	public static bool NotSpawnDefined(this IEntity bus) => !bus.IsDefined(Spawn);
 //
	// 	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	// 	public static bool UndefSpawn(this IEntity bus) => bus.Undef(Spawn);
 //
	// 	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	// 	public static bool DefSpawn(this IEntity bus) => bus.Def<GameObject,Vector3,Quaternion>(Spawn);
 //
	// 	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	// 	public static Action<GameObject,Vector3,Quaternion> SubscribeOnSpawn(this IEntity bus, Action<GameObject,Vector3,Quaternion> action) => bus.Subscribe(Spawn, action);
 //
	// 	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	// 	public static void UnsubscribeFromSpawn(this IEntity bus, Action<GameObject,Vector3,Quaternion> action) => bus.Unsubscribe(Spawn, action);
 //
	// 	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	// 	public static void InvokeSpawn(this IEntity bus,GameObject prefab,Vector3 position,Quaternion rotation) => bus.Invoke(Spawn,prefab,position,rotation);
 //    }