/**
* Code generation. Don't modify! 
**/

using System.Runtime.CompilerServices;
using UnityEngine;
using Atomic.Elements;
using Unity.Mathematics;

namespace SampleGame
{
	public static class GameEntityAPI
	{
		///Tags
		public const int Character = 227434008;
		public const int Coin = 17805890;


		///Values
		public const int Transform = -284094885; // Transform
		public const int GameObject = 2006407362; // GameObject
		public const int MoveSpeed = -279498267; // IValue<float>
		public const int MoveDirection = 1403009565; // IVariable<float3>
		public const int RotationSpeed = 1037734616; // IValue<float>
		public const int RotationDirection = -19666544; // IVariable<float3>
		public const int TriggerEvents = -1182757792; // TriggerEvents
		public const int Money = 561222447; // IValue<int>


		///Tag Extensions

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCharacterTag(this IGameEntity obj) => obj.HasTag(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddCharacterTag(this IGameEntity obj) => obj.AddTag(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCharacterTag(this IGameEntity obj) => obj.DelTag(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCoinTag(this IGameEntity obj) => obj.HasTag(Coin);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddCoinTag(this IGameEntity obj) => obj.AddTag(Coin);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCoinTag(this IGameEntity obj) => obj.DelTag(Coin);


		///Value Extensions

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Transform GetTransform(this IGameEntity obj) => obj.GetValue<Transform>(Transform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTransform(this IGameEntity obj, out Transform value) => obj.TryGetValue(Transform, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTransform(this IGameEntity obj, Transform value) => obj.AddValue(Transform, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTransform(this IGameEntity obj) => obj.HasValue(Transform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTransform(this IGameEntity obj) => obj.DelValue(Transform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTransform(this IGameEntity obj, Transform value) => obj.SetValue(Transform, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static GameObject GetGameObject(this IGameEntity obj) => obj.GetValue<GameObject>(GameObject);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetGameObject(this IGameEntity obj, out GameObject value) => obj.TryGetValue(GameObject, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddGameObject(this IGameEntity obj, GameObject value) => obj.AddValue(GameObject, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasGameObject(this IGameEntity obj) => obj.HasValue(GameObject);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelGameObject(this IGameEntity obj) => obj.DelValue(GameObject);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetGameObject(this IGameEntity obj, GameObject value) => obj.SetValue(GameObject, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetMoveSpeed(this IGameEntity obj) => obj.GetValue<IValue<float>>(MoveSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoveSpeed(this IGameEntity obj, out IValue<float> value) => obj.TryGetValue(MoveSpeed, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoveSpeed(this IGameEntity obj, IValue<float> value) => obj.AddValue(MoveSpeed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoveSpeed(this IGameEntity obj) => obj.HasValue(MoveSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoveSpeed(this IGameEntity obj) => obj.DelValue(MoveSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoveSpeed(this IGameEntity obj, IValue<float> value) => obj.SetValue(MoveSpeed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IVariable<float3> GetMoveDirection(this IGameEntity obj) => obj.GetValue<IVariable<float3>>(MoveDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoveDirection(this IGameEntity obj, out IVariable<float3> value) => obj.TryGetValue(MoveDirection, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoveDirection(this IGameEntity obj, IVariable<float3> value) => obj.AddValue(MoveDirection, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoveDirection(this IGameEntity obj) => obj.HasValue(MoveDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoveDirection(this IGameEntity obj) => obj.DelValue(MoveDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoveDirection(this IGameEntity obj, IVariable<float3> value) => obj.SetValue(MoveDirection, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetRotationSpeed(this IGameEntity obj) => obj.GetValue<IValue<float>>(RotationSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRotationSpeed(this IGameEntity obj, out IValue<float> value) => obj.TryGetValue(RotationSpeed, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRotationSpeed(this IGameEntity obj, IValue<float> value) => obj.AddValue(RotationSpeed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRotationSpeed(this IGameEntity obj) => obj.HasValue(RotationSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRotationSpeed(this IGameEntity obj) => obj.DelValue(RotationSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotationSpeed(this IGameEntity obj, IValue<float> value) => obj.SetValue(RotationSpeed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IVariable<float3> GetRotationDirection(this IGameEntity obj) => obj.GetValue<IVariable<float3>>(RotationDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRotationDirection(this IGameEntity obj, out IVariable<float3> value) => obj.TryGetValue(RotationDirection, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRotationDirection(this IGameEntity obj, IVariable<float3> value) => obj.AddValue(RotationDirection, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRotationDirection(this IGameEntity obj) => obj.HasValue(RotationDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRotationDirection(this IGameEntity obj) => obj.DelValue(RotationDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotationDirection(this IGameEntity obj, IVariable<float3> value) => obj.SetValue(RotationDirection, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TriggerEvents GetTriggerEvents(this IGameEntity obj) => obj.GetValue<TriggerEvents>(TriggerEvents);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTriggerEvents(this IGameEntity obj, out TriggerEvents value) => obj.TryGetValue(TriggerEvents, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTriggerEvents(this IGameEntity obj, TriggerEvents value) => obj.AddValue(TriggerEvents, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTriggerEvents(this IGameEntity obj) => obj.HasValue(TriggerEvents);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTriggerEvents(this IGameEntity obj) => obj.DelValue(TriggerEvents);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTriggerEvents(this IGameEntity obj, TriggerEvents value) => obj.SetValue(TriggerEvents, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<int> GetMoney(this IGameEntity obj) => obj.GetValue<IValue<int>>(Money);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoney(this IGameEntity obj, out IValue<int> value) => obj.TryGetValue(Money, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoney(this IGameEntity obj, IValue<int> value) => obj.AddValue(Money, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoney(this IGameEntity obj) => obj.HasValue(Money);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoney(this IGameEntity obj) => obj.DelValue(Money);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoney(this IGameEntity obj, IValue<int> value) => obj.SetValue(Money, value);
    }
}
