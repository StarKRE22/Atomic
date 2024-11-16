using UnityEditor;

namespace Atomic.Entities
{
    public static class MenuPresenter
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

        [MenuItem("Tools/Atomic/Entities/Create API", priority = 7)]
        internal static void CreateAPI()
        {
            EntityAPIManager.CreateAPI();
        }
    }
}