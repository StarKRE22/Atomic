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
	public static class AppContextAPI
	{
		///Values
		public static readonly int CurrentLevel; // IReactiveVariable<int>
		public static readonly int ExitKeyCode; // IValue<KeyCode>

		static AppContextAPI()
		{
			//Values
			CurrentLevel = NameToId(nameof(CurrentLevel));
			ExitKeyCode = NameToId(nameof(ExitKeyCode));
		}


		///Value Extensions

		#region CurrentLevel

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<int> GetCurrentLevel(this IAppContext entity) => entity.GetValue<IReactiveVariable<int>>(CurrentLevel);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCurrentLevel(this IAppContext entity, out IReactiveVariable<int> value) => entity.TryGetValue(CurrentLevel, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddCurrentLevel(this IAppContext entity, IReactiveVariable<int> value) => entity.AddValue(CurrentLevel, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCurrentLevel(this IAppContext entity) => entity.HasValue(CurrentLevel);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCurrentLevel(this IAppContext entity) => entity.DelValue(CurrentLevel);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCurrentLevel(this IAppContext entity, IReactiveVariable<int> value) => entity.SetValue(CurrentLevel, value);

		#endregion

		#region ExitKeyCode

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<KeyCode> GetExitKeyCode(this IAppContext entity) => entity.GetValue<IValue<KeyCode>>(ExitKeyCode);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetExitKeyCode(this IAppContext entity, out IValue<KeyCode> value) => entity.TryGetValue(ExitKeyCode, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddExitKeyCode(this IAppContext entity, IValue<KeyCode> value) => entity.AddValue(ExitKeyCode, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasExitKeyCode(this IAppContext entity) => entity.HasValue(ExitKeyCode);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelExitKeyCode(this IAppContext entity) => entity.DelValue(ExitKeyCode);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetExitKeyCode(this IAppContext entity, IValue<KeyCode> value) => entity.SetValue(ExitKeyCode, value);

		#endregion
    }
}
