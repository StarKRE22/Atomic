// /**
// * Code generation. Don't modify! 
// **/
//
// using Atomic.Contexts;
// using System.Runtime.CompilerServices;
// using UnityEngine;
// using Atomic.Entities;
// using Atomic.Elements;
// using System.Collections.Generic;
//
// namespace ShooterGame.Gameplay
// {
// 	public static class GameContextAPI
// 	{
//
//
// 		///Values
// 		public const int Players = -369919430; // IDictionary<TeamType, IPlayerContext>
// 		public const int GameTime = 1625382674; // IReactiveVariable<float>
// 		public const int TeamConfig = -783971258; // TeamConfig
// 		public const int BulletPool = 1915726678; // IEntityPool
// 		public const int WorldTransform = -486031409; // Transform
// 		public const int Leaderboard = -1632445052; // IReactiveDictionary<TeamType, int>
// 		public const int KillEvent = 2047281642; // IEvent<KillArgs>
// 		public const int RespawnTime = -354209826; // IValue<float>
// 		public const int AllSpawnPoints = 81331131; // Transform[]
// 		public const int FreeSpawnPoints = 658212610; // List<Transform>
//
//
// 		///Value Extensions
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IDictionary<TeamType, IPlayerContext> GetPlayers(this IGameContext obj) => obj.GetValue<IDictionary<TeamType, IPlayerContext>>(Players);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetPlayers(this IGameContext obj, out IDictionary<TeamType, IPlayerContext> value) => obj.TryGetValue(Players, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddPlayers(this IGameContext obj, IDictionary<TeamType, IPlayerContext> value) => obj.AddValue(Players, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasPlayers(this IGameContext obj) => obj.HasValue(Players);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelPlayers(this IGameContext obj) => obj.DelValue(Players);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetPlayers(this IGameContext obj, IDictionary<TeamType, IPlayerContext> value) => obj.SetValue(Players, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IReactiveVariable<float> GetGameTime(this IGameContext obj) => obj.GetValue<IReactiveVariable<float>>(GameTime);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetGameTime(this IGameContext obj, out IReactiveVariable<float> value) => obj.TryGetValue(GameTime, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddGameTime(this IGameContext obj, IReactiveVariable<float> value) => obj.AddValue(GameTime, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasGameTime(this IGameContext obj) => obj.HasValue(GameTime);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelGameTime(this IGameContext obj) => obj.DelValue(GameTime);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetGameTime(this IGameContext obj, IReactiveVariable<float> value) => obj.SetValue(GameTime, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static TeamConfig GetTeamConfig(this IGameContext obj) => obj.GetValue<TeamConfig>(TeamConfig);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetTeamConfig(this IGameContext obj, out TeamConfig value) => obj.TryGetValue(TeamConfig, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddTeamConfig(this IGameContext obj, TeamConfig value) => obj.AddValue(TeamConfig, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasTeamConfig(this IGameContext obj) => obj.HasValue(TeamConfig);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelTeamConfig(this IGameContext obj) => obj.DelValue(TeamConfig);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetTeamConfig(this IGameContext obj, TeamConfig value) => obj.SetValue(TeamConfig, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IEntityPool GetBulletPool(this IGameContext obj) => obj.GetValue<IEntityPool>(BulletPool);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetBulletPool(this IGameContext obj, out IEntityPool value) => obj.TryGetValue(BulletPool, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddBulletPool(this IGameContext obj, IEntityPool value) => obj.AddValue(BulletPool, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasBulletPool(this IGameContext obj) => obj.HasValue(BulletPool);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelBulletPool(this IGameContext obj) => obj.DelValue(BulletPool);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetBulletPool(this IGameContext obj, IEntityPool value) => obj.SetValue(BulletPool, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static Transform GetWorldTransform(this IGameContext obj) => obj.GetValue<Transform>(WorldTransform);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetWorldTransform(this IGameContext obj, out Transform value) => obj.TryGetValue(WorldTransform, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddWorldTransform(this IGameContext obj, Transform value) => obj.AddValue(WorldTransform, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasWorldTransform(this IGameContext obj) => obj.HasValue(WorldTransform);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelWorldTransform(this IGameContext obj) => obj.DelValue(WorldTransform);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetWorldTransform(this IGameContext obj, Transform value) => obj.SetValue(WorldTransform, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IReactiveDictionary<TeamType, int> GetLeaderboard(this IGameContext obj) => obj.GetValue<IReactiveDictionary<TeamType, int>>(Leaderboard);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetLeaderboard(this IGameContext obj, out IReactiveDictionary<TeamType, int> value) => obj.TryGetValue(Leaderboard, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddLeaderboard(this IGameContext obj, IReactiveDictionary<TeamType, int> value) => obj.AddValue(Leaderboard, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasLeaderboard(this IGameContext obj) => obj.HasValue(Leaderboard);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelLeaderboard(this IGameContext obj) => obj.DelValue(Leaderboard);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetLeaderboard(this IGameContext obj, IReactiveDictionary<TeamType, int> value) => obj.SetValue(Leaderboard, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IEvent<KillArgs> GetKillEvent(this IGameContext obj) => obj.GetValue<IEvent<KillArgs>>(KillEvent);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetKillEvent(this IGameContext obj, out IEvent<KillArgs> value) => obj.TryGetValue(KillEvent, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddKillEvent(this IGameContext obj, IEvent<KillArgs> value) => obj.AddValue(KillEvent, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasKillEvent(this IGameContext obj) => obj.HasValue(KillEvent);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelKillEvent(this IGameContext obj) => obj.DelValue(KillEvent);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetKillEvent(this IGameContext obj, IEvent<KillArgs> value) => obj.SetValue(KillEvent, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IValue<float> GetRespawnTime(this IGameContext obj) => obj.GetValue<IValue<float>>(RespawnTime);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetRespawnTime(this IGameContext obj, out IValue<float> value) => obj.TryGetValue(RespawnTime, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddRespawnTime(this IGameContext obj, IValue<float> value) => obj.AddValue(RespawnTime, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasRespawnTime(this IGameContext obj) => obj.HasValue(RespawnTime);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelRespawnTime(this IGameContext obj) => obj.DelValue(RespawnTime);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetRespawnTime(this IGameContext obj, IValue<float> value) => obj.SetValue(RespawnTime, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static Transform[] GetAllSpawnPoints(this IGameContext obj) => obj.GetValue<Transform[]>(AllSpawnPoints);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetAllSpawnPoints(this IGameContext obj, out Transform[] value) => obj.TryGetValue(AllSpawnPoints, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddAllSpawnPoints(this IGameContext obj, Transform[] value) => obj.AddValue(AllSpawnPoints, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasAllSpawnPoints(this IGameContext obj) => obj.HasValue(AllSpawnPoints);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelAllSpawnPoints(this IGameContext obj) => obj.DelValue(AllSpawnPoints);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetAllSpawnPoints(this IGameContext obj, Transform[] value) => obj.SetValue(AllSpawnPoints, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static List<Transform> GetFreeSpawnPoints(this IGameContext obj) => obj.GetValue<List<Transform>>(FreeSpawnPoints);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetFreeSpawnPoints(this IGameContext obj, out List<Transform> value) => obj.TryGetValue(FreeSpawnPoints, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddFreeSpawnPoints(this IGameContext obj, List<Transform> value) => obj.AddValue(FreeSpawnPoints, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasFreeSpawnPoints(this IGameContext obj) => obj.HasValue(FreeSpawnPoints);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelFreeSpawnPoints(this IGameContext obj) => obj.DelValue(FreeSpawnPoints);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetFreeSpawnPoints(this IGameContext obj, List<Transform> value) => obj.SetValue(FreeSpawnPoints, value);
//     }
// }
