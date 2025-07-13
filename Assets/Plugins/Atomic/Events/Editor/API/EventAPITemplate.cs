#if UNITY_EDITOR
namespace Atomic.Events
{
    internal static class EventAPITemplate
    {
        internal const string Value =
            "header: EventBusAPI\n" +
            "entityType: IEventBus\n" +
            "aggressiveInlining: true\n" +
            "\n" +
            "namespace: SampleGame\n" +
            "className: SampleEventAPI\n" +
            "directory: Assets/Scripts/Codegen\n " +
            "\n" +
            "imports:\n" +
            "- UnityEngine\n" +
            "- Atomic.Events\n" +
            "\n" +
            "events:\n" +
            "- Hello()\n" +
            "- Attack(GameObject target)\n" +
            "- Spawn(GameObject prefab, Vector3 position, Quaternion rotation) \n" +
            "\n";
    }
}
#endif