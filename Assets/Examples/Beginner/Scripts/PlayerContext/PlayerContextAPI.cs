/**
* Code generation. Don't modify! 
**/

using Atomic.Entities;
using static Atomic.Entities.EntityNames;
using System.Runtime.CompilerServices;
#if UNITY_EDITOR
using UnityEditor;
#endif
using BeginnerGame;
using Atomic.Elements;
using UnityEngine;

namespace BeginnerGame
{
#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	public static class PlayerContextAPI
	{
		///Values
		public static readonly int InputMap; // InputMap
		public static readonly int Character; // IGameEntity
		public static readonly int Money; // IReactiveVariable<int>
		public static readonly int TeamType; // IValue<TeamType>
		public static readonly int Camera; // Camera

		static PlayerContextAPI()
		{
			//Values
			InputMap = NameToId(nameof(InputMap));
			Character = NameToId(nameof(Character));
			Money = NameToId(nameof(Money));
			TeamType = NameToId(nameof(TeamType));
			Camera = NameToId(nameof(Camera));
		}


		///Value Extensions

		#region InputMap

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static InputMap GetInputMap(this IPlayerContext entity) => entity.GetValue<InputMap>(InputMap);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetInputMap(this IPlayerContext entity, out InputMap value) => entity.TryGetValue(InputMap, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddInputMap(this IPlayerContext entity, InputMap value) => entity.AddValue(InputMap, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasInputMap(this IPlayerContext entity) => entity.HasValue(InputMap);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelInputMap(this IPlayerContext entity) => entity.DelValue(InputMap);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetInputMap(this IPlayerContext entity, InputMap value) => entity.SetValue(InputMap, value);

		#endregion

		#region Character

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IGameEntity GetCharacter(this IPlayerContext entity) => entity.GetValue<IGameEntity>(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCharacter(this IPlayerContext entity, out IGameEntity value) => entity.TryGetValue(Character, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddCharacter(this IPlayerContext entity, IGameEntity value) => entity.AddValue(Character, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCharacter(this IPlayerContext entity) => entity.HasValue(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCharacter(this IPlayerContext entity) => entity.DelValue(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCharacter(this IPlayerContext entity, IGameEntity value) => entity.SetValue(Character, value);

		#endregion

		#region Money

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<int> GetMoney(this IPlayerContext entity) => entity.GetValue<IReactiveVariable<int>>(Money);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoney(this IPlayerContext entity, out IReactiveVariable<int> value) => entity.TryGetValue(Money, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoney(this IPlayerContext entity, IReactiveVariable<int> value) => entity.AddValue(Money, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoney(this IPlayerContext entity) => entity.HasValue(Money);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoney(this IPlayerContext entity) => entity.DelValue(Money);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoney(this IPlayerContext entity, IReactiveVariable<int> value) => entity.SetValue(Money, value);

		#endregion

		#region TeamType

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<TeamType> GetTeamType(this IPlayerContext entity) => entity.GetValue<IValue<TeamType>>(TeamType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTeamType(this IPlayerContext entity, out IValue<TeamType> value) => entity.TryGetValue(TeamType, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTeamType(this IPlayerContext entity, IValue<TeamType> value) => entity.AddValue(TeamType, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTeamType(this IPlayerContext entity) => entity.HasValue(TeamType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTeamType(this IPlayerContext entity) => entity.DelValue(TeamType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTeamType(this IPlayerContext entity, IValue<TeamType> value) => entity.SetValue(TeamType, value);

		#endregion

		#region Camera

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Camera GetCamera(this IPlayerContext entity) => entity.GetValue<Camera>(Camera);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCamera(this IPlayerContext entity, out Camera value) => entity.TryGetValue(Camera, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddCamera(this IPlayerContext entity, Camera value) => entity.AddValue(Camera, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCamera(this IPlayerContext entity) => entity.HasValue(Camera);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCamera(this IPlayerContext entity) => entity.DelValue(Camera);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCamera(this IPlayerContext entity, Camera value) => entity.SetValue(Camera, value);

		#endregion
    }
}
