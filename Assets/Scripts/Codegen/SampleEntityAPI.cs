/**
* Code generation. Don't modify! 
**/

using Atomic.Entities;
using System.Runtime.CompilerServices;
using UnityEngine;
using Atomic.Elements;

namespace SampleGame
{
	public static class SampleEntityAPI
	{


		///Values
		public const int Health = -915003867; // IValue<int>
		public const int Speed = -823668238; // IValue<float>
		public const int Transform = -180157682; // Transform


		///Value Extensions

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<int> GetHealth(this IEntity obj) => obj.GetValue<IValue<int>>(Health);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetHealth(this IEntity obj, out IValue<int> value) => obj.TryGetValue(Health, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddHealth(this IEntity obj, IValue<int> value) => obj.AddValue(Health, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasHealth(this IEntity obj) => obj.HasValue(Health);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelHealth(this IEntity obj) => obj.DelValue(Health);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetHealth(this IEntity obj, IValue<int> value) => obj.SetValue(Health, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetSpeed(this IEntity obj) => obj.GetValue<IValue<float>>(Speed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetSpeed(this IEntity obj, out IValue<float> value) => obj.TryGetValue(Speed, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddSpeed(this IEntity obj, IValue<float> value) => obj.AddValue(Speed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasSpeed(this IEntity obj) => obj.HasValue(Speed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelSpeed(this IEntity obj) => obj.DelValue(Speed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetSpeed(this IEntity obj, IValue<float> value) => obj.SetValue(Speed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Transform GetTransform(this IEntity obj) => obj.GetValue<Transform>(Transform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTransform(this IEntity obj, out Transform value) => obj.TryGetValue(Transform, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddTransform(this IEntity obj, Transform value) => obj.AddValue(Transform, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTransform(this IEntity obj) => obj.HasValue(Transform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTransform(this IEntity obj) => obj.DelValue(Transform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTransform(this IEntity obj, Transform value) => obj.SetValue(Transform, value);
    }
}
