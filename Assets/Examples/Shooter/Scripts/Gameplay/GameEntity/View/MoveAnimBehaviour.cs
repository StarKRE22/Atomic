using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class MoveAnimBehaviour : IEntitySpawn<IGameEntity>, IEntityDespawn, IEntityLateUpdate
    {
        private const float MOVE_DURATION = 0.08f;
        private static readonly int IsMoving = Animator.StringToHash(nameof(IsMoving));

        private Animator _animator;
        private ISignal<Vector3> _moveEvent;
        private IReactiveVariable<bool> _isMoving;

        private float _moveTime;

        public void OnSpawn(IGameEntity entity)
        {
            _animator = entity.GetAnimator();
            _moveEvent = entity.GetMovementEvent();
            _moveEvent.Subscribe(this.OnMoved);
        }

        public void OnDespawn(IEntity entity)
        {
            _moveEvent.Unsubscribe(this.OnMoved);
        }
        
        private void OnMoved(Vector3 _)
        {
            _animator.SetBool(IsMoving, true);
            _moveTime = MOVE_DURATION;
        }

        public void OnLateUpdate(IEntity entity, float deltaTime)
        {
            if (_moveTime <= 0)
                return;

            _moveTime -= deltaTime;
            if (_moveTime <= 0)
                _animator.SetBool(IsMoving, false);
        }
    }
}