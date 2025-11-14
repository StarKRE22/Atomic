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
using Atomic.Elements;
using System;

namespace RTSGame
{
#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	public static class UnitEntityAPI
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
		public static readonly int Target; // IReactiveVariable<IUnit>
		public static readonly int FireDistance; // IValue<float>
		public static readonly int FireRequest; // IRequest<IUnit>
		public static readonly int FireEvent; // IEvent<IUnit>
		public static readonly int FireCooldown; // Cooldown
		public static readonly int FirePoint; // IValue<Vector3>
		public static readonly int DetectionRadius; // IValue<float>

		static UnitEntityAPI()
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
			DetectionRadius = NameToId(nameof(DetectionRadius));
		}


		///Tag Extensions

		#region Damageable

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasDamageableTag(this IUnit entity) => entity.HasTag(Damageable);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddDamageableTag(this IUnit entity) => entity.AddTag(Damageable);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelDamageableTag(this IUnit entity) => entity.DelTag(Damageable);

		#endregion

		#region Moveable

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoveableTag(this IUnit entity) => entity.HasTag(Moveable);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddMoveableTag(this IUnit entity) => entity.AddTag(Moveable);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoveableTag(this IUnit entity) => entity.DelTag(Moveable);

		#endregion

		#region Projectile

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasProjectileTag(this IUnit entity) => entity.HasTag(Projectile);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddProjectileTag(this IUnit entity) => entity.AddTag(Projectile);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelProjectileTag(this IUnit entity) => entity.DelTag(Projectile);

		#endregion

		#region Unit

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasUnitTag(this IUnit entity) => entity.HasTag(Unit);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddUnitTag(this IUnit entity) => entity.AddTag(Unit);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelUnitTag(this IUnit entity) => entity.DelTag(Unit);

		#endregion

		#region Targeted

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTargetedTag(this IUnit entity) => entity.HasTag(Targeted);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddTargetedTag(this IUnit entity) => entity.AddTag(Targeted);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTargetedTag(this IUnit entity) => entity.DelTag(Targeted);

		#endregion


		///Value Extensions

		#region Position

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<Vector3> GetPosition(this IUnit entity) => entity.GetValueUnsafe<IReactiveVariable<Vector3>>(Position);

		public static ref IReactiveVariable<Vector3> RefPosition(this IUnit entity) => ref entity.GetValueUnsafe<IReactiveVariable<Vector3>>(Position);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetPosition(this IUnit entity, out IReactiveVariable<Vector3> value) => entity.TryGetValueUnsafe(Position, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddPosition(this IUnit entity, IReactiveVariable<Vector3> value) => entity.AddValue(Position, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasPosition(this IUnit entity) => entity.HasValue(Position);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelPosition(this IUnit entity) => entity.DelValue(Position);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetPosition(this IUnit entity, IReactiveVariable<Vector3> value) => entity.SetValue(Position, value);

		#endregion

		#region Rotation

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<Quaternion> GetRotation(this IUnit entity) => entity.GetValueUnsafe<IReactiveVariable<Quaternion>>(Rotation);

		public static ref IReactiveVariable<Quaternion> RefRotation(this IUnit entity) => ref entity.GetValueUnsafe<IReactiveVariable<Quaternion>>(Rotation);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRotation(this IUnit entity, out IReactiveVariable<Quaternion> value) => entity.TryGetValueUnsafe(Rotation, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRotation(this IUnit entity, IReactiveVariable<Quaternion> value) => entity.AddValue(Rotation, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRotation(this IUnit entity) => entity.HasValue(Rotation);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRotation(this IUnit entity) => entity.DelValue(Rotation);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotation(this IUnit entity, IReactiveVariable<Quaternion> value) => entity.SetValue(Rotation, value);

		#endregion

		#region Scale

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetScale(this IUnit entity) => entity.GetValueUnsafe<IValue<float>>(Scale);

		public static ref IValue<float> RefScale(this IUnit entity) => ref entity.GetValueUnsafe<IValue<float>>(Scale);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetScale(this IUnit entity, out IValue<float> value) => entity.TryGetValueUnsafe(Scale, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddScale(this IUnit entity, IValue<float> value) => entity.AddValue(Scale, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasScale(this IUnit entity) => entity.HasValue(Scale);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelScale(this IUnit entity) => entity.DelValue(Scale);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetScale(this IUnit entity, IValue<float> value) => entity.SetValue(Scale, value);

		#endregion

		#region MoveSpeed

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetMoveSpeed(this IUnit entity) => entity.GetValueUnsafe<IValue<float>>(MoveSpeed);

		public static ref IValue<float> RefMoveSpeed(this IUnit entity) => ref entity.GetValueUnsafe<IValue<float>>(MoveSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoveSpeed(this IUnit entity, out IValue<float> value) => entity.TryGetValueUnsafe(MoveSpeed, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoveSpeed(this IUnit entity, IValue<float> value) => entity.AddValue(MoveSpeed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoveSpeed(this IUnit entity) => entity.HasValue(MoveSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoveSpeed(this IUnit entity) => entity.DelValue(MoveSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoveSpeed(this IUnit entity, IValue<float> value) => entity.SetValue(MoveSpeed, value);

		#endregion

		#region MoveRequest

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IRequest<Vector3> GetMoveRequest(this IUnit entity) => entity.GetValueUnsafe<IRequest<Vector3>>(MoveRequest);

		public static ref IRequest<Vector3> RefMoveRequest(this IUnit entity) => ref entity.GetValueUnsafe<IRequest<Vector3>>(MoveRequest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoveRequest(this IUnit entity, out IRequest<Vector3> value) => entity.TryGetValueUnsafe(MoveRequest, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoveRequest(this IUnit entity, IRequest<Vector3> value) => entity.AddValue(MoveRequest, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoveRequest(this IUnit entity) => entity.HasValue(MoveRequest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoveRequest(this IUnit entity) => entity.DelValue(MoveRequest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoveRequest(this IUnit entity, IRequest<Vector3> value) => entity.SetValue(MoveRequest, value);

		#endregion

		#region MoveEvent

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEvent<Vector3> GetMoveEvent(this IUnit entity) => entity.GetValueUnsafe<IEvent<Vector3>>(MoveEvent);

		public static ref IEvent<Vector3> RefMoveEvent(this IUnit entity) => ref entity.GetValueUnsafe<IEvent<Vector3>>(MoveEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoveEvent(this IUnit entity, out IEvent<Vector3> value) => entity.TryGetValueUnsafe(MoveEvent, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoveEvent(this IUnit entity, IEvent<Vector3> value) => entity.AddValue(MoveEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoveEvent(this IUnit entity) => entity.HasValue(MoveEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoveEvent(this IUnit entity) => entity.DelValue(MoveEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoveEvent(this IUnit entity, IEvent<Vector3> value) => entity.SetValue(MoveEvent, value);

		#endregion

		#region RotationSpeed

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetRotationSpeed(this IUnit entity) => entity.GetValueUnsafe<IValue<float>>(RotationSpeed);

		public static ref IValue<float> RefRotationSpeed(this IUnit entity) => ref entity.GetValueUnsafe<IValue<float>>(RotationSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRotationSpeed(this IUnit entity, out IValue<float> value) => entity.TryGetValueUnsafe(RotationSpeed, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRotationSpeed(this IUnit entity, IValue<float> value) => entity.AddValue(RotationSpeed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRotationSpeed(this IUnit entity) => entity.HasValue(RotationSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRotationSpeed(this IUnit entity) => entity.DelValue(RotationSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotationSpeed(this IUnit entity, IValue<float> value) => entity.SetValue(RotationSpeed, value);

		#endregion

		#region Health

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Health GetHealth(this IUnit entity) => entity.GetValueUnsafe<Health>(Health);

		public static ref Health RefHealth(this IUnit entity) => ref entity.GetValueUnsafe<Health>(Health);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetHealth(this IUnit entity, out Health value) => entity.TryGetValueUnsafe(Health, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddHealth(this IUnit entity, Health value) => entity.AddValue(Health, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasHealth(this IUnit entity) => entity.HasValue(Health);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelHealth(this IUnit entity) => entity.DelValue(Health);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetHealth(this IUnit entity, Health value) => entity.SetValue(Health, value);

		#endregion

		#region Lifetime

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Cooldown GetLifetime(this IUnit entity) => entity.GetValueUnsafe<Cooldown>(Lifetime);

		public static ref Cooldown RefLifetime(this IUnit entity) => ref entity.GetValueUnsafe<Cooldown>(Lifetime);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetLifetime(this IUnit entity, out Cooldown value) => entity.TryGetValueUnsafe(Lifetime, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddLifetime(this IUnit entity, Cooldown value) => entity.AddValue(Lifetime, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasLifetime(this IUnit entity) => entity.HasValue(Lifetime);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelLifetime(this IUnit entity) => entity.DelValue(Lifetime);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetLifetime(this IUnit entity, Cooldown value) => entity.SetValue(Lifetime, value);

		#endregion

		#region DestroyAction

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IAction GetDestroyAction(this IUnit entity) => entity.GetValueUnsafe<IAction>(DestroyAction);

		public static ref IAction RefDestroyAction(this IUnit entity) => ref entity.GetValueUnsafe<IAction>(DestroyAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetDestroyAction(this IUnit entity, out IAction value) => entity.TryGetValueUnsafe(DestroyAction, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddDestroyAction(this IUnit entity, IAction value) => entity.AddValue(DestroyAction, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasDestroyAction(this IUnit entity) => entity.HasValue(DestroyAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelDestroyAction(this IUnit entity) => entity.DelValue(DestroyAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetDestroyAction(this IUnit entity, IAction value) => entity.SetValue(DestroyAction, value);

		#endregion

		#region TakeDamageEvent

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEvent<int> GetTakeDamageEvent(this IUnit entity) => entity.GetValueUnsafe<IEvent<int>>(TakeDamageEvent);

		public static ref IEvent<int> RefTakeDamageEvent(this IUnit entity) => ref entity.GetValueUnsafe<IEvent<int>>(TakeDamageEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTakeDamageEvent(this IUnit entity, out IEvent<int> value) => entity.TryGetValueUnsafe(TakeDamageEvent, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTakeDamageEvent(this IUnit entity, IEvent<int> value) => entity.AddValue(TakeDamageEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTakeDamageEvent(this IUnit entity) => entity.HasValue(TakeDamageEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTakeDamageEvent(this IUnit entity) => entity.DelValue(TakeDamageEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTakeDamageEvent(this IUnit entity, IEvent<int> value) => entity.SetValue(TakeDamageEvent, value);

		#endregion

		#region Team

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<TeamType> GetTeam(this IUnit entity) => entity.GetValueUnsafe<IReactiveVariable<TeamType>>(Team);

		public static ref IReactiveVariable<TeamType> RefTeam(this IUnit entity) => ref entity.GetValueUnsafe<IReactiveVariable<TeamType>>(Team);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTeam(this IUnit entity, out IReactiveVariable<TeamType> value) => entity.TryGetValueUnsafe(Team, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTeam(this IUnit entity, IReactiveVariable<TeamType> value) => entity.AddValue(Team, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTeam(this IUnit entity) => entity.HasValue(Team);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTeam(this IUnit entity) => entity.DelValue(Team);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTeam(this IUnit entity, IReactiveVariable<TeamType> value) => entity.SetValue(Team, value);

		#endregion

		#region Damage

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<int> GetDamage(this IUnit entity) => entity.GetValueUnsafe<IValue<int>>(Damage);

		public static ref IValue<int> RefDamage(this IUnit entity) => ref entity.GetValueUnsafe<IValue<int>>(Damage);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetDamage(this IUnit entity, out IValue<int> value) => entity.TryGetValueUnsafe(Damage, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddDamage(this IUnit entity, IValue<int> value) => entity.AddValue(Damage, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasDamage(this IUnit entity) => entity.HasValue(Damage);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelDamage(this IUnit entity) => entity.DelValue(Damage);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetDamage(this IUnit entity, IValue<int> value) => entity.SetValue(Damage, value);

		#endregion

		#region Target

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<IUnit> GetTarget(this IUnit entity) => entity.GetValueUnsafe<IReactiveVariable<IUnit>>(Target);

		public static ref IReactiveVariable<IUnit> RefTarget(this IUnit entity) => ref entity.GetValueUnsafe<IReactiveVariable<IUnit>>(Target);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTarget(this IUnit entity, out IReactiveVariable<IUnit> value) => entity.TryGetValueUnsafe(Target, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTarget(this IUnit entity, IReactiveVariable<IUnit> value) => entity.AddValue(Target, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTarget(this IUnit entity) => entity.HasValue(Target);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTarget(this IUnit entity) => entity.DelValue(Target);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTarget(this IUnit entity, IReactiveVariable<IUnit> value) => entity.SetValue(Target, value);

		#endregion

		#region FireDistance

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetFireDistance(this IUnit entity) => entity.GetValueUnsafe<IValue<float>>(FireDistance);

		public static ref IValue<float> RefFireDistance(this IUnit entity) => ref entity.GetValueUnsafe<IValue<float>>(FireDistance);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFireDistance(this IUnit entity, out IValue<float> value) => entity.TryGetValueUnsafe(FireDistance, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFireDistance(this IUnit entity, IValue<float> value) => entity.AddValue(FireDistance, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFireDistance(this IUnit entity) => entity.HasValue(FireDistance);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFireDistance(this IUnit entity) => entity.DelValue(FireDistance);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFireDistance(this IUnit entity, IValue<float> value) => entity.SetValue(FireDistance, value);

		#endregion

		#region FireRequest

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IRequest<IUnit> GetFireRequest(this IUnit entity) => entity.GetValueUnsafe<IRequest<IUnit>>(FireRequest);

		public static ref IRequest<IUnit> RefFireRequest(this IUnit entity) => ref entity.GetValueUnsafe<IRequest<IUnit>>(FireRequest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFireRequest(this IUnit entity, out IRequest<IUnit> value) => entity.TryGetValueUnsafe(FireRequest, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFireRequest(this IUnit entity, IRequest<IUnit> value) => entity.AddValue(FireRequest, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFireRequest(this IUnit entity) => entity.HasValue(FireRequest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFireRequest(this IUnit entity) => entity.DelValue(FireRequest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFireRequest(this IUnit entity, IRequest<IUnit> value) => entity.SetValue(FireRequest, value);

		#endregion

		#region FireEvent

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEvent<IUnit> GetFireEvent(this IUnit entity) => entity.GetValueUnsafe<IEvent<IUnit>>(FireEvent);

		public static ref IEvent<IUnit> RefFireEvent(this IUnit entity) => ref entity.GetValueUnsafe<IEvent<IUnit>>(FireEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFireEvent(this IUnit entity, out IEvent<IUnit> value) => entity.TryGetValueUnsafe(FireEvent, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFireEvent(this IUnit entity, IEvent<IUnit> value) => entity.AddValue(FireEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFireEvent(this IUnit entity) => entity.HasValue(FireEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFireEvent(this IUnit entity) => entity.DelValue(FireEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFireEvent(this IUnit entity, IEvent<IUnit> value) => entity.SetValue(FireEvent, value);

		#endregion

		#region FireCooldown

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Cooldown GetFireCooldown(this IUnit entity) => entity.GetValueUnsafe<Cooldown>(FireCooldown);

		public static ref Cooldown RefFireCooldown(this IUnit entity) => ref entity.GetValueUnsafe<Cooldown>(FireCooldown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFireCooldown(this IUnit entity, out Cooldown value) => entity.TryGetValueUnsafe(FireCooldown, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFireCooldown(this IUnit entity, Cooldown value) => entity.AddValue(FireCooldown, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFireCooldown(this IUnit entity) => entity.HasValue(FireCooldown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFireCooldown(this IUnit entity) => entity.DelValue(FireCooldown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFireCooldown(this IUnit entity, Cooldown value) => entity.SetValue(FireCooldown, value);

		#endregion

		#region FirePoint

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<Vector3> GetFirePoint(this IUnit entity) => entity.GetValueUnsafe<IValue<Vector3>>(FirePoint);

		public static ref IValue<Vector3> RefFirePoint(this IUnit entity) => ref entity.GetValueUnsafe<IValue<Vector3>>(FirePoint);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFirePoint(this IUnit entity, out IValue<Vector3> value) => entity.TryGetValueUnsafe(FirePoint, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFirePoint(this IUnit entity, IValue<Vector3> value) => entity.AddValue(FirePoint, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFirePoint(this IUnit entity) => entity.HasValue(FirePoint);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFirePoint(this IUnit entity) => entity.DelValue(FirePoint);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFirePoint(this IUnit entity, IValue<Vector3> value) => entity.SetValue(FirePoint, value);

		#endregion

		#region DetectionRadius

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetDetectionRadius(this IUnit entity) => entity.GetValueUnsafe<IValue<float>>(DetectionRadius);

		public static ref IValue<float> RefDetectionRadius(this IUnit entity) => ref entity.GetValueUnsafe<IValue<float>>(DetectionRadius);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetDetectionRadius(this IUnit entity, out IValue<float> value) => entity.TryGetValueUnsafe(DetectionRadius, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddDetectionRadius(this IUnit entity, IValue<float> value) => entity.AddValue(DetectionRadius, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasDetectionRadius(this IUnit entity) => entity.HasValue(DetectionRadius);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelDetectionRadius(this IUnit entity) => entity.DelValue(DetectionRadius);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetDetectionRadius(this IUnit entity, IValue<float> value) => entity.SetValue(DetectionRadius, value);

		#endregion
    }
}
