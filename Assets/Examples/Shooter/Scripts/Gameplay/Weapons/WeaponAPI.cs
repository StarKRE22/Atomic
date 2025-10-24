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

namespace ShooterGame.Gameplay
{
#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	public static class WeaponAPI
	{
		///Values
		public static readonly int FireAction; // IAction
		public static readonly int FireEvent; // IEvent
		public static readonly int Ammo; // IReactiveVariable<int>
		public static readonly int Cooldown; // Cooldown

		static WeaponAPI()
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
		public static IAction GetFireAction(this IWeapon entity) => entity.GetValue<IAction>(FireAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFireAction(this IWeapon entity, out IAction value) => entity.TryGetValue(FireAction, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFireAction(this IWeapon entity, IAction value) => entity.AddValue(FireAction, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFireAction(this IWeapon entity) => entity.HasValue(FireAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFireAction(this IWeapon entity) => entity.DelValue(FireAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFireAction(this IWeapon entity, IAction value) => entity.SetValue(FireAction, value);

		#endregion

		#region FireEvent

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEvent GetFireEvent(this IWeapon entity) => entity.GetValue<IEvent>(FireEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFireEvent(this IWeapon entity, out IEvent value) => entity.TryGetValue(FireEvent, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFireEvent(this IWeapon entity, IEvent value) => entity.AddValue(FireEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFireEvent(this IWeapon entity) => entity.HasValue(FireEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFireEvent(this IWeapon entity) => entity.DelValue(FireEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFireEvent(this IWeapon entity, IEvent value) => entity.SetValue(FireEvent, value);

		#endregion

		#region Ammo

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<int> GetAmmo(this IWeapon entity) => entity.GetValue<IReactiveVariable<int>>(Ammo);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetAmmo(this IWeapon entity, out IReactiveVariable<int> value) => entity.TryGetValue(Ammo, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddAmmo(this IWeapon entity, IReactiveVariable<int> value) => entity.AddValue(Ammo, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasAmmo(this IWeapon entity) => entity.HasValue(Ammo);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelAmmo(this IWeapon entity) => entity.DelValue(Ammo);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetAmmo(this IWeapon entity, IReactiveVariable<int> value) => entity.SetValue(Ammo, value);

		#endregion

		#region Cooldown

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Cooldown GetCooldown(this IWeapon entity) => entity.GetValue<Cooldown>(Cooldown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCooldown(this IWeapon entity, out Cooldown value) => entity.TryGetValue(Cooldown, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddCooldown(this IWeapon entity, Cooldown value) => entity.AddValue(Cooldown, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCooldown(this IWeapon entity) => entity.HasValue(Cooldown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCooldown(this IWeapon entity) => entity.DelValue(Cooldown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCooldown(this IWeapon entity, Cooldown value) => entity.SetValue(Cooldown, value);

		#endregion
    }
}
