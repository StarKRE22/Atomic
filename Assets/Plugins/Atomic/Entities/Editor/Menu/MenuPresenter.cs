using UnityEditor;

namespace Atomic.Entities
{
    public static class MenuPresenter
    {
        [MenuItem("Tools/Atomic/Entities/Compile API", priority = 7)]
        internal static void Compile()
        {
            EntityAPIManager.Compile();
        }
        
        [MenuItem("Tools/Atomic/Entities/Refresh API", priority = 7)]
        internal static void Refresh()
        {
            EntityAPIManager.Refresh();
        }
        
        //TODO: CREATE YAML FILE
    }
}