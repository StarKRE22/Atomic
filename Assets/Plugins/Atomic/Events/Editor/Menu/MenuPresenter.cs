#if UNITY_EDITOR
using UnityEditor;

namespace Atomic.Events
{
    internal static class MenuPresenter
    {
        [MenuItem("Tools/Atomic/Events/Compile Event API", priority = 7)]
        internal static void Compile() => EventAPIManager.CompileAPI();

        [MenuItem("Tools/Atomic/Events/Refresh Event API", priority = 7)]
        internal static void Refresh() => EventAPIManager.RefreshAPI();

        [MenuItem("Assets/Create/Atomic/Events/New Event API", priority = 7)]
        internal static void CreateAPI() => EventAPIManager.CreateAPI();

        [MenuItem("Tools/Atomic/Events/Select Event API Settings", priority = 7)]
        internal static void SelectSetttings() => Selection.activeObject = EventAPISettings.Instance;
    }
}
#endif