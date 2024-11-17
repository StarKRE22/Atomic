#if UNITY_EDITOR
using UnityEditor;

namespace Atomic.Contexts
{
    internal static class MenuItems
    {
        [MenuItem("Window/Atomic/Context/Show Console", priority = 7)]
        internal static void ShowValueCatalogWindow()
        {
            EditorWindow.GetWindow(typeof(APICatalogWindow));
        }

        [MenuItem("Tools/Atomic/Context/Select Settings", priority = 7)]
        internal static void SelectValueCatalog()
        {
            Selection.activeObject = APICatalogService.GetCatalog();
            
            if (Selection.activeObject == null)
            {
                Selection.activeObject = APICatalogService.CreateCatalog();
            }
        }
    }
}
#endif