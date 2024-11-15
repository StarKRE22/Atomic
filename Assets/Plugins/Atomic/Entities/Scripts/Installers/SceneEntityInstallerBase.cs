using System;
using UnityEngine;

namespace Atomic.Entities
{
    public abstract class SceneEntityInstallerBase : MonoBehaviour, IEntityInstaller
    {
#if UNITY_EDITOR
        internal Action mRefreshCallback;
#endif
        public abstract void Install(IEntity entity);
    }
}