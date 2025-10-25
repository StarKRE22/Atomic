using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class MoveAnimBehaviour : IEntityInit<IActor>, IEntityDispose, IEntityLateTick
    {
        private static readonly int IsMoving = Animator.StringToHash(nameof(IsMoving));

        private Animator _animator;
        private ISignal<Vector3> _moveEvent;
        private IReactiveVariable<bool> _isMoving;

        private readonly float _moveDuration;
        private float _moveTime;

        public MoveAnimBehaviour(float moveDuration = 0.08f)
        {
            _moveDuration = moveDuration;
        }

        public void Init(IActor entity)
        {
            _animator = entity.GetAnimator();
            _moveEvent = entity.GetMovementEvent();
            _moveEvent.Subscribe(this.OnMoved);
        }

        public void Dispose(IEntity entity)
        {
            _moveEvent.Unsubscribe(this.OnMoved);
        }
        
        private void OnMoved(Vector3 _)
        {
            _animator.SetBool(IsMoving, true);
            _moveTime = _moveDuration;
        }

        public void LateTick(IEntity entity, float deltaTime)
        {
            if (_moveTime <= 0)
                return;

            _moveTime -= deltaTime;
            if (_moveTime <= 0)
                _animator.SetBool(IsMoving, false);
        }
    }
}