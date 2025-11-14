using UnityEngine;
using UnityEditor;

namespace Atomic.Entities
{
    public static class EntityBaking
    {
        [MenuItem("Tools/Atomic/Entities/Refresh Scene Bakers")]
        public static void RefreshSceneBakers()
        {
            Debug.Log("Refreshing all editor-refreshable objects...");

            var allComponents = Object.FindObjectsOfType<MonoBehaviour>(true);

            int count = 0;

            foreach (MonoBehaviour comp in allComponents)
            {
                if (comp is ISceneEntityBakerOptimized baker)
                {
                    baker.Refresh();
                    EditorUtility.SetDirty(comp);
                    count++;
                }
            }

            Debug.Log($"Refreshed {count} bakers on the scene.");
        }
    }
}