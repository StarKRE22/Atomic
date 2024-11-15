//
// public static class InteractableValuesAPI
//     {
//         ///Keys
//         public const int InteractionCountdown = 11; // PrimitiveCountdown
//
//
//         ///Extensions
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static PrimitiveCountdown GetInteractionCountdown(this IEntity obj) => obj.GetValue<PrimitiveCountdown>(InteractionCountdown);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool TryGetInteractionCountdown(this IEntity obj, out PrimitiveCountdown value) => obj.TryGetValue(InteractionCountdown, out value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool AddInteractionCountdown(this IEntity obj, PrimitiveCountdown value) => obj.AddValue(InteractionCountdown, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool HasInteractionCountdown(this IEntity obj) => obj.HasValue(InteractionCountdown);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool DelInteractionCountdown(this IEntity obj) => obj.DelValue(InteractionCountdown);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static void SetInteractionCountdown(this IEntity obj, PrimitiveCountdown value) => obj.SetValue(InteractionCountdown, value);
//     }
//
// public static class MovementValuesAPI
//     {
//         ///Keys
//         public const int Position = 6; // float3Reactive
//         public const int MovementSpeed = 7; // Const<float>
//         public const int MovementDirection = 8; // float3Reactive
//
//
//         ///Extensions
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static float3Reactive GetPosition(this IEntity obj) => obj.GetValue<float3Reactive>(Position);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool TryGetPosition(this IEntity obj, out float3Reactive value) => obj.TryGetValue(Position, out value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool AddPosition(this IEntity obj, float3Reactive value) => obj.AddValue(Position, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool HasPosition(this IEntity obj) => obj.HasValue(Position);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool DelPosition(this IEntity obj) => obj.DelValue(Position);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static void SetPosition(this IEntity obj, float3Reactive value) => obj.SetValue(Position, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static Const<float> GetMovementSpeed(this IEntity obj) => obj.GetValue<Const<float>>(MovementSpeed);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool TryGetMovementSpeed(this IEntity obj, out Const<float> value) => obj.TryGetValue(MovementSpeed, out value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool AddMovementSpeed(this IEntity obj, Const<float> value) => obj.AddValue(MovementSpeed, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool HasMovementSpeed(this IEntity obj) => obj.HasValue(MovementSpeed);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool DelMovementSpeed(this IEntity obj) => obj.DelValue(MovementSpeed);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static void SetMovementSpeed(this IEntity obj, Const<float> value) => obj.SetValue(MovementSpeed, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static float3Reactive GetMovementDirection(this IEntity obj) => obj.GetValue<float3Reactive>(MovementDirection);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool TryGetMovementDirection(this IEntity obj, out float3Reactive value) => obj.TryGetValue(MovementDirection, out value);
//
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool AddMovementDirection(this IEntity obj, float3Reactive value) => obj.AddValue(MovementDirection, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool HasMovementDirection(this IEntity obj) => obj.HasValue(MovementDirection);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool DelMovementDirection(this IEntity obj) => obj.DelValue(MovementDirection);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static void SetMovementDirection(this IEntity obj, float3Reactive value) => obj.SetValue(MovementDirection, value);
//     }
//
// public static class PhysicsValuesAPI
//     {
//         ///Keys
//         public const int Rigidbody = 3; // Rigidbody
//         public const int TriggerEventReceiver = 4; // TriggerEventReceiver
//         public const int CollisionEventReceiver = 5; // CollisionEventReceiver
//
//
//         ///Extensions
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static Rigidbody GetRigidbody(this IEntity obj) => obj.GetValue<Rigidbody>(Rigidbody);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool TryGetRigidbody(this IEntity obj, out Rigidbody value) => obj.TryGetValue(Rigidbody, out value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool AddRigidbody(this IEntity obj, Rigidbody value) => obj.AddValue(Rigidbody, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool HasRigidbody(this IEntity obj) => obj.HasValue(Rigidbody);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool DelRigidbody(this IEntity obj) => obj.DelValue(Rigidbody);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static void SetRigidbody(this IEntity obj, Rigidbody value) => obj.SetValue(Rigidbody, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static TriggerEventReceiver GetTriggerEventReceiver(this IEntity obj) => obj.GetValue<TriggerEventReceiver>(TriggerEventReceiver);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool TryGetTriggerEventReceiver(this IEntity obj, out TriggerEventReceiver value) => obj.TryGetValue(TriggerEventReceiver, out value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool AddTriggerEventReceiver(this IEntity obj, TriggerEventReceiver value) => obj.AddValue(TriggerEventReceiver, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool HasTriggerEventReceiver(this IEntity obj) => obj.HasValue(TriggerEventReceiver);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool DelTriggerEventReceiver(this IEntity obj) => obj.DelValue(TriggerEventReceiver);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static void SetTriggerEventReceiver(this IEntity obj, TriggerEventReceiver value) => obj.SetValue(TriggerEventReceiver, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static CollisionEventReceiver GetCollisionEventReceiver(this IEntity obj) => obj.GetValue<CollisionEventReceiver>(CollisionEventReceiver);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool TryGetCollisionEventReceiver(this IEntity obj, out CollisionEventReceiver value) => obj.TryGetValue(CollisionEventReceiver, out value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool AddCollisionEventReceiver(this IEntity obj, CollisionEventReceiver value) => obj.AddValue(CollisionEventReceiver, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool HasCollisionEventReceiver(this IEntity obj) => obj.HasValue(CollisionEventReceiver);
//
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool DelCollisionEventReceiver(this IEntity obj) => obj.DelValue(CollisionEventReceiver);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static void SetCollisionEventReceiver(this IEntity obj, CollisionEventReceiver value) => obj.SetValue(CollisionEventReceiver, value);
//     }
//
// public static class PlantValuesAPI
//     {
//         ///Keys
//         public const int PlantState = 12; // ReactiveVariable<PlantState>
//         public const int GrowthCountdown = 13; // PrimitiveCountdown
//         public const int RandomRangeTimeData = 14; // RandomRangeTimeData
//         public const int PlantDeathCountdown = 15; // PrimitiveCountdown
//
//
//         ///Extensions
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static ReactiveVariable<PlantState> GetPlantState(this IEntity obj) => obj.GetValue<ReactiveVariable<PlantState>>(PlantState);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool TryGetPlantState(this IEntity obj, out ReactiveVariable<PlantState> value) => obj.TryGetValue(PlantState, out value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool AddPlantState(this IEntity obj, ReactiveVariable<PlantState> value) => obj.AddValue(PlantState, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool HasPlantState(this IEntity obj) => obj.HasValue(PlantState);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool DelPlantState(this IEntity obj) => obj.DelValue(PlantState);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static void SetPlantState(this IEntity obj, ReactiveVariable<PlantState> value) => obj.SetValue(PlantState, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static PrimitiveCountdown GetGrowthCountdown(this IEntity obj) => obj.GetValue<PrimitiveCountdown>(GrowthCountdown);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool TryGetGrowthCountdown(this IEntity obj, out PrimitiveCountdown value) => obj.TryGetValue(GrowthCountdown, out value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool AddGrowthCountdown(this IEntity obj, PrimitiveCountdown value) => obj.AddValue(GrowthCountdown, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool HasGrowthCountdown(this IEntity obj) => obj.HasValue(GrowthCountdown);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool DelGrowthCountdown(this IEntity obj) => obj.DelValue(GrowthCountdown);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static void SetGrowthCountdown(this IEntity obj, PrimitiveCountdown value) => obj.SetValue(GrowthCountdown, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static RandomRangeTimeData GetRandomRangeTimeData(this IEntity obj) => obj.GetValue<RandomRangeTimeData>(RandomRangeTimeData);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool TryGetRandomRangeTimeData(this IEntity obj, out RandomRangeTimeData value) => obj.TryGetValue(RandomRangeTimeData, out value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool AddRandomRangeTimeData(this IEntity obj, RandomRangeTimeData value) => obj.AddValue(RandomRangeTimeData, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool HasRandomRangeTimeData(this IEntity obj) => obj.HasValue(RandomRangeTimeData);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool DelRandomRangeTimeData(this IEntity obj) => obj.DelValue(RandomRangeTimeData);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static void SetRandomRangeTimeData(this IEntity obj, RandomRangeTimeData value) => obj.SetValue(RandomRangeTimeData, value);
//
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static PrimitiveCountdown GetPlantDeathCountdown(this IEntity obj) => obj.GetValue<PrimitiveCountdown>(PlantDeathCountdown);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool TryGetPlantDeathCountdown(this IEntity obj, out PrimitiveCountdown value) => obj.TryGetValue(PlantDeathCountdown, out value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool AddPlantDeathCountdown(this IEntity obj, PrimitiveCountdown value) => obj.AddValue(PlantDeathCountdown, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool HasPlantDeathCountdown(this IEntity obj) => obj.HasValue(PlantDeathCountdown);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool DelPlantDeathCountdown(this IEntity obj) => obj.DelValue(PlantDeathCountdown);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static void SetPlantDeathCountdown(this IEntity obj, PrimitiveCountdown value) => obj.SetValue(PlantDeathCountdown, value);
//     }
//
// public static class RenderingValuesAPI
//     {
//         ///Keys
//         public const int GameObject = 1; // GameObject
//         public const int Transform = 2; // Transform
//
//
//         ///Extensions
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static GameObject GetGameObject(this IEntity obj) => obj.GetValue<GameObject>(GameObject);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool TryGetGameObject(this IEntity obj, out GameObject value) => obj.TryGetValue(GameObject, out value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool AddGameObject(this IEntity obj, GameObject value) => obj.AddValue(GameObject, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool HasGameObject(this IEntity obj) => obj.HasValue(GameObject);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool DelGameObject(this IEntity obj) => obj.DelValue(GameObject);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static void SetGameObject(this IEntity obj, GameObject value) => obj.SetValue(GameObject, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static Transform GetTransform(this IEntity obj) => obj.GetValue<Transform>(Transform);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool TryGetTransform(this IEntity obj, out Transform value) => obj.TryGetValue(Transform, out value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool AddTransform(this IEntity obj, Transform value) => obj.AddValue(Transform, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool HasTransform(this IEntity obj) => obj.HasValue(Transform);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool DelTransform(this IEntity obj) => obj.DelValue(Transform);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static void SetTransform(this IEntity obj, Transform value) => obj.SetValue(Transform, value);
//     }
//
// public static class RotationValuesAPI
//     {
//         ///Keys
//         public const int Rotation = 9; // quaternionReactive
//         public const int RotationSpeed = 10; // ReactiveFloat
//
//
//         ///Extensions
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static quaternionReactive GetRotation(this IEntity obj) => obj.GetValue<quaternionReactive>(Rotation);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool TryGetRotation(this IEntity obj, out quaternionReactive value) => obj.TryGetValue(Rotation, out value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool AddRotation(this IEntity obj, quaternionReactive value) => obj.AddValue(Rotation, value);
//
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool HasRotation(this IEntity obj) => obj.HasValue(Rotation);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool DelRotation(this IEntity obj) => obj.DelValue(Rotation);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static void SetRotation(this IEntity obj, quaternionReactive value) => obj.SetValue(Rotation, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static ReactiveFloat GetRotationSpeed(this IEntity obj) => obj.GetValue<ReactiveFloat>(RotationSpeed);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool TryGetRotationSpeed(this IEntity obj, out ReactiveFloat value) => obj.TryGetValue(RotationSpeed, out value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool AddRotationSpeed(this IEntity obj, ReactiveFloat value) => obj.AddValue(RotationSpeed, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool HasRotationSpeed(this IEntity obj) => obj.HasValue(RotationSpeed);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool DelRotationSpeed(this IEntity obj) => obj.DelValue(RotationSpeed);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static void SetRotationSpeed(this IEntity obj, ReactiveFloat value) => obj.SetValue(RotationSpeed, value);
//     }
//
// public static class SeedValuesAPI
//     {
//         ///Keys
//         public const int SeedAmount = 16; // ReactiveVariable<int>
//         public const int SeedDeathCountdown = 17; // PrimitiveCountdown
//
//
//         ///Extensions
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static ReactiveVariable<int> GetSeedAmount(this IEntity obj) => obj.GetValue<ReactiveVariable<int>>(SeedAmount);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool TryGetSeedAmount(this IEntity obj, out ReactiveVariable<int> value) => obj.TryGetValue(SeedAmount, out value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool AddSeedAmount(this IEntity obj, ReactiveVariable<int> value) => obj.AddValue(SeedAmount, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool HasSeedAmount(this IEntity obj) => obj.HasValue(SeedAmount);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool DelSeedAmount(this IEntity obj) => obj.DelValue(SeedAmount);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static void SetSeedAmount(this IEntity obj, ReactiveVariable<int> value) => obj.SetValue(SeedAmount, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static PrimitiveCountdown GetSeedDeathCountdown(this IEntity obj) => obj.GetValue<PrimitiveCountdown>(SeedDeathCountdown);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool TryGetSeedDeathCountdown(this IEntity obj, out PrimitiveCountdown value) => obj.TryGetValue(SeedDeathCountdown, out value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool AddSeedDeathCountdown(this IEntity obj, PrimitiveCountdown value) => obj.AddValue(SeedDeathCountdown, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool HasSeedDeathCountdown(this IEntity obj) => obj.HasValue(SeedDeathCountdown);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool DelSeedDeathCountdown(this IEntity obj) => obj.DelValue(SeedDeathCountdown);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static void SetSeedDeathCountdown(this IEntity obj, PrimitiveCountdown value) => obj.SetValue(SeedDeathCountdown, value);
//     }
//
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool HasRotation(this IEntity obj) => obj.HasValue(Rotation);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool DelRotation(this IEntity obj) => obj.DelValue(Rotation);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static void SetRotation(this IEntity obj, quaternionReactive value) => obj.SetValue(Rotation, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static ReactiveFloat GetRotationSpeed(this IEntity obj) => obj.GetValue<ReactiveFloat>(RotationSpeed);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool TryGetRotationSpeed(this IEntity obj, out ReactiveFloat value) => obj.TryGetValue(RotationSpeed, out value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool AddRotationSpeed(this IEntity obj, ReactiveFloat value) => obj.AddValue(RotationSpeed, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool HasRotationSpeed(this IEntity obj) => obj.HasValue(RotationSpeed);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool DelRotationSpeed(this IEntity obj) => obj.DelValue(RotationSpeed);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static void SetRotationSpeed(this IEntity obj, ReactiveFloat value) => obj.SetValue(RotationSpeed, value);
//     }
//
// public static class SeedValuesAPI
//     {
//         ///Keys
//         public const int SeedAmount = 16; // ReactiveVariable<int>
//         public const int SeedDeathCountdown = 17; // PrimitiveCountdown
//
//
//         ///Extensions
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static ReactiveVariable<int> GetSeedAmount(this IEntity obj) => obj.GetValue<ReactiveVariable<int>>(SeedAmount);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool TryGetSeedAmount(this IEntity obj, out ReactiveVariable<int> value) => obj.TryGetValue(SeedAmount, out value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool AddSeedAmount(this IEntity obj, ReactiveVariable<int> value) => obj.AddValue(SeedAmount, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool HasSeedAmount(this IEntity obj) => obj.HasValue(SeedAmount);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool DelSeedAmount(this IEntity obj) => obj.DelValue(SeedAmount);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static void SetSeedAmount(this IEntity obj, ReactiveVariable<int> value) => obj.SetValue(SeedAmount, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static PrimitiveCountdown GetSeedDeathCountdown(this IEntity obj) => obj.GetValue<PrimitiveCountdown>(SeedDeathCountdown);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool TryGetSeedDeathCountdown(this IEntity obj, out PrimitiveCountdown value) => obj.TryGetValue(SeedDeathCountdown, out value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool AddSeedDeathCountdown(this IEntity obj, PrimitiveCountdown value) => obj.AddValue(SeedDeathCountdown, value);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool HasSeedDeathCountdown(this IEntity obj) => obj.HasValue(SeedDeathCountdown);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static bool DelSeedDeathCountdown(this IEntity obj) => obj.DelValue(SeedDeathCountdown);
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static void SetSeedDeathCountdown(this IEntity obj, PrimitiveCountdown value) => obj.SetValue(SeedDeathCountdown, value);
//     }
