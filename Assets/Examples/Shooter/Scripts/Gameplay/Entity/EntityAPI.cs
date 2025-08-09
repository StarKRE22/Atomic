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
//
// namespace ShooterGame.Gameplay
// {
// 	public static class EntityAPI
// 	{
// 		///Tags
// 		public const int Damageable = 563499515;
//
//
// 		///Values
// 		public const int GameObject = 1482111001; // GameObject
// 		public const int Transform = -180157682; // Transform
// 		public const int MoveSpeed = 526065662; // IValue<float>
// 		public const int AngularSpeed = -1089183267; // IValue<float>
// 		public const int MoveCondition = 1466174948; // IExpression<bool>
// 		public const int MoveDirection = -721923052; // IReactiveVariable<Vector3>
// 		public const int Health = -915003867; // Health
// 		public const int Lifetime = -997109026; // Cooldown
// 		public const int TakeDamageEvent = 1486057413; // IEvent<DamageArgs>
// 		public const int TakeDeathEvent = 420717635; // IEvent<DamageArgs>
// 		public const int DestroyAction = 85938956; // IAction
// 		public const int Team = 1691486497; // IReactiveVariable<TeamType>
// 		public const int Weapon = 1855955664; // IEntity
// 		public const int Damage = 375673178; // IValue<int>
// 		public const int Target = 1103309514; // IReactiveVariable<IEntity>
// 		public const int FireCondition = -280402907; // IExpression<bool>
// 		public const int FireCooldown = 695041130; // Cooldown
// 		public const int FirePoint = 397255013; // Transform
// 		public const int FireAction = 1186461126; // IAction
// 		public const int FireEvent = -1683597082; // IEvent
// 		public const int Trigger = -707381567; // TriggerEventReceiver
// 		public const int MeshRenderer = 1670531983; // Renderer
// 		public const int Animator = -1714818978; // Animator
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
//
// 		///Value Extensions
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static GameObject GetGameObject(this IEntity obj) => obj.GetValue<GameObject>(GameObject);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetGameObject(this IEntity obj, out GameObject value) => obj.TryGetValue(GameObject, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddGameObject(this IEntity obj, GameObject value) => obj.AddValue(GameObject, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasGameObject(this IEntity obj) => obj.HasValue(GameObject);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelGameObject(this IEntity obj) => obj.DelValue(GameObject);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetGameObject(this IEntity obj, GameObject value) => obj.SetValue(GameObject, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static Transform GetTransform(this IEntity obj) => obj.GetValue<Transform>(Transform);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetTransform(this IEntity obj, out Transform value) => obj.TryGetValue(Transform, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddTransform(this IEntity obj, Transform value) => obj.AddValue(Transform, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasTransform(this IEntity obj) => obj.HasValue(Transform);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelTransform(this IEntity obj) => obj.DelValue(Transform);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetTransform(this IEntity obj, Transform value) => obj.SetValue(Transform, value);
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
// 		public static IValue<float> GetAngularSpeed(this IEntity obj) => obj.GetValue<IValue<float>>(AngularSpeed);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetAngularSpeed(this IEntity obj, out IValue<float> value) => obj.TryGetValue(AngularSpeed, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddAngularSpeed(this IEntity obj, IValue<float> value) => obj.AddValue(AngularSpeed, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasAngularSpeed(this IEntity obj) => obj.HasValue(AngularSpeed);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelAngularSpeed(this IEntity obj) => obj.DelValue(AngularSpeed);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetAngularSpeed(this IEntity obj, IValue<float> value) => obj.SetValue(AngularSpeed, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IExpression<bool> GetMoveCondition(this IEntity obj) => obj.GetValue<IExpression<bool>>(MoveCondition);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetMoveCondition(this IEntity obj, out IExpression<bool> value) => obj.TryGetValue(MoveCondition, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddMoveCondition(this IEntity obj, IExpression<bool> value) => obj.AddValue(MoveCondition, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasMoveCondition(this IEntity obj) => obj.HasValue(MoveCondition);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelMoveCondition(this IEntity obj) => obj.DelValue(MoveCondition);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetMoveCondition(this IEntity obj, IExpression<bool> value) => obj.SetValue(MoveCondition, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IReactiveVariable<Vector3> GetMoveDirection(this IEntity obj) => obj.GetValue<IReactiveVariable<Vector3>>(MoveDirection);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetMoveDirection(this IEntity obj, out IReactiveVariable<Vector3> value) => obj.TryGetValue(MoveDirection, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddMoveDirection(this IEntity obj, IReactiveVariable<Vector3> value) => obj.AddValue(MoveDirection, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasMoveDirection(this IEntity obj) => obj.HasValue(MoveDirection);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelMoveDirection(this IEntity obj) => obj.DelValue(MoveDirection);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetMoveDirection(this IEntity obj, IReactiveVariable<Vector3> value) => obj.SetValue(MoveDirection, value);
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
// 		public static IEvent<DamageArgs> GetTakeDamageEvent(this IEntity obj) => obj.GetValue<IEvent<DamageArgs>>(TakeDamageEvent);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetTakeDamageEvent(this IEntity obj, out IEvent<DamageArgs> value) => obj.TryGetValue(TakeDamageEvent, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddTakeDamageEvent(this IEntity obj, IEvent<DamageArgs> value) => obj.AddValue(TakeDamageEvent, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasTakeDamageEvent(this IEntity obj) => obj.HasValue(TakeDamageEvent);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelTakeDamageEvent(this IEntity obj) => obj.DelValue(TakeDamageEvent);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetTakeDamageEvent(this IEntity obj, IEvent<DamageArgs> value) => obj.SetValue(TakeDamageEvent, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IEvent<DamageArgs> GetTakeDeathEvent(this IEntity obj) => obj.GetValue<IEvent<DamageArgs>>(TakeDeathEvent);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetTakeDeathEvent(this IEntity obj, out IEvent<DamageArgs> value) => obj.TryGetValue(TakeDeathEvent, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddTakeDeathEvent(this IEntity obj, IEvent<DamageArgs> value) => obj.AddValue(TakeDeathEvent, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasTakeDeathEvent(this IEntity obj) => obj.HasValue(TakeDeathEvent);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelTakeDeathEvent(this IEntity obj) => obj.DelValue(TakeDeathEvent);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetTakeDeathEvent(this IEntity obj, IEvent<DamageArgs> value) => obj.SetValue(TakeDeathEvent, value);
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
// 		public static IEntity GetWeapon(this IEntity obj) => obj.GetValue<IEntity>(Weapon);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetWeapon(this IEntity obj, out IEntity value) => obj.TryGetValue(Weapon, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddWeapon(this IEntity obj, IEntity value) => obj.AddValue(Weapon, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasWeapon(this IEntity obj) => obj.HasValue(Weapon);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelWeapon(this IEntity obj) => obj.DelValue(Weapon);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetWeapon(this IEntity obj, IEntity value) => obj.SetValue(Weapon, value);
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
// 		public static IExpression<bool> GetFireCondition(this IEntity obj) => obj.GetValue<IExpression<bool>>(FireCondition);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetFireCondition(this IEntity obj, out IExpression<bool> value) => obj.TryGetValue(FireCondition, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddFireCondition(this IEntity obj, IExpression<bool> value) => obj.AddValue(FireCondition, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasFireCondition(this IEntity obj) => obj.HasValue(FireCondition);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelFireCondition(this IEntity obj) => obj.DelValue(FireCondition);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetFireCondition(this IEntity obj, IExpression<bool> value) => obj.SetValue(FireCondition, value);
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
// 		public static Transform GetFirePoint(this IEntity obj) => obj.GetValue<Transform>(FirePoint);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetFirePoint(this IEntity obj, out Transform value) => obj.TryGetValue(FirePoint, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddFirePoint(this IEntity obj, Transform value) => obj.AddValue(FirePoint, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasFirePoint(this IEntity obj) => obj.HasValue(FirePoint);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelFirePoint(this IEntity obj) => obj.DelValue(FirePoint);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetFirePoint(this IEntity obj, Transform value) => obj.SetValue(FirePoint, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IAction GetFireAction(this IEntity obj) => obj.GetValue<IAction>(FireAction);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetFireAction(this IEntity obj, out IAction value) => obj.TryGetValue(FireAction, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddFireAction(this IEntity obj, IAction value) => obj.AddValue(FireAction, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasFireAction(this IEntity obj) => obj.HasValue(FireAction);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelFireAction(this IEntity obj) => obj.DelValue(FireAction);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetFireAction(this IEntity obj, IAction value) => obj.SetValue(FireAction, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IEvent GetFireEvent(this IEntity obj) => obj.GetValue<IEvent>(FireEvent);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetFireEvent(this IEntity obj, out IEvent value) => obj.TryGetValue(FireEvent, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddFireEvent(this IEntity obj, IEvent value) => obj.AddValue(FireEvent, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasFireEvent(this IEntity obj) => obj.HasValue(FireEvent);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelFireEvent(this IEntity obj) => obj.DelValue(FireEvent);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetFireEvent(this IEntity obj, IEvent value) => obj.SetValue(FireEvent, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static TriggerEventReceiver GetTrigger(this IEntity obj) => obj.GetValue<TriggerEventReceiver>(Trigger);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetTrigger(this IEntity obj, out TriggerEventReceiver value) => obj.TryGetValue(Trigger, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddTrigger(this IEntity obj, TriggerEventReceiver value) => obj.AddValue(Trigger, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasTrigger(this IEntity obj) => obj.HasValue(Trigger);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelTrigger(this IEntity obj) => obj.DelValue(Trigger);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetTrigger(this IEntity obj, TriggerEventReceiver value) => obj.SetValue(Trigger, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static Renderer GetMeshRenderer(this IEntity obj) => obj.GetValue<Renderer>(MeshRenderer);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetMeshRenderer(this IEntity obj, out Renderer value) => obj.TryGetValue(MeshRenderer, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddMeshRenderer(this IEntity obj, Renderer value) => obj.AddValue(MeshRenderer, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasMeshRenderer(this IEntity obj) => obj.HasValue(MeshRenderer);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelMeshRenderer(this IEntity obj) => obj.DelValue(MeshRenderer);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetMeshRenderer(this IEntity obj, Renderer value) => obj.SetValue(MeshRenderer, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static Animator GetAnimator(this IEntity obj) => obj.GetValue<Animator>(Animator);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetAnimator(this IEntity obj, out Animator value) => obj.TryGetValue(Animator, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddAnimator(this IEntity obj, Animator value) => obj.AddValue(Animator, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasAnimator(this IEntity obj) => obj.HasValue(Animator);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelAnimator(this IEntity obj) => obj.DelValue(Animator);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetAnimator(this IEntity obj, Animator value) => obj.SetValue(Animator, value);
//     }
// }
