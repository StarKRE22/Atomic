/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Entities;
using System.Runtime.CompilerServices;
using UnityEngine;
using Atomic.Elements;

namespace SampleGame
{
	public static class MovementAPI
	{
		///Tags
		public const int Moveable = 448011500;


		///Values
		public const int Position = -1084586333; // bool
		public const int MovementSpeed = -124615418; 
		public const int MovementDirection = 1140346022; // float


		///Tag Extensions

		public static bool HasMoveableTag(this IEntity obj) => obj.HasTag(Moveable);
		public static bool NotMoveableTag(this IEntity obj) => !obj.HasTag(Moveable);
		public static bool AddMoveableTag(this IEntity obj) => obj.AddTag(Moveable);
		public static bool DelMoveableTag(this IEntity obj) => obj.DelTag(Moveable);


		///Value Extensions

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool GetPosition(this IEntity obj) => obj.GetValue<bool>(Position);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetPosition(this IEntity obj, out bool value) => obj.TryGetValue(Position, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddPosition(this IEntity obj, bool value) => obj.AddValue(Position, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasPosition(this IEntity obj) => obj.HasValue(Position);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelPosition(this IEntity obj) => obj.DelValue(Position);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetPosition(this IEntity obj, bool value) => obj.SetValue(Position, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static object GetMovementSpeed(this IEntity obj) => obj.GetValue(MovementSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMovementSpeed(this IEntity obj, out object value) => obj.TryGetValue(MovementSpeed, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddMovementSpeed(this IEntity obj, object value) => obj.AddValue(MovementSpeed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMovementSpeed(this IEntity obj) => obj.DelValue(MovementSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMovementSpeed(this IEntity obj, object value) => obj.SetValue(MovementSpeed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMovementSpeed(this IEntity obj) => obj.HasValue(MovementSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float GetMovementDirection(this IEntity obj) => obj.GetValue<float>(MovementDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMovementDirection(this IEntity obj, out float value) => obj.TryGetValue(MovementDirection, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddMovementDirection(this IEntity obj, float value) => obj.AddValue(MovementDirection, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMovementDirection(this IEntity obj) => obj.HasValue(MovementDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMovementDirection(this IEntity obj) => obj.DelValue(MovementDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMovementDirection(this IEntity obj, float value) => obj.SetValue(MovementDirection, value);
    }
}
