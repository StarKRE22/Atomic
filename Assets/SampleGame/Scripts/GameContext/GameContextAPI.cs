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
		public const int GameCountdown = -1631187506; // ICooldown
		public const int GameOverEvent = 520413635; // IEvent
		public const int WinnerTeam = -928171475; // IReactiveVariable<TeamType>
		public const int CoinPool = -1313944194; // IEntityPool<IGameEntity>
		public const int CoinSpawnArea = 748018310; // Bounds


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
		public static ICooldown GetGameCountdown(this IGameContext obj) => obj.GetValue<ICooldown>(GameCountdown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetGameCountdown(this IGameContext obj, out ICooldown value) => obj.TryGetValue(GameCountdown, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddGameCountdown(this IGameContext obj, ICooldown value) => obj.AddValue(GameCountdown, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasGameCountdown(this IGameContext obj) => obj.HasValue(GameCountdown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelGameCountdown(this IGameContext obj) => obj.DelValue(GameCountdown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetGameCountdown(this IGameContext obj, ICooldown value) => obj.SetValue(GameCountdown, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEvent GetGameOverEvent(this IGameContext obj) => obj.GetValue<IEvent>(GameOverEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetGameOverEvent(this IGameContext obj, out IEvent value) => obj.TryGetValue(GameOverEvent, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddGameOverEvent(this IGameContext obj, IEvent value) => obj.AddValue(GameOverEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasGameOverEvent(this IGameContext obj) => obj.HasValue(GameOverEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelGameOverEvent(this IGameContext obj) => obj.DelValue(GameOverEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetGameOverEvent(this IGameContext obj, IEvent value) => obj.SetValue(GameOverEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<TeamType> GetWinnerTeam(this IGameContext obj) => obj.GetValue<IReactiveVariable<TeamType>>(WinnerTeam);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetWinnerTeam(this IGameContext obj, out IReactiveVariable<TeamType> value) => obj.TryGetValue(WinnerTeam, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddWinnerTeam(this IGameContext obj, IReactiveVariable<TeamType> value) => obj.AddValue(WinnerTeam, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasWinnerTeam(this IGameContext obj) => obj.HasValue(WinnerTeam);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelWinnerTeam(this IGameContext obj) => obj.DelValue(WinnerTeam);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetWinnerTeam(this IGameContext obj, IReactiveVariable<TeamType> value) => obj.SetValue(WinnerTeam, value);

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
    }
}
