// /**
// * Code generation. Don't modify! 
// **/
//
// using Atomic.Entities;
// using System.Runtime.CompilerServices;
// using UnityEngine;
// using Atomic.Entities;
// using System;
// using Atomic.Elements;
// using Modules.Gameplay;
// using Atomic.Extensions;
//
// namespace RTSGame
// {
// 	public static class GameEntityAPI
// 	{
// 		///Tags
// 		public const int Damageable = 563499515;
// 		public const int Moveable = 448011500;
// 		public const int Projectile = 1940275645;
// 		public const int Unit = 2089395053;
//
//
// 		///Values
// 		public const int Position = -1084586333; // IReactiveVariable<Vector3>
// 		public const int Rotation = -804586841; // IReactiveVariable<Quaternion>
// 		public const int Radius = 1020291948; // IValue<float>
// 		public const int MoveSpeed = 526065662; // IValue<float>
// 		public const int MoveCondition = 1466174948; // IFunction<Vector3, float, bool>
// 		public const int MoveAction = 1225226561; // IAction<Vector3, float>
// 		public const int MoveEvent = 735308719; // IEvent<Vector3, float>
// 		public const int RotationSpeed = 1771316350; // IValue<float>
// 		public const int Health = -915003867; // Health
// 		public const int Lifetime = -997109026; // Cooldown
// 		public const int DestroyAction = 85938956; // IAction
// 		public const int Team = 1691486497; // IReactiveVariable<TeamType>
// 		public const int Damage = 375673178; // IValue<int>
// 		public const int Target = 1103309514; // IReactiveVariable<IEntity>
// 		public const int FireDistance = -342046158; // IValue<float>
// 		public const int FireCondition = -280402907; // IFunction<IEntity, bool>
// 		public const int FireAction = 1186461126; // IAction<IEntity>
// 		public const int FireEvent = -1683597082; // IEvent<IEntity>
// 		public const int FireCooldown = 695041130; // Cooldown
// 		public const int FirePoint = 397255013; // IValue<Vector3>
//
//
// 		///Tag Extensions
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasDamageableTag(this IEntity obj) => obj.HasTag(Damageable);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddDamageableTag(this IEntity obj) => obj.AddTag(Damageable);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelDamageableTag(this IEntity obj) => obj.DelTag(Damageable);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasMoveableTag(this IEntity obj) => obj.HasTag(Moveable);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddMoveableTag(this IEntity obj) => obj.AddTag(Moveable);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelMoveableTag(this IEntity obj) => obj.DelTag(Moveable);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasProjectileTag(this IEntity obj) => obj.HasTag(Projectile);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddProjectileTag(this IEntity obj) => obj.AddTag(Projectile);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelProjectileTag(this IEntity obj) => obj.DelTag(Projectile);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasUnitTag(this IEntity obj) => obj.HasTag(Unit);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddUnitTag(this IEntity obj) => obj.AddTag(Unit);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelUnitTag(this IEntity obj) => obj.DelTag(Unit);
//
//
// 		///Value Extensions
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IReactiveVariable<Vector3> GetPosition(this IEntity obj) => obj.GetValue<IReactiveVariable<Vector3>>(Position);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetPosition(this IEntity obj, out IReactiveVariable<Vector3> value) => obj.TryGetValue(Position, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddPosition(this IEntity obj, IReactiveVariable<Vector3> value) => obj.AddValue(Position, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasPosition(this IEntity obj) => obj.HasValue(Position);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelPosition(this IEntity obj) => obj.DelValue(Position);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetPosition(this IEntity obj, IReactiveVariable<Vector3> value) => obj.SetValue(Position, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IReactiveVariable<Quaternion> GetRotation(this IEntity obj) => obj.GetValue<IReactiveVariable<Quaternion>>(Rotation);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetRotation(this IEntity obj, out IReactiveVariable<Quaternion> value) => obj.TryGetValue(Rotation, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddRotation(this IEntity obj, IReactiveVariable<Quaternion> value) => obj.AddValue(Rotation, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasRotation(this IEntity obj) => obj.HasValue(Rotation);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelRotation(this IEntity obj) => obj.DelValue(Rotation);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetRotation(this IEntity obj, IReactiveVariable<Quaternion> value) => obj.SetValue(Rotation, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IValue<float> GetRadius(this IEntity obj) => obj.GetValue<IValue<float>>(Radius);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetRadius(this IEntity obj, out IValue<float> value) => obj.TryGetValue(Radius, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddRadius(this IEntity obj, IValue<float> value) => obj.AddValue(Radius, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasRadius(this IEntity obj) => obj.HasValue(Radius);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelRadius(this IEntity obj) => obj.DelValue(Radius);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetRadius(this IEntity obj, IValue<float> value) => obj.SetValue(Radius, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IValue<float> GetMoveSpeed(this IEntity obj) => obj.GetValue<IValue<float>>(MoveSpeed);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetMoveSpeed(this IEntity obj, out IValue<float> value) => obj.TryGetValue(MoveSpeed, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddMoveSpeed(this IEntity obj, IValue<float> value) => obj.AddValue(MoveSpeed, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasMoveSpeed(this IEntity obj) => obj.HasValue(MoveSpeed);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelMoveSpeed(this IEntity obj) => obj.DelValue(MoveSpeed);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetMoveSpeed(this IEntity obj, IValue<float> value) => obj.SetValue(MoveSpeed, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IFunction<Vector3, float, bool> GetMoveCondition(this IEntity obj) => obj.GetValue<IFunction<Vector3, float, bool>>(MoveCondition);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetMoveCondition(this IEntity obj, out IFunction<Vector3, float, bool> value) => obj.TryGetValue(MoveCondition, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddMoveCondition(this IEntity obj, IFunction<Vector3, float, bool> value) => obj.AddValue(MoveCondition, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasMoveCondition(this IEntity obj) => obj.HasValue(MoveCondition);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelMoveCondition(this IEntity obj) => obj.DelValue(MoveCondition);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetMoveCondition(this IEntity obj, IFunction<Vector3, float, bool> value) => obj.SetValue(MoveCondition, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IAction<Vector3, float> GetMoveAction(this IEntity obj) => obj.GetValue<IAction<Vector3, float>>(MoveAction);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetMoveAction(this IEntity obj, out IAction<Vector3, float> value) => obj.TryGetValue(MoveAction, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddMoveAction(this IEntity obj, IAction<Vector3, float> value) => obj.AddValue(MoveAction, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasMoveAction(this IEntity obj) => obj.HasValue(MoveAction);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelMoveAction(this IEntity obj) => obj.DelValue(MoveAction);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetMoveAction(this IEntity obj, IAction<Vector3, float> value) => obj.SetValue(MoveAction, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IEvent<Vector3, float> GetMoveEvent(this IEntity obj) => obj.GetValue<IEvent<Vector3, float>>(MoveEvent);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetMoveEvent(this IEntity obj, out IEvent<Vector3, float> value) => obj.TryGetValue(MoveEvent, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddMoveEvent(this IEntity obj, IEvent<Vector3, float> value) => obj.AddValue(MoveEvent, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasMoveEvent(this IEntity obj) => obj.HasValue(MoveEvent);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelMoveEvent(this IEntity obj) => obj.DelValue(MoveEvent);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetMoveEvent(this IEntity obj, IEvent<Vector3, float> value) => obj.SetValue(MoveEvent, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IValue<float> GetRotationSpeed(this IEntity obj) => obj.GetValue<IValue<float>>(RotationSpeed);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetRotationSpeed(this IEntity obj, out IValue<float> value) => obj.TryGetValue(RotationSpeed, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddRotationSpeed(this IEntity obj, IValue<float> value) => obj.AddValue(RotationSpeed, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasRotationSpeed(this IEntity obj) => obj.HasValue(RotationSpeed);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelRotationSpeed(this IEntity obj) => obj.DelValue(RotationSpeed);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetRotationSpeed(this IEntity obj, IValue<float> value) => obj.SetValue(RotationSpeed, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static Health GetHealth(this IEntity obj) => obj.GetValue<Health>(Health);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetHealth(this IEntity obj, out Health value) => obj.TryGetValue(Health, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddHealth(this IEntity obj, Health value) => obj.AddValue(Health, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasHealth(this IEntity obj) => obj.HasValue(Health);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelHealth(this IEntity obj) => obj.DelValue(Health);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetHealth(this IEntity obj, Health value) => obj.SetValue(Health, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static Cooldown GetLifetime(this IEntity obj) => obj.GetValue<Cooldown>(Lifetime);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetLifetime(this IEntity obj, out Cooldown value) => obj.TryGetValue(Lifetime, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddLifetime(this IEntity obj, Cooldown value) => obj.AddValue(Lifetime, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasLifetime(this IEntity obj) => obj.HasValue(Lifetime);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelLifetime(this IEntity obj) => obj.DelValue(Lifetime);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetLifetime(this IEntity obj, Cooldown value) => obj.SetValue(Lifetime, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IAction GetDestroyAction(this IEntity obj) => obj.GetValue<IAction>(DestroyAction);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetDestroyAction(this IEntity obj, out IAction value) => obj.TryGetValue(DestroyAction, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddDestroyAction(this IEntity obj, IAction value) => obj.AddValue(DestroyAction, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasDestroyAction(this IEntity obj) => obj.HasValue(DestroyAction);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelDestroyAction(this IEntity obj) => obj.DelValue(DestroyAction);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetDestroyAction(this IEntity obj, IAction value) => obj.SetValue(DestroyAction, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IReactiveVariable<TeamType> GetTeam(this IEntity obj) => obj.GetValue<IReactiveVariable<TeamType>>(Team);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetTeam(this IEntity obj, out IReactiveVariable<TeamType> value) => obj.TryGetValue(Team, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddTeam(this IEntity obj, IReactiveVariable<TeamType> value) => obj.AddValue(Team, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasTeam(this IEntity obj) => obj.HasValue(Team);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelTeam(this IEntity obj) => obj.DelValue(Team);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetTeam(this IEntity obj, IReactiveVariable<TeamType> value) => obj.SetValue(Team, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IValue<int> GetDamage(this IEntity obj) => obj.GetValue<IValue<int>>(Damage);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetDamage(this IEntity obj, out IValue<int> value) => obj.TryGetValue(Damage, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddDamage(this IEntity obj, IValue<int> value) => obj.AddValue(Damage, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasDamage(this IEntity obj) => obj.HasValue(Damage);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelDamage(this IEntity obj) => obj.DelValue(Damage);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetDamage(this IEntity obj, IValue<int> value) => obj.SetValue(Damage, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IReactiveVariable<IEntity> GetTarget(this IEntity obj) => obj.GetValue<IReactiveVariable<IEntity>>(Target);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetTarget(this IEntity obj, out IReactiveVariable<IEntity> value) => obj.TryGetValue(Target, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddTarget(this IEntity obj, IReactiveVariable<IEntity> value) => obj.AddValue(Target, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasTarget(this IEntity obj) => obj.HasValue(Target);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelTarget(this IEntity obj) => obj.DelValue(Target);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetTarget(this IEntity obj, IReactiveVariable<IEntity> value) => obj.SetValue(Target, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IValue<float> GetFireDistance(this IEntity obj) => obj.GetValue<IValue<float>>(FireDistance);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetFireDistance(this IEntity obj, out IValue<float> value) => obj.TryGetValue(FireDistance, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddFireDistance(this IEntity obj, IValue<float> value) => obj.AddValue(FireDistance, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasFireDistance(this IEntity obj) => obj.HasValue(FireDistance);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelFireDistance(this IEntity obj) => obj.DelValue(FireDistance);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetFireDistance(this IEntity obj, IValue<float> value) => obj.SetValue(FireDistance, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IFunction<IEntity, bool> GetFireCondition(this IEntity obj) => obj.GetValue<IFunction<IEntity, bool>>(FireCondition);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetFireCondition(this IEntity obj, out IFunction<IEntity, bool> value) => obj.TryGetValue(FireCondition, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddFireCondition(this IEntity obj, IFunction<IEntity, bool> value) => obj.AddValue(FireCondition, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasFireCondition(this IEntity obj) => obj.HasValue(FireCondition);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelFireCondition(this IEntity obj) => obj.DelValue(FireCondition);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetFireCondition(this IEntity obj, IFunction<IEntity, bool> value) => obj.SetValue(FireCondition, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IAction<IEntity> GetFireAction(this IEntity obj) => obj.GetValue<IAction<IEntity>>(FireAction);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetFireAction(this IEntity obj, out IAction<IEntity> value) => obj.TryGetValue(FireAction, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddFireAction(this IEntity obj, IAction<IEntity> value) => obj.AddValue(FireAction, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasFireAction(this IEntity obj) => obj.HasValue(FireAction);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelFireAction(this IEntity obj) => obj.DelValue(FireAction);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetFireAction(this IEntity obj, IAction<IEntity> value) => obj.SetValue(FireAction, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IEvent<IEntity> GetFireEvent(this IEntity obj) => obj.GetValue<IEvent<IEntity>>(FireEvent);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetFireEvent(this IEntity obj, out IEvent<IEntity> value) => obj.TryGetValue(FireEvent, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddFireEvent(this IEntity obj, IEvent<IEntity> value) => obj.AddValue(FireEvent, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasFireEvent(this IEntity obj) => obj.HasValue(FireEvent);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelFireEvent(this IEntity obj) => obj.DelValue(FireEvent);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetFireEvent(this IEntity obj, IEvent<IEntity> value) => obj.SetValue(FireEvent, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static Cooldown GetFireCooldown(this IEntity obj) => obj.GetValue<Cooldown>(FireCooldown);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetFireCooldown(this IEntity obj, out Cooldown value) => obj.TryGetValue(FireCooldown, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddFireCooldown(this IEntity obj, Cooldown value) => obj.AddValue(FireCooldown, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasFireCooldown(this IEntity obj) => obj.HasValue(FireCooldown);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelFireCooldown(this IEntity obj) => obj.DelValue(FireCooldown);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetFireCooldown(this IEntity obj, Cooldown value) => obj.SetValue(FireCooldown, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IValue<Vector3> GetFirePoint(this IEntity obj) => obj.GetValue<IValue<Vector3>>(FirePoint);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetFirePoint(this IEntity obj, out IValue<Vector3> value) => obj.TryGetValue(FirePoint, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddFirePoint(this IEntity obj, IValue<Vector3> value) => obj.AddValue(FirePoint, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasFirePoint(this IEntity obj) => obj.HasValue(FirePoint);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelFirePoint(this IEntity obj) => obj.DelValue(FirePoint);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetFirePoint(this IEntity obj, IValue<Vector3> value) => obj.SetValue(FirePoint, value);
//     }
// }
