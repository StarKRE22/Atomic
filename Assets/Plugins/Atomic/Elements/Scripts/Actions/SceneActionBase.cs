using UnityEngine;

namespace Atomic.Elements
{
    public abstract class SceneActionBase : MonoBehaviour, IAction
    {
        public abstract void Invoke();
    }
}