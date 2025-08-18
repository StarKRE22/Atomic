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
using System;
using Atomic.Elements;

namespace RTSGame
{
#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	public static class PlayerContextAPI
	{
		///Values
		public static readonly int EnemyFilter; // EntityFilter<IGameEntity>
		public static readonly int Team; // IValue<TeamType>

		static PlayerContextAPI()
		{
			//Values
			EnemyFilter = NameToId(nameof(EnemyFilter));
			Team = NameToId(nameof(Team));
		}


		///Value Extensions

		#region EnemyFilter

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static EntityFilter<IGameEntity> GetEnemyFilter(this IPlayerContext entity) => entity.GetValue<EntityFilter<IGameEntity>>(EnemyFilter);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetEnemyFilter(this IPlayerContext entity, out EntityFilter<IGameEntity> value) => entity.TryGetValue(EnemyFilter, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddEnemyFilter(this IPlayerContext entity, EntityFilter<IGameEntity> value) => entity.AddValue(EnemyFilter, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasEnemyFilter(this IPlayerContext entity) => entity.HasValue(EnemyFilter);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelEnemyFilter(this IPlayerContext entity) => entity.DelValue(EnemyFilter);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetEnemyFilter(this IPlayerContext entity, EntityFilter<IGameEntity> value) => entity.SetValue(EnemyFilter, value);

		#endregion

		#region Team

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<TeamType> GetTeam(this IPlayerContext entity) => entity.GetValue<IValue<TeamType>>(Team);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTeam(this IPlayerContext entity, out IValue<TeamType> value) => entity.TryGetValue(Team, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTeam(this IPlayerContext entity, IValue<TeamType> value) => entity.AddValue(Team, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTeam(this IPlayerContext entity) => entity.HasValue(Team);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTeam(this IPlayerContext entity) => entity.DelValue(Team);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTeam(this IPlayerContext entity, IValue<TeamType> value) => entity.SetValue(Team, value);

		#endregion
    }
}
