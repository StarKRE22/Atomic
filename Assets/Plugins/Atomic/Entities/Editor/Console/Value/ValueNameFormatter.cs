using Atomic.Entities;

namespace Atomic.Entities
{
    public sealed class ValueNameFormatter : SceneEntity.IValueNameFormatter
    {
        public string GetName(int id)
        {
            ValueConfig config = ValueManager.GetValueConfig();
            if (config == null)
            {
                return id.ToString();
            }
            
            return config.GetFullItemNameById(id);
        }
    }
}