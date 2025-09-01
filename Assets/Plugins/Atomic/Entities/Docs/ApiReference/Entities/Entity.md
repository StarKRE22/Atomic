# Entity

Represents the core implementation of an `IEntity`.  
This class follows the Entity–State–Behaviour pattern, providing a modular container for dynamic state, tags, values, behaviours, and lifecycle management.

## Key Features

- **Complete Implementation** – Full IEntity interface implementation
- **Lifecycle Management** – Built-in spawn, activate, update, despawn support
- **Dynamic Composition** – Runtime attachment of behaviours
- **Event System** – Comprehensive event notifications
- **Registry Integration** – Automatic registration with EntityRegistry
- **Memory Efficient** – Pre-allocation support for collections


---


## Constructors

#### Creates a new entity with the specified name, tags, values, behaviours, and optional settings.
```csharp
Entity(
    string name,
    IEnumerable<string> tags = null,
    IEnumerable<KeyValuePair<string, object>> values = null,
    IEnumerable<IEntityBehaviour> behaviours = null,
    Settings? settings = null
)
```
### Parameters

- `name` — Entity name.
- `tags` — Optional initial tags.
- `values` — Optional initial values.
- `behaviours` — Optional initial behaviours.
- `settings` — Optional settings (disposeValues defaults to true).
----

#### Creates a new entity with the specified name, tags, values, behaviours, and optional settings.
```csharp
Entity(
    string name,
    IEnumerable<int> tags = null,
    IEnumerable<KeyValuePair<int, object>> values = null,
    IEnumerable<IEntityBehaviour> behaviours = null,
    Settings? settings = null
)
```
### Parameters

- `name` — Entity name.
- `tags` — Optional initial tags.
- `values` — Optional initial values.
- `behaviours` — Optional initial behaviours.
- `settings` — Optional settings (disposeValues defaults to true).

---
#### Creates a new entity with the specified name and initial capacities for tags, values, and behaviours.
```csharp
Entity(
    string name = null,
    int tagCapacity = 0,
    int valueCapacity = 0,
    int behaviourCapacity = 0,
    Settings? settings = null
)
```
### Parameters

- `name` — Entity name.
- `tagCapacity` — Initial capacity for tags.
- `valueCapacity` — Initial capacity for values.
- `behaviourCapacity` — Initial capacity for behaviours.
- `settings` — Optional settings (disposeValues defaults to true).

---

## Events

### Lifecycle Events
- `OnStateChanged` — Triggered whenever the entity's state changes.
- `OnInitialized` — Triggered when the entity is initialized.
- `OnDisposed` — Triggered when the entity is disposed.
- `OnEnabled` — Triggered when the entity is enabled.
- `OnDisabled` — Triggered when the entity is disabled.
- `OnUpdated(float deltaTime)` — Triggered on each update while the entity is enabled.
- `OnFixedUpdated(float deltaTime)` — Triggered on each fixed update while the entity is enabled.
- `OnLateUpdated(float deltaTime)` — Triggered on each late update while the entity is enabled.

### Behaviours Events
- `OnBehaviourAdded(IEntityBehaviour behaviour)` — Triggered when a new behaviour is added.
- `OnBehaviourDeleted(IEntityBehaviour behaviour)` — Triggered when a behaviour is removed.

### Tags Events
- `OnTagAdded(int key)` — Triggered when a tag is added.
- `OnTagDeleted(int key)` — Triggered when a tag is removed.

### Values Events
- `OnValueAdded(int key)` — Triggered when a value is added.
- `OnValueDeleted(int key)` — Triggered when a value is removed.
- `OnValueChanged(int key)` — Triggered when a value is changed.

---

## Properties

- `int InstanceID` — Unique identifier for this entity instance.
- `string Name` — The entity's name.
- `bool Initialized` — Indicates if the entity has been initialized.
- `bool Enabled` — Indicates if the entity is currently enabled.
- `int BehaviourCount` — Total number of attached behaviours.
- `int TagCount` — Total number of tags.
- `int ValueCount` — Total number of values.

---

## Tags Management

- `bool HasTag(int key)` — Checks if the entity has a specific tag.
- `bool AddTag(int key)` — Adds a tag.
- `bool DelTag(int key)` — Deletes a tag.
- `void ClearTags()` — Clears all tags.
- `int[] GetTags()` — Returns all tags as an array.
- `int CopyTags(int[] results)` — Copies tags into the provided array.
- `TagEnumerator GetTagEnumerator()` — Returns an enumerator for iterating tags.

## Values Management

- `bool HasValue(int key)` — Checks if a value exists.
- `T GetValue<T>(int key)` — Gets the value of type `T`.
- `object GetValue(int key)` — Gets the value as an object.
- `bool TryGetValue<T>(int key, out T value)` — Attempts to get a value of type `T`.
- `bool TryGetValue(int key, out object value)` — Attempts to get a value as object.
- `ref T GetValueUnsafe<T>(int key)` — Returns a reference to a struct value (unsafe, no boxing).
- `bool TryGetValueUnsafe<T>(int key, out T value)` — Tries to get a reference to a struct value.
- `void AddValue<T>(int key, T value)` — Adds a struct value.
- `void AddValue(int key, object value)` — Adds a reference type value.
- `void SetValue<T>(int key, T value)` — Updates or adds a struct value.
- `void SetValue(int key, object value)` — Updates or adds a reference value.
- `bool DelValue(int key)` — Deletes a value by key.
- `void ClearValues()` — Clears all values.
- `KeyValuePair<int, object>[] GetValues()` — Returns all key-value pairs.
- `int CopyValues(KeyValuePair<int, object>[] results)` — Copies all key-value pairs into an array.
- `ValueEnumerator GetValueEnumerator()` — Returns an enumerator for values.

## Behaviours Management

- `bool HasBehaviour(IEntityBehaviour behaviour)` — Checks if a specific behaviour is attached.
- `bool HasBehaviour<T>()` — Checks if a behaviour of type `T` exists.
- `void AddBehaviour(IEntityBehaviour behaviour)` — Adds a behaviour.
- `bool DelBehaviour(IEntityBehaviour behaviour)` — Deletes a behaviour instance.
- `bool DelBehaviour<T>()` — Deletes the first behaviour of type `T`.
- `void DelBehaviours<T>()` — Deletes all behaviours of type `T`.
- `bool DelBehaviourAt(int index)` — Deletes behaviour at a given index.
- `void ClearBehaviours()` — Clears all behaviours.
- `T GetBehaviour<T>()` — Gets the first behaviour of type `T`.
- `bool TryGetBehaviour<T>(out T behaviour)` — Tries to get the first behaviour of type `T`.
- `IEntityBehaviour GetBehaviourAt(int index)` — Gets behaviour at index.
- `IEntityBehaviour[] GetBehaviours()` — Returns all behaviours.
- `T[] GetBehaviours<T>()` — Returns all behaviours of type `T`.
- `int CopyBehaviours(IEntityBehaviour[] results)` — Copies behaviours to array.
- `int CopyBehaviours<T>(T[] results)` — Copies behaviours of type `T` to array.
- `BehaviourEnumerator GetBehaviourEnumerator()` — Returns an enumerator for behaviours.


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
  - Transitions an entity to not `Initialized` state
  - Calls `Dispose` on all behaviours implementing `IEntityDispose`
  - Clears all tags, values, and behaviours
  - Unsubscribes from all events
  - Unregisters from EntityRegistry
  - Disposing stored values if `Settings.disposeValues` is `true`
  - If the entity is enabled, this method call `Entity.Disable` automatically
  - If the entity is not initialized yet, this method doesn't call `IEntityDispose.Dispose` and `OnDisposed`

---

## Nested Types

- `Settings` — Entity configuration (e.g., `disposeValues`).
- `BehaviourEnumerator` — Enumerator for behaviours.
- `TagEnumerator` — Enumerator for tags.
- `ValueEnumerator` — Enumerator for values.

### Debug Properties (Editor Only)

> **Note:** These properties are available only in **Unity Editor** when using **Odin Inspector**.

- `DebugName` — Displays entity name in the Unity Editor.
- `DebugSpawned` — Displays if the entity is initialized.
- `DebugActive` — Displays if the entity is enabled.
- `DebugTags` — Sorted list of tags for debug display.
- `DebugValues` — Sorted list of values for debug display.
- `DebugBehaviours` — Sorted list of attached behaviours for debug display.

## Examples

### Example #1: Creating and setting up an entity

```csharp
var entity = new Entity(
    "Character", 
    tagCapacity: 2, //Optionally precompile memory for tags
    valueCapacity: 2, //Optionally precompile memory for values
    behaviourCapacity: 2, //Optionally precompile memory for behaviours
    new Settings { disposeValues = false } //Optionally don't dispose values when Entity.Dispose()
);

// Add tags
entity.AddTag(EntityNames.NameToId("Moveable"));
entity.AddTag("Damageable"); //There is an extension method

// Add values
entity.AddValue(EntityNames.NameToId("Health"), 100);
entity.AddValue("Speed", 5.5f); //There is an extension method

// Add behaviours
entity.AddBehaviour(new MoveBehaviour());
entity.AddBehaviour<HealthBehaviour>(); //There is an extension method
```

### Example #2. Creating an entity through constructor
```csharp
var entity = new Entity(
    name: "Character",
    tags: new[] { "Moveable", "Damageable" },
    values: new[]
    {
        new KeyValuePair<string, object>("Health", 100),
        new KeyValuePair<string, object>("Speed", 5.5f)
    }, 
    behaviours: new IEntityBehaviour[]
    {
        new MoveBehaviour(),
        new HealthBehaviour()
    }
);
```

### Example #3. Lifecycle management

```csharp
// Create a new entity
var entity = new Entity("Enemy");

// Initialize the entity and attached behaviours
entity.Init();

// Enable the entity (registers update behaviours)
entity.Enable();

// Simulate game loop updates
float deltaTime = 0.016f; // Example: 60 FPS

entity.OnUpdate(deltaTime);       // Calls Update on all IEntityUpdate behaviours
entity.OnFixedUpdate(deltaTime);  // Calls FixedUpdate on all IEntityFixedUpdate behaviours
entity.OnLateUpdate(deltaTime);   // Calls LateUpdate on all IEntityLateUpdate behaviours

// Disable the entity (unregisters update behaviours)
entity.Disable();

// Dispose the entity when it is no longer needed
entity.Dispose();
```