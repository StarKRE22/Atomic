/**
* Code generation. Don't modify! 
**/

using Atomic.Entities;
using static Atomic.Entities.EntityKeyStore;
using System.Runtime.CompilerServices;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections.Generic;
using Modules.SpatialStructures;

namespace RTSGame
{
#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	public static class GameContextAPI
	{
		///Values
		public static readonly int EntityWorld; // EntityWorld<IGameEntity>
		public static readonly int EntityPool; // IMultiEntityPool<GameEntityType, IGameEntity>
		public static readonly int Players; // Dictionary<TeamType, IPlayerContext>
		public static readonly int TeamViewConfig; // TeamViewConfig
		public static readonly int PlayerPoint; // Transform
		public static readonly int EntitySpace; // SpatialGrid2D<IGameEntity>

		static GameContextAPI()
		{
			//Values
			EntityWorld = NameToId(nameof(EntityWorld));
			EntityPool = NameToId(nameof(EntityPool));
			Players = NameToId(nameof(Players));
			TeamViewConfig = NameToId(nameof(TeamViewConfig));
			PlayerPoint = NameToId(nameof(PlayerPoint));
			EntitySpace = NameToId(nameof(EntitySpace));
		}


		///Value Extensions

		#region EntityWorld

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static EntityWorld<IGameEntity> GetEntityWorld(this IGameContext entity) => entity.GetValueUnsafe<EntityWorld<IGameEntity>>(EntityWorld);

		public static ref EntityWorld<IGameEntity> RefEntityWorld(this IGameContext entity) => ref entity.GetValueUnsafe<EntityWorld<IGameEntity>>(EntityWorld);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetEntityWorld(this IGameContext entity, out EntityWorld<IGameEntity> value) => entity.TryGetValueUnsafe(EntityWorld, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddEntityWorld(this IGameContext entity, EntityWorld<IGameEntity> value) => entity.AddValue(EntityWorld, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasEntityWorld(this IGameContext entity) => entity.HasValue(EntityWorld);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelEntityWorld(this IGameContext entity) => entity.DelValue(EntityWorld);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetEntityWorld(this IGameContext entity, EntityWorld<IGameEntity> value) => entity.SetValue(EntityWorld, value);

		#endregion

		#region EntityPool

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IMultiEntityPool<GameEntityType, IGameEntity> GetEntityPool(this IGameContext entity) => entity.GetValueUnsafe<IMultiEntityPool<GameEntityType, IGameEntity>>(EntityPool);

		public static ref IMultiEntityPool<GameEntityType, IGameEntity> RefEntityPool(this IGameContext entity) => ref entity.GetValueUnsafe<IMultiEntityPool<GameEntityType, IGameEntity>>(EntityPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetEntityPool(this IGameContext entity, out IMultiEntityPool<GameEntityType, IGameEntity> value) => entity.TryGetValueUnsafe(EntityPool, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddEntityPool(this IGameContext entity, IMultiEntityPool<GameEntityType, IGameEntity> value) => entity.AddValue(EntityPool, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasEntityPool(this IGameContext entity) => entity.HasValue(EntityPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelEntityPool(this IGameContext entity) => entity.DelValue(EntityPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetEntityPool(this IGameContext entity, IMultiEntityPool<GameEntityType, IGameEntity> value) => entity.SetValue(EntityPool, value);

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

		#region PlayerPoint

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Transform GetPlayerPoint(this IGameContext entity) => entity.GetValueUnsafe<Transform>(PlayerPoint);

		public static ref Transform RefPlayerPoint(this IGameContext entity) => ref entity.GetValueUnsafe<Transform>(PlayerPoint);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetPlayerPoint(this IGameContext entity, out Transform value) => entity.TryGetValueUnsafe(PlayerPoint, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddPlayerPoint(this IGameContext entity, Transform value) => entity.AddValue(PlayerPoint, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasPlayerPoint(this IGameContext entity) => entity.HasValue(PlayerPoint);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelPlayerPoint(this IGameContext entity) => entity.DelValue(PlayerPoint);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetPlayerPoint(this IGameContext entity, Transform value) => entity.SetValue(PlayerPoint, value);

		#endregion

		#region EntitySpace

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SpatialGrid2D<IGameEntity> GetEntitySpace(this IGameContext entity) => entity.GetValueUnsafe<SpatialGrid2D<IGameEntity>>(EntitySpace);

		public static ref SpatialGrid2D<IGameEntity> RefEntitySpace(this IGameContext entity) => ref entity.GetValueUnsafe<SpatialGrid2D<IGameEntity>>(EntitySpace);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetEntitySpace(this IGameContext entity, out SpatialGrid2D<IGameEntity> value) => entity.TryGetValueUnsafe(EntitySpace, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddEntitySpace(this IGameContext entity, SpatialGrid2D<IGameEntity> value) => entity.AddValue(EntitySpace, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasEntitySpace(this IGameContext entity) => entity.HasValue(EntitySpace);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelEntitySpace(this IGameContext entity) => entity.DelValue(EntitySpace);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetEntitySpace(this IGameContext entity, SpatialGrid2D<IGameEntity> value) => entity.SetValue(EntitySpace, value);

		#endregion
    }
}
