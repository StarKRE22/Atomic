/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Contexts;
using System.Runtime.CompilerServices;
using Atomic.Entities;
using Atomic.Elements;
using GameExample.Engine;
using System.Collections.Generic;

namespace Atomic.Contexts
{
	public static class PlayerContextAPI
	{
		///Keys
		public const int MoveableUnitsFilter = 3; // IEntityFilter
		public const int InputMap = 4; // InputMap
		public const int Character = 2; // Const<IEntity>
		public const int Money = 6; // ReactiveVariable<int>
		public const int CameraData = 8; // CameraData
		public const int TeamType = 9; // Const<TeamType>


		///Extensions
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEntityFilter GetMoveableUnitsFilter(this IContext obj) => obj.ResolveValue<IEntityFilter>(MoveableUnitsFilter);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoveableUnitsFilter(this IContext obj, out IEntityFilter value) => obj.TryResolveValue(MoveableUnitsFilter, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddMoveableUnitsFilter(this IContext obj, IEntityFilter value) => obj.AddValue(MoveableUnitsFilter, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoveableUnitsFilter(this IContext obj) => obj.DelValue(MoveableUnitsFilter);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoveableUnitsFilter(this IContext obj, IEntityFilter value) => obj.SetValue(MoveableUnitsFilter, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoveableUnitsFilter(this IContext obj) => obj.HasValue(MoveableUnitsFilter);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static InputMap GetInputMap(this IContext obj) => obj.ResolveValue<InputMap>(InputMap);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetInputMap(this IContext obj, out InputMap value) => obj.TryResolveValue(InputMap, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddInputMap(this IContext obj, InputMap value) => obj.AddValue(InputMap, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelInputMap(this IContext obj) => obj.DelValue(InputMap);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetInputMap(this IContext obj, InputMap value) => obj.SetValue(InputMap, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasInputMap(this IContext obj) => obj.HasValue(InputMap);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Const<IEntity> GetCharacter(this IContext obj) => obj.ResolveValue<Const<IEntity>>(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCharacter(this IContext obj, out Const<IEntity> value) => obj.TryResolveValue(Character, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddCharacter(this IContext obj, Const<IEntity> value) => obj.AddValue(Character, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCharacter(this IContext obj) => obj.DelValue(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCharacter(this IContext obj, Const<IEntity> value) => obj.SetValue(Character, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCharacter(this IContext obj) => obj.HasValue(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReactiveVariable<int> GetMoney(this IContext obj) => obj.ResolveValue<ReactiveVariable<int>>(Money);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoney(this IContext obj, out ReactiveVariable<int> value) => obj.TryResolveValue(Money, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddMoney(this IContext obj, ReactiveVariable<int> value) => obj.AddValue(Money, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoney(this IContext obj) => obj.DelValue(Money);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoney(this IContext obj, ReactiveVariable<int> value) => obj.SetValue(Money, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoney(this IContext obj) => obj.HasValue(Money);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static CameraData GetCameraData(this IContext obj) => obj.ResolveValue<CameraData>(CameraData);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCameraData(this IContext obj, out CameraData value) => obj.TryResolveValue(CameraData, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddCameraData(this IContext obj, CameraData value) => obj.AddValue(CameraData, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCameraData(this IContext obj) => obj.DelValue(CameraData);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCameraData(this IContext obj, CameraData value) => obj.SetValue(CameraData, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCameraData(this IContext obj) => obj.HasValue(CameraData);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Const<TeamType> GetTeamType(this IContext obj) => obj.ResolveValue<Const<TeamType>>(TeamType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTeamType(this IContext obj, out Const<TeamType> value) => obj.TryResolveValue(TeamType, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddTeamType(this IContext obj, Const<TeamType> value) => obj.AddValue(TeamType, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTeamType(this IContext obj) => obj.DelValue(TeamType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTeamType(this IContext obj, Const<TeamType> value) => obj.SetValue(TeamType, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTeamType(this IContext obj) => obj.HasValue(TeamType);
    }
}
