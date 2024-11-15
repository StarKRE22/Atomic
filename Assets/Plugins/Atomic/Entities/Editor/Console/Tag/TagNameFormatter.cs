using Atomic.Entities;

namespace Atomic.Entities
{
    public sealed class TagNameFormatter : SceneEntity.ITagNameFormatter
    {
        public string GetName(int id)
        {
            TagsConfig config = TagManager.GetTagConfig();
            if (config == null)
            {
                return id.ToString();
            }

            if (!config.TryFindNameById(id, out string name))
            {
                return id.ToString();
            }

            return $"{name} ({id})";
        }
    }
}