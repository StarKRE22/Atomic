using Atomic.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace SampleGame
{
    public abstract class EntityView : MonoBehaviour
    {
        public abstract void Show(IEntity entity);
        
        public abstract void Hide(IEntity entity);
    }

    public sealed partial class DeerView : EntityView
    {
        [SerializeField]
        private Text _text;

        [SerializeField]
        private Animator _animator;
        
        public override void Show(IEntity entity)
        {
            int health = entity.GetValue<int>("Health");
            _text.text = health.ToString();
        }

        public override void Hide(IEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}