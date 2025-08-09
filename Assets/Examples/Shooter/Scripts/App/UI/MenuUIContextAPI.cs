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

namespace ShooterGame.App
{
#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	public static class MenuUIContextAPI
	{
		///Values
		public static readonly int MainMenuView; // MainMenuView

		static MenuUIContextAPI()
		{
			//Values
			MainMenuView = NameToId(nameof(MainMenuView));
		}


		///Value Extensions

		#region MainMenuView

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static MainMenuView GetMainMenuView(this IMenuUIContext entity) => entity.GetValue<MainMenuView>(MainMenuView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMainMenuView(this IMenuUIContext entity, out MainMenuView value) => entity.TryGetValue(MainMenuView, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMainMenuView(this IMenuUIContext entity, MainMenuView value) => entity.AddValue(MainMenuView, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMainMenuView(this IMenuUIContext entity) => entity.HasValue(MainMenuView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMainMenuView(this IMenuUIContext entity) => entity.DelValue(MainMenuView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMainMenuView(this IMenuUIContext entity, MainMenuView value) => entity.SetValue(MainMenuView, value);

		#endregion
    }
}
