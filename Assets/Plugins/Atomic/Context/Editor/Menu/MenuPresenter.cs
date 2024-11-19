using UnityEditor;

namespace Atomic.Contexts
{
    internal static class MenuPresenter
    {
        [MenuItem("Tools/Atomic/Contexts/Compile API", priority = 7)]
        internal static void Compile()
        {
            ContextAPIManager.CompileAPI();
        }
        
        [MenuItem("Tools/Atomic/Contexts/Refresh API", priority = 7)]
        internal static void Refresh()
        {
            ContextAPIManager.RefreshAPI();
        }

        [MenuItem("Assets/Create/Atomic/Contexts/Create API", priority = 7)]
        internal static void CreateAPI()
        {
            ContextAPIManager.CreateAPI();
        }
    }
}