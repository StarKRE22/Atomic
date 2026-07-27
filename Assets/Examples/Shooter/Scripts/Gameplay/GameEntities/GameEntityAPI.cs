using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    [GenerateEntityExtensionsAPI]
    public static partial class GameEntityAPI
    {
        public static readonly TagKey<IGameEntity> Damageable = new(nameof(Damageable));

        public static readonly ValueKey<IGameEntity, IVariable<Vector3>> Position = new(nameof(Position));
        public static readonly ValueKey<IGameEntity, IVariable<Quaternion>> Rotation = new(nameof(Rotation));
        public static readonly ValueKey<IGameEntity, IVariable<Transform>> Parent = new(nameof(Parent));
        public static readonly ValueKey<IGameEntity, IValue<float>> MovementSpeed = new(nameof(MovementSpeed));
        public static readonly ValueKey<IGameEntity, IExpression<bool>> MovementCondition = new(nameof(MovementCondition));
        public static readonly ValueKey<IGameEntity, IReactiveVariable<Vector3>> MovementDirection = new(nameof(MovementDirection));
        public static readonly ValueKey<IGameEntity, IEvent<Vector3>> MovementEvent = new(nameof(MovementEvent));
        public static readonly ValueKey<IGameEntity, IExpression<bool>> RotationCondition = new(nameof(RotationCondition));
        public static readonly ValueKey<IGameEntity, IValue<float>> RotationSpeed = new(nameof(RotationSpeed));
        public static readonly ValueKey<IGameEntity, IReactiveVariable<Vector3>> RotationDirection = new(nameof(RotationDirection));
        public static readonly ValueKey<IGameEntity, IEvent<Vector3>> RotationEvent = new(nameof(RotationEvent));
        public static readonly ValueKey<IGameEntity, Health> Health = new(nameof(Health));
        public static readonly ValueKey<IGameEntity, Cooldown> Lifetime = new(nameof(Lifetime));
        public static readonly ValueKey<IGameEntity, IEvent<DamageArgs>> TakeDamageEvent = new(nameof(TakeDamageEvent));
        public static readonly ValueKey<IGameEntity, IEvent<DamageArgs>> TakeDeathEvent = new(nameof(TakeDeathEvent));
        public static readonly ValueKey<IGameEntity, IAction> DestroyAction = new(nameof(DestroyAction));
        public static readonly ValueKey<IGameEntity, IEvent> RespawnEvent = new(nameof(RespawnEvent));
        public static readonly ValueKey<IGameEntity, IReactiveVariable<TeamType>> TeamType = new(nameof(TeamType));
        public static readonly ValueKey<IGameEntity, IWeapon> Weapon = new(nameof(Weapon));
        public static readonly ValueKey<IGameEntity, IValue<int>> Damage = new(nameof(Damage));
        public static readonly ValueKey<IGameEntity, IReactiveVariable<IGameEntity>> Target = new(nameof(Target));
        public static readonly ValueKey<IGameEntity, IExpression<bool>> FireCondition = new(nameof(FireCondition));
        public static readonly ValueKey<IGameEntity, Cooldown> FireCooldown = new(nameof(FireCooldown));
        public static readonly ValueKey<IGameEntity, Transform> FirePoint = new(nameof(FirePoint));
        public static readonly ValueKey<IGameEntity, IAction> FireAction = new(nameof(FireAction));
        public static readonly ValueKey<IGameEntity, IEvent> FireEvent = new(nameof(FireEvent));
        public static readonly ValueKey<IGameEntity, TriggerEvents> Trigger = new(nameof(Trigger));
        public static readonly ValueKey<IGameEntity, IVariable<int>> PhysicsLayer = new(nameof(PhysicsLayer));
        public static readonly ValueKey<IGameEntity, Rigidbody> Rigidbody = new(nameof(Rigidbody));
        public static readonly ValueKey<IGameEntity, Renderer> Renderer = new(nameof(Renderer));
        public static readonly ValueKey<IGameEntity, Animator> Animator = new(nameof(Animator));
        public static readonly ValueKey<IGameEntity, HitPointsView> HitPointsView = new(nameof(HitPointsView));
    }
}
