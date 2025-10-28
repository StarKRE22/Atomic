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
using System;
using Atomic.Elements;

namespace ShooterGame.Gameplay
{
#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	public static class WorldEntityAPI
	{

		///Tags
		public static readonly int Damageable;

		///Values
		public static readonly int Position; // IVariable<Vector3>
		public static readonly int Rotation; // IVariable<Quaternion>
		public static readonly int Parent; // IVariable<Transform>
		public static readonly int MovementSpeed; // IValue<float>
		public static readonly int MovementCondition; // IExpression<bool>
		public static readonly int MovementDirection; // IReactiveVariable<Vector3>
		public static readonly int MovementEvent; // IEvent<Vector3>
		public static readonly int RotationCondition; // IExpression<bool>
		public static readonly int RotationSpeed; // IValue<float>
		public static readonly int RotationDirection; // IReactiveVariable<Vector3>
		public static readonly int RotationEvent; // IEvent<Vector3>
		public static readonly int Health; // Health
		public static readonly int Lifetime; // Cooldown
		public static readonly int TakeDamageEvent; // IEvent<DamageArgs>
		public static readonly int TakeDeathEvent; // IEvent<DamageArgs>
		public static readonly int DestroyAction; // IAction
		public static readonly int RespawnEvent; // IEvent
		public static readonly int TeamType; // IReactiveVariable<TeamType>
		public static readonly int Weapon; // IWeapon
		public static readonly int Damage; // IValue<int>
		public static readonly int Target; // IReactiveVariable<IEntity>
		public static readonly int FireCondition; // IExpression<bool>
		public static readonly int FireCooldown; // Cooldown
		public static readonly int FirePoint; // Transform
		public static readonly int FireAction; // IAction
		public static readonly int FireEvent; // IEvent
		public static readonly int Trigger; // TriggerEvents
		public static readonly int PhysicsLayer; // IVariable<int>
		public static readonly int Rigidbody; // Rigidbody
		public static readonly int Renderer; // Renderer
		public static readonly int Animator; // Animator
		public static readonly int HitPointsView; // HitPointsView

		static WorldEntityAPI()
		{
			//Tags
			Damageable = NameToId(nameof(Damageable));

			//Values
			Position = NameToId(nameof(Position));
			Rotation = NameToId(nameof(Rotation));
			Parent = NameToId(nameof(Parent));
			MovementSpeed = NameToId(nameof(MovementSpeed));
			MovementCondition = NameToId(nameof(MovementCondition));
			MovementDirection = NameToId(nameof(MovementDirection));
			MovementEvent = NameToId(nameof(MovementEvent));
			RotationCondition = NameToId(nameof(RotationCondition));
			RotationSpeed = NameToId(nameof(RotationSpeed));
			RotationDirection = NameToId(nameof(RotationDirection));
			RotationEvent = NameToId(nameof(RotationEvent));
			Health = NameToId(nameof(Health));
			Lifetime = NameToId(nameof(Lifetime));
			TakeDamageEvent = NameToId(nameof(TakeDamageEvent));
			TakeDeathEvent = NameToId(nameof(TakeDeathEvent));
			DestroyAction = NameToId(nameof(DestroyAction));
			RespawnEvent = NameToId(nameof(RespawnEvent));
			TeamType = NameToId(nameof(TeamType));
			Weapon = NameToId(nameof(Weapon));
			Damage = NameToId(nameof(Damage));
			Target = NameToId(nameof(Target));
			FireCondition = NameToId(nameof(FireCondition));
			FireCooldown = NameToId(nameof(FireCooldown));
			FirePoint = NameToId(nameof(FirePoint));
			FireAction = NameToId(nameof(FireAction));
			FireEvent = NameToId(nameof(FireEvent));
			Trigger = NameToId(nameof(Trigger));
			PhysicsLayer = NameToId(nameof(PhysicsLayer));
			Rigidbody = NameToId(nameof(Rigidbody));
			Renderer = NameToId(nameof(Renderer));
			Animator = NameToId(nameof(Animator));
			HitPointsView = NameToId(nameof(HitPointsView));
		}


		///Tag Extensions

		#region Damageable

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasDamageableTag(this IActor entity) => entity.HasTag(Damageable);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddDamageableTag(this IActor entity) => entity.AddTag(Damageable);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelDamageableTag(this IActor entity) => entity.DelTag(Damageable);

		#endregion


		///Value Extensions

		#region Position

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IVariable<Vector3> GetPosition(this IActor entity) => entity.GetValue<IVariable<Vector3>>(Position);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetPosition(this IActor entity, out IVariable<Vector3> value) => entity.TryGetValue(Position, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddPosition(this IActor entity, IVariable<Vector3> value) => entity.AddValue(Position, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasPosition(this IActor entity) => entity.HasValue(Position);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelPosition(this IActor entity) => entity.DelValue(Position);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetPosition(this IActor entity, IVariable<Vector3> value) => entity.SetValue(Position, value);

		#endregion

		#region Rotation

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IVariable<Quaternion> GetRotation(this IActor entity) => entity.GetValue<IVariable<Quaternion>>(Rotation);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRotation(this IActor entity, out IVariable<Quaternion> value) => entity.TryGetValue(Rotation, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRotation(this IActor entity, IVariable<Quaternion> value) => entity.AddValue(Rotation, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRotation(this IActor entity) => entity.HasValue(Rotation);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRotation(this IActor entity) => entity.DelValue(Rotation);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotation(this IActor entity, IVariable<Quaternion> value) => entity.SetValue(Rotation, value);

		#endregion

		#region Parent

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IVariable<Transform> GetParent(this IActor entity) => entity.GetValue<IVariable<Transform>>(Parent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetParent(this IActor entity, out IVariable<Transform> value) => entity.TryGetValue(Parent, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddParent(this IActor entity, IVariable<Transform> value) => entity.AddValue(Parent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasParent(this IActor entity) => entity.HasValue(Parent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelParent(this IActor entity) => entity.DelValue(Parent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetParent(this IActor entity, IVariable<Transform> value) => entity.SetValue(Parent, value);

		#endregion

		#region MovementSpeed

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetMovementSpeed(this IActor entity) => entity.GetValue<IValue<float>>(MovementSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMovementSpeed(this IActor entity, out IValue<float> value) => entity.TryGetValue(MovementSpeed, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMovementSpeed(this IActor entity, IValue<float> value) => entity.AddValue(MovementSpeed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMovementSpeed(this IActor entity) => entity.HasValue(MovementSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMovementSpeed(this IActor entity) => entity.DelValue(MovementSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMovementSpeed(this IActor entity, IValue<float> value) => entity.SetValue(MovementSpeed, value);

		#endregion

		#region MovementCondition

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IExpression<bool> GetMovementCondition(this IActor entity) => entity.GetValue<IExpression<bool>>(MovementCondition);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMovementCondition(this IActor entity, out IExpression<bool> value) => entity.TryGetValue(MovementCondition, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMovementCondition(this IActor entity, IExpression<bool> value) => entity.AddValue(MovementCondition, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMovementCondition(this IActor entity) => entity.HasValue(MovementCondition);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMovementCondition(this IActor entity) => entity.DelValue(MovementCondition);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMovementCondition(this IActor entity, IExpression<bool> value) => entity.SetValue(MovementCondition, value);

		#endregion

		#region MovementDirection

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<Vector3> GetMovementDirection(this IActor entity) => entity.GetValue<IReactiveVariable<Vector3>>(MovementDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMovementDirection(this IActor entity, out IReactiveVariable<Vector3> value) => entity.TryGetValue(MovementDirection, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMovementDirection(this IActor entity, IReactiveVariable<Vector3> value) => entity.AddValue(MovementDirection, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMovementDirection(this IActor entity) => entity.HasValue(MovementDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMovementDirection(this IActor entity) => entity.DelValue(MovementDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMovementDirection(this IActor entity, IReactiveVariable<Vector3> value) => entity.SetValue(MovementDirection, value);

		#endregion

		#region MovementEvent

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEvent<Vector3> GetMovementEvent(this IActor entity) => entity.GetValue<IEvent<Vector3>>(MovementEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMovementEvent(this IActor entity, out IEvent<Vector3> value) => entity.TryGetValue(MovementEvent, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMovementEvent(this IActor entity, IEvent<Vector3> value) => entity.AddValue(MovementEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMovementEvent(this IActor entity) => entity.HasValue(MovementEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMovementEvent(this IActor entity) => entity.DelValue(MovementEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMovementEvent(this IActor entity, IEvent<Vector3> value) => entity.SetValue(MovementEvent, value);

		#endregion

		#region RotationCondition

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IExpression<bool> GetRotationCondition(this IActor entity) => entity.GetValue<IExpression<bool>>(RotationCondition);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRotationCondition(this IActor entity, out IExpression<bool> value) => entity.TryGetValue(RotationCondition, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRotationCondition(this IActor entity, IExpression<bool> value) => entity.AddValue(RotationCondition, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRotationCondition(this IActor entity) => entity.HasValue(RotationCondition);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRotationCondition(this IActor entity) => entity.DelValue(RotationCondition);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotationCondition(this IActor entity, IExpression<bool> value) => entity.SetValue(RotationCondition, value);

		#endregion

		#region RotationSpeed

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetRotationSpeed(this IActor entity) => entity.GetValue<IValue<float>>(RotationSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRotationSpeed(this IActor entity, out IValue<float> value) => entity.TryGetValue(RotationSpeed, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRotationSpeed(this IActor entity, IValue<float> value) => entity.AddValue(RotationSpeed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRotationSpeed(this IActor entity) => entity.HasValue(RotationSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRotationSpeed(this IActor entity) => entity.DelValue(RotationSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotationSpeed(this IActor entity, IValue<float> value) => entity.SetValue(RotationSpeed, value);

		#endregion

		#region RotationDirection

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<Vector3> GetRotationDirection(this IActor entity) => entity.GetValue<IReactiveVariable<Vector3>>(RotationDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRotationDirection(this IActor entity, out IReactiveVariable<Vector3> value) => entity.TryGetValue(RotationDirection, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRotationDirection(this IActor entity, IReactiveVariable<Vector3> value) => entity.AddValue(RotationDirection, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRotationDirection(this IActor entity) => entity.HasValue(RotationDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRotationDirection(this IActor entity) => entity.DelValue(RotationDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotationDirection(this IActor entity, IReactiveVariable<Vector3> value) => entity.SetValue(RotationDirection, value);

		#endregion

		#region RotationEvent

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEvent<Vector3> GetRotationEvent(this IActor entity) => entity.GetValue<IEvent<Vector3>>(RotationEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRotationEvent(this IActor entity, out IEvent<Vector3> value) => entity.TryGetValue(RotationEvent, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRotationEvent(this IActor entity, IEvent<Vector3> value) => entity.AddValue(RotationEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRotationEvent(this IActor entity) => entity.HasValue(RotationEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRotationEvent(this IActor entity) => entity.DelValue(RotationEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotationEvent(this IActor entity, IEvent<Vector3> value) => entity.SetValue(RotationEvent, value);

		#endregion

		#region Health

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Health GetHealth(this IActor entity) => entity.GetValue<Health>(Health);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetHealth(this IActor entity, out Health value) => entity.TryGetValue(Health, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddHealth(this IActor entity, Health value) => entity.AddValue(Health, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasHealth(this IActor entity) => entity.HasValue(Health);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelHealth(this IActor entity) => entity.DelValue(Health);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetHealth(this IActor entity, Health value) => entity.SetValue(Health, value);

		#endregion

		#region Lifetime

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Cooldown GetLifetime(this IActor entity) => entity.GetValue<Cooldown>(Lifetime);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetLifetime(this IActor entity, out Cooldown value) => entity.TryGetValue(Lifetime, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddLifetime(this IActor entity, Cooldown value) => entity.AddValue(Lifetime, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasLifetime(this IActor entity) => entity.HasValue(Lifetime);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelLifetime(this IActor entity) => entity.DelValue(Lifetime);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetLifetime(this IActor entity, Cooldown value) => entity.SetValue(Lifetime, value);

		#endregion

		#region TakeDamageEvent

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEvent<DamageArgs> GetTakeDamageEvent(this IActor entity) => entity.GetValue<IEvent<DamageArgs>>(TakeDamageEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTakeDamageEvent(this IActor entity, out IEvent<DamageArgs> value) => entity.TryGetValue(TakeDamageEvent, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTakeDamageEvent(this IActor entity, IEvent<DamageArgs> value) => entity.AddValue(TakeDamageEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTakeDamageEvent(this IActor entity) => entity.HasValue(TakeDamageEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTakeDamageEvent(this IActor entity) => entity.DelValue(TakeDamageEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTakeDamageEvent(this IActor entity, IEvent<DamageArgs> value) => entity.SetValue(TakeDamageEvent, value);

		#endregion

		#region TakeDeathEvent

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEvent<DamageArgs> GetTakeDeathEvent(this IActor entity) => entity.GetValue<IEvent<DamageArgs>>(TakeDeathEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTakeDeathEvent(this IActor entity, out IEvent<DamageArgs> value) => entity.TryGetValue(TakeDeathEvent, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTakeDeathEvent(this IActor entity, IEvent<DamageArgs> value) => entity.AddValue(TakeDeathEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTakeDeathEvent(this IActor entity) => entity.HasValue(TakeDeathEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTakeDeathEvent(this IActor entity) => entity.DelValue(TakeDeathEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTakeDeathEvent(this IActor entity, IEvent<DamageArgs> value) => entity.SetValue(TakeDeathEvent, value);

		#endregion

		#region DestroyAction

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IAction GetDestroyAction(this IActor entity) => entity.GetValue<IAction>(DestroyAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetDestroyAction(this IActor entity, out IAction value) => entity.TryGetValue(DestroyAction, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddDestroyAction(this IActor entity, IAction value) => entity.AddValue(DestroyAction, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasDestroyAction(this IActor entity) => entity.HasValue(DestroyAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelDestroyAction(this IActor entity) => entity.DelValue(DestroyAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetDestroyAction(this IActor entity, IAction value) => entity.SetValue(DestroyAction, value);

		#endregion

		#region RespawnEvent

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEvent GetRespawnEvent(this IActor entity) => entity.GetValue<IEvent>(RespawnEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRespawnEvent(this IActor entity, out IEvent value) => entity.TryGetValue(RespawnEvent, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRespawnEvent(this IActor entity, IEvent value) => entity.AddValue(RespawnEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRespawnEvent(this IActor entity) => entity.HasValue(RespawnEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRespawnEvent(this IActor entity) => entity.DelValue(RespawnEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRespawnEvent(this IActor entity, IEvent value) => entity.SetValue(RespawnEvent, value);

		#endregion

		#region TeamType

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<TeamType> GetTeamType(this IActor entity) => entity.GetValue<IReactiveVariable<TeamType>>(TeamType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTeamType(this IActor entity, out IReactiveVariable<TeamType> value) => entity.TryGetValue(TeamType, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTeamType(this IActor entity, IReactiveVariable<TeamType> value) => entity.AddValue(TeamType, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTeamType(this IActor entity) => entity.HasValue(TeamType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTeamType(this IActor entity) => entity.DelValue(TeamType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTeamType(this IActor entity, IReactiveVariable<TeamType> value) => entity.SetValue(TeamType, value);

		#endregion

		#region Weapon

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IWeapon GetWeapon(this IActor entity) => entity.GetValue<IWeapon>(Weapon);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetWeapon(this IActor entity, out IWeapon value) => entity.TryGetValue(Weapon, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddWeapon(this IActor entity, IWeapon value) => entity.AddValue(Weapon, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasWeapon(this IActor entity) => entity.HasValue(Weapon);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelWeapon(this IActor entity) => entity.DelValue(Weapon);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetWeapon(this IActor entity, IWeapon value) => entity.SetValue(Weapon, value);

		#endregion

		#region Damage

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<int> GetDamage(this IActor entity) => entity.GetValue<IValue<int>>(Damage);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetDamage(this IActor entity, out IValue<int> value) => entity.TryGetValue(Damage, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddDamage(this IActor entity, IValue<int> value) => entity.AddValue(Damage, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasDamage(this IActor entity) => entity.HasValue(Damage);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelDamage(this IActor entity) => entity.DelValue(Damage);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetDamage(this IActor entity, IValue<int> value) => entity.SetValue(Damage, value);

		#endregion

		#region Target

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<IEntity> GetTarget(this IActor entity) => entity.GetValue<IReactiveVariable<IEntity>>(Target);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTarget(this IActor entity, out IReactiveVariable<IEntity> value) => entity.TryGetValue(Target, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTarget(this IActor entity, IReactiveVariable<IEntity> value) => entity.AddValue(Target, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTarget(this IActor entity) => entity.HasValue(Target);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTarget(this IActor entity) => entity.DelValue(Target);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTarget(this IActor entity, IReactiveVariable<IEntity> value) => entity.SetValue(Target, value);

		#endregion

		#region FireCondition

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IExpression<bool> GetFireCondition(this IActor entity) => entity.GetValue<IExpression<bool>>(FireCondition);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFireCondition(this IActor entity, out IExpression<bool> value) => entity.TryGetValue(FireCondition, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFireCondition(this IActor entity, IExpression<bool> value) => entity.AddValue(FireCondition, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFireCondition(this IActor entity) => entity.HasValue(FireCondition);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFireCondition(this IActor entity) => entity.DelValue(FireCondition);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFireCondition(this IActor entity, IExpression<bool> value) => entity.SetValue(FireCondition, value);

		#endregion

		#region FireCooldown

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Cooldown GetFireCooldown(this IActor entity) => entity.GetValue<Cooldown>(FireCooldown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFireCooldown(this IActor entity, out Cooldown value) => entity.TryGetValue(FireCooldown, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFireCooldown(this IActor entity, Cooldown value) => entity.AddValue(FireCooldown, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFireCooldown(this IActor entity) => entity.HasValue(FireCooldown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFireCooldown(this IActor entity) => entity.DelValue(FireCooldown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFireCooldown(this IActor entity, Cooldown value) => entity.SetValue(FireCooldown, value);

		#endregion

		#region FirePoint

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Transform GetFirePoint(this IActor entity) => entity.GetValue<Transform>(FirePoint);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFirePoint(this IActor entity, out Transform value) => entity.TryGetValue(FirePoint, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFirePoint(this IActor entity, Transform value) => entity.AddValue(FirePoint, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFirePoint(this IActor entity) => entity.HasValue(FirePoint);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFirePoint(this IActor entity) => entity.DelValue(FirePoint);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFirePoint(this IActor entity, Transform value) => entity.SetValue(FirePoint, value);

		#endregion

		#region FireAction

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IAction GetFireAction(this IActor entity) => entity.GetValue<IAction>(FireAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFireAction(this IActor entity, out IAction value) => entity.TryGetValue(FireAction, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFireAction(this IActor entity, IAction value) => entity.AddValue(FireAction, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFireAction(this IActor entity) => entity.HasValue(FireAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFireAction(this IActor entity) => entity.DelValue(FireAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFireAction(this IActor entity, IAction value) => entity.SetValue(FireAction, value);

		#endregion

		#region FireEvent

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEvent GetFireEvent(this IActor entity) => entity.GetValue<IEvent>(FireEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFireEvent(this IActor entity, out IEvent value) => entity.TryGetValue(FireEvent, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFireEvent(this IActor entity, IEvent value) => entity.AddValue(FireEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFireEvent(this IActor entity) => entity.HasValue(FireEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFireEvent(this IActor entity) => entity.DelValue(FireEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFireEvent(this IActor entity, IEvent value) => entity.SetValue(FireEvent, value);

		#endregion

		#region Trigger

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TriggerEvents GetTrigger(this IActor entity) => entity.GetValue<TriggerEvents>(Trigger);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTrigger(this IActor entity, out TriggerEvents value) => entity.TryGetValue(Trigger, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTrigger(this IActor entity, TriggerEvents value) => entity.AddValue(Trigger, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTrigger(this IActor entity) => entity.HasValue(Trigger);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTrigger(this IActor entity) => entity.DelValue(Trigger);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTrigger(this IActor entity, TriggerEvents value) => entity.SetValue(Trigger, value);

		#endregion

		#region PhysicsLayer

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IVariable<int> GetPhysicsLayer(this IActor entity) => entity.GetValue<IVariable<int>>(PhysicsLayer);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetPhysicsLayer(this IActor entity, out IVariable<int> value) => entity.TryGetValue(PhysicsLayer, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddPhysicsLayer(this IActor entity, IVariable<int> value) => entity.AddValue(PhysicsLayer, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasPhysicsLayer(this IActor entity) => entity.HasValue(PhysicsLayer);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelPhysicsLayer(this IActor entity) => entity.DelValue(PhysicsLayer);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetPhysicsLayer(this IActor entity, IVariable<int> value) => entity.SetValue(PhysicsLayer, value);

		#endregion

		#region Rigidbody

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Rigidbody GetRigidbody(this IActor entity) => entity.GetValue<Rigidbody>(Rigidbody);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRigidbody(this IActor entity, out Rigidbody value) => entity.TryGetValue(Rigidbody, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRigidbody(this IActor entity, Rigidbody value) => entity.AddValue(Rigidbody, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRigidbody(this IActor entity) => entity.HasValue(Rigidbody);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRigidbody(this IActor entity) => entity.DelValue(Rigidbody);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRigidbody(this IActor entity, Rigidbody value) => entity.SetValue(Rigidbody, value);

		#endregion

		#region Renderer

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Renderer GetRenderer(this IActor entity) => entity.GetValue<Renderer>(Renderer);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRenderer(this IActor entity, out Renderer value) => entity.TryGetValue(Renderer, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRenderer(this IActor entity, Renderer value) => entity.AddValue(Renderer, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRenderer(this IActor entity) => entity.HasValue(Renderer);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRenderer(this IActor entity) => entity.DelValue(Renderer);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRenderer(this IActor entity, Renderer value) => entity.SetValue(Renderer, value);

		#endregion

		#region Animator

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Animator GetAnimator(this IActor entity) => entity.GetValue<Animator>(Animator);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetAnimator(this IActor entity, out Animator value) => entity.TryGetValue(Animator, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddAnimator(this IActor entity, Animator value) => entity.AddValue(Animator, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasAnimator(this IActor entity) => entity.HasValue(Animator);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelAnimator(this IActor entity) => entity.DelValue(Animator);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetAnimator(this IActor entity, Animator value) => entity.SetValue(Animator, value);

		#endregion

		#region HitPointsView

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static HitPointsView GetHitPointsView(this IActor entity) => entity.GetValue<HitPointsView>(HitPointsView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetHitPointsView(this IActor entity, out HitPointsView value) => entity.TryGetValue(HitPointsView, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddHitPointsView(this IActor entity, HitPointsView value) => entity.AddValue(HitPointsView, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasHitPointsView(this IActor entity) => entity.HasValue(HitPointsView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelHitPointsView(this IActor entity) => entity.DelValue(HitPointsView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetHitPointsView(this IActor entity, HitPointsView value) => entity.SetValue(HitPointsView, value);

		#endregion
    }
}
