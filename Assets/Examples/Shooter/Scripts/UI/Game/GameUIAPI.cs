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
	public static class GameUIAPI
	{
		///Values
		public static readonly int PopupTransform; // Transform
		public static readonly int GameOverViewPrefab; // GameOverView
		public static readonly int GameOverView; // GameOverView

		static GameUIAPI()
		{
			//Values
			PopupTransform = NameToId(nameof(PopupTransform));
			GameOverViewPrefab = NameToId(nameof(GameOverViewPrefab));
			GameOverView = NameToId(nameof(GameOverView));
		}


		///Value Extensions

		#region PopupTransform

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Transform GetPopupTransform(this IGameUI entity) => entity.GetValue<Transform>(PopupTransform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetPopupTransform(this IGameUI entity, out Transform value) => entity.TryGetValue(PopupTransform, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddPopupTransform(this IGameUI entity, Transform value) => entity.AddValue(PopupTransform, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasPopupTransform(this IGameUI entity) => entity.HasValue(PopupTransform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelPopupTransform(this IGameUI entity) => entity.DelValue(PopupTransform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetPopupTransform(this IGameUI entity, Transform value) => entity.SetValue(PopupTransform, value);

		#endregion

		#region GameOverViewPrefab

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static GameOverView GetGameOverViewPrefab(this IGameUI entity) => entity.GetValue<GameOverView>(GameOverViewPrefab);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetGameOverViewPrefab(this IGameUI entity, out GameOverView value) => entity.TryGetValue(GameOverViewPrefab, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddGameOverViewPrefab(this IGameUI entity, GameOverView value) => entity.AddValue(GameOverViewPrefab, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasGameOverViewPrefab(this IGameUI entity) => entity.HasValue(GameOverViewPrefab);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelGameOverViewPrefab(this IGameUI entity) => entity.DelValue(GameOverViewPrefab);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetGameOverViewPrefab(this IGameUI entity, GameOverView value) => entity.SetValue(GameOverViewPrefab, value);

		#endregion

		#region GameOverView

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static GameOverView GetGameOverView(this IGameUI entity) => entity.GetValue<GameOverView>(GameOverView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetGameOverView(this IGameUI entity, out GameOverView value) => entity.TryGetValue(GameOverView, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddGameOverView(this IGameUI entity, GameOverView value) => entity.AddValue(GameOverView, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasGameOverView(this IGameUI entity) => entity.HasValue(GameOverView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelGameOverView(this IGameUI entity) => entity.DelValue(GameOverView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetGameOverView(this IGameUI entity, GameOverView value) => entity.SetValue(GameOverView, value);

		#endregion
    }
}
