/**
* Code generation. Don't modify! 
**/

using Atomic.Entities;
using static Atomic.Entities.EntityNames;
using System.Runtime.CompilerServices;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Atomic.Entities;
using System;
using Atomic.Elements;

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
		public static readonly int Projectile;
		public static readonly int Unit;
		public static readonly int Targeted;

		///Values
		public static readonly int Position; // IReactiveVariable<Vector3>
		public static readonly int Rotation; // IReactiveVariable<Quaternion>
		public static readonly int Scale; // IValue<float>
		public static readonly int MoveSpeed; // IValue<float>
		public static readonly int MoveRequest; // IRequest<Vector3>
		public static readonly int MoveEvent; // IEvent<Vector3>
		public static readonly int RotationSpeed; // IValue<float>
		public static readonly int Health; // Health
		public static readonly int Lifetime; // Cooldown
		public static readonly int DestroyAction; // IAction
		public static readonly int TakeDamageEvent; // IEvent<int>
		public static readonly int Team; // IReactiveVariable<TeamType>
		public static readonly int Damage; // IValue<int>
		public static readonly int Target; // IReactiveVariable<IUnitEntity>
		public static readonly int FireDistance; // IValue<float>
		public static readonly int FireRequest; // IRequest<IUnitEntity>
		public static readonly int FireEvent; // IEvent<IUnitEntity>
		public static readonly int FireCooldown; // Cooldown
		public static readonly int FirePoint; // IValue<Vector3>

		static GameEntityAPI()
		{
			//Tags
			Damageable = NameToId(nameof(Damageable));
			Moveable = NameToId(nameof(Moveable));
			Projectile = NameToId(nameof(Projectile));
			Unit = NameToId(nameof(Unit));
			Targeted = NameToId(nameof(Targeted));

			//Values
			Position = NameToId(nameof(Position));
			Rotation = NameToId(nameof(Rotation));
			Scale = NameToId(nameof(Scale));
			MoveSpeed = NameToId(nameof(MoveSpeed));
			MoveRequest = NameToId(nameof(MoveRequest));
			MoveEvent = NameToId(nameof(MoveEvent));
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
			FireEvent = NameToId(nameof(FireEvent));
			FireCooldown = NameToId(nameof(FireCooldown));
			FirePoint = NameToId(nameof(FirePoint));
		}


		///Tag Extensions

		#region Damageable

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasDamageableTag(this IUnitEntity entity) => entity.HasTag(Damageable);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddDamageableTag(this IUnitEntity entity) => entity.AddTag(Damageable);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelDamageableTag(this IUnitEntity entity) => entity.DelTag(Damageable);

		#endregion

		#region Moveable

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoveableTag(this IUnitEntity entity) => entity.HasTag(Moveable);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddMoveableTag(this IUnitEntity entity) => entity.AddTag(Moveable);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoveableTag(this IUnitEntity entity) => entity.DelTag(Moveable);

		#endregion

		#region Projectile

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasProjectileTag(this IUnitEntity entity) => entity.HasTag(Projectile);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddProjectileTag(this IUnitEntity entity) => entity.AddTag(Projectile);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelProjectileTag(this IUnitEntity entity) => entity.DelTag(Projectile);

		#endregion

		#region Unit

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasUnitTag(this IUnitEntity entity) => entity.HasTag(Unit);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddUnitTag(this IUnitEntity entity) => entity.AddTag(Unit);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelUnitTag(this IUnitEntity entity) => entity.DelTag(Unit);

		#endregion

		#region Targeted

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTargetedTag(this IUnitEntity entity) => entity.HasTag(Targeted);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddTargetedTag(this IUnitEntity entity) => entity.AddTag(Targeted);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTargetedTag(this IUnitEntity entity) => entity.DelTag(Targeted);

		#endregion


		///Value Extensions

		#region Position

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<Vector3> GetPosition(this IUnitEntity entity) => entity.GetValueUnsafe<IReactiveVariable<Vector3>>(Position);

		public static ref IReactiveVariable<Vector3> RefPosition(this IUnitEntity entity) => ref entity.GetValueUnsafe<IReactiveVariable<Vector3>>(Position);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetPosition(this IUnitEntity entity, out IReactiveVariable<Vector3> value) => entity.TryGetValueUnsafe(Position, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddPosition(this IUnitEntity entity, IReactiveVariable<Vector3> value) => entity.AddValue(Position, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasPosition(this IUnitEntity entity) => entity.HasValue(Position);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelPosition(this IUnitEntity entity) => entity.DelValue(Position);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetPosition(this IUnitEntity entity, IReactiveVariable<Vector3> value) => entity.SetValue(Position, value);

		#endregion

		#region Rotation

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<Quaternion> GetRotation(this IUnitEntity entity) => entity.GetValueUnsafe<IReactiveVariable<Quaternion>>(Rotation);

		public static ref IReactiveVariable<Quaternion> RefRotation(this IUnitEntity entity) => ref entity.GetValueUnsafe<IReactiveVariable<Quaternion>>(Rotation);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRotation(this IUnitEntity entity, out IReactiveVariable<Quaternion> value) => entity.TryGetValueUnsafe(Rotation, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRotation(this IUnitEntity entity, IReactiveVariable<Quaternion> value) => entity.AddValue(Rotation, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRotation(this IUnitEntity entity) => entity.HasValue(Rotation);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRotation(this IUnitEntity entity) => entity.DelValue(Rotation);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotation(this IUnitEntity entity, IReactiveVariable<Quaternion> value) => entity.SetValue(Rotation, value);

		#endregion

		#region Scale

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetScale(this IUnitEntity entity) => entity.GetValueUnsafe<IValue<float>>(Scale);

		public static ref IValue<float> RefScale(this IUnitEntity entity) => ref entity.GetValueUnsafe<IValue<float>>(Scale);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetScale(this IUnitEntity entity, out IValue<float> value) => entity.TryGetValueUnsafe(Scale, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddScale(this IUnitEntity entity, IValue<float> value) => entity.AddValue(Scale, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasScale(this IUnitEntity entity) => entity.HasValue(Scale);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelScale(this IUnitEntity entity) => entity.DelValue(Scale);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetScale(this IUnitEntity entity, IValue<float> value) => entity.SetValue(Scale, value);

		#endregion

		#region MoveSpeed

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetMoveSpeed(this IUnitEntity entity) => entity.GetValueUnsafe<IValue<float>>(MoveSpeed);

		public static ref IValue<float> RefMoveSpeed(this IUnitEntity entity) => ref entity.GetValueUnsafe<IValue<float>>(MoveSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoveSpeed(this IUnitEntity entity, out IValue<float> value) => entity.TryGetValueUnsafe(MoveSpeed, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoveSpeed(this IUnitEntity entity, IValue<float> value) => entity.AddValue(MoveSpeed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoveSpeed(this IUnitEntity entity) => entity.HasValue(MoveSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoveSpeed(this IUnitEntity entity) => entity.DelValue(MoveSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoveSpeed(this IUnitEntity entity, IValue<float> value) => entity.SetValue(MoveSpeed, value);

		#endregion

		#region MoveRequest

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IRequest<Vector3> GetMoveRequest(this IUnitEntity entity) => entity.GetValueUnsafe<IRequest<Vector3>>(MoveRequest);

		public static ref IRequest<Vector3> RefMoveRequest(this IUnitEntity entity) => ref entity.GetValueUnsafe<IRequest<Vector3>>(MoveRequest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoveRequest(this IUnitEntity entity, out IRequest<Vector3> value) => entity.TryGetValueUnsafe(MoveRequest, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoveRequest(this IUnitEntity entity, IRequest<Vector3> value) => entity.AddValue(MoveRequest, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoveRequest(this IUnitEntity entity) => entity.HasValue(MoveRequest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoveRequest(this IUnitEntity entity) => entity.DelValue(MoveRequest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoveRequest(this IUnitEntity entity, IRequest<Vector3> value) => entity.SetValue(MoveRequest, value);

		#endregion

		#region MoveEvent

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEvent<Vector3> GetMoveEvent(this IUnitEntity entity) => entity.GetValueUnsafe<IEvent<Vector3>>(MoveEvent);

		public static ref IEvent<Vector3> RefMoveEvent(this IUnitEntity entity) => ref entity.GetValueUnsafe<IEvent<Vector3>>(MoveEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoveEvent(this IUnitEntity entity, out IEvent<Vector3> value) => entity.TryGetValueUnsafe(MoveEvent, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoveEvent(this IUnitEntity entity, IEvent<Vector3> value) => entity.AddValue(MoveEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoveEvent(this IUnitEntity entity) => entity.HasValue(MoveEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoveEvent(this IUnitEntity entity) => entity.DelValue(MoveEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoveEvent(this IUnitEntity entity, IEvent<Vector3> value) => entity.SetValue(MoveEvent, value);

		#endregion

		#region RotationSpeed

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetRotationSpeed(this IUnitEntity entity) => entity.GetValueUnsafe<IValue<float>>(RotationSpeed);

		public static ref IValue<float> RefRotationSpeed(this IUnitEntity entity) => ref entity.GetValueUnsafe<IValue<float>>(RotationSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRotationSpeed(this IUnitEntity entity, out IValue<float> value) => entity.TryGetValueUnsafe(RotationSpeed, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRotationSpeed(this IUnitEntity entity, IValue<float> value) => entity.AddValue(RotationSpeed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRotationSpeed(this IUnitEntity entity) => entity.HasValue(RotationSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRotationSpeed(this IUnitEntity entity) => entity.DelValue(RotationSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotationSpeed(this IUnitEntity entity, IValue<float> value) => entity.SetValue(RotationSpeed, value);

		#endregion

		#region Health

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Health GetHealth(this IUnitEntity entity) => entity.GetValueUnsafe<Health>(Health);

		public static ref Health RefHealth(this IUnitEntity entity) => ref entity.GetValueUnsafe<Health>(Health);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetHealth(this IUnitEntity entity, out Health value) => entity.TryGetValueUnsafe(Health, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddHealth(this IUnitEntity entity, Health value) => entity.AddValue(Health, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasHealth(this IUnitEntity entity) => entity.HasValue(Health);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelHealth(this IUnitEntity entity) => entity.DelValue(Health);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetHealth(this IUnitEntity entity, Health value) => entity.SetValue(Health, value);

		#endregion

		#region Lifetime

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Cooldown GetLifetime(this IUnitEntity entity) => entity.GetValueUnsafe<Cooldown>(Lifetime);

		public static ref Cooldown RefLifetime(this IUnitEntity entity) => ref entity.GetValueUnsafe<Cooldown>(Lifetime);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetLifetime(this IUnitEntity entity, out Cooldown value) => entity.TryGetValueUnsafe(Lifetime, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddLifetime(this IUnitEntity entity, Cooldown value) => entity.AddValue(Lifetime, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasLifetime(this IUnitEntity entity) => entity.HasValue(Lifetime);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelLifetime(this IUnitEntity entity) => entity.DelValue(Lifetime);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetLifetime(this IUnitEntity entity, Cooldown value) => entity.SetValue(Lifetime, value);

		#endregion

		#region DestroyAction

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IAction GetDestroyAction(this IUnitEntity entity) => entity.GetValueUnsafe<IAction>(DestroyAction);

		public static ref IAction RefDestroyAction(this IUnitEntity entity) => ref entity.GetValueUnsafe<IAction>(DestroyAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetDestroyAction(this IUnitEntity entity, out IAction value) => entity.TryGetValueUnsafe(DestroyAction, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddDestroyAction(this IUnitEntity entity, IAction value) => entity.AddValue(DestroyAction, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasDestroyAction(this IUnitEntity entity) => entity.HasValue(DestroyAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelDestroyAction(this IUnitEntity entity) => entity.DelValue(DestroyAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetDestroyAction(this IUnitEntity entity, IAction value) => entity.SetValue(DestroyAction, value);

		#endregion

		#region TakeDamageEvent

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEvent<int> GetTakeDamageEvent(this IUnitEntity entity) => entity.GetValueUnsafe<IEvent<int>>(TakeDamageEvent);

		public static ref IEvent<int> RefTakeDamageEvent(this IUnitEntity entity) => ref entity.GetValueUnsafe<IEvent<int>>(TakeDamageEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTakeDamageEvent(this IUnitEntity entity, out IEvent<int> value) => entity.TryGetValueUnsafe(TakeDamageEvent, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTakeDamageEvent(this IUnitEntity entity, IEvent<int> value) => entity.AddValue(TakeDamageEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTakeDamageEvent(this IUnitEntity entity) => entity.HasValue(TakeDamageEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTakeDamageEvent(this IUnitEntity entity) => entity.DelValue(TakeDamageEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTakeDamageEvent(this IUnitEntity entity, IEvent<int> value) => entity.SetValue(TakeDamageEvent, value);

		#endregion

		#region Team

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<TeamType> GetTeam(this IUnitEntity entity) => entity.GetValueUnsafe<IReactiveVariable<TeamType>>(Team);

		public static ref IReactiveVariable<TeamType> RefTeam(this IUnitEntity entity) => ref entity.GetValueUnsafe<IReactiveVariable<TeamType>>(Team);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTeam(this IUnitEntity entity, out IReactiveVariable<TeamType> value) => entity.TryGetValueUnsafe(Team, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTeam(this IUnitEntity entity, IReactiveVariable<TeamType> value) => entity.AddValue(Team, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTeam(this IUnitEntity entity) => entity.HasValue(Team);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTeam(this IUnitEntity entity) => entity.DelValue(Team);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTeam(this IUnitEntity entity, IReactiveVariable<TeamType> value) => entity.SetValue(Team, value);

		#endregion

		#region Damage

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<int> GetDamage(this IUnitEntity entity) => entity.GetValueUnsafe<IValue<int>>(Damage);

		public static ref IValue<int> RefDamage(this IUnitEntity entity) => ref entity.GetValueUnsafe<IValue<int>>(Damage);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetDamage(this IUnitEntity entity, out IValue<int> value) => entity.TryGetValueUnsafe(Damage, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddDamage(this IUnitEntity entity, IValue<int> value) => entity.AddValue(Damage, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasDamage(this IUnitEntity entity) => entity.HasValue(Damage);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelDamage(this IUnitEntity entity) => entity.DelValue(Damage);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetDamage(this IUnitEntity entity, IValue<int> value) => entity.SetValue(Damage, value);

		#endregion

		#region Target

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<IUnitEntity> GetTarget(this IUnitEntity entity) => entity.GetValueUnsafe<IReactiveVariable<IUnitEntity>>(Target);

		public static ref IReactiveVariable<IUnitEntity> RefTarget(this IUnitEntity entity) => ref entity.GetValueUnsafe<IReactiveVariable<IUnitEntity>>(Target);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTarget(this IUnitEntity entity, out IReactiveVariable<IUnitEntity> value) => entity.TryGetValueUnsafe(Target, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTarget(this IUnitEntity entity, IReactiveVariable<IUnitEntity> value) => entity.AddValue(Target, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTarget(this IUnitEntity entity) => entity.HasValue(Target);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTarget(this IUnitEntity entity) => entity.DelValue(Target);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTarget(this IUnitEntity entity, IReactiveVariable<IUnitEntity> value) => entity.SetValue(Target, value);

		#endregion

		#region FireDistance

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetFireDistance(this IUnitEntity entity) => entity.GetValueUnsafe<IValue<float>>(FireDistance);

		public static ref IValue<float> RefFireDistance(this IUnitEntity entity) => ref entity.GetValueUnsafe<IValue<float>>(FireDistance);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFireDistance(this IUnitEntity entity, out IValue<float> value) => entity.TryGetValueUnsafe(FireDistance, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFireDistance(this IUnitEntity entity, IValue<float> value) => entity.AddValue(FireDistance, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFireDistance(this IUnitEntity entity) => entity.HasValue(FireDistance);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFireDistance(this IUnitEntity entity) => entity.DelValue(FireDistance);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFireDistance(this IUnitEntity entity, IValue<float> value) => entity.SetValue(FireDistance, value);

		#endregion

		#region FireRequest

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IRequest<IUnitEntity> GetFireRequest(this IUnitEntity entity) => entity.GetValueUnsafe<IRequest<IUnitEntity>>(FireRequest);

		public static ref IRequest<IUnitEntity> RefFireRequest(this IUnitEntity entity) => ref entity.GetValueUnsafe<IRequest<IUnitEntity>>(FireRequest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFireRequest(this IUnitEntity entity, out IRequest<IUnitEntity> value) => entity.TryGetValueUnsafe(FireRequest, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFireRequest(this IUnitEntity entity, IRequest<IUnitEntity> value) => entity.AddValue(FireRequest, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFireRequest(this IUnitEntity entity) => entity.HasValue(FireRequest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFireRequest(this IUnitEntity entity) => entity.DelValue(FireRequest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFireRequest(this IUnitEntity entity, IRequest<IUnitEntity> value) => entity.SetValue(FireRequest, value);

		#endregion

		#region FireEvent

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEvent<IUnitEntity> GetFireEvent(this IUnitEntity entity) => entity.GetValueUnsafe<IEvent<IUnitEntity>>(FireEvent);

		public static ref IEvent<IUnitEntity> RefFireEvent(this IUnitEntity entity) => ref entity.GetValueUnsafe<IEvent<IUnitEntity>>(FireEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFireEvent(this IUnitEntity entity, out IEvent<IUnitEntity> value) => entity.TryGetValueUnsafe(FireEvent, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFireEvent(this IUnitEntity entity, IEvent<IUnitEntity> value) => entity.AddValue(FireEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFireEvent(this IUnitEntity entity) => entity.HasValue(FireEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFireEvent(this IUnitEntity entity) => entity.DelValue(FireEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFireEvent(this IUnitEntity entity, IEvent<IUnitEntity> value) => entity.SetValue(FireEvent, value);

		#endregion

		#region FireCooldown

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Cooldown GetFireCooldown(this IUnitEntity entity) => entity.GetValueUnsafe<Cooldown>(FireCooldown);

		public static ref Cooldown RefFireCooldown(this IUnitEntity entity) => ref entity.GetValueUnsafe<Cooldown>(FireCooldown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFireCooldown(this IUnitEntity entity, out Cooldown value) => entity.TryGetValueUnsafe(FireCooldown, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFireCooldown(this IUnitEntity entity, Cooldown value) => entity.AddValue(FireCooldown, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFireCooldown(this IUnitEntity entity) => entity.HasValue(FireCooldown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFireCooldown(this IUnitEntity entity) => entity.DelValue(FireCooldown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFireCooldown(this IUnitEntity entity, Cooldown value) => entity.SetValue(FireCooldown, value);

		#endregion

		#region FirePoint

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<Vector3> GetFirePoint(this IUnitEntity entity) => entity.GetValueUnsafe<IValue<Vector3>>(FirePoint);

		public static ref IValue<Vector3> RefFirePoint(this IUnitEntity entity) => ref entity.GetValueUnsafe<IValue<Vector3>>(FirePoint);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFirePoint(this IUnitEntity entity, out IValue<Vector3> value) => entity.TryGetValueUnsafe(FirePoint, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFirePoint(this IUnitEntity entity, IValue<Vector3> value) => entity.AddValue(FirePoint, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFirePoint(this IUnitEntity entity) => entity.HasValue(FirePoint);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFirePoint(this IUnitEntity entity) => entity.DelValue(FirePoint);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFirePoint(this IUnitEntity entity, IValue<Vector3> value) => entity.SetValue(FirePoint, value);

		#endregion
    }
}
