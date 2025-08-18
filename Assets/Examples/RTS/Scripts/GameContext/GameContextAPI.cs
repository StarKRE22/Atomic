// /**
// * Code generation. Don't modify! 
// **/
//
// using Atomic.Contexts;
// using System.Runtime.CompilerServices;
// using UnityEngine;
// using Atomic.Contexts;
// using Atomic.Entities;
// using Atomic.Elements;
// using System.Collections.Generic;
//
// namespace RTSGame
// {
// 	public static class GameContextAPI
// 	{
//
//
// 		///Values
// 		public const int EntityWorld = 1757640864; // IEntityWorld
// 		public const int EntityPool = 1931115573; // IGenericEntityPool
// 		public const int Players = -369919430; // IDictionary<TeamType, PlayerContext>
// 		public const int TeamViewConfig = 1134404919; // TeamViewConfig
//
//
// 		///Value Extensions
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IEntityWorld GetEntityWorld(this GameContext obj) => obj.GetValue<IEntityWorld>(EntityWorld);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetEntityWorld(this GameContext obj, out IEntityWorld value) => obj.TryGetValue(EntityWorld, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddEntityWorld(this GameContext obj, IEntityWorld value) => obj.AddValue(EntityWorld, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasEntityWorld(this GameContext obj) => obj.HasValue(EntityWorld);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelEntityWorld(this GameContext obj) => obj.DelValue(EntityWorld);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetEntityWorld(this GameContext obj, IEntityWorld value) => obj.SetValue(EntityWorld, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IGenericEntityPool GetEntityPool(this GameContext obj) => obj.GetValue<IGenericEntityPool>(EntityPool);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetEntityPool(this GameContext obj, out IGenericEntityPool value) => obj.TryGetValue(EntityPool, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddEntityPool(this GameContext obj, IGenericEntityPool value) => obj.AddValue(EntityPool, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasEntityPool(this GameContext obj) => obj.HasValue(EntityPool);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelEntityPool(this GameContext obj) => obj.DelValue(EntityPool);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetEntityPool(this GameContext obj, IGenericEntityPool value) => obj.SetValue(EntityPool, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IDictionary<TeamType, PlayerContext> GetPlayers(this GameContext obj) => obj.GetValue<IDictionary<TeamType, PlayerContext>>(Players);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetPlayers(this GameContext obj, out IDictionary<TeamType, PlayerContext> value) => obj.TryGetValue(Players, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddPlayers(this GameContext obj, IDictionary<TeamType, PlayerContext> value) => obj.AddValue(Players, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasPlayers(this GameContext obj) => obj.HasValue(Players);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelPlayers(this GameContext obj) => obj.DelValue(Players);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetPlayers(this GameContext obj, IDictionary<TeamType, PlayerContext> value) => obj.SetValue(Players, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static TeamViewConfig GetTeamViewConfig(this GameContext obj) => obj.GetValue<TeamViewConfig>(TeamViewConfig);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetTeamViewConfig(this GameContext obj, out TeamViewConfig value) => obj.TryGetValue(TeamViewConfig, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddTeamViewConfig(this GameContext obj, TeamViewConfig value) => obj.AddValue(TeamViewConfig, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasTeamViewConfig(this GameContext obj) => obj.HasValue(TeamViewConfig);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelTeamViewConfig(this GameContext obj) => obj.DelValue(TeamViewConfig);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetTeamViewConfig(this GameContext obj, TeamViewConfig value) => obj.SetValue(TeamViewConfig, value);
//     }
// }
