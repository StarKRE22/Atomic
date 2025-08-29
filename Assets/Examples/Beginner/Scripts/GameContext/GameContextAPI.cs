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
using System.Collections.Generic;
using Atomic.Entities;
using BeginnerGame;
using Atomic.Elements;

namespace BeginnerGame
{
#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	public static class GameContextAPI
	{
		///Values
		public static readonly int WorldTransform; // Transform
		public static readonly int Players; // IDictionary<TeamType, IPlayerContext>
		public static readonly int GameCountdown; // ICooldown
		public static readonly int GameOverEvent; // IEvent
		public static readonly int WinnerTeam; // IReactiveVariable<TeamType>
		public static readonly int TeamCatalog; // TeamCatalog
		public static readonly int CoinPool; // IEntityPool<IGameEntity>
		public static readonly int CoinSpawnArea; // Bounds

		static GameContextAPI()
		{
			//Values
			WorldTransform = NameToId(nameof(WorldTransform));
			Players = NameToId(nameof(Players));
			GameCountdown = NameToId(nameof(GameCountdown));
			GameOverEvent = NameToId(nameof(GameOverEvent));
			WinnerTeam = NameToId(nameof(WinnerTeam));
			TeamCatalog = NameToId(nameof(TeamCatalog));
			CoinPool = NameToId(nameof(CoinPool));
			CoinSpawnArea = NameToId(nameof(CoinSpawnArea));
		}


		///Value Extensions

		#region WorldTransform

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Transform GetWorldTransform(this IGameContext entity) => entity.GetValue<Transform>(WorldTransform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetWorldTransform(this IGameContext entity, out Transform value) => entity.TryGetValue(WorldTransform, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddWorldTransform(this IGameContext entity, Transform value) => entity.AddValue(WorldTransform, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasWorldTransform(this IGameContext entity) => entity.HasValue(WorldTransform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelWorldTransform(this IGameContext entity) => entity.DelValue(WorldTransform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetWorldTransform(this IGameContext entity, Transform value) => entity.SetValue(WorldTransform, value);

		#endregion

		#region Players

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IDictionary<TeamType, IPlayerContext> GetPlayers(this IGameContext entity) => entity.GetValue<IDictionary<TeamType, IPlayerContext>>(Players);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetPlayers(this IGameContext entity, out IDictionary<TeamType, IPlayerContext> value) => entity.TryGetValue(Players, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddPlayers(this IGameContext entity, IDictionary<TeamType, IPlayerContext> value) => entity.AddValue(Players, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasPlayers(this IGameContext entity) => entity.HasValue(Players);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelPlayers(this IGameContext entity) => entity.DelValue(Players);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetPlayers(this IGameContext entity, IDictionary<TeamType, IPlayerContext> value) => entity.SetValue(Players, value);

		#endregion

		#region GameCountdown

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ICooldown GetGameCountdown(this IGameContext entity) => entity.GetValue<ICooldown>(GameCountdown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetGameCountdown(this IGameContext entity, out ICooldown value) => entity.TryGetValue(GameCountdown, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddGameCountdown(this IGameContext entity, ICooldown value) => entity.AddValue(GameCountdown, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasGameCountdown(this IGameContext entity) => entity.HasValue(GameCountdown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelGameCountdown(this IGameContext entity) => entity.DelValue(GameCountdown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetGameCountdown(this IGameContext entity, ICooldown value) => entity.SetValue(GameCountdown, value);

		#endregion

		#region GameOverEvent

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEvent GetGameOverEvent(this IGameContext entity) => entity.GetValue<IEvent>(GameOverEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetGameOverEvent(this IGameContext entity, out IEvent value) => entity.TryGetValue(GameOverEvent, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddGameOverEvent(this IGameContext entity, IEvent value) => entity.AddValue(GameOverEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasGameOverEvent(this IGameContext entity) => entity.HasValue(GameOverEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelGameOverEvent(this IGameContext entity) => entity.DelValue(GameOverEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetGameOverEvent(this IGameContext entity, IEvent value) => entity.SetValue(GameOverEvent, value);

		#endregion

		#region WinnerTeam

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<TeamType> GetWinnerTeam(this IGameContext entity) => entity.GetValue<IReactiveVariable<TeamType>>(WinnerTeam);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetWinnerTeam(this IGameContext entity, out IReactiveVariable<TeamType> value) => entity.TryGetValue(WinnerTeam, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddWinnerTeam(this IGameContext entity, IReactiveVariable<TeamType> value) => entity.AddValue(WinnerTeam, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasWinnerTeam(this IGameContext entity) => entity.HasValue(WinnerTeam);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelWinnerTeam(this IGameContext entity) => entity.DelValue(WinnerTeam);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetWinnerTeam(this IGameContext entity, IReactiveVariable<TeamType> value) => entity.SetValue(WinnerTeam, value);

		#endregion

		#region TeamCatalog

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TeamCatalog GetTeamCatalog(this IGameContext entity) => entity.GetValue<TeamCatalog>(TeamCatalog);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTeamCatalog(this IGameContext entity, out TeamCatalog value) => entity.TryGetValue(TeamCatalog, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTeamCatalog(this IGameContext entity, TeamCatalog value) => entity.AddValue(TeamCatalog, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTeamCatalog(this IGameContext entity) => entity.HasValue(TeamCatalog);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTeamCatalog(this IGameContext entity) => entity.DelValue(TeamCatalog);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTeamCatalog(this IGameContext entity, TeamCatalog value) => entity.SetValue(TeamCatalog, value);

		#endregion

		#region CoinPool

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEntityPool<IGameEntity> GetCoinPool(this IGameContext entity) => entity.GetValue<IEntityPool<IGameEntity>>(CoinPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCoinPool(this IGameContext entity, out IEntityPool<IGameEntity> value) => entity.TryGetValue(CoinPool, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddCoinPool(this IGameContext entity, IEntityPool<IGameEntity> value) => entity.AddValue(CoinPool, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCoinPool(this IGameContext entity) => entity.HasValue(CoinPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCoinPool(this IGameContext entity) => entity.DelValue(CoinPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCoinPool(this IGameContext entity, IEntityPool<IGameEntity> value) => entity.SetValue(CoinPool, value);

		#endregion

		#region CoinSpawnArea

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Bounds GetCoinSpawnArea(this IGameContext entity) => entity.GetValue<Bounds>(CoinSpawnArea);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCoinSpawnArea(this IGameContext entity, out Bounds value) => entity.TryGetValue(CoinSpawnArea, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddCoinSpawnArea(this IGameContext entity, Bounds value) => entity.AddValue(CoinSpawnArea, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCoinSpawnArea(this IGameContext entity) => entity.HasValue(CoinSpawnArea);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCoinSpawnArea(this IGameContext entity) => entity.DelValue(CoinSpawnArea);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCoinSpawnArea(this IGameContext entity, Bounds value) => entity.SetValue(CoinSpawnArea, value);

		#endregion
    }
}
