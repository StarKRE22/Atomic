using UnityEditor;

namespace Atomic.Events
{
    internal static class MenuPresenter
    {
        [MenuItem("Tools/Atomic/Events/Compile API", priority = 7)]
        internal static void Compile()
        {
            EventAPIManager.CompileAPI();
        }
        
        [MenuItem("Tools/Atomic/Events/Refresh API", priority = 7)]
        internal static void Refresh()
        {
            EventAPIManager.RefreshAPI();
        }

        [MenuItem("Assets/Create/Atomic/Events/Create API", priority = 7)]
        internal static void CreateAPI()
        {
            EventAPIManager.CreateAPI();
        }
        
        [MenuItem("Tools/Atomic/Events/Select Settings", priority = 7)]
        internal static void SelectSetttings()
        {
            Selection.activeObject = EventBusAPISettings.Instance;
        }
    }
}