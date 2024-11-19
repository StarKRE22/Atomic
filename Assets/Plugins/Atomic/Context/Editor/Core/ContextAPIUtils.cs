#if UNITY_EDITOR
namespace Atomic.Contexts
{
    internal static class ContextAPIUtils
    {
        internal const string AssetContent =
            "contextType: IContext\n" +
            "aggressiveInlining: true\n" +
            "\n" +
            "namespace: SampleGame\n" +
            "className: SampleEntityAPI\n" +
            "directory: Assets/Scripts/Codegen\n " +
            "\n" +
            "imports:\n" +
            "- UnityEngine\n" +
            "- Atomic.Entities\n" +
            "\n" +
            "values:\n" +
            "- Health: int\n" +
            "- Speed: float\n" +
            "- Transform: Transform\n";
    }
}
#endif