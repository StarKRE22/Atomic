#if UNITY_EDITOR
using UnityEditor;

namespace Atomic.Entities
{
    internal static class MenuItems
    {
        [MenuItem("Window/Atomic/Entities/Show Value Console", priority = 7)]
        internal static void ShowValueCatalogWindow()
        {
            EditorWindow.GetWindow(typeof(ValueWindow));
        }

        [MenuItem("Tools/Atomic/Entities/Select Value Settings", priority = 7)]
        internal static void SelectValueCatalog()
        {
            Selection.activeObject = ValueManager.GetValueConfig();
            if (Selection.activeObject == null)
            {
                Selection.activeObject = ValueManager.CreateValueConfig();
            }
        }
    }
}
#endif