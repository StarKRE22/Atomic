/**
* Code generation. Don't modify! 
**/

using Atomic.Entities;
using System.Runtime.CompilerServices;
using UnityEngine;
using Atomic.Entities;

namespace SampleGame
{
	public static class PlantEntityAPI
	{


		///Values
		public const int PlantState = 331501094; // int
		public const int Capacity = 1285477154; // float
		public const int Transform = -180157682; // Transform


		///Value Extensions

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetPlantState(this IPlant obj) => obj.GetValue<int>(PlantState);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetPlantState(this IPlant obj, out int value) => obj.TryGetValue(PlantState, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddPlantState(this IPlant obj, int value) => obj.AddValue(PlantState, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasPlantState(this IPlant obj) => obj.HasValue(PlantState);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelPlantState(this IPlant obj) => obj.DelValue(PlantState);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetPlantState(this IPlant obj, int value) => obj.SetValue(PlantState, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float GetCapacity(this IPlant obj) => obj.GetValue<float>(Capacity);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCapacity(this IPlant obj, out float value) => obj.TryGetValue(Capacity, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddCapacity(this IPlant obj, float value) => obj.AddValue(Capacity, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCapacity(this IPlant obj) => obj.HasValue(Capacity);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCapacity(this IPlant obj) => obj.DelValue(Capacity);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCapacity(this IPlant obj, float value) => obj.SetValue(Capacity, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Transform GetTransform(this IPlant obj) => obj.GetValue<Transform>(Transform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTransform(this IPlant obj, out Transform value) => obj.TryGetValue(Transform, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddTransform(this IPlant obj, Transform value) => obj.AddValue(Transform, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTransform(this IPlant obj) => obj.HasValue(Transform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTransform(this IPlant obj) => obj.DelValue(Transform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTransform(this IPlant obj, Transform value) => obj.SetValue(Transform, value);
    }
}
