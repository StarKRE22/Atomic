/**
* Code generation. Don't modify! 
**/

using System.Runtime.CompilerServices;
using SampleGame;
using UnityEditor;
using UnityEngine;
using static Atomic.Entities.EntityNames;

namespace SampleGame
{
	[InitializeOnLoad]
	public static class UIContextAPI
	{
		///Values
		public static readonly int MoneyView; // MoneyView
		public static readonly int GameCountdownView; // GameCountdownView
		public static readonly int PopupTransform; // Transform
		public static readonly int GameOverViewPrefab; // GameOverView
		public static readonly int GameOverView; // GameOverView

		static UIContextAPI()
		{
			//Values
			GameCountdownView = NameToId(nameof(GameCountdownView));
			PopupTransform = NameToId(nameof(PopupTransform));
			GameOverViewPrefab = NameToId(nameof(GameOverViewPrefab));
			GameOverView = NameToId(nameof(GameOverView));
			MoneyView = NameToId(nameof(MoneyView));
		}

		///Value Extensions

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static MoneyView GetMoneyView(this IUIContext obj) => obj.GetValue<MoneyView>(MoneyView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoneyView(this IUIContext obj, out MoneyView value) => obj.TryGetValue(MoneyView, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoneyView(this IUIContext obj, MoneyView value) => obj.AddValue(MoneyView, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoneyView(this IUIContext obj) => obj.HasValue(MoneyView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoneyView(this IUIContext obj) => obj.DelValue(MoneyView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoneyView(this IUIContext obj, MoneyView value) => obj.SetValue(MoneyView, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static CountdownView GetGameCountdownView(this IUIContext obj) => obj.GetValue<CountdownView>(GameCountdownView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetGameCountdownView(this IUIContext obj, out CountdownView value) => obj.TryGetValue(GameCountdownView, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddGameCountdownView(this IUIContext obj, CountdownView value) => obj.AddValue(GameCountdownView, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasGameCountdownView(this IUIContext obj) => obj.HasValue(GameCountdownView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelGameCountdownView(this IUIContext obj) => obj.DelValue(GameCountdownView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetGameCountdownView(this IUIContext obj, CountdownView value) => obj.SetValue(GameCountdownView, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Transform GetPopupTransform(this IUIContext obj) => obj.GetValue<Transform>(PopupTransform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetPopupTransform(this IUIContext obj, out Transform value) => obj.TryGetValue(PopupTransform, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddPopupTransform(this IUIContext obj, Transform value) => obj.AddValue(PopupTransform, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasPopupTransform(this IUIContext obj) => obj.HasValue(PopupTransform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelPopupTransform(this IUIContext obj) => obj.DelValue(PopupTransform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetPopupTransform(this IUIContext obj, Transform value) => obj.SetValue(PopupTransform, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static GameOverView GetGameOverViewPrefab(this IUIContext obj) => obj.GetValue<GameOverView>(GameOverViewPrefab);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetGameOverViewPrefab(this IUIContext obj, out GameOverView value) => obj.TryGetValue(GameOverViewPrefab, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddGameOverViewPrefab(this IUIContext obj, GameOverView value) => obj.AddValue(GameOverViewPrefab, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasGameOverViewPrefab(this IUIContext obj) => obj.HasValue(GameOverViewPrefab);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelGameOverViewPrefab(this IUIContext obj) => obj.DelValue(GameOverViewPrefab);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetGameOverViewPrefab(this IUIContext obj, GameOverView value) => obj.SetValue(GameOverViewPrefab, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static GameOverView GetGameOverView(this IUIContext obj) => obj.GetValue<GameOverView>(GameOverView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetGameOverView(this IUIContext obj, out GameOverView value) => obj.TryGetValue(GameOverView, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddGameOverView(this IUIContext obj, GameOverView value) => obj.AddValue(GameOverView, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasGameOverView(this IUIContext obj) => obj.HasValue(GameOverView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelGameOverView(this IUIContext obj) => obj.DelValue(GameOverView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetGameOverView(this IUIContext obj, GameOverView value) => obj.SetValue(GameOverView, value);
    }
}
