/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Contexts;
using System.Runtime.CompilerServices;
using Atomic.Entities;
using Atomic.Elements;
using System.Collections.Generic;
using GameExample.Engine;

namespace Atomic.Contexts
{
	public static class GameContextAPI
	{
		///Keys
		public const int PlayerMap = 5; // Dictionary<TeamType, IContext>
		public const int GameCountdown = 10; // Countdown
		public const int CoinSystemData = 13; // CoinSystemData
		public const int WorldTransform = 7; // Transform


		///Extensions
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Dictionary<TeamType, IContext> GetPlayerMap(this IContext obj) => obj.ResolveValue<Dictionary<TeamType, IContext>>(PlayerMap);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetPlayerMap(this IContext obj, out Dictionary<TeamType, IContext> value) => obj.TryResolveValue(PlayerMap, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddPlayerMap(this IContext obj, Dictionary<TeamType, IContext> value) => obj.AddValue(PlayerMap, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelPlayerMap(this IContext obj) => obj.DelValue(PlayerMap);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetPlayerMap(this IContext obj, Dictionary<TeamType, IContext> value) => obj.SetValue(PlayerMap, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasPlayerMap(this IContext obj) => obj.HasValue(PlayerMap);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Countdown GetGameCountdown(this IContext obj) => obj.ResolveValue<Countdown>(GameCountdown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetGameCountdown(this IContext obj, out Countdown value) => obj.TryResolveValue(GameCountdown, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddGameCountdown(this IContext obj, Countdown value) => obj.AddValue(GameCountdown, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelGameCountdown(this IContext obj) => obj.DelValue(GameCountdown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetGameCountdown(this IContext obj, Countdown value) => obj.SetValue(GameCountdown, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasGameCountdown(this IContext obj) => obj.HasValue(GameCountdown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static CoinSystemData GetCoinSystemData(this IContext obj) => obj.ResolveValue<CoinSystemData>(CoinSystemData);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCoinSystemData(this IContext obj, out CoinSystemData value) => obj.TryResolveValue(CoinSystemData, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddCoinSystemData(this IContext obj, CoinSystemData value) => obj.AddValue(CoinSystemData, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCoinSystemData(this IContext obj) => obj.DelValue(CoinSystemData);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCoinSystemData(this IContext obj, CoinSystemData value) => obj.SetValue(CoinSystemData, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCoinSystemData(this IContext obj) => obj.HasValue(CoinSystemData);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Transform GetWorldTransform(this IContext obj) => obj.ResolveValue<Transform>(WorldTransform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetWorldTransform(this IContext obj, out Transform value) => obj.TryResolveValue(WorldTransform, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddWorldTransform(this IContext obj, Transform value) => obj.AddValue(WorldTransform, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelWorldTransform(this IContext obj) => obj.DelValue(WorldTransform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetWorldTransform(this IContext obj, Transform value) => obj.SetValue(WorldTransform, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasWorldTransform(this IContext obj) => obj.HasValue(WorldTransform);
    }
}
