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
	public static class SampleEntityAPI
	{
		///Tags
		public const int Player = -1615495341;
		public const int Enemy = 979269037;
		public const int Resource = 1172805184;


		///Values
		public const int Health = -915003867; // IValue<int>
		public const int Speed = -823668238; // IValue<float>
		public const int Transform = -180157682; // Transform


		///Tag Extensions

		public static bool HasPlayerTag(this IEntity obj) => obj.HasTag(Player);
		public static bool NotPlayerTag(this IEntity obj) => !obj.HasTag(Player);
		public static bool AddPlayerTag(this IEntity obj) => obj.AddTag(Player);
		public static bool DelPlayerTag(this IEntity obj) => obj.DelTag(Player);

		public static bool HasEnemyTag(this IEntity obj) => obj.HasTag(Enemy);
		public static bool NotEnemyTag(this IEntity obj) => !obj.HasTag(Enemy);
		public static bool AddEnemyTag(this IEntity obj) => obj.AddTag(Enemy);
		public static bool DelEnemyTag(this IEntity obj) => obj.DelTag(Enemy);

		public static bool HasResourceTag(this IEntity obj) => obj.HasTag(Resource);
		public static bool NotResourceTag(this IEntity obj) => !obj.HasTag(Resource);
		public static bool AddResourceTag(this IEntity obj) => obj.AddTag(Resource);
		public static bool DelResourceTag(this IEntity obj) => obj.DelTag(Resource);


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
