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
		public static readonly int Character; // IEntity
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
		public static InputMap GetInputMap(this PlayerContext entity) => entity.GetValue<InputMap>(InputMap);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetInputMap(this PlayerContext entity, out InputMap value) => entity.TryGetValue(InputMap, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddInputMap(this PlayerContext entity, InputMap value) => entity.AddValue(InputMap, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasInputMap(this PlayerContext entity) => entity.HasValue(InputMap);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelInputMap(this PlayerContext entity) => entity.DelValue(InputMap);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetInputMap(this PlayerContext entity, InputMap value) => entity.SetValue(InputMap, value);

		#endregion

		#region Character

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEntity GetCharacter(this PlayerContext entity) => entity.GetValue<IEntity>(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCharacter(this PlayerContext entity, out IEntity value) => entity.TryGetValue(Character, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddCharacter(this PlayerContext entity, IEntity value) => entity.AddValue(Character, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCharacter(this PlayerContext entity) => entity.HasValue(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCharacter(this PlayerContext entity) => entity.DelValue(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCharacter(this PlayerContext entity, IEntity value) => entity.SetValue(Character, value);

		#endregion

		#region Money

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<int> GetMoney(this PlayerContext entity) => entity.GetValue<IReactiveVariable<int>>(Money);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoney(this PlayerContext entity, out IReactiveVariable<int> value) => entity.TryGetValue(Money, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoney(this PlayerContext entity, IReactiveVariable<int> value) => entity.AddValue(Money, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoney(this PlayerContext entity) => entity.HasValue(Money);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoney(this PlayerContext entity) => entity.DelValue(Money);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoney(this PlayerContext entity, IReactiveVariable<int> value) => entity.SetValue(Money, value);

		#endregion

		#region TeamType

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<TeamType> GetTeamType(this PlayerContext entity) => entity.GetValue<IValue<TeamType>>(TeamType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTeamType(this PlayerContext entity, out IValue<TeamType> value) => entity.TryGetValue(TeamType, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTeamType(this PlayerContext entity, IValue<TeamType> value) => entity.AddValue(TeamType, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTeamType(this PlayerContext entity) => entity.HasValue(TeamType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTeamType(this PlayerContext entity) => entity.DelValue(TeamType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTeamType(this PlayerContext entity, IValue<TeamType> value) => entity.SetValue(TeamType, value);

		#endregion

		#region Camera

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Camera GetCamera(this PlayerContext entity) => entity.GetValue<Camera>(Camera);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCamera(this PlayerContext entity, out Camera value) => entity.TryGetValue(Camera, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddCamera(this PlayerContext entity, Camera value) => entity.AddValue(Camera, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCamera(this PlayerContext entity) => entity.HasValue(Camera);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCamera(this PlayerContext entity) => entity.DelValue(Camera);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCamera(this PlayerContext entity, Camera value) => entity.SetValue(Camera, value);

		#endregion
    }
}
