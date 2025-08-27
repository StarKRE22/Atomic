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
using Atomic.Elements;
using System.Collections.Generic;
using System;

namespace ShooterGame.App
{
#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	public static class MenuUIContextAPI
	{
		///Values
		public static readonly int Screens; // IDictionary<Type, (ScreenView, IEntityBehaviour)>
		public static readonly int CurrentScreen; // IReactiveVariable<ScreenView>

		static MenuUIContextAPI()
		{
			//Values
			Screens = NameToId(nameof(Screens));
			CurrentScreen = NameToId(nameof(CurrentScreen));
		}


		///Value Extensions

		#region Screens

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IDictionary<Type, (ScreenView, IEntityBehaviour)> GetScreens(this IMenuUIContext entity) => entity.GetValue<IDictionary<Type, (ScreenView, IEntityBehaviour)>>(Screens);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetScreens(this IMenuUIContext entity, out IDictionary<Type, (ScreenView, IEntityBehaviour)> value) => entity.TryGetValue(Screens, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddScreens(this IMenuUIContext entity, IDictionary<Type, (ScreenView, IEntityBehaviour)> value) => entity.AddValue(Screens, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasScreens(this IMenuUIContext entity) => entity.HasValue(Screens);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelScreens(this IMenuUIContext entity) => entity.DelValue(Screens);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetScreens(this IMenuUIContext entity, IDictionary<Type, (ScreenView, IEntityBehaviour)> value) => entity.SetValue(Screens, value);

		#endregion

		#region CurrentScreen

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<ScreenView> GetCurrentScreen(this IMenuUIContext entity) => entity.GetValue<IReactiveVariable<ScreenView>>(CurrentScreen);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCurrentScreen(this IMenuUIContext entity, out IReactiveVariable<ScreenView> value) => entity.TryGetValue(CurrentScreen, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddCurrentScreen(this IMenuUIContext entity, IReactiveVariable<ScreenView> value) => entity.AddValue(CurrentScreen, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCurrentScreen(this IMenuUIContext entity) => entity.HasValue(CurrentScreen);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCurrentScreen(this IMenuUIContext entity) => entity.DelValue(CurrentScreen);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCurrentScreen(this IMenuUIContext entity, IReactiveVariable<ScreenView> value) => entity.SetValue(CurrentScreen, value);

		#endregion
    }
}
