/**
* Code generation. Don't modify! 
**/

using System.Runtime.CompilerServices;
using Atomic.Entities;
using Unity.Mathematics;

namespace SampleGame
{
	public static class EntityAPIExtensions
	{
		///Tags
		public const int Player = 300774994;
		public const int Enemy = 553796087;


		///Values
		public const int Health222 = -1307511547; // float2
		public const int Speed = 566784694; // int
		public const int cdd = 604786; // int
		public const int efece = 583110101; // float


		///Tag Extensions

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasPlayerTag(this IEntity obj) => obj.HasTag(Player);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddPlayerTag(this IEntity obj) => obj.AddTag(Player);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelPlayerTag(this IEntity obj) => obj.DelTag(Player);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasEnemyTag(this IEntity obj) => obj.HasTag(Enemy);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddEnemyTag(this IEntity obj) => obj.AddTag(Enemy);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelEnemyTag(this IEntity obj) => obj.DelTag(Enemy);


		///Value Extensions

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 GetHealth222(this IEntity obj) => obj.GetValue<float2>(Health222);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetHealth222(this IEntity obj, out float2 value) => obj.TryGetValue(Health222, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddHealth222(this IEntity obj, float2 value) => obj.AddValue(Health222, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasHealth222(this IEntity obj) => obj.HasValue(Health222);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelHealth222(this IEntity obj) => obj.DelValue(Health222);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetHealth222(this IEntity obj, float2 value) => obj.SetValue(Health222, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetSpeed(this IEntity obj) => obj.GetValue<int>(Speed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetSpeed(this IEntity obj, out int value) => obj.TryGetValue(Speed, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddSpeed(this IEntity obj, int value) => obj.AddValue(Speed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasSpeed(this IEntity obj) => obj.HasValue(Speed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelSpeed(this IEntity obj) => obj.DelValue(Speed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetSpeed(this IEntity obj, int value) => obj.SetValue(Speed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Getcdd(this IEntity obj) => obj.GetValue<int>(cdd);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetcdd(this IEntity obj, out int value) => obj.TryGetValue(cdd, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Addcdd(this IEntity obj, int value) => obj.AddValue(cdd, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Hascdd(this IEntity obj) => obj.HasValue(cdd);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Delcdd(this IEntity obj) => obj.DelValue(cdd);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Setcdd(this IEntity obj, int value) => obj.SetValue(cdd, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Getefece(this IEntity obj) => obj.GetValue<float>(efece);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetefece(this IEntity obj, out float value) => obj.TryGetValue(efece, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Addefece(this IEntity obj, float value) => obj.AddValue(efece, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Hasefece(this IEntity obj) => obj.HasValue(efece);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Delefece(this IEntity obj) => obj.DelValue(efece);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Setefece(this IEntity obj, float value) => obj.SetValue(efece, value);
    }
}
