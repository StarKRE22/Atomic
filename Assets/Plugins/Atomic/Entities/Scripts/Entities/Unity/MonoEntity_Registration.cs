#if UNITY_5_3_OR_NEWER
namespace Atomic.Entities
{
    public partial class MonoEntity
    {
        private bool _registered;

        private void Register()
        {
            if (_registered && _instanceId > 0)
                return;

            EntityRegistry.Instance.Register(this);
            _registered = true;
        }

        private void Unregister()
        {
            if (_registered)
            {
                EntityRegistry.Instance.Unregister(this);
                _registered = false;
            }
        }
    }
}
#endif