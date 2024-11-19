/**
* Code generation. Don't modify! 
**/

using Atomic.Contexts;
using System.Runtime.CompilerServices;
using UnityEngine;
using Atomic.Entities;
using Atomic.Elements;

namespace SampleGame
{
	public static class CommonContextAPI
	{


		///Values
		public const int Transform = -180157682; // Transform
		public const int GameObject = 1482111001; // GameObject
		public const int Name = -32386760; // string


		///Value Extensions

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Transform GetTransform(this IContext obj) => obj.GetValue<Transform>(Transform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTransform(this IContext obj, out Transform value) => obj.TryGetValue(Transform, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddTransform(this IContext obj, Transform value) => obj.AddValue(Transform, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTransform(this IContext obj) => obj.HasValue(Transform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTransform(this IContext obj) => obj.DelValue(Transform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTransform(this IContext obj, Transform value) => obj.SetValue(Transform, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static GameObject GetGameObject(this IContext obj) => obj.GetValue<GameObject>(GameObject);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetGameObject(this IContext obj, out GameObject value) => obj.TryGetValue(GameObject, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddGameObject(this IContext obj, GameObject value) => obj.AddValue(GameObject, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasGameObject(this IContext obj) => obj.HasValue(GameObject);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelGameObject(this IContext obj) => obj.DelValue(GameObject);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetGameObject(this IContext obj, GameObject value) => obj.SetValue(GameObject, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string GetName(this IContext obj) => obj.GetValue<string>(Name);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetName(this IContext obj, out string value) => obj.TryGetValue(Name, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddName(this IContext obj, string value) => obj.AddValue(Name, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasName(this IContext obj) => obj.HasValue(Name);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelName(this IContext obj) => obj.DelValue(Name);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetName(this IContext obj, string value) => obj.SetValue(Name, value);
    }
}
