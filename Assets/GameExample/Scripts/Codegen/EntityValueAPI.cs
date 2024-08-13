/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Entities;
using System.Runtime.CompilerServices;
using Atomic.Elements;
using Atomic.Extensions;
using GameExample.Engine;
using Unity.Mathematics;

namespace Atomic.Entities
{
    public static class EntityValueAPI
    {
        ///Keys
        public const int Transform = 2; // Transform
        public const int MoveSpeed = 3; // Const<float>
        public const int MoveDirection = 4; // float3Reactive
        public const int Position = 1; // float3Reactive
        public const int Rotation = 5; // quaternionReactive
        public const int AngularSpeed = 8; // Const<float>
        public const int TriggerEventReceiver = 9; // TriggerEventReceiver
        public const int Money = 10; // Const<int>
        public const int GameObject = 12; // GameObject


        ///Extensions
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
        public static Const<float> GetMoveSpeed(this IEntity obj) => obj.GetValue<Const<float>>(MoveSpeed);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetMoveSpeed(this IEntity obj, out Const<float> value) => obj.TryGetValue(MoveSpeed, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddMoveSpeed(this IEntity obj, Const<float> value) => obj.AddValue(MoveSpeed, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasMoveSpeed(this IEntity obj) => obj.HasValue(MoveSpeed);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelMoveSpeed(this IEntity obj) => obj.DelValue(MoveSpeed);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetMoveSpeed(this IEntity obj, Const<float> value) => obj.SetValue(MoveSpeed, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3Reactive GetMoveDirection(this IEntity obj) => obj.GetValue<float3Reactive>(MoveDirection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetMoveDirection(this IEntity obj, out float3Reactive value) => obj.TryGetValue(MoveDirection, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddMoveDirection(this IEntity obj, float3Reactive value) => obj.AddValue(MoveDirection, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasMoveDirection(this IEntity obj) => obj.HasValue(MoveDirection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelMoveDirection(this IEntity obj) => obj.DelValue(MoveDirection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetMoveDirection(this IEntity obj, float3Reactive value) => obj.SetValue(MoveDirection, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3Reactive GetPosition(this IEntity obj) => obj.GetValue<float3Reactive>(Position);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetPosition(this IEntity obj, out float3Reactive value) => obj.TryGetValue(Position, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddPosition(this IEntity obj, float3Reactive value) => obj.AddValue(Position, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasPosition(this IEntity obj) => obj.HasValue(Position);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelPosition(this IEntity obj) => obj.DelValue(Position);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosition(this IEntity obj, float3Reactive value) => obj.SetValue(Position, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternionReactive GetRotation(this IEntity obj) => obj.GetValue<quaternionReactive>(Rotation);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetRotation(this IEntity obj, out quaternionReactive value) => obj.TryGetValue(Rotation, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddRotation(this IEntity obj, quaternionReactive value) => obj.AddValue(Rotation, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasRotation(this IEntity obj) => obj.HasValue(Rotation);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelRotation(this IEntity obj) => obj.DelValue(Rotation);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetRotation(this IEntity obj, quaternionReactive value) => obj.SetValue(Rotation, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Const<float> GetAngularSpeed(this IEntity obj) => obj.GetValue<Const<float>>(AngularSpeed);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAngularSpeed(this IEntity obj, out Const<float> value) => obj.TryGetValue(AngularSpeed, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAngularSpeed(this IEntity obj, Const<float> value) => obj.AddValue(AngularSpeed, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAngularSpeed(this IEntity obj) => obj.HasValue(AngularSpeed);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelAngularSpeed(this IEntity obj) => obj.DelValue(AngularSpeed);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAngularSpeed(this IEntity obj, Const<float> value) => obj.SetValue(AngularSpeed, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TriggerEventReceiver GetTriggerEventReceiver(this IEntity obj) => obj.GetValue<TriggerEventReceiver>(TriggerEventReceiver);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetTriggerEventReceiver(this IEntity obj, out TriggerEventReceiver value) => obj.TryGetValue(TriggerEventReceiver, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddTriggerEventReceiver(this IEntity obj, TriggerEventReceiver value) => obj.AddValue(TriggerEventReceiver, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasTriggerEventReceiver(this IEntity obj) => obj.HasValue(TriggerEventReceiver);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelTriggerEventReceiver(this IEntity obj) => obj.DelValue(TriggerEventReceiver);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetTriggerEventReceiver(this IEntity obj, TriggerEventReceiver value) => obj.SetValue(TriggerEventReceiver, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Const<int> GetMoney(this IEntity obj) => obj.GetValue<Const<int>>(Money);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetMoney(this IEntity obj, out Const<int> value) => obj.TryGetValue(Money, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddMoney(this IEntity obj, Const<int> value) => obj.AddValue(Money, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasMoney(this IEntity obj) => obj.HasValue(Money);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelMoney(this IEntity obj) => obj.DelValue(Money);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetMoney(this IEntity obj, Const<int> value) => obj.SetValue(Money, value);

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
    }
}
