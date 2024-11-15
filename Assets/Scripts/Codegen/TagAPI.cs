/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Entities;
using System.Runtime.CompilerServices;
using UnityEngine;
using Elements;

namespace SampleGame
{
	public static class TagAPI
	{
		///Tags
		public const int Character = 294335127;
		public const int Enemy = 979269037;
		public const int Coin = -178571165;


		///Values
		public const int Health = -915003867; // IValue<int>
		public const int Damage = 375673178; // IValue<int>
		public const int Speed = -823668238; // IValue<float>
		public const int Transform = -180157682; // Transform
		public const int GameObject = 1482111001; // GameObject
		public const int Rigidbody = -2101481708; // Rigidbody


		///Tag Extensions

		public static bool HasCharacterTag(this IEntity obj) => obj.HasTag(Character);
		public static bool NotCharacterTag(this IEntity obj) => !obj.HasTag(Character);
		public static bool AddCharacterTag(this IEntity obj) => obj.AddTag(Character);
		public static bool DelCharacterTag(this IEntity obj) => obj.DelTag(Character);

		public static bool HasEnemyTag(this IEntity obj) => obj.HasTag(Enemy);
		public static bool NotEnemyTag(this IEntity obj) => !obj.HasTag(Enemy);
		public static bool AddEnemyTag(this IEntity obj) => obj.AddTag(Enemy);
		public static bool DelEnemyTag(this IEntity obj) => obj.DelTag(Enemy);

		public static bool HasCoinTag(this IEntity obj) => obj.HasTag(Coin);
		public static bool NotCoinTag(this IEntity obj) => !obj.HasTag(Coin);
		public static bool AddCoinTag(this IEntity obj) => obj.AddTag(Coin);
		public static bool DelCoinTag(this IEntity obj) => obj.DelTag(Coin);


		///Value Extensions

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<int> GetHealth(this IEntity obj) => obj.GetValue<IValue<int>>(Health);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetHealth(this IEntity obj, out IValue<int> value) => obj.TryGetValue(Health, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddHealth(this IEntity obj, IValue<int> value) => obj.AddValue(Health, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasHealth(this IEntity obj) => obj.HasValue(IValue<int>);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelHealth(this IEntity obj) => obj.DelValue(Health);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetHealth(this IEntity obj, IValue<int> value) => obj.SetValue(Health, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<int> GetDamage(this IEntity obj) => obj.GetValue<IValue<int>>(Damage);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetDamage(this IEntity obj, out IValue<int> value) => obj.TryGetValue(Damage, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddDamage(this IEntity obj, IValue<int> value) => obj.AddValue(Damage, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasDamage(this IEntity obj) => obj.HasValue(IValue<int>);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelDamage(this IEntity obj) => obj.DelValue(Damage);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetDamage(this IEntity obj, IValue<int> value) => obj.SetValue(Damage, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetSpeed(this IEntity obj) => obj.GetValue<IValue<float>>(Speed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetSpeed(this IEntity obj, out IValue<float> value) => obj.TryGetValue(Speed, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddSpeed(this IEntity obj, IValue<float> value) => obj.AddValue(Speed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasSpeed(this IEntity obj) => obj.HasValue(IValue<float>);

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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static GameObject GetGameObject(this IEntity obj) => obj.GetValue<GameObject>(GameObject);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetGameObject(this IEntity obj, out GameObject value) => obj.TryGetValue(GameObject, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddGameObject(this IEntity obj, GameObject value) => obj.AddValue(GameObject, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasGameObject(this IEntity obj) => obj.HasValue(GameObject);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelGameObject(this IEntity obj) => obj.DelValue(GameObject);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetGameObject(this IEntity obj, GameObject value) => obj.SetValue(GameObject, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Rigidbody GetRigidbody(this IEntity obj) => obj.GetValue<Rigidbody>(Rigidbody);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRigidbody(this IEntity obj, out Rigidbody value) => obj.TryGetValue(Rigidbody, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddRigidbody(this IEntity obj, Rigidbody value) => obj.AddValue(Rigidbody, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRigidbody(this IEntity obj) => obj.HasValue(Rigidbody);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRigidbody(this IEntity obj) => obj.DelValue(Rigidbody);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRigidbody(this IEntity obj, Rigidbody value) => obj.SetValue(Rigidbody, value);
    }
}
