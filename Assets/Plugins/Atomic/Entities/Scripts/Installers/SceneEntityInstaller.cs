using System;
using UnityEngine;

namespace Atomic.Entities
{
    public abstract class SceneEntityInstaller : MonoBehaviour, IEntityInstaller
    {

        public abstract void Install(IEntity entity);
    }
}