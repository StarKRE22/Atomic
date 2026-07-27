using Atomic.Elements;
using Atomic.Entities;
using Modules.SpatialStructures;
using System;
using UnityEngine;

namespace RTSGame
{
    [EntityAPI(Unsafe = true)]
    public static partial class GameEntityAPI
    {
        public static readonly TagKey<IGameEntity> Damageable = new(nameof(Damageable));
        public static readonly TagKey<IGameEntity> Moveable = new(nameof(Moveable));
        public static readonly TagKey<IGameEntity> Fireable = new(nameof(Fireable));
        public static readonly TagKey<IGameEntity> Projectile = new(nameof(Projectile));
        public static readonly TagKey<IGameEntity> Unit = new(nameof(Unit));
        public static readonly TagKey<IGameEntity> Targeted = new(nameof(Targeted));
        public static readonly TagKey<IGameEntity> Detector = new(nameof(Detector));
        public static readonly TagKey<IGameEntity> Attacker = new(nameof(Attacker));

        public static readonly ValueKey<IGameEntity, IValue<GameEntityType>> EntityType = new(nameof(EntityType));
        public static readonly ValueKey<IGameEntity, IReactiveVariable<Vector3>> Position = new(nameof(Position));
        public static readonly ValueKey<IGameEntity, IReactiveVariable<Quaternion>> Rotation = new(nameof(Rotation));
        public static readonly ValueKey<IGameEntity, IValue<float>> Scale = new(nameof(Scale));
        public static readonly ValueKey<IGameEntity, IValue<float>> MoveSpeed = new(nameof(MoveSpeed));
        public static readonly ValueKey<IGameEntity, IRequest<Vector3>> MoveRequest = new(nameof(MoveRequest));
        public static readonly ValueKey<IGameEntity, ICommand<Vector3, float>> MoveCommand = new(nameof(MoveCommand));
        public static readonly ValueKey<IGameEntity, IValue<float>> RotationSpeed = new(nameof(RotationSpeed));
        public static readonly ValueKey<IGameEntity, Health> Health = new(nameof(Health));
        public static readonly ValueKey<IGameEntity, Cooldown> Lifetime = new(nameof(Lifetime));
        public static readonly ValueKey<IGameEntity, IAction> DestroyAction = new(nameof(DestroyAction));
        public static readonly ValueKey<IGameEntity, IEvent<int>> TakeDamageEvent = new(nameof(TakeDamageEvent));
        public static readonly ValueKey<IGameEntity, IReactiveVariable<TeamType>> Team = new(nameof(Team));
        public static readonly ValueKey<IGameEntity, IValue<int>> Damage = new(nameof(Damage));
        public static readonly ValueKey<IGameEntity, IReactiveVariable<IGameEntity>> Target = new(nameof(Target));
        public static readonly ValueKey<IGameEntity, IValue<float>> FireDistance = new(nameof(FireDistance));
        public static readonly ValueKey<IGameEntity, IRequest<IGameEntity>> FireRequest = new(nameof(FireRequest));
        public static readonly ValueKey<IGameEntity, ICommand<IGameEntity>> FireCommand = new(nameof(FireCommand));
        public static readonly ValueKey<IGameEntity, Cooldown> FireCooldown = new(nameof(FireCooldown));
        public static readonly ValueKey<IGameEntity, IValue<Vector3>> FirePoint = new(nameof(FirePoint));
        public static readonly ValueKey<IGameEntity, IValue<float>> DetectionRadius = new(nameof(DetectionRadius));
        public static readonly ValueKey<IGameEntity, ICooldown> DetectionCooldown = new(nameof(DetectionCooldown));
        public static readonly ValueKey<IGameEntity, IReactiveVariable<EntityUpdatePriority>> UpdatePriority = new(nameof(UpdatePriority));
    }
}
