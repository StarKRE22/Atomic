using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Entities
{
    //TODO:
    public abstract class EntityViewBase : MonoBehaviour
    {
        public virtual string Name => this.name;


#if ODIN_INSPECTOR
        [Title("Debug")]
        [ShowInInspector, HideInEditorMode]
#endif
        protected IEntity _entity;

        public abstract void Show(IEntity entity);

        public abstract void Hide();
    }
}


// public struct Unit
// {
// }
//
// public class Graph<TNode> : Graph<TNode, Unit>
// {
// }
//
// var graph = new Graph<ConcreteNode, Unit>();