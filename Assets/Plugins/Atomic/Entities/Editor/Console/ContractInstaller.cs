using UnityEditor;

namespace Atomic.Entities
{
    [InitializeOnLoad]
    public static class ContractInstaller
    {
        static ContractInstaller()
        {
            SceneEntity.TagNameFormatter = new TagNameFormatter();
            SceneEntity.ValueNameFormatter = new ValueNameFormatter();
        }
    }
}