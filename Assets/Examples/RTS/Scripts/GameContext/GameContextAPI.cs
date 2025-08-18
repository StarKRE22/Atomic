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
using Atomic.Entities;
using Atomic.Elements;
using System.Collections.Generic;

namespace RTSGame
{
#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	public static class GameContextAPI
	{
		///Values
		public static readonly int EntityWorld; // IGameEntityWorld
		public static readonly int EntityPool; // IMultiEntityPool<string, IGameEntity>
		public static readonly int TeamViewConfig; // TeamViewConfig

		static GameContextAPI()
		{
			//Values
			EntityWorld = NameToId(nameof(EntityWorld));
			EntityPool = NameToId(nameof(EntityPool));
			TeamViewConfig = NameToId(nameof(TeamViewConfig));
		}


		///Value Extensions

		#region EntityWorld

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IGameEntityWorld GetEntityWorld(this IGameContext entity) => entity.GetValue<IGameEntityWorld>(EntityWorld);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetEntityWorld(this IGameContext entity, out IGameEntityWorld value) => entity.TryGetValue(EntityWorld, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddEntityWorld(this IGameContext entity, IGameEntityWorld value) => entity.AddValue(EntityWorld, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasEntityWorld(this IGameContext entity) => entity.HasValue(EntityWorld);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelEntityWorld(this IGameContext entity) => entity.DelValue(EntityWorld);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetEntityWorld(this IGameContext entity, IGameEntityWorld value) => entity.SetValue(EntityWorld, value);

		#endregion

		#region EntityPool

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IMultiEntityPool<string, IGameEntity> GetEntityPool(this IGameContext entity) => entity.GetValue<IMultiEntityPool<string, IGameEntity>>(EntityPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetEntityPool(this IGameContext entity, out IMultiEntityPool<string, IGameEntity> value) => entity.TryGetValue(EntityPool, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddEntityPool(this IGameContext entity, IMultiEntityPool<string, IGameEntity> value) => entity.AddValue(EntityPool, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasEntityPool(this IGameContext entity) => entity.HasValue(EntityPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelEntityPool(this IGameContext entity) => entity.DelValue(EntityPool);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetEntityPool(this IGameContext entity, IMultiEntityPool<string, IGameEntity> value) => entity.SetValue(EntityPool, value);

		#endregion

		#region TeamViewConfig

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TeamViewConfig GetTeamViewConfig(this IGameContext entity) => entity.GetValue<TeamViewConfig>(TeamViewConfig);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTeamViewConfig(this IGameContext entity, out TeamViewConfig value) => entity.TryGetValue(TeamViewConfig, out value);

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
