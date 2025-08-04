/**
* Code generation. Don't modify! 
**/

using System.Runtime.CompilerServices;
using UnityEngine;
using System.Collections.Generic;
using Atomic.Entities;
using SampleGame;
using Atomic.Elements;

namespace SampleGame
{
	public static class GameContextAPI
	{
		///Values
		public const int WorldTransform = -2059850133; // Transform
		public const int Players = 734090337; // IDictionary<TeamType,IPlayerContext>
		public const int CoinPool = -1313944194; // IEntityPool<IGameEntity>
		public const int CoinSpawnArea = 748018310; // Bounds
		public const int CoinSpawnPeriod = 2003887482; // Cooldown


		///Value Extensions

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Transform GetWorldTransform(this IGameContext obj) => obj.GetValue<Transform>(WorldTransform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetWorldTransform(this IGameContext obj, out Transform value) => obj.TryGetValue(WorldTransform, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddWorldTransform(this IGameContext obj, Transform value) => obj.AddValue(WorldTransform, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasWorldTransform(this IGameContext obj) => obj.HasValue(WorldTransform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelWorldTransform(this IGameContext obj) => obj.DelValue(WorldTransform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetWorldTransform(this IGameContext obj, Transform value) => obj.SetValue(WorldTransform, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IDictionary<TeamType,IPlayerContext> GetPlayers(this IGameContext obj) => obj.GetValue<IDictionary<TeamType,IPlayerContext>>(Players);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetPlayers(this IGameContext obj, out IDictionary<TeamType,IPlayerContext> value) => obj.TryGetValue(Players, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddPlayers(this IGameContext obj, IDictionary<TeamType,IPlayerContext> value) => obj.AddValue(Players, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasPlayers(this IGameContext obj) => obj.HasValue(Players);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelPlayers(this IGameContext obj) => obj.DelValue(Players);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetPlayers(this IGameContext obj, IDictionary<TeamType,IPlayerContext> value) => obj.SetValue(Players, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEntityPool<IGameEntity> GetCoinPool(this IGameContext obj) => obj.GetValue<IEntityPool<IGameEntity>>(CoinPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCoinPool(this IGameContext obj, out IEntityPool<IGameEntity> value) => obj.TryGetValue(CoinPool, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddCoinPool(this IGameContext obj, IEntityPool<IGameEntity> value) => obj.AddValue(CoinPool, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCoinPool(this IGameContext obj) => obj.HasValue(CoinPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCoinPool(this IGameContext obj) => obj.DelValue(CoinPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCoinPool(this IGameContext obj, IEntityPool<IGameEntity> value) => obj.SetValue(CoinPool, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Bounds GetCoinSpawnArea(this IGameContext obj) => obj.GetValue<Bounds>(CoinSpawnArea);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCoinSpawnArea(this IGameContext obj, out Bounds value) => obj.TryGetValue(CoinSpawnArea, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddCoinSpawnArea(this IGameContext obj, Bounds value) => obj.AddValue(CoinSpawnArea, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCoinSpawnArea(this IGameContext obj) => obj.HasValue(CoinSpawnArea);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCoinSpawnArea(this IGameContext obj) => obj.DelValue(CoinSpawnArea);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCoinSpawnArea(this IGameContext obj, Bounds value) => obj.SetValue(CoinSpawnArea, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Cooldown GetCoinSpawnPeriod(this IGameContext obj) => obj.GetValue<Cooldown>(CoinSpawnPeriod);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCoinSpawnPeriod(this IGameContext obj, out Cooldown value) => obj.TryGetValue(CoinSpawnPeriod, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddCoinSpawnPeriod(this IGameContext obj, Cooldown value) => obj.AddValue(CoinSpawnPeriod, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCoinSpawnPeriod(this IGameContext obj) => obj.HasValue(CoinSpawnPeriod);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCoinSpawnPeriod(this IGameContext obj) => obj.DelValue(CoinSpawnPeriod);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCoinSpawnPeriod(this IGameContext obj, Cooldown value) => obj.SetValue(CoinSpawnPeriod, value);
    }
}
