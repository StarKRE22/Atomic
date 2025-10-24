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
using System;

namespace RTSGame
{
#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	public static class PlayerContextAPI
	{
		///Values
		public static readonly int FreeEnemyFilter; // EntityFilter<IUnitEntity>
		public static readonly int Team; // IValue<TeamType>

		static PlayerContextAPI()
		{
			//Values
			FreeEnemyFilter = NameToId(nameof(FreeEnemyFilter));
			Team = NameToId(nameof(Team));
		}


		///Value Extensions

		#region FreeEnemyFilter

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static EntityFilter<IUnitEntity> GetFreeEnemyFilter(this IPlayerContext entity) => entity.GetValueUnsafe<EntityFilter<IUnitEntity>>(FreeEnemyFilter);

		public static ref EntityFilter<IUnitEntity> RefFreeEnemyFilter(this IPlayerContext entity) => ref entity.GetValueUnsafe<EntityFilter<IUnitEntity>>(FreeEnemyFilter);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFreeEnemyFilter(this IPlayerContext entity, out EntityFilter<IUnitEntity> value) => entity.TryGetValueUnsafe(FreeEnemyFilter, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddFreeEnemyFilter(this IPlayerContext entity, EntityFilter<IUnitEntity> value) => entity.AddValue(FreeEnemyFilter, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFreeEnemyFilter(this IPlayerContext entity) => entity.HasValue(FreeEnemyFilter);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFreeEnemyFilter(this IPlayerContext entity) => entity.DelValue(FreeEnemyFilter);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFreeEnemyFilter(this IPlayerContext entity, EntityFilter<IUnitEntity> value) => entity.SetValue(FreeEnemyFilter, value);

		#endregion

		#region Team

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<TeamType> GetTeam(this IPlayerContext entity) => entity.GetValueUnsafe<IValue<TeamType>>(Team);

		public static ref IValue<TeamType> RefTeam(this IPlayerContext entity) => ref entity.GetValueUnsafe<IValue<TeamType>>(Team);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTeam(this IPlayerContext entity, out IValue<TeamType> value) => entity.TryGetValueUnsafe(Team, out value);

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
