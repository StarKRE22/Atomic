// /**
// * Code generation. Don't modify! 
// **/
//
// using Atomic.Contexts;
// using System.Runtime.CompilerServices;
// using UnityEngine;
// using Atomic.Entities;
// using Atomic.Elements;
//
// namespace ShooterGame.Gameplay
// {
// 	public static class PlayerContextAPI
// 	{
//
//
// 		///Values
// 		public const int Character = 294335127; // IEntity
// 		public const int Team = 1691486497; // IValue<TeamType>
// 		public const int InputMap = 43340267; // InputMap
// 		public const int Camera = 1018227507; // Camera
//
//
// 		///Value Extensions
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IEntity GetCharacter(this IPlayerContext obj) => obj.GetValue<IEntity>(Character);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetCharacter(this IPlayerContext obj, out IEntity value) => obj.TryGetValue(Character, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddCharacter(this IPlayerContext obj, IEntity value) => obj.AddValue(Character, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasCharacter(this IPlayerContext obj) => obj.HasValue(Character);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelCharacter(this IPlayerContext obj) => obj.DelValue(Character);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetCharacter(this IPlayerContext obj, IEntity value) => obj.SetValue(Character, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static IValue<TeamType> GetTeam(this IPlayerContext obj) => obj.GetValue<IValue<TeamType>>(Team);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetTeam(this IPlayerContext obj, out IValue<TeamType> value) => obj.TryGetValue(Team, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddTeam(this IPlayerContext obj, IValue<TeamType> value) => obj.AddValue(Team, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasTeam(this IPlayerContext obj) => obj.HasValue(Team);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelTeam(this IPlayerContext obj) => obj.DelValue(Team);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetTeam(this IPlayerContext obj, IValue<TeamType> value) => obj.SetValue(Team, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static InputMap GetInputMap(this IPlayerContext obj) => obj.GetValue<InputMap>(InputMap);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetInputMap(this IPlayerContext obj, out InputMap value) => obj.TryGetValue(InputMap, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddInputMap(this IPlayerContext obj, InputMap value) => obj.AddValue(InputMap, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasInputMap(this IPlayerContext obj) => obj.HasValue(InputMap);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelInputMap(this IPlayerContext obj) => obj.DelValue(InputMap);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetInputMap(this IPlayerContext obj, InputMap value) => obj.SetValue(InputMap, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static Camera GetCamera(this IPlayerContext obj) => obj.GetValue<Camera>(Camera);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetCamera(this IPlayerContext obj, out Camera value) => obj.TryGetValue(Camera, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddCamera(this IPlayerContext obj, Camera value) => obj.AddValue(Camera, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasCamera(this IPlayerContext obj) => obj.HasValue(Camera);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelCamera(this IPlayerContext obj) => obj.DelValue(Camera);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetCamera(this IPlayerContext obj, Camera value) => obj.SetValue(Camera, value);
//     }
// }
