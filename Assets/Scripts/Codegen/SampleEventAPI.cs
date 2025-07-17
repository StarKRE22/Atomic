/**
* Code generation. Don't modify! 
**/

using Atomic.Events;
using System.Runtime.CompilerServices;
using UnityEngine;
using Atomic.Events;
using System;

namespace SampleGame
{
	public static class SampleEventAPI
	{
		///Events
		public const int Hello = -137262718;
		public const int Attack = 1080829965;
		public const int Spawn = 1372578019;


		///Event Extensions


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DisposeHello(this IEventBus bus) => bus.Dispose(Hello);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Subscription SubscribeHello(this IEventBus bus, Action action) => bus.Subscribe(Hello, action);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void UnsubscribeHello(this IEventBus bus, Action action) => bus.Unsubscribe(Hello, action);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void InvokeHello(this IEventBus bus) => bus.Invoke(Hello);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsSubscribedHello(this IEventBus bus) => bus.IsSubscribed(Hello);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DisposeAttack(this IEventBus bus) => bus.Dispose(Attack);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Subscription<GameObject> SubscribeAttack(this IEventBus bus, Action<GameObject> action) => bus.Subscribe(Attack, action);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void UnsubscribeAttack(this IEventBus bus, Action<GameObject> action) => bus.Unsubscribe(Attack, action);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void InvokeAttack(this IEventBus bus, GameObject target) => bus.Invoke(Attack, target);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsSubscribedAttack(this IEventBus bus) => bus.IsSubscribed(Attack);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DisposeSpawn(this IEventBus bus) => bus.Dispose(Spawn);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Subscription<GameObject, Vector3, Quaternion> SubscribeSpawn(this IEventBus bus, Action<GameObject, Vector3, Quaternion> action) => bus.Subscribe(Spawn, action);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void UnsubscribeSpawn(this IEventBus bus, Action<GameObject, Vector3, Quaternion> action) => bus.Unsubscribe(Spawn, action);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void InvokeSpawn(this IEventBus bus, GameObject prefab, Vector3 position, Quaternion rotation) => bus.Invoke(Spawn, prefab, position, rotation);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsSubscribedSpawn(this IEventBus bus) => bus.IsSubscribed(Spawn);
    }
}
