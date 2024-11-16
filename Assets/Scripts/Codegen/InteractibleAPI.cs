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
		public const int Interactible = -2055148603;


		///Values
		public const int InteractionCountdown = -1150090393; // IValue<float>


		///Tag Extensions

		public static bool HasInteractibleTag(this IEntity obj) => obj.HasTag(Interactible);
		public static bool NotInteractibleTag(this IEntity obj) => !obj.HasTag(Interactible);
		public static bool AddInteractibleTag(this IEntity obj) => obj.AddTag(Interactible);
		public static bool DelInteractibleTag(this IEntity obj) => obj.DelTag(Interactible);


		///Value Extensions

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetInteractionCountdown(this IEntity obj) => obj.GetValue<IValue<float>>(InteractionCountdown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetInteractionCountdown(this IEntity obj, out IValue<float> value) => obj.TryGetValue(InteractionCountdown, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddInteractionCountdown(this IEntity obj, IValue<float> value) => obj.AddValue(InteractionCountdown, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasInteractionCountdown(this IEntity obj) => obj.HasValue(InteractionCountdown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelInteractionCountdown(this IEntity obj) => obj.DelValue(InteractionCountdown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetInteractionCountdown(this IEntity obj, IValue<float> value) => obj.SetValue(InteractionCountdown, value);
    }
}
