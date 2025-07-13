using UnityEditor;

namespace Atomic.Entities
{
    internal static class MenuPresenter
    {
        [MenuItem("Tools/Atomic/Entities/Compile API", priority = 7)]
        internal static void Compile()
        {
            EntityAPIManager.CompileAPI();
        }
        
        [MenuItem("Tools/Atomic/Entities/Refresh API", priority = 7)]
        internal static void Refresh()
        {
            EntityAPIManager.RefreshAPI();
        }

        [MenuItem("Assets/Create/Atomic/Entities/Create API", priority = 7)]
        internal static void CreateAPI()
        {
            EntityAPIManager.CreateAPI();
        }
        
        [MenuItem("Tools/Atomic/Entities/Select Settings", priority = 7)]
        internal static void SelectSetttings()
        {
            Selection.activeObject = EntityAPISettings.Instance;
        }
    }
}