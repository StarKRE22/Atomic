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
using System.Collections.Generic;
using System;

namespace ShooterGame.UI
{
#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	public static class MenuUIAPI
	{
		///Values
		public static readonly int Screens; // IDictionary<Type, (ScreenView, IEntityBehaviour)>
		public static readonly int CurrentScreen; // IReactiveVariable<ScreenView>

		static MenuUIAPI()
		{
			//Values
			Screens = NameToId(nameof(Screens));
			CurrentScreen = NameToId(nameof(CurrentScreen));
		}


		///Value Extensions

		#region Screens

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IDictionary<Type, (ScreenView, IEntityBehaviour)> GetScreens(this IMenuUI entity) => entity.GetValue<IDictionary<Type, (ScreenView, IEntityBehaviour)>>(Screens);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetScreens(this IMenuUI entity, out IDictionary<Type, (ScreenView, IEntityBehaviour)> value) => entity.TryGetValue(Screens, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddScreens(this IMenuUI entity, IDictionary<Type, (ScreenView, IEntityBehaviour)> value) => entity.AddValue(Screens, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasScreens(this IMenuUI entity) => entity.HasValue(Screens);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelScreens(this IMenuUI entity) => entity.DelValue(Screens);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetScreens(this IMenuUI entity, IDictionary<Type, (ScreenView, IEntityBehaviour)> value) => entity.SetValue(Screens, value);

		#endregion

		#region CurrentScreen

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<ScreenView> GetCurrentScreen(this IMenuUI entity) => entity.GetValue<IReactiveVariable<ScreenView>>(CurrentScreen);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCurrentScreen(this IMenuUI entity, out IReactiveVariable<ScreenView> value) => entity.TryGetValue(CurrentScreen, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddCurrentScreen(this IMenuUI entity, IReactiveVariable<ScreenView> value) => entity.AddValue(CurrentScreen, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCurrentScreen(this IMenuUI entity) => entity.HasValue(CurrentScreen);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCurrentScreen(this IMenuUI entity) => entity.DelValue(CurrentScreen);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCurrentScreen(this IMenuUI entity, IReactiveVariable<ScreenView> value) => entity.SetValue(CurrentScreen, value);

		#endregion
    }
}
