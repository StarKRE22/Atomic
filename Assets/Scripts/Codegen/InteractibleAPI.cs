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
	public static class InteractibleAPI
	{
		///Tags


		///Values
		public const int InteractionCountdown = -1150090393; // IValue<int>


		///Tag Extensions


		///Value Extensions

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<int> GetInteractionCountdown(this IEntity obj) => obj.GetValue<IValue<int>>(InteractionCountdown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetInteractionCountdown(this IEntity obj, out IValue<int> value) => obj.TryGetValue(InteractionCountdown, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddInteractionCountdown(this IEntity obj, IValue<int> value) => obj.AddValue(InteractionCountdown, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasInteractionCountdown(this IEntity obj) => obj.HasValue(InteractionCountdown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelInteractionCountdown(this IEntity obj) => obj.DelValue(InteractionCountdown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetInteractionCountdown(this IEntity obj, IValue<int> value) => obj.SetValue(InteractionCountdown, value);
    }
}
