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

namespace ShooterGame.Gameplay
{
#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	public static class GameContextAPI
	{
		///Values
		public static readonly int Players; // IDictionary<TeamType, IPlayerContext>
		public static readonly int GameTime; // IReactiveVariable<float>
		public static readonly int TeamCatalog; // TeamCatalog
		public static readonly int BulletPool; // IEntityPool<IGameEntity>
		public static readonly int WorldTransform; // Transform
		public static readonly int Leaderboard; // IReactiveDictionary<TeamType, int>
		public static readonly int KillEvent; // IEvent<KillArgs>
		public static readonly int RespawnDelay; // IValue<float>
		public static readonly int GameOverEvent; // IEvent
		public static readonly int AllSpawnPoints; // Transform[]
		public static readonly int FreeSpawnPoints; // List<Transform>

		static GameContextAPI()
		{
			//Values
			Players = NameToId(nameof(Players));
			GameTime = NameToId(nameof(GameTime));
			TeamCatalog = NameToId(nameof(TeamCatalog));
			BulletPool = NameToId(nameof(BulletPool));
			WorldTransform = NameToId(nameof(WorldTransform));
			Leaderboard = NameToId(nameof(Leaderboard));
			KillEvent = NameToId(nameof(KillEvent));
			RespawnDelay = NameToId(nameof(RespawnDelay));
			GameOverEvent = NameToId(nameof(GameOverEvent));
			AllSpawnPoints = NameToId(nameof(AllSpawnPoints));
			FreeSpawnPoints = NameToId(nameof(FreeSpawnPoints));
		}


		///Value Extensions

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

		#region GameTime

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<float> GetGameTime(this IGameContext entity) => entity.GetValue<IReactiveVariable<float>>(GameTime);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetGameTime(this IGameContext entity, out IReactiveVariable<float> value) => entity.TryGetValue(GameTime, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddGameTime(this IGameContext entity, IReactiveVariable<float> value) => entity.AddValue(GameTime, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasGameTime(this IGameContext entity) => entity.HasValue(GameTime);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelGameTime(this IGameContext entity) => entity.DelValue(GameTime);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetGameTime(this IGameContext entity, IReactiveVariable<float> value) => entity.SetValue(GameTime, value);

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

		#region BulletPool

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEntityPool<IGameEntity> GetBulletPool(this IGameContext entity) => entity.GetValue<IEntityPool<IGameEntity>>(BulletPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetBulletPool(this IGameContext entity, out IEntityPool<IGameEntity> value) => entity.TryGetValue(BulletPool, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddBulletPool(this IGameContext entity, IEntityPool<IGameEntity> value) => entity.AddValue(BulletPool, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasBulletPool(this IGameContext entity) => entity.HasValue(BulletPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelBulletPool(this IGameContext entity) => entity.DelValue(BulletPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetBulletPool(this IGameContext entity, IEntityPool<IGameEntity> value) => entity.SetValue(BulletPool, value);

		#endregion

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

		#region Leaderboard

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveDictionary<TeamType, int> GetLeaderboard(this IGameContext entity) => entity.GetValue<IReactiveDictionary<TeamType, int>>(Leaderboard);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetLeaderboard(this IGameContext entity, out IReactiveDictionary<TeamType, int> value) => entity.TryGetValue(Leaderboard, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddLeaderboard(this IGameContext entity, IReactiveDictionary<TeamType, int> value) => entity.AddValue(Leaderboard, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasLeaderboard(this IGameContext entity) => entity.HasValue(Leaderboard);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelLeaderboard(this IGameContext entity) => entity.DelValue(Leaderboard);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetLeaderboard(this IGameContext entity, IReactiveDictionary<TeamType, int> value) => entity.SetValue(Leaderboard, value);

		#endregion

		#region KillEvent

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEvent<KillArgs> GetKillEvent(this IGameContext entity) => entity.GetValue<IEvent<KillArgs>>(KillEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetKillEvent(this IGameContext entity, out IEvent<KillArgs> value) => entity.TryGetValue(KillEvent, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddKillEvent(this IGameContext entity, IEvent<KillArgs> value) => entity.AddValue(KillEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasKillEvent(this IGameContext entity) => entity.HasValue(KillEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelKillEvent(this IGameContext entity) => entity.DelValue(KillEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetKillEvent(this IGameContext entity, IEvent<KillArgs> value) => entity.SetValue(KillEvent, value);

		#endregion

		#region RespawnDelay

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetRespawnDelay(this IGameContext entity) => entity.GetValue<IValue<float>>(RespawnDelay);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRespawnDelay(this IGameContext entity, out IValue<float> value) => entity.TryGetValue(RespawnDelay, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRespawnDelay(this IGameContext entity, IValue<float> value) => entity.AddValue(RespawnDelay, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRespawnDelay(this IGameContext entity) => entity.HasValue(RespawnDelay);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRespawnDelay(this IGameContext entity) => entity.DelValue(RespawnDelay);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRespawnDelay(this IGameContext entity, IValue<float> value) => entity.SetValue(RespawnDelay, value);

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

		#region AllSpawnPoints

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Transform[] GetAllSpawnPoints(this IGameContext entity) => entity.GetValue<Transform[]>(AllSpawnPoints);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetAllSpawnPoints(this IGameContext entity, out Transform[] value) => entity.TryGetValue(AllSpawnPoints, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddAllSpawnPoints(this IGameContext entity, Transform[] value) => entity.AddValue(AllSpawnPoints, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasAllSpawnPoints(this IGameContext entity) => entity.HasValue(AllSpawnPoints);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelAllSpawnPoints(this IGameContext entity) => entity.DelValue(AllSpawnPoints);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetAllSpawnPoints(this IGameContext entity, Transform[] value) => entity.SetValue(AllSpawnPoints, value);

		#endregion

		#region FreeSpawnPoints

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static List<Transform> GetFreeSpawnPoints(this IGameContext entity) => entity.GetValue<List<Transform>>(FreeSpawnPoints);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFreeSpawnPoints(this IGameContext entity, out List<Transform> value) => entity.TryGetValue(FreeSpawnPoints, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFreeSpawnPoints(this IGameContext entity, List<Transform> value) => entity.AddValue(FreeSpawnPoints, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFreeSpawnPoints(this IGameContext entity) => entity.HasValue(FreeSpawnPoints);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFreeSpawnPoints(this IGameContext entity) => entity.DelValue(FreeSpawnPoints);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFreeSpawnPoints(this IGameContext entity, List<Transform> value) => entity.SetValue(FreeSpawnPoints, value);

		#endregion
    }
}
