#if UNITY_EDITOR
namespace Atomic.Entities
{
    internal static class EntityAPITemplate
    {
        internal const string Instance =
            "header: EntityAPI\n" +
            "entityType: IEntity\n" +
            "aggressiveInlining: true\n" +
            "unsafeAccess: true\n" +
            "\n" +
            "namespace: SampleGame\n" +
            "className: SampleEntityAPI\n" +
            "directory: Assets/Scripts/Codegen\n " +
            "\n" +
            "imports:\n" +
            "- UnityEngine\n" +
            "- Atomic.Entities\n" +
            "\n" +
            "tags:\n" +
            "- Player\n" +
            "- Enemy\n" +
            "- Resource\n" +
            "\n" +
            "values:\n" +
            "- Health: int\n" +
            "- Speed: float\n" +
            "- Transform: Transform\n";
    }
}
#endif