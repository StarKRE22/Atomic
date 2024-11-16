/**
* Code generation. Don't modify! 
**/

using Atomic.Entities;
using System.Runtime.CompilerServices;
using UnityEngine;
using Atomic.Entities;
using Atomic.Elements;

namespace SampleGame
{
	public static class CommonEntityAPI
	{


		///Values
		public const int Transform = -180157682; // Transform
		public const int GameObject = 1482111001; // GameObject
		public const int Name = -32386760; // string


		///Value Extensions

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
		public static string GetName(this IEntity obj) => obj.GetValue<string>(Name);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetName(this IEntity obj, out string value) => obj.TryGetValue(Name, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddName(this IEntity obj, string value) => obj.AddValue(Name, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasName(this IEntity obj) => obj.HasValue(Name);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelName(this IEntity obj) => obj.DelValue(Name);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetName(this IEntity obj, string value) => obj.SetValue(Name, value);
    }
}
