/**
* Code generation. Don't modify! 
**/

using System.Runtime.CompilerServices;
using SampleGame;
using Atomic.Elements;
using UnityEditor;
using static Atomic.Entities.EntityNames;

namespace SampleGame
{
	[InitializeOnLoad]
	public static class PlayerContextAPI
	{
		///Values
		public static readonly int InputMap; // InputMap
		public static readonly int Character; // IGameEntity
		public static readonly int Money; // IReactiveVariable<int>
		public static readonly int TeamType; // IValue<TeamType>

		static PlayerContextAPI()
		{
			//Values
			Character = NameToId(nameof(Character));
			Money = NameToId(nameof(Money));
			TeamType = NameToId(nameof(TeamType));
			InputMap = NameToId(nameof(InputMap));
		}

		///Value Extensions

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static InputMap GetInputMap(this IPlayerContext obj) => obj.GetValue<InputMap>(InputMap);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetInputMap(this IPlayerContext obj, out InputMap value) => obj.TryGetValue(InputMap, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddInputMap(this IPlayerContext obj, InputMap value) => obj.AddValue(InputMap, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasInputMap(this IPlayerContext obj) => obj.HasValue(InputMap);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelInputMap(this IPlayerContext obj) => obj.DelValue(InputMap);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetInputMap(this IPlayerContext obj, InputMap value) => obj.SetValue(InputMap, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IGameEntity GetCharacter(this IPlayerContext obj) => obj.GetValue<IGameEntity>(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCharacter(this IPlayerContext obj, out IGameEntity value) => obj.TryGetValue(Character, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddCharacter(this IPlayerContext obj, IGameEntity value) => obj.AddValue(Character, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCharacter(this IPlayerContext obj) => obj.HasValue(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCharacter(this IPlayerContext obj) => obj.DelValue(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCharacter(this IPlayerContext obj, IGameEntity value) => obj.SetValue(Character, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<int> GetMoney(this IPlayerContext obj) => obj.GetValue<IReactiveVariable<int>>(Money);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoney(this IPlayerContext obj, out IReactiveVariable<int> value) => obj.TryGetValue(Money, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoney(this IPlayerContext obj, IReactiveVariable<int> value) => obj.AddValue(Money, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoney(this IPlayerContext obj) => obj.HasValue(Money);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoney(this IPlayerContext obj) => obj.DelValue(Money);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoney(this IPlayerContext obj, IReactiveVariable<int> value) => obj.SetValue(Money, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<TeamType> GetTeamType(this IPlayerContext obj) => obj.GetValue<IValue<TeamType>>(TeamType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTeamType(this IPlayerContext obj, out IValue<TeamType> value) => obj.TryGetValue(TeamType, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTeamType(this IPlayerContext obj, IValue<TeamType> value) => obj.AddValue(TeamType, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTeamType(this IPlayerContext obj) => obj.HasValue(TeamType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTeamType(this IPlayerContext obj) => obj.DelValue(TeamType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTeamType(this IPlayerContext obj, IValue<TeamType> value) => obj.SetValue(TeamType, value);
    }
}
