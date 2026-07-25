/**
* Code generation. Don't modify! 
**/

using Atomic.Entities;
using static Atomic.Entities.EntityKeyStore;
using System.Runtime.CompilerServices;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Atomic.Elements;
using System;

namespace RTSGame
{
#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	public static class GameEntityAPI
	{

		///Tags
		public static readonly int Damageable;
		public static readonly int Moveable;
		public static readonly int Fireable;
		public static readonly int Projectile;
		public static readonly int Unit;
		public static readonly int Targeted;
		public static readonly int Detector;
		public static readonly int Attacker;

		///Values
		public static readonly int EntityType; // IValue<GameEntityType>
		public static readonly int Position; // IReactiveVariable<Vector3>
		public static readonly int Rotation; // IReactiveVariable<Quaternion>
		public static readonly int Scale; // IValue<float>
		public static readonly int MoveSpeed; // IValue<float>
		public static readonly int MoveRequest; // IRequest<Vector3>
		public static readonly int MoveCommand; // ICommand<Vector3, float>
		public static readonly int RotationSpeed; // IValue<float>
		public static readonly int Health; // Health
		public static readonly int Lifetime; // Cooldown
		public static readonly int DestroyAction; // IAction
		public static readonly int TakeDamageEvent; // IEvent<int>
		public static readonly int Team; // IReactiveVariable<TeamType>
		public static readonly int Damage; // IValue<int>
		public static readonly int Target; // IReactiveVariable<IGameEntity>
		public static readonly int FireDistance; // IValue<float>
		public static readonly int FireRequest; // IRequest<IGameEntity>
		public static readonly int FireCommand; // ICommand<IGameEntity>
		public static readonly int FireCooldown; // Cooldown
		public static readonly int FirePoint; // IValue<Vector3>
		public static readonly int DetectionRadius; // IValue<float>
		public static readonly int DetectionCooldown; // ICooldown
		public static readonly int UpdatePriority; // IReactiveVariable<EntityUpdatePriority>

		static GameEntityAPI()
		{
			//Tags
			Damageable = NameToId(nameof(Damageable));
			Moveable = NameToId(nameof(Moveable));
			Fireable = NameToId(nameof(Fireable));
			Projectile = NameToId(nameof(Projectile));
			Unit = NameToId(nameof(Unit));
			Targeted = NameToId(nameof(Targeted));
			Detector = NameToId(nameof(Detector));
			Attacker = NameToId(nameof(Attacker));

			//Values
			EntityType = NameToId(nameof(EntityType));
			Position = NameToId(nameof(Position));
			Rotation = NameToId(nameof(Rotation));
			Scale = NameToId(nameof(Scale));
			MoveSpeed = NameToId(nameof(MoveSpeed));
			MoveRequest = NameToId(nameof(MoveRequest));
			MoveCommand = NameToId(nameof(MoveCommand));
			RotationSpeed = NameToId(nameof(RotationSpeed));
			Health = NameToId(nameof(Health));
			Lifetime = NameToId(nameof(Lifetime));
			DestroyAction = NameToId(nameof(DestroyAction));
			TakeDamageEvent = NameToId(nameof(TakeDamageEvent));
			Team = NameToId(nameof(Team));
			Damage = NameToId(nameof(Damage));
			Target = NameToId(nameof(Target));
			FireDistance = NameToId(nameof(FireDistance));
			FireRequest = NameToId(nameof(FireRequest));
			FireCommand = NameToId(nameof(FireCommand));
			FireCooldown = NameToId(nameof(FireCooldown));
			FirePoint = NameToId(nameof(FirePoint));
			DetectionRadius = NameToId(nameof(DetectionRadius));
			DetectionCooldown = NameToId(nameof(DetectionCooldown));
			UpdatePriority = NameToId(nameof(UpdatePriority));
		}


		///Tag Extensions

		#region Damageable

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasDamageableTag(this IGameEntity entity) => entity.HasTag(Damageable);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddDamageableTag(this IGameEntity entity) => entity.AddTag(Damageable);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelDamageableTag(this IGameEntity entity) => entity.DelTag(Damageable);

		#endregion

		#region Moveable

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoveableTag(this IGameEntity entity) => entity.HasTag(Moveable);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddMoveableTag(this IGameEntity entity) => entity.AddTag(Moveable);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoveableTag(this IGameEntity entity) => entity.DelTag(Moveable);

		#endregion

		#region Fireable

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFireableTag(this IGameEntity entity) => entity.HasTag(Fireable);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddFireableTag(this IGameEntity entity) => entity.AddTag(Fireable);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFireableTag(this IGameEntity entity) => entity.DelTag(Fireable);

		#endregion

		#region Projectile

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasProjectileTag(this IGameEntity entity) => entity.HasTag(Projectile);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddProjectileTag(this IGameEntity entity) => entity.AddTag(Projectile);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelProjectileTag(this IGameEntity entity) => entity.DelTag(Projectile);

		#endregion

		#region Unit

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasUnitTag(this IGameEntity entity) => entity.HasTag(Unit);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddUnitTag(this IGameEntity entity) => entity.AddTag(Unit);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelUnitTag(this IGameEntity entity) => entity.DelTag(Unit);

		#endregion

		#region Targeted

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTargetedTag(this IGameEntity entity) => entity.HasTag(Targeted);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddTargetedTag(this IGameEntity entity) => entity.AddTag(Targeted);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTargetedTag(this IGameEntity entity) => entity.DelTag(Targeted);

		#endregion

		#region Detector

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasDetectorTag(this IGameEntity entity) => entity.HasTag(Detector);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddDetectorTag(this IGameEntity entity) => entity.AddTag(Detector);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelDetectorTag(this IGameEntity entity) => entity.DelTag(Detector);

		#endregion

		#region Attacker

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasAttackerTag(this IGameEntity entity) => entity.HasTag(Attacker);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddAttackerTag(this IGameEntity entity) => entity.AddTag(Attacker);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelAttackerTag(this IGameEntity entity) => entity.DelTag(Attacker);

		#endregion


		///Value Extensions

		#region EntityType

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<GameEntityType> GetEntityType(this IGameEntity entity) => entity.GetValueUnsafe<IValue<GameEntityType>>(EntityType);

		public static ref IValue<GameEntityType> RefEntityType(this IGameEntity entity) => ref entity.GetValueUnsafe<IValue<GameEntityType>>(EntityType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetEntityType(this IGameEntity entity, out IValue<GameEntityType> value) => entity.TryGetValueUnsafe(EntityType, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddEntityType(this IGameEntity entity, IValue<GameEntityType> value) => entity.AddValue(EntityType, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasEntityType(this IGameEntity entity) => entity.HasValue(EntityType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelEntityType(this IGameEntity entity) => entity.DelValue(EntityType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetEntityType(this IGameEntity entity, IValue<GameEntityType> value) => entity.SetValue(EntityType, value);

		#endregion

		#region Position

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<Vector3> GetPosition(this IGameEntity entity) => entity.GetValueUnsafe<IReactiveVariable<Vector3>>(Position);

		public static ref IReactiveVariable<Vector3> RefPosition(this IGameEntity entity) => ref entity.GetValueUnsafe<IReactiveVariable<Vector3>>(Position);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetPosition(this IGameEntity entity, out IReactiveVariable<Vector3> value) => entity.TryGetValueUnsafe(Position, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddPosition(this IGameEntity entity, IReactiveVariable<Vector3> value) => entity.AddValue(Position, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasPosition(this IGameEntity entity) => entity.HasValue(Position);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelPosition(this IGameEntity entity) => entity.DelValue(Position);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetPosition(this IGameEntity entity, IReactiveVariable<Vector3> value) => entity.SetValue(Position, value);

		#endregion

		#region Rotation

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<Quaternion> GetRotation(this IGameEntity entity) => entity.GetValueUnsafe<IReactiveVariable<Quaternion>>(Rotation);

		public static ref IReactiveVariable<Quaternion> RefRotation(this IGameEntity entity) => ref entity.GetValueUnsafe<IReactiveVariable<Quaternion>>(Rotation);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRotation(this IGameEntity entity, out IReactiveVariable<Quaternion> value) => entity.TryGetValueUnsafe(Rotation, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRotation(this IGameEntity entity, IReactiveVariable<Quaternion> value) => entity.AddValue(Rotation, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRotation(this IGameEntity entity) => entity.HasValue(Rotation);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRotation(this IGameEntity entity) => entity.DelValue(Rotation);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotation(this IGameEntity entity, IReactiveVariable<Quaternion> value) => entity.SetValue(Rotation, value);

		#endregion

		#region Scale

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetScale(this IGameEntity entity) => entity.GetValueUnsafe<IValue<float>>(Scale);

		public static ref IValue<float> RefScale(this IGameEntity entity) => ref entity.GetValueUnsafe<IValue<float>>(Scale);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetScale(this IGameEntity entity, out IValue<float> value) => entity.TryGetValueUnsafe(Scale, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddScale(this IGameEntity entity, IValue<float> value) => entity.AddValue(Scale, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasScale(this IGameEntity entity) => entity.HasValue(Scale);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelScale(this IGameEntity entity) => entity.DelValue(Scale);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetScale(this IGameEntity entity, IValue<float> value) => entity.SetValue(Scale, value);

		#endregion

		#region MoveSpeed

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetMoveSpeed(this IGameEntity entity) => entity.GetValueUnsafe<IValue<float>>(MoveSpeed);

		public static ref IValue<float> RefMoveSpeed(this IGameEntity entity) => ref entity.GetValueUnsafe<IValue<float>>(MoveSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoveSpeed(this IGameEntity entity, out IValue<float> value) => entity.TryGetValueUnsafe(MoveSpeed, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoveSpeed(this IGameEntity entity, IValue<float> value) => entity.AddValue(MoveSpeed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoveSpeed(this IGameEntity entity) => entity.HasValue(MoveSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoveSpeed(this IGameEntity entity) => entity.DelValue(MoveSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoveSpeed(this IGameEntity entity, IValue<float> value) => entity.SetValue(MoveSpeed, value);

		#endregion

		#region MoveRequest

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IRequest<Vector3> GetMoveRequest(this IGameEntity entity) => entity.GetValueUnsafe<IRequest<Vector3>>(MoveRequest);

		public static ref IRequest<Vector3> RefMoveRequest(this IGameEntity entity) => ref entity.GetValueUnsafe<IRequest<Vector3>>(MoveRequest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoveRequest(this IGameEntity entity, out IRequest<Vector3> value) => entity.TryGetValueUnsafe(MoveRequest, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoveRequest(this IGameEntity entity, IRequest<Vector3> value) => entity.AddValue(MoveRequest, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoveRequest(this IGameEntity entity) => entity.HasValue(MoveRequest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoveRequest(this IGameEntity entity) => entity.DelValue(MoveRequest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoveRequest(this IGameEntity entity, IRequest<Vector3> value) => entity.SetValue(MoveRequest, value);

		#endregion

		#region MoveCommand

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ICommand<Vector3, float> GetMoveCommand(this IGameEntity entity) => entity.GetValueUnsafe<ICommand<Vector3, float>>(MoveCommand);

		public static ref ICommand<Vector3, float> RefMoveCommand(this IGameEntity entity) => ref entity.GetValueUnsafe<ICommand<Vector3, float>>(MoveCommand);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoveCommand(this IGameEntity entity, out ICommand<Vector3, float> value) => entity.TryGetValueUnsafe(MoveCommand, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoveCommand(this IGameEntity entity, ICommand<Vector3, float> value) => entity.AddValue(MoveCommand, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoveCommand(this IGameEntity entity) => entity.HasValue(MoveCommand);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoveCommand(this IGameEntity entity) => entity.DelValue(MoveCommand);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoveCommand(this IGameEntity entity, ICommand<Vector3, float> value) => entity.SetValue(MoveCommand, value);

		#endregion

		#region RotationSpeed

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetRotationSpeed(this IGameEntity entity) => entity.GetValueUnsafe<IValue<float>>(RotationSpeed);

		public static ref IValue<float> RefRotationSpeed(this IGameEntity entity) => ref entity.GetValueUnsafe<IValue<float>>(RotationSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRotationSpeed(this IGameEntity entity, out IValue<float> value) => entity.TryGetValueUnsafe(RotationSpeed, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRotationSpeed(this IGameEntity entity, IValue<float> value) => entity.AddValue(RotationSpeed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRotationSpeed(this IGameEntity entity) => entity.HasValue(RotationSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRotationSpeed(this IGameEntity entity) => entity.DelValue(RotationSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotationSpeed(this IGameEntity entity, IValue<float> value) => entity.SetValue(RotationSpeed, value);

		#endregion

		#region Health

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Health GetHealth(this IGameEntity entity) => entity.GetValueUnsafe<Health>(Health);

		public static ref Health RefHealth(this IGameEntity entity) => ref entity.GetValueUnsafe<Health>(Health);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetHealth(this IGameEntity entity, out Health value) => entity.TryGetValueUnsafe(Health, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddHealth(this IGameEntity entity, Health value) => entity.AddValue(Health, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasHealth(this IGameEntity entity) => entity.HasValue(Health);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelHealth(this IGameEntity entity) => entity.DelValue(Health);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetHealth(this IGameEntity entity, Health value) => entity.SetValue(Health, value);

		#endregion

		#region Lifetime

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Cooldown GetLifetime(this IGameEntity entity) => entity.GetValueUnsafe<Cooldown>(Lifetime);

		public static ref Cooldown RefLifetime(this IGameEntity entity) => ref entity.GetValueUnsafe<Cooldown>(Lifetime);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetLifetime(this IGameEntity entity, out Cooldown value) => entity.TryGetValueUnsafe(Lifetime, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddLifetime(this IGameEntity entity, Cooldown value) => entity.AddValue(Lifetime, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasLifetime(this IGameEntity entity) => entity.HasValue(Lifetime);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelLifetime(this IGameEntity entity) => entity.DelValue(Lifetime);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetLifetime(this IGameEntity entity, Cooldown value) => entity.SetValue(Lifetime, value);

		#endregion

		#region DestroyAction

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IAction GetDestroyAction(this IGameEntity entity) => entity.GetValueUnsafe<IAction>(DestroyAction);

		public static ref IAction RefDestroyAction(this IGameEntity entity) => ref entity.GetValueUnsafe<IAction>(DestroyAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetDestroyAction(this IGameEntity entity, out IAction value) => entity.TryGetValueUnsafe(DestroyAction, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddDestroyAction(this IGameEntity entity, IAction value) => entity.AddValue(DestroyAction, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasDestroyAction(this IGameEntity entity) => entity.HasValue(DestroyAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelDestroyAction(this IGameEntity entity) => entity.DelValue(DestroyAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetDestroyAction(this IGameEntity entity, IAction value) => entity.SetValue(DestroyAction, value);

		#endregion

		#region TakeDamageEvent

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEvent<int> GetTakeDamageEvent(this IGameEntity entity) => entity.GetValueUnsafe<IEvent<int>>(TakeDamageEvent);

		public static ref IEvent<int> RefTakeDamageEvent(this IGameEntity entity) => ref entity.GetValueUnsafe<IEvent<int>>(TakeDamageEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTakeDamageEvent(this IGameEntity entity, out IEvent<int> value) => entity.TryGetValueUnsafe(TakeDamageEvent, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTakeDamageEvent(this IGameEntity entity, IEvent<int> value) => entity.AddValue(TakeDamageEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTakeDamageEvent(this IGameEntity entity) => entity.HasValue(TakeDamageEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTakeDamageEvent(this IGameEntity entity) => entity.DelValue(TakeDamageEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTakeDamageEvent(this IGameEntity entity, IEvent<int> value) => entity.SetValue(TakeDamageEvent, value);

		#endregion

		#region Team

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<TeamType> GetTeam(this IGameEntity entity) => entity.GetValueUnsafe<IReactiveVariable<TeamType>>(Team);

		public static ref IReactiveVariable<TeamType> RefTeam(this IGameEntity entity) => ref entity.GetValueUnsafe<IReactiveVariable<TeamType>>(Team);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTeam(this IGameEntity entity, out IReactiveVariable<TeamType> value) => entity.TryGetValueUnsafe(Team, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTeam(this IGameEntity entity, IReactiveVariable<TeamType> value) => entity.AddValue(Team, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTeam(this IGameEntity entity) => entity.HasValue(Team);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTeam(this IGameEntity entity) => entity.DelValue(Team);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTeam(this IGameEntity entity, IReactiveVariable<TeamType> value) => entity.SetValue(Team, value);

		#endregion

		#region Damage

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<int> GetDamage(this IGameEntity entity) => entity.GetValueUnsafe<IValue<int>>(Damage);

		public static ref IValue<int> RefDamage(this IGameEntity entity) => ref entity.GetValueUnsafe<IValue<int>>(Damage);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetDamage(this IGameEntity entity, out IValue<int> value) => entity.TryGetValueUnsafe(Damage, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddDamage(this IGameEntity entity, IValue<int> value) => entity.AddValue(Damage, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasDamage(this IGameEntity entity) => entity.HasValue(Damage);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelDamage(this IGameEntity entity) => entity.DelValue(Damage);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetDamage(this IGameEntity entity, IValue<int> value) => entity.SetValue(Damage, value);

		#endregion

		#region Target

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<IGameEntity> GetTarget(this IGameEntity entity) => entity.GetValueUnsafe<IReactiveVariable<IGameEntity>>(Target);

		public static ref IReactiveVariable<IGameEntity> RefTarget(this IGameEntity entity) => ref entity.GetValueUnsafe<IReactiveVariable<IGameEntity>>(Target);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTarget(this IGameEntity entity, out IReactiveVariable<IGameEntity> value) => entity.TryGetValueUnsafe(Target, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTarget(this IGameEntity entity, IReactiveVariable<IGameEntity> value) => entity.AddValue(Target, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTarget(this IGameEntity entity) => entity.HasValue(Target);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTarget(this IGameEntity entity) => entity.DelValue(Target);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTarget(this IGameEntity entity, IReactiveVariable<IGameEntity> value) => entity.SetValue(Target, value);

		#endregion

		#region FireDistance

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetFireDistance(this IGameEntity entity) => entity.GetValueUnsafe<IValue<float>>(FireDistance);

		public static ref IValue<float> RefFireDistance(this IGameEntity entity) => ref entity.GetValueUnsafe<IValue<float>>(FireDistance);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFireDistance(this IGameEntity entity, out IValue<float> value) => entity.TryGetValueUnsafe(FireDistance, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFireDistance(this IGameEntity entity, IValue<float> value) => entity.AddValue(FireDistance, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFireDistance(this IGameEntity entity) => entity.HasValue(FireDistance);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFireDistance(this IGameEntity entity) => entity.DelValue(FireDistance);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFireDistance(this IGameEntity entity, IValue<float> value) => entity.SetValue(FireDistance, value);

		#endregion

		#region FireRequest

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IRequest<IGameEntity> GetFireRequest(this IGameEntity entity) => entity.GetValueUnsafe<IRequest<IGameEntity>>(FireRequest);

		public static ref IRequest<IGameEntity> RefFireRequest(this IGameEntity entity) => ref entity.GetValueUnsafe<IRequest<IGameEntity>>(FireRequest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFireRequest(this IGameEntity entity, out IRequest<IGameEntity> value) => entity.TryGetValueUnsafe(FireRequest, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFireRequest(this IGameEntity entity, IRequest<IGameEntity> value) => entity.AddValue(FireRequest, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFireRequest(this IGameEntity entity) => entity.HasValue(FireRequest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFireRequest(this IGameEntity entity) => entity.DelValue(FireRequest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFireRequest(this IGameEntity entity, IRequest<IGameEntity> value) => entity.SetValue(FireRequest, value);

		#endregion

		#region FireCommand

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ICommand<IGameEntity> GetFireCommand(this IGameEntity entity) => entity.GetValueUnsafe<ICommand<IGameEntity>>(FireCommand);

		public static ref ICommand<IGameEntity> RefFireCommand(this IGameEntity entity) => ref entity.GetValueUnsafe<ICommand<IGameEntity>>(FireCommand);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFireCommand(this IGameEntity entity, out ICommand<IGameEntity> value) => entity.TryGetValueUnsafe(FireCommand, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFireCommand(this IGameEntity entity, ICommand<IGameEntity> value) => entity.AddValue(FireCommand, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFireCommand(this IGameEntity entity) => entity.HasValue(FireCommand);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFireCommand(this IGameEntity entity) => entity.DelValue(FireCommand);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFireCommand(this IGameEntity entity, ICommand<IGameEntity> value) => entity.SetValue(FireCommand, value);

		#endregion

		#region FireCooldown

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Cooldown GetFireCooldown(this IGameEntity entity) => entity.GetValueUnsafe<Cooldown>(FireCooldown);

		public static ref Cooldown RefFireCooldown(this IGameEntity entity) => ref entity.GetValueUnsafe<Cooldown>(FireCooldown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFireCooldown(this IGameEntity entity, out Cooldown value) => entity.TryGetValueUnsafe(FireCooldown, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFireCooldown(this IGameEntity entity, Cooldown value) => entity.AddValue(FireCooldown, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFireCooldown(this IGameEntity entity) => entity.HasValue(FireCooldown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFireCooldown(this IGameEntity entity) => entity.DelValue(FireCooldown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFireCooldown(this IGameEntity entity, Cooldown value) => entity.SetValue(FireCooldown, value);

		#endregion

		#region FirePoint

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<Vector3> GetFirePoint(this IGameEntity entity) => entity.GetValueUnsafe<IValue<Vector3>>(FirePoint);

		public static ref IValue<Vector3> RefFirePoint(this IGameEntity entity) => ref entity.GetValueUnsafe<IValue<Vector3>>(FirePoint);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFirePoint(this IGameEntity entity, out IValue<Vector3> value) => entity.TryGetValueUnsafe(FirePoint, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFirePoint(this IGameEntity entity, IValue<Vector3> value) => entity.AddValue(FirePoint, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFirePoint(this IGameEntity entity) => entity.HasValue(FirePoint);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFirePoint(this IGameEntity entity) => entity.DelValue(FirePoint);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFirePoint(this IGameEntity entity, IValue<Vector3> value) => entity.SetValue(FirePoint, value);

		#endregion

		#region DetectionRadius

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetDetectionRadius(this IGameEntity entity) => entity.GetValueUnsafe<IValue<float>>(DetectionRadius);

		public static ref IValue<float> RefDetectionRadius(this IGameEntity entity) => ref entity.GetValueUnsafe<IValue<float>>(DetectionRadius);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetDetectionRadius(this IGameEntity entity, out IValue<float> value) => entity.TryGetValueUnsafe(DetectionRadius, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddDetectionRadius(this IGameEntity entity, IValue<float> value) => entity.AddValue(DetectionRadius, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasDetectionRadius(this IGameEntity entity) => entity.HasValue(DetectionRadius);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelDetectionRadius(this IGameEntity entity) => entity.DelValue(DetectionRadius);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetDetectionRadius(this IGameEntity entity, IValue<float> value) => entity.SetValue(DetectionRadius, value);

		#endregion

		#region DetectionCooldown

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ICooldown GetDetectionCooldown(this IGameEntity entity) => entity.GetValueUnsafe<ICooldown>(DetectionCooldown);

		public static ref ICooldown RefDetectionCooldown(this IGameEntity entity) => ref entity.GetValueUnsafe<ICooldown>(DetectionCooldown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetDetectionCooldown(this IGameEntity entity, out ICooldown value) => entity.TryGetValueUnsafe(DetectionCooldown, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddDetectionCooldown(this IGameEntity entity, ICooldown value) => entity.AddValue(DetectionCooldown, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasDetectionCooldown(this IGameEntity entity) => entity.HasValue(DetectionCooldown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelDetectionCooldown(this IGameEntity entity) => entity.DelValue(DetectionCooldown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetDetectionCooldown(this IGameEntity entity, ICooldown value) => entity.SetValue(DetectionCooldown, value);

		#endregion

		#region UpdatePriority

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<EntityUpdatePriority> GetUpdatePriority(this IGameEntity entity) => entity.GetValueUnsafe<IReactiveVariable<EntityUpdatePriority>>(UpdatePriority);

		public static ref IReactiveVariable<EntityUpdatePriority> RefUpdatePriority(this IGameEntity entity) => ref entity.GetValueUnsafe<IReactiveVariable<EntityUpdatePriority>>(UpdatePriority);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetUpdatePriority(this IGameEntity entity, out IReactiveVariable<EntityUpdatePriority> value) => entity.TryGetValueUnsafe(UpdatePriority, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddUpdatePriority(this IGameEntity entity, IReactiveVariable<EntityUpdatePriority> value) => entity.AddValue(UpdatePriority, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasUpdatePriority(this IGameEntity entity) => entity.HasValue(UpdatePriority);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelUpdatePriority(this IGameEntity entity) => entity.DelValue(UpdatePriority);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetUpdatePriority(this IGameEntity entity, IReactiveVariable<EntityUpdatePriority> value) => entity.SetValue(UpdatePriority, value);

		#endregion
    }
}
