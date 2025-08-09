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

namespace ShooterGame.Gameplay
{
#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	public static class GameEntityAPI
	{

		///Tags
		public static readonly int Damageable;

		///Values
		public static readonly int Position; // IReactiveVariable<Vector3>
		public static readonly int Rotation; // IReactiveVariable<Quaternion>
		public static readonly int MoveSpeed; // IValue<float>
		public static readonly int RotationSpeed; // IValue<float>
		public static readonly int MoveCondition; // IExpression<bool>
		public static readonly int MoveDirection; // IReactiveVariable<Vector3>
		public static readonly int Health; // Health
		public static readonly int Lifetime; // Cooldown
		public static readonly int TakeDamageEvent; // IEvent<DamageArgs>
		public static readonly int TakeDeathEvent; // IEvent<DamageArgs>
		public static readonly int DestroyAction; // IAction
		public static readonly int Team; // IReactiveVariable<TeamType>
		public static readonly int Weapon; // IWeapon
		public static readonly int Damage; // IValue<int>
		public static readonly int Target; // IReactiveVariable<IEntity>
		public static readonly int FireCondition; // IExpression<bool>
		public static readonly int FireCooldown; // Cooldown
		public static readonly int FirePoint; // Transform
		public static readonly int FireAction; // IAction
		public static readonly int FireEvent; // IEvent
		public static readonly int Trigger; // TriggerEvents
		public static readonly int MeshRenderer; // Renderer
		public static readonly int Animator; // Animator

		static GameEntityAPI()
		{
			//Tags
			Damageable = NameToId(nameof(Damageable));

			//Values
			Position = NameToId(nameof(Position));
			Rotation = NameToId(nameof(Rotation));
			MoveSpeed = NameToId(nameof(MoveSpeed));
			RotationSpeed = NameToId(nameof(RotationSpeed));
			MoveCondition = NameToId(nameof(MoveCondition));
			MoveDirection = NameToId(nameof(MoveDirection));
			Health = NameToId(nameof(Health));
			Lifetime = NameToId(nameof(Lifetime));
			TakeDamageEvent = NameToId(nameof(TakeDamageEvent));
			TakeDeathEvent = NameToId(nameof(TakeDeathEvent));
			DestroyAction = NameToId(nameof(DestroyAction));
			Team = NameToId(nameof(Team));
			Weapon = NameToId(nameof(Weapon));
			Damage = NameToId(nameof(Damage));
			Target = NameToId(nameof(Target));
			FireCondition = NameToId(nameof(FireCondition));
			FireCooldown = NameToId(nameof(FireCooldown));
			FirePoint = NameToId(nameof(FirePoint));
			FireAction = NameToId(nameof(FireAction));
			FireEvent = NameToId(nameof(FireEvent));
			Trigger = NameToId(nameof(Trigger));
			MeshRenderer = NameToId(nameof(MeshRenderer));
			Animator = NameToId(nameof(Animator));
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


		///Value Extensions

		#region Position

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<Vector3> GetPosition(this IGameEntity entity) => entity.GetValue<IReactiveVariable<Vector3>>(Position);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetPosition(this IGameEntity entity, out IReactiveVariable<Vector3> value) => entity.TryGetValue(Position, out value);

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
		public static IReactiveVariable<Quaternion> GetRotation(this IGameEntity entity) => entity.GetValue<IReactiveVariable<Quaternion>>(Rotation);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRotation(this IGameEntity entity, out IReactiveVariable<Quaternion> value) => entity.TryGetValue(Rotation, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRotation(this IGameEntity entity, IReactiveVariable<Quaternion> value) => entity.AddValue(Rotation, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRotation(this IGameEntity entity) => entity.HasValue(Rotation);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRotation(this IGameEntity entity) => entity.DelValue(Rotation);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotation(this IGameEntity entity, IReactiveVariable<Quaternion> value) => entity.SetValue(Rotation, value);

		#endregion

		#region MoveSpeed

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetMoveSpeed(this IGameEntity entity) => entity.GetValue<IValue<float>>(MoveSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoveSpeed(this IGameEntity entity, out IValue<float> value) => entity.TryGetValue(MoveSpeed, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoveSpeed(this IGameEntity entity, IValue<float> value) => entity.AddValue(MoveSpeed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoveSpeed(this IGameEntity entity) => entity.HasValue(MoveSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoveSpeed(this IGameEntity entity) => entity.DelValue(MoveSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoveSpeed(this IGameEntity entity, IValue<float> value) => entity.SetValue(MoveSpeed, value);

		#endregion

		#region RotationSpeed

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetRotationSpeed(this IGameEntity entity) => entity.GetValue<IValue<float>>(RotationSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRotationSpeed(this IGameEntity entity, out IValue<float> value) => entity.TryGetValue(RotationSpeed, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRotationSpeed(this IGameEntity entity, IValue<float> value) => entity.AddValue(RotationSpeed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRotationSpeed(this IGameEntity entity) => entity.HasValue(RotationSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRotationSpeed(this IGameEntity entity) => entity.DelValue(RotationSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotationSpeed(this IGameEntity entity, IValue<float> value) => entity.SetValue(RotationSpeed, value);

		#endregion

		#region MoveCondition

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IExpression<bool> GetMoveCondition(this IGameEntity entity) => entity.GetValue<IExpression<bool>>(MoveCondition);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoveCondition(this IGameEntity entity, out IExpression<bool> value) => entity.TryGetValue(MoveCondition, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoveCondition(this IGameEntity entity, IExpression<bool> value) => entity.AddValue(MoveCondition, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoveCondition(this IGameEntity entity) => entity.HasValue(MoveCondition);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoveCondition(this IGameEntity entity) => entity.DelValue(MoveCondition);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoveCondition(this IGameEntity entity, IExpression<bool> value) => entity.SetValue(MoveCondition, value);

		#endregion

		#region MoveDirection

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<Vector3> GetMoveDirection(this IGameEntity entity) => entity.GetValue<IReactiveVariable<Vector3>>(MoveDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoveDirection(this IGameEntity entity, out IReactiveVariable<Vector3> value) => entity.TryGetValue(MoveDirection, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoveDirection(this IGameEntity entity, IReactiveVariable<Vector3> value) => entity.AddValue(MoveDirection, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoveDirection(this IGameEntity entity) => entity.HasValue(MoveDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoveDirection(this IGameEntity entity) => entity.DelValue(MoveDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoveDirection(this IGameEntity entity, IReactiveVariable<Vector3> value) => entity.SetValue(MoveDirection, value);

		#endregion

		#region Health

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Health GetHealth(this IGameEntity entity) => entity.GetValue<Health>(Health);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetHealth(this IGameEntity entity, out Health value) => entity.TryGetValue(Health, out value);

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
		public static Cooldown GetLifetime(this IGameEntity entity) => entity.GetValue<Cooldown>(Lifetime);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetLifetime(this IGameEntity entity, out Cooldown value) => entity.TryGetValue(Lifetime, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddLifetime(this IGameEntity entity, Cooldown value) => entity.AddValue(Lifetime, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasLifetime(this IGameEntity entity) => entity.HasValue(Lifetime);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelLifetime(this IGameEntity entity) => entity.DelValue(Lifetime);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetLifetime(this IGameEntity entity, Cooldown value) => entity.SetValue(Lifetime, value);

		#endregion

		#region TakeDamageEvent

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEvent<DamageArgs> GetTakeDamageEvent(this IGameEntity entity) => entity.GetValue<IEvent<DamageArgs>>(TakeDamageEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTakeDamageEvent(this IGameEntity entity, out IEvent<DamageArgs> value) => entity.TryGetValue(TakeDamageEvent, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTakeDamageEvent(this IGameEntity entity, IEvent<DamageArgs> value) => entity.AddValue(TakeDamageEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTakeDamageEvent(this IGameEntity entity) => entity.HasValue(TakeDamageEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTakeDamageEvent(this IGameEntity entity) => entity.DelValue(TakeDamageEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTakeDamageEvent(this IGameEntity entity, IEvent<DamageArgs> value) => entity.SetValue(TakeDamageEvent, value);

		#endregion

		#region TakeDeathEvent

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEvent<DamageArgs> GetTakeDeathEvent(this IGameEntity entity) => entity.GetValue<IEvent<DamageArgs>>(TakeDeathEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTakeDeathEvent(this IGameEntity entity, out IEvent<DamageArgs> value) => entity.TryGetValue(TakeDeathEvent, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTakeDeathEvent(this IGameEntity entity, IEvent<DamageArgs> value) => entity.AddValue(TakeDeathEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTakeDeathEvent(this IGameEntity entity) => entity.HasValue(TakeDeathEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTakeDeathEvent(this IGameEntity entity) => entity.DelValue(TakeDeathEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTakeDeathEvent(this IGameEntity entity, IEvent<DamageArgs> value) => entity.SetValue(TakeDeathEvent, value);

		#endregion

		#region DestroyAction

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IAction GetDestroyAction(this IGameEntity entity) => entity.GetValue<IAction>(DestroyAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetDestroyAction(this IGameEntity entity, out IAction value) => entity.TryGetValue(DestroyAction, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddDestroyAction(this IGameEntity entity, IAction value) => entity.AddValue(DestroyAction, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasDestroyAction(this IGameEntity entity) => entity.HasValue(DestroyAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelDestroyAction(this IGameEntity entity) => entity.DelValue(DestroyAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetDestroyAction(this IGameEntity entity, IAction value) => entity.SetValue(DestroyAction, value);

		#endregion

		#region Team

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<TeamType> GetTeam(this IGameEntity entity) => entity.GetValue<IReactiveVariable<TeamType>>(Team);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTeam(this IGameEntity entity, out IReactiveVariable<TeamType> value) => entity.TryGetValue(Team, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTeam(this IGameEntity entity, IReactiveVariable<TeamType> value) => entity.AddValue(Team, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTeam(this IGameEntity entity) => entity.HasValue(Team);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTeam(this IGameEntity entity) => entity.DelValue(Team);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTeam(this IGameEntity entity, IReactiveVariable<TeamType> value) => entity.SetValue(Team, value);

		#endregion

		#region Weapon

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IWeapon GetWeapon(this IGameEntity entity) => entity.GetValue<IWeapon>(Weapon);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetWeapon(this IGameEntity entity, out IWeapon value) => entity.TryGetValue(Weapon, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddWeapon(this IGameEntity entity, IWeapon value) => entity.AddValue(Weapon, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasWeapon(this IGameEntity entity) => entity.HasValue(Weapon);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelWeapon(this IGameEntity entity) => entity.DelValue(Weapon);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetWeapon(this IGameEntity entity, IWeapon value) => entity.SetValue(Weapon, value);

		#endregion

		#region Damage

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<int> GetDamage(this IGameEntity entity) => entity.GetValue<IValue<int>>(Damage);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetDamage(this IGameEntity entity, out IValue<int> value) => entity.TryGetValue(Damage, out value);

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
		public static IReactiveVariable<IEntity> GetTarget(this IGameEntity entity) => entity.GetValue<IReactiveVariable<IEntity>>(Target);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTarget(this IGameEntity entity, out IReactiveVariable<IEntity> value) => entity.TryGetValue(Target, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTarget(this IGameEntity entity, IReactiveVariable<IEntity> value) => entity.AddValue(Target, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTarget(this IGameEntity entity) => entity.HasValue(Target);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTarget(this IGameEntity entity) => entity.DelValue(Target);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTarget(this IGameEntity entity, IReactiveVariable<IEntity> value) => entity.SetValue(Target, value);

		#endregion

		#region FireCondition

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IExpression<bool> GetFireCondition(this IGameEntity entity) => entity.GetValue<IExpression<bool>>(FireCondition);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFireCondition(this IGameEntity entity, out IExpression<bool> value) => entity.TryGetValue(FireCondition, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFireCondition(this IGameEntity entity, IExpression<bool> value) => entity.AddValue(FireCondition, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFireCondition(this IGameEntity entity) => entity.HasValue(FireCondition);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFireCondition(this IGameEntity entity) => entity.DelValue(FireCondition);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFireCondition(this IGameEntity entity, IExpression<bool> value) => entity.SetValue(FireCondition, value);

		#endregion

		#region FireCooldown

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Cooldown GetFireCooldown(this IGameEntity entity) => entity.GetValue<Cooldown>(FireCooldown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFireCooldown(this IGameEntity entity, out Cooldown value) => entity.TryGetValue(FireCooldown, out value);

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
		public static Transform GetFirePoint(this IGameEntity entity) => entity.GetValue<Transform>(FirePoint);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFirePoint(this IGameEntity entity, out Transform value) => entity.TryGetValue(FirePoint, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFirePoint(this IGameEntity entity, Transform value) => entity.AddValue(FirePoint, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFirePoint(this IGameEntity entity) => entity.HasValue(FirePoint);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFirePoint(this IGameEntity entity) => entity.DelValue(FirePoint);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFirePoint(this IGameEntity entity, Transform value) => entity.SetValue(FirePoint, value);

		#endregion

		#region FireAction

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IAction GetFireAction(this IGameEntity entity) => entity.GetValue<IAction>(FireAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFireAction(this IGameEntity entity, out IAction value) => entity.TryGetValue(FireAction, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFireAction(this IGameEntity entity, IAction value) => entity.AddValue(FireAction, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFireAction(this IGameEntity entity) => entity.HasValue(FireAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFireAction(this IGameEntity entity) => entity.DelValue(FireAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFireAction(this IGameEntity entity, IAction value) => entity.SetValue(FireAction, value);

		#endregion

		#region FireEvent

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEvent GetFireEvent(this IGameEntity entity) => entity.GetValue<IEvent>(FireEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFireEvent(this IGameEntity entity, out IEvent value) => entity.TryGetValue(FireEvent, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFireEvent(this IGameEntity entity, IEvent value) => entity.AddValue(FireEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFireEvent(this IGameEntity entity) => entity.HasValue(FireEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFireEvent(this IGameEntity entity) => entity.DelValue(FireEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFireEvent(this IGameEntity entity, IEvent value) => entity.SetValue(FireEvent, value);

		#endregion

		#region Trigger

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TriggerEvents GetTrigger(this IGameEntity entity) => entity.GetValue<TriggerEvents>(Trigger);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTrigger(this IGameEntity entity, out TriggerEvents value) => entity.TryGetValue(Trigger, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTrigger(this IGameEntity entity, TriggerEvents value) => entity.AddValue(Trigger, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTrigger(this IGameEntity entity) => entity.HasValue(Trigger);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTrigger(this IGameEntity entity) => entity.DelValue(Trigger);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTrigger(this IGameEntity entity, TriggerEvents value) => entity.SetValue(Trigger, value);

		#endregion

		#region MeshRenderer

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Renderer GetMeshRenderer(this IGameEntity entity) => entity.GetValue<Renderer>(MeshRenderer);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMeshRenderer(this IGameEntity entity, out Renderer value) => entity.TryGetValue(MeshRenderer, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMeshRenderer(this IGameEntity entity, Renderer value) => entity.AddValue(MeshRenderer, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMeshRenderer(this IGameEntity entity) => entity.HasValue(MeshRenderer);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMeshRenderer(this IGameEntity entity) => entity.DelValue(MeshRenderer);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMeshRenderer(this IGameEntity entity, Renderer value) => entity.SetValue(MeshRenderer, value);

		#endregion

		#region Animator

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Animator GetAnimator(this IGameEntity entity) => entity.GetValue<Animator>(Animator);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetAnimator(this IGameEntity entity, out Animator value) => entity.TryGetValue(Animator, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddAnimator(this IGameEntity entity, Animator value) => entity.AddValue(Animator, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasAnimator(this IGameEntity entity) => entity.HasValue(Animator);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelAnimator(this IGameEntity entity) => entity.DelValue(Animator);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetAnimator(this IGameEntity entity, Animator value) => entity.SetValue(Animator, value);

		#endregion
    }
}
