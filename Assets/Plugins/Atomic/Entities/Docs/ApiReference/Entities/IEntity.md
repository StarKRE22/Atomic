# 🧩️ IEntity

`IEntity` represents an entity following the **Entity–State–Behaviour** pattern.  
It provides a modular architecture for composing logic, storing dynamic state, managing tags, and handling behaviours throughout the entity lifecycle.

---

## Overview

An `IEntity` encapsulates:

- A **key-value data store** for dynamic state.
- **Tag-based identifiers** for categorization and filtering.
- A collection of **IEntityBehaviour** components that define runtime behaviour.
- **Lifecycle management**: initialization, enable, update, disable, dispose.

Behaviours are automatically invoked during lifecycle events via interfaces such as:
`IEntityInit`, `IEntityEnable`, `IEntityUpdate`, and others.

`IEntity` also inherits:

- `IInitSource` – supports explicit initialization and disposal.
- `IEnableSource` – supports enabling and disabling at runtime.
- `IUpdateSource` – supports Update, FixedUpdate, and LateUpdate callbacks.

---

## Properties

- `int InstanceID` – runtime-generated unique identifier, valid only during runtime.
- `string Name` – optional user-defined name, useful for debugging or editor tooling.
- `int BehaviourCount` – number of behaviours attached.
- `int TagCount` – number of tags associated.
- `int ValueCount` – number of stored values.

---

## Events

### State Events

- `event Action OnStateChanged` – triggered when the entity’s internal state changes.

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