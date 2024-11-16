using UnityEditor;

namespace Atomic.Entities
{
    public static class MenuPresenter
    {
        [MenuItem("Tools/Atomic/Entities/Generate API", priority = 7)]
        internal static void GenerateAPI()
        {
            //TODO: SELECT YAML FILE
            EntityAPIController.Generate();
        }
    }
}