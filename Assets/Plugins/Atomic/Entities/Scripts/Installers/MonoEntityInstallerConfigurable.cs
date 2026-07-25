using UnityEngine;

namespace Atomic.Entities
{
    public sealed class MonoEntityInstallerConfigurable : MonoEntityInstaller
    {
        [SerializeReference]
        private IEntityInstaller[] _installers;

        public override void Install(IEntity entity)
        {
            if (_installers == null)
                return;

            foreach (IEntityInstaller installer in _installers)
                if (installer != null)
                    installer.Install(entity);
        }
    }
}