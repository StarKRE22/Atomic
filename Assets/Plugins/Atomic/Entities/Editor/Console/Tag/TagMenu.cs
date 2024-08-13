#if UNITY_EDITOR
using UnityEditor;

namespace Atomic.Entities
{
    public static class TagMenu
    {
        [MenuItem("Window/Atomic/Entities/Show Tag Console", priority = 7)]
        internal static void ShowTagCatalogWindow()
        {
            EditorWindow.GetWindow(typeof(TagWindow));
        }
        
        [MenuItem("Tools/Atomic/Entities/Select Tag Settings", priority = 7)]
        internal static void SelectTagCatalog()
        {
            Selection.activeObject = TagManager.GetTagConfig();
        }
    }
}
#endif