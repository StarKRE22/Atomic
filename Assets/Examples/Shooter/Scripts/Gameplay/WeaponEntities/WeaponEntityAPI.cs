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
	public static class WeaponEntityAPI
	{
		///Values
		public static readonly int FireAction; // IAction
		public static readonly int FireEvent; // IEvent
		public static readonly int Ammo; // IReactiveVariable<int>
		public static readonly int Cooldown; // Cooldown

		static WeaponEntityAPI()
		{
			//Values
			FireAction = NameToId(nameof(FireAction));
			FireEvent = NameToId(nameof(FireEvent));
			Ammo = NameToId(nameof(Ammo));
			Cooldown = NameToId(nameof(Cooldown));
		}


		///Value Extensions

		#region FireAction

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IAction GetFireAction(this IWeaponEntity entity) => entity.GetValue<IAction>(FireAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFireAction(this IWeaponEntity entity, out IAction value) => entity.TryGetValue(FireAction, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFireAction(this IWeaponEntity entity, IAction value) => entity.AddValue(FireAction, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFireAction(this IWeaponEntity entity) => entity.HasValue(FireAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFireAction(this IWeaponEntity entity) => entity.DelValue(FireAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFireAction(this IWeaponEntity entity, IAction value) => entity.SetValue(FireAction, value);

		#endregion

		#region FireEvent

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEvent GetFireEvent(this IWeaponEntity entity) => entity.GetValue<IEvent>(FireEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFireEvent(this IWeaponEntity entity, out IEvent value) => entity.TryGetValue(FireEvent, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFireEvent(this IWeaponEntity entity, IEvent value) => entity.AddValue(FireEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFireEvent(this IWeaponEntity entity) => entity.HasValue(FireEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFireEvent(this IWeaponEntity entity) => entity.DelValue(FireEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFireEvent(this IWeaponEntity entity, IEvent value) => entity.SetValue(FireEvent, value);

		#endregion

		#region Ammo

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<int> GetAmmo(this IWeaponEntity entity) => entity.GetValue<IReactiveVariable<int>>(Ammo);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetAmmo(this IWeaponEntity entity, out IReactiveVariable<int> value) => entity.TryGetValue(Ammo, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddAmmo(this IWeaponEntity entity, IReactiveVariable<int> value) => entity.AddValue(Ammo, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasAmmo(this IWeaponEntity entity) => entity.HasValue(Ammo);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelAmmo(this IWeaponEntity entity) => entity.DelValue(Ammo);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetAmmo(this IWeaponEntity entity, IReactiveVariable<int> value) => entity.SetValue(Ammo, value);

		#endregion

		#region Cooldown

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Cooldown GetCooldown(this IWeaponEntity entity) => entity.GetValue<Cooldown>(Cooldown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCooldown(this IWeaponEntity entity, out Cooldown value) => entity.TryGetValue(Cooldown, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddCooldown(this IWeaponEntity entity, Cooldown value) => entity.AddValue(Cooldown, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCooldown(this IWeaponEntity entity) => entity.HasValue(Cooldown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCooldown(this IWeaponEntity entity) => entity.DelValue(Cooldown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCooldown(this IWeaponEntity entity, Cooldown value) => entity.SetValue(Cooldown, value);

		#endregion
    }
}
