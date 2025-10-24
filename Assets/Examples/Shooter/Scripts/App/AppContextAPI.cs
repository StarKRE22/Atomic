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
using Cysharp.Threading.Tasks;

namespace ShooterGame.App
{
#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	public static class AppContextAPI
	{
		///Values
		public static readonly int ExitKeyCode; // IValue<KeyCode>
		public static readonly int StartLevel; // IValue<int>
		public static readonly int MaxLevel; // IValue<int>
		public static readonly int CurrentLevel; // IReactiveVariable<int>
		public static readonly int GameLoadingAction; // ILoadingTask

		static AppContextAPI()
		{
			//Values
			ExitKeyCode = NameToId(nameof(ExitKeyCode));
			StartLevel = NameToId(nameof(StartLevel));
			MaxLevel = NameToId(nameof(MaxLevel));
			CurrentLevel = NameToId(nameof(CurrentLevel));
			GameLoadingAction = NameToId(nameof(GameLoadingAction));
		}


		///Value Extensions

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

		#region StartLevel

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<int> GetStartLevel(this IAppContext entity) => entity.GetValue<IValue<int>>(StartLevel);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetStartLevel(this IAppContext entity, out IValue<int> value) => entity.TryGetValue(StartLevel, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddStartLevel(this IAppContext entity, IValue<int> value) => entity.AddValue(StartLevel, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasStartLevel(this IAppContext entity) => entity.HasValue(StartLevel);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelStartLevel(this IAppContext entity) => entity.DelValue(StartLevel);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetStartLevel(this IAppContext entity, IValue<int> value) => entity.SetValue(StartLevel, value);

		#endregion

		#region MaxLevel

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<int> GetMaxLevel(this IAppContext entity) => entity.GetValue<IValue<int>>(MaxLevel);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMaxLevel(this IAppContext entity, out IValue<int> value) => entity.TryGetValue(MaxLevel, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMaxLevel(this IAppContext entity, IValue<int> value) => entity.AddValue(MaxLevel, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMaxLevel(this IAppContext entity) => entity.HasValue(MaxLevel);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMaxLevel(this IAppContext entity) => entity.DelValue(MaxLevel);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMaxLevel(this IAppContext entity, IValue<int> value) => entity.SetValue(MaxLevel, value);

		#endregion

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

		#region GameLoadingAction

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ILoadingTask GetGameLoadingAction(this IAppContext entity) => entity.GetValue<ILoadingTask>(GameLoadingAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetGameLoadingAction(this IAppContext entity, out ILoadingTask value) => entity.TryGetValue(GameLoadingAction, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddGameLoadingAction(this IAppContext entity, ILoadingTask value) => entity.AddValue(GameLoadingAction, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasGameLoadingAction(this IAppContext entity) => entity.HasValue(GameLoadingAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelGameLoadingAction(this IAppContext entity) => entity.DelValue(GameLoadingAction);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetGameLoadingAction(this IAppContext entity, ILoadingTask value) => entity.SetValue(GameLoadingAction, value);

		#endregion
    }
}
