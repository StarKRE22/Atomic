# 🧩️ IEntity

`IEntity` is the fundamental interface representing an entity in the framework. It follows the Entity-State-Behaviour
pattern, serving as a container for data (values), identity (tags), and modular logic (behaviours).

---

## Key Features

- **State Management** – Dynamic key-value store for runtime data
- **Tag System** – Lightweight categorization and filtering
- **Behaviour Composition** – Attach/detach modular logic at runtime
- **Lifecycle Control** – Built-in init, enable, update, disable and disposal phases
- **Event-Driven** – State change notifications for reactive programming
- **Unique Identity** – Runtime instance ID for entity tracking

## Inheritance

- `IInitSource` – supports explicit initialization and disposal.
- `IEnableSource` – supports enabling and disabling at runtime.
- `IUpdateSource` – supports Update, FixedUpdate, and LateUpdate callbacks.

---

## Events

### State Events

- `event Action OnStateChanged` – triggered when the entity’s internal state changes.
- `event Action OnInitialize` – triggered when the entity has been initialized.
- `event Action OnEnabled` – triggered when the entity has been enabled.
- `event Action OnDisabled` – triggered when the entity has been disabled.
- `event Action OnDisposed` – triggered when the entity has been disposed and its resources released.
- `event Action<float> OnUpdated` – triggered when the entity has been updated.
- `event Action<float> OnFixedUpdated` – triggered when the entity has been fixed updated.
- `event Action<float> OnLateUpdated` – triggered when the entity has been late updated.

### Behaviour Events

- `event Action<IEntity, IEntityBehaviour> OnBehaviourAdded` – when a behaviour is added.
- `event Action<IEntity, IEntityBehaviour> OnBehaviourDeleted` – when a behaviour is removed.

### Tag Events

- `event Action<IEntity, int> OnTagAdded` – when a tag is added.
- `event Action<IEntity, int> OnTagDeleted` – when a tag is removed.

### Value Events

- `event Action<IEntity, int> OnValueAdded` – when a value is added.
- `event Action<IEntity, int> OnValueDeleted` – when a value is deleted.
- `event Action<IEntity, int> OnValueChanged` – when a value is updated.

---

## Properties

- `int InstanceID` – runtime-generated unique identifier, valid only during runtime.
- `string Name` – optional user-defined name, useful for debugging or editor tooling.
- `int BehaviourCount` – number of behaviours attached.
- `int TagCount` – number of tags associated.
- `int ValueCount` – number of stored values.
- `bool Initialized` – Gets a value indicating whether the entity is currently initialized.
- `bool Enabled` – Gets a value indicating whether the entity is currently initialized.

---

## Tag Methods

- `bool HasTag(int key)` – checks for a tag.
- `bool AddTag(int key)` – adds a tag.
- `bool DelTag(int key)` – removes a tag.
- `void ClearTags()` – removes all tags.
- `int[] GetTags()` – returns all tag keys.
- `int CopyTags(int[] results)` – copies tag keys into an array.
- `IEnumerator<int> GetTagEnumerator()` – enumerates all tags.

---

## Value Methods

- `T GetValue<T>(int key)` – retrieves a value by key.
- `ref T GetValueUnsafe<T>(int key)` – retrieves a value by reference (unsafe, no boxing).
- `object GetValue(int key)` – retrieves a value as an object.
- `bool TryGetValue<T>(int key, out T value)` – tries to retrieve a typed value.
- `bool TryGetValueUnsafe<T>(int key, out T value)` – tries to retrieve by reference.
- `bool TryGetValue(int key, out object value)` – tries to retrieve as object.
- `void SetValue(int key, object value)` – sets or updates a value.
- `void SetValue<T>(int key, T value)` – sets or updates a struct value.
- `bool HasValue(int key)` – checks if a value exists.
- `void AddValue(int key, object value)` – adds a value.
- `void AddValue<T>(int key, T value)` – adds a struct value.
- `bool DelValue(int key)` – deletes a value.
- `void ClearValues()` – clears all values.
- `KeyValuePair<int, object>[] GetValues()` – returns all key-value pairs.
- `int CopyValues(KeyValuePair<int, object>[] results)` – copies all key-value pairs.
- `IEnumerator<KeyValuePair<int, object>> GetValueEnumerator()` – enumerates all values.

---

## Behaviour Methods

- `void AddBehaviour(IEntityBehaviour behaviour)` – adds a behaviour.
- `T GetBehaviour<T>()` – returns the first behaviour of type `T`.
- `bool TryGetBehaviour<T>(out T behaviour)` – attempts to get a behaviour.
- `bool HasBehaviour(IEntityBehaviour behaviour)` – checks if a specific behaviour exists.
- `bool HasBehaviour<T>()` – checks if a behaviour of type `T` exists.
- `bool DelBehaviour(IEntityBehaviour behaviour)` – removes a specific behaviour.
- `bool DelBehaviour<T>()` – removes the first behaviour of type `T`.
- `void DelBehaviours<T>()` – removes all behaviours of type `T`.
- `void ClearBehaviours()` – removes all behaviours.
- `IEntityBehaviour[] GetBehaviours()` – returns all behaviours.
- `T[] GetBehaviours<T>()` – returns all behaviours of type `T`.
- `int CopyBehaviours(IEntityBehaviour[] results)` – copies behaviours into an array.
- `int CopyBehaviours<T>(T[] results)` – copies behaviours of type `T`.
- `IEnumerator<IEntityBehaviour> GetBehaviourEnumerator()` – enumerates behaviours.

---

## Lifecycle Methods

- `void Init()` – initializes the entity.
    - Transitions an entity to `Initialized` state
    - Calls `Init` on all behaviours implementing `IEntityInit`
    - Triggers `OnInitialized` event
    - If the entity is already initialized, this method does nothing


- `void Enable()` – enables the entity for updates
    - Transitions an entity to `Enabled` state
    - Calls `Enable` on all behaviours implementing `IEntityEnable`
    - Triggers `OnEnabled` event
    - If the entity is not initialized yet, this method initializes the entity also
    - If the entity is already enabled, this method does nothing

- `void OnUpdate(float deltaTime)` — calls the update on the entity
  - Calls `Update` on all `IEntityUpdate` behaviours
  - Triggers `OnUpdated` event
  - Can be invoked only if entity is enabled


- `void OnFixedUpdate(float deltaTime)` — calls the fixed update on the entity
  - Calls `FixedUpdate` on all `IEntityFixedUpdate` behaviours
  - Triggers `OnFixedUpdated` event
  - Can be invoked only if entity is enabled


- `void OnLateUpdate(float deltaTime)` — calls the late update on the entity
  - Calls `LateUpdate` on all `IEntityLateUpdate` behaviours
  - Triggers `OnLateUpdated` event
  - Can be invoked only if entity is enabled


- `void Disable()` — disables the entity for updates
    - Transitions an entity to `not Enabled` state
    - Calls `Disable` on all behaviours implementing `IEntityDisable`
    - Triggers `OnDisabled` event
    - If the entity is not enabled yet, this method does nothing


- `void Dispose()` – cleans up all resources used by the entity.
    - Transitions an entity to  `not Initialized` state
    - Calls `Dispose` on all behaviours implementing `IEntityDispose`
    - Clears all tags, values, and behaviours
    - Unsubscribes from all events
    - Unregisters from EntityRegistry
    - If the entity is not initialized yet, this method doesn't call `IEntityDispose.Dispose` and `OnDisposed`
    - If the entity is enabled, this method call `Entity.Disable` automatically

---

## Notes

- **Reactive Programming** – Entities support reactive patterns through the `OnStateChanged` event
- **Separation of Concerns** – Interface focuses on entity contract, not implementation
- **Flexibility** – Can be implemented by both pure C# classes and Unity MonoBehaviours
- **Testability** – Interface-based design enables easy mocking and testing