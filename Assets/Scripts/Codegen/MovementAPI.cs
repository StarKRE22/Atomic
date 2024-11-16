// /**
// * Code generation. Don't modify! 
// **/
//
// using UnityEngine;
// using Atomic.Entities;
// using System.Runtime.CompilerServices;
// using UnityEngine;
// using Atomic.Elements;
//
// namespace SampleGame
// {
// 	public static class MovementAPI
// 	{
// 		///Tags
// 		public const int Moveable = 448011500;
//
//
// 		///Values
// 		public const int Position = -1084586333; // float3Reactive
// 		public const int MovementSpeed = -124615418; // Const<float>
// 		public const int MovementDirection = 1140346022; // float3Reactive
//
//
// 		///Tag Extensions
//
// 		public static bool HasMoveableTag(this IEntity obj) => obj.HasTag(Moveable);
// 		public static bool NotMoveableTag(this IEntity obj) => !obj.HasTag(Moveable);
// 		public static bool AddMoveableTag(this IEntity obj) => obj.AddTag(Moveable);
// 		public static bool DelMoveableTag(this IEntity obj) => obj.DelTag(Moveable);
//
//
// 		///Value Extensions
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static float3Reactive GetPosition(this IEntity obj) => obj.GetValue<float3Reactive>(Position);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetPosition(this IEntity obj, out float3Reactive value) => obj.TryGetValue(Position, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddPosition(this IEntity obj, float3Reactive value) => obj.AddValue(Position, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasPosition(this IEntity obj) => obj.HasValue(Position);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelPosition(this IEntity obj) => obj.DelValue(Position);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetPosition(this IEntity obj, float3Reactive value) => obj.SetValue(Position, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static Const<float> GetMovementSpeed(this IEntity obj) => obj.GetValue<Const<float>>(MovementSpeed);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetMovementSpeed(this IEntity obj, out Const<float> value) => obj.TryGetValue(MovementSpeed, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddMovementSpeed(this IEntity obj, Const<float> value) => obj.AddValue(MovementSpeed, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasMovementSpeed(this IEntity obj) => obj.HasValue(MovementSpeed);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelMovementSpeed(this IEntity obj) => obj.DelValue(MovementSpeed);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetMovementSpeed(this IEntity obj, Const<float> value) => obj.SetValue(MovementSpeed, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static float3Reactive GetMovementDirection(this IEntity obj) => obj.GetValue<float3Reactive>(MovementDirection);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool TryGetMovementDirection(this IEntity obj, out float3Reactive value) => obj.TryGetValue(MovementDirection, out value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool AddMovementDirection(this IEntity obj, float3Reactive value) => obj.AddValue(MovementDirection, value);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool HasMovementDirection(this IEntity obj) => obj.HasValue(MovementDirection);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static bool DelMovementDirection(this IEntity obj) => obj.DelValue(MovementDirection);
//
// 		[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 		public static void SetMovementDirection(this IEntity obj, float3Reactive value) => obj.SetValue(MovementDirection, value);
//     }
// }
