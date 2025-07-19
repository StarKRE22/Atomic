using System;
using UnityEngine;

namespace Atomic.Entities
{
    // [AddComponentMenu("Atomic/Entities/Entity Proxy")]
    // [DisallowMultipleComponent]
    // public class SceneEntityProxy<E> : SceneEntityProxy<E> where E : SceneEntity<E>
    // {
    // }

    public abstract partial class SceneEntityProxy<E> : MonoBehaviour, IEntity<E> where E : SceneEntity<E>
    {
        public event Action OnStateChanged
        {
            add => _source.OnStateChanged += value;
            remove => _source.OnStateChanged -= value;
        }

        public SceneEntity<E> Source => _source;
        
        [SerializeField]
        private SceneEntity<E> _source;

        public int InstanceID => _source.InstanceID;

        public string Name
        {
            get => _source.Name;
            set => _source.Name = value;
        }
        
        public void Clear() => _source.Clear();

        public override bool Equals(object obj) => 
            obj is IEntity<E> entity && _source.InstanceID == entity.InstanceID;

        public override int GetHashCode() => _source.GetHashCode();

        private void Reset() => _source = this.GetComponentInParent<SceneEntity<E>>();
    }
}