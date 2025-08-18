using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "WarriorInstaller",
        menuName = "RTSGame/Entities/New WarriorInstaller"
    )]
    public sealed class WarriorFactory : ScriptableEntityInstaller<IGameEntity>
    {
        [SerializeField]
        private Const<float> _moveSpeed = 3;

        [SerializeField]
        private Const<float> _rotationSpeed = 12;

        [SerializeField]
        private Const<float> _attackDistance = 1;

        [SerializeField]
        private Const<int> _damage;

        [SerializeField]
        private int _health;

        [SerializeField]
        private float _attackCooldown = 1;

        protected override void Install(IGameEntity entity)
        {
            entity.AddUnitTag();

            this.InstallMove(entity);
            this.InstallCombat(entity);
            this.InstallRotation(entity);
            this.InstallLife(entity);
            this.InstallAI(entity);
        }

        private void InstallAI(IEntity entity)
        {
            entity.AddTarget(new ReactiveVariable<IEntity>());
            entity.AddBehaviour(new DetectTargetBehaviour(new RandomCooldown(0.2f, 0.3f)));
            entity.AddBehaviour<AttackTargetBehaviour>();
        }

        private void InstallRotation(IEntity entity)
        {
            entity.AddRotationSpeed(_rotationSpeed);
        }

        private void InstallLife(IEntity entity)
        {
            entity.AddDamageableTag();
            entity.AddHealth(new Health(_health, _health));
            entity.AddBehaviour<DeathBehaviour>();
        }

        private void InstallCombat(IEntity entity)
        {
            entity.AddDamage(_damage);
            entity.AddFireCooldown(new Cooldown(_attackCooldown));
            entity.AddFireAction(new BaseAction<IEntity>(target =>
            {
                TakeDamageUseCase.TakeDamage(target, entity.GetDamage().Value);
                entity.GetFireCooldown().Reset();
            }));
            entity.AddFireCondition(new BaseFunction<IEntity, bool>(_ =>
                HealthUseCase.IsAlive(entity) && entity.GetFireCooldown().IsExpired())
            );

            entity.WhenFixedUpdate(entity.GetFireCooldown().Tick);
            entity.AddFireDistance(_attackDistance);
            entity.AddFireEvent(new BaseEvent<IEntity>());
        }

        private void InstallMove(IEntity entity)
        {
            entity.AddMoveableTag();
            entity.AddMoveSpeed(_moveSpeed);
            entity.AddMoveCondition(new BaseFunction<Vector3, float, bool>((_, _) =>
                HealthUseCase.IsAlive(entity)));

            entity.AddMoveAction(new BaseAction<Vector3, float>((direction, deltaTime) =>
            {
                MoveUseCase.MoveStep(entity, direction, deltaTime);
                RotateUseCase.RotateStep(entity, direction, deltaTime);
            }));
            entity.AddMoveEvent(new BaseEvent<Vector3, float>());
        }
    }
}