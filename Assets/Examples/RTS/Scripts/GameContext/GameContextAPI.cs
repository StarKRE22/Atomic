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

namespace RTSGame
{
#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	public static class GameContextAPI
	{
		///Values
		public static readonly int EntityWorld; // EntityWorld<IUnitEntity>
		public static readonly int EntityPool; // IMultiEntityPool<string, IUnitEntity>
		public static readonly int Players; // Dictionary<TeamType, IPlayerContext>
		public static readonly int TeamViewConfig; // TeamViewConfig

		static GameContextAPI()
		{
			//Values
			EntityWorld = NameToId(nameof(EntityWorld));
			EntityPool = NameToId(nameof(EntityPool));
			Players = NameToId(nameof(Players));
			TeamViewConfig = NameToId(nameof(TeamViewConfig));
		}


		///Value Extensions

		#region EntityWorld

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static EntityWorld<IUnitEntity> GetEntityWorld(this IGameContext entity) => entity.GetValueUnsafe<EntityWorld<IUnitEntity>>(EntityWorld);

		public static ref EntityWorld<IUnitEntity> RefEntityWorld(this IGameContext entity) => ref entity.GetValueUnsafe<EntityWorld<IUnitEntity>>(EntityWorld);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetEntityWorld(this IGameContext entity, out EntityWorld<IUnitEntity> value) => entity.TryGetValueUnsafe(EntityWorld, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddEntityWorld(this IGameContext entity, EntityWorld<IUnitEntity> value) => entity.AddValue(EntityWorld, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasEntityWorld(this IGameContext entity) => entity.HasValue(EntityWorld);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelEntityWorld(this IGameContext entity) => entity.DelValue(EntityWorld);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetEntityWorld(this IGameContext entity, EntityWorld<IUnitEntity> value) => entity.SetValue(EntityWorld, value);

		#endregion

		#region EntityPool

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IMultiEntityPool<string, IUnitEntity> GetEntityPool(this IGameContext entity) => entity.GetValueUnsafe<IMultiEntityPool<string, IUnitEntity>>(EntityPool);

		public static ref IMultiEntityPool<string, IUnitEntity> RefEntityPool(this IGameContext entity) => ref entity.GetValueUnsafe<IMultiEntityPool<string, IUnitEntity>>(EntityPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetEntityPool(this IGameContext entity, out IMultiEntityPool<string, IUnitEntity> value) => entity.TryGetValueUnsafe(EntityPool, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddEntityPool(this IGameContext entity, IMultiEntityPool<string, IUnitEntity> value) => entity.AddValue(EntityPool, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasEntityPool(this IGameContext entity) => entity.HasValue(EntityPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelEntityPool(this IGameContext entity) => entity.DelValue(EntityPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetEntityPool(this IGameContext entity, IMultiEntityPool<string, IUnitEntity> value) => entity.SetValue(EntityPool, value);

		#endregion

		#region Players

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Dictionary<TeamType, IPlayerContext> GetPlayers(this IGameContext entity) => entity.GetValueUnsafe<Dictionary<TeamType, IPlayerContext>>(Players);

		public static ref Dictionary<TeamType, IPlayerContext> RefPlayers(this IGameContext entity) => ref entity.GetValueUnsafe<Dictionary<TeamType, IPlayerContext>>(Players);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetPlayers(this IGameContext entity, out Dictionary<TeamType, IPlayerContext> value) => entity.TryGetValueUnsafe(Players, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddPlayers(this IGameContext entity, Dictionary<TeamType, IPlayerContext> value) => entity.AddValue(Players, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasPlayers(this IGameContext entity) => entity.HasValue(Players);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelPlayers(this IGameContext entity) => entity.DelValue(Players);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetPlayers(this IGameContext entity, Dictionary<TeamType, IPlayerContext> value) => entity.SetValue(Players, value);

		#endregion

		#region TeamViewConfig

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TeamViewConfig GetTeamViewConfig(this IGameContext entity) => entity.GetValueUnsafe<TeamViewConfig>(TeamViewConfig);

		public static ref TeamViewConfig RefTeamViewConfig(this IGameContext entity) => ref entity.GetValueUnsafe<TeamViewConfig>(TeamViewConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTeamViewConfig(this IGameContext entity, out TeamViewConfig value) => entity.TryGetValueUnsafe(TeamViewConfig, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTeamViewConfig(this IGameContext entity, TeamViewConfig value) => entity.AddValue(TeamViewConfig, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTeamViewConfig(this IGameContext entity) => entity.HasValue(TeamViewConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTeamViewConfig(this IGameContext entity) => entity.DelValue(TeamViewConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTeamViewConfig(this IGameContext entity, TeamViewConfig value) => entity.SetValue(TeamViewConfig, value);

		#endregion
    }
}
