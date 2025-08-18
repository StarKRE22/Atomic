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
// 	public static class PlayerContextAPI
// 	{
//
//
// 		///Values
// 		public const int SelectedUnits = -1886818885; // IReactiveSet<IEntity>
// 		public const int UnitsFilter = 1638952418; // IEntityFilter
// 		public const int EnemiesFilter = 399427208; // IEntityFilter
//
//
// 		///Value Extensions
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IReactiveSet<IEntity> GetSelectedUnits(this PlayerContext obj) => obj.GetValue<IReactiveSet<IEntity>>(SelectedUnits);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetSelectedUnits(this PlayerContext obj, out IReactiveSet<IEntity> value) => obj.TryGetValue(SelectedUnits, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddSelectedUnits(this PlayerContext obj, IReactiveSet<IEntity> value) => obj.AddValue(SelectedUnits, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasSelectedUnits(this PlayerContext obj) => obj.HasValue(SelectedUnits);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelSelectedUnits(this PlayerContext obj) => obj.DelValue(SelectedUnits);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetSelectedUnits(this PlayerContext obj, IReactiveSet<IEntity> value) => obj.SetValue(SelectedUnits, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IEntityFilter GetUnitsFilter(this PlayerContext obj) => obj.GetValue<IEntityFilter>(UnitsFilter);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetUnitsFilter(this PlayerContext obj, out IEntityFilter value) => obj.TryGetValue(UnitsFilter, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddUnitsFilter(this PlayerContext obj, IEntityFilter value) => obj.AddValue(UnitsFilter, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasUnitsFilter(this PlayerContext obj) => obj.HasValue(UnitsFilter);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelUnitsFilter(this PlayerContext obj) => obj.DelValue(UnitsFilter);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetUnitsFilter(this PlayerContext obj, IEntityFilter value) => obj.SetValue(UnitsFilter, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IEntityFilter GetEnemiesFilter(this PlayerContext obj) => obj.GetValue<IEntityFilter>(EnemiesFilter);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetEnemiesFilter(this PlayerContext obj, out IEntityFilter value) => obj.TryGetValue(EnemiesFilter, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddEnemiesFilter(this PlayerContext obj, IEntityFilter value) => obj.AddValue(EnemiesFilter, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasEnemiesFilter(this PlayerContext obj) => obj.HasValue(EnemiesFilter);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelEnemiesFilter(this PlayerContext obj) => obj.DelValue(EnemiesFilter);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetEnemiesFilter(this PlayerContext obj, IEntityFilter value) => obj.SetValue(EnemiesFilter, value);
//     }
// }
