# 🧩 ValueEntityTrigger

```csharp
public class ValueEntityTrigger : ValueEntityTrigger<IEntity>
```

- **Description:** A non-generic shortcut for [ValueEntityTrigger\<E>](ValueEntityTrigger%601.md).  
  Provides value-based tracking for general [IEntity](../Entities/IEntity.md) instances, including reactions to value additions, removals, and changes.
- **Inheritance:** [ValueEntityTrigger\<E>](ValueEntityTrigger%601.md)

---

## 🏗️ Constructor

```csharp
public ValueEntityTrigger(bool added = true, bool deleted = true, bool changed = true)
```

- **Description:** Creates a new non-generic `ValueEntityTrigger` instance with configurable listening behavior.
- **Parameters:**
  - `added` — Whether to react to value additions via the `OnValueAdded` event. Default is `true`.
  - `deleted` — Whether to react to value removals via the `OnValueDeleted` event. Default is `true`.
  - `changed` — Whether to react to value modifications via the `OnValueChanged` event. Default is `true`.

---

## 🏹 Methods

#### `SetAction(Action<IEntity>)`

```csharp
public void SetAction(Action<IEntity> action);
```

- **Description:** Sets the callback action that will be invoked whenever a tracked entity’s values change.
- **Parameter:** `action` — The delegate to invoke on the entity. Cannot be `null`.
- **Exception:** `ArgumentNullException` — Thrown if `action` is `null`.

#### `Track(IEntity)`

```csharp
public void Track(IEntity entity);
```

- **Description:** Starts tracking the specified entity for value changes.
- **Parameter:** `entity` — The entity to track.
- **Note:** Subscribes to `OnValueAdded`, `OnValueDeleted`, and/or `OnValueChanged` events depending on constructor parameters.

#### `Untrack(IEntity)`

```csharp
public void Untrack(IEntity entity);
```

- **Description:** Stops tracking the specified entity for value changes.
- **Parameter:** `entity` — The entity to stop monitoring.
- **Note:** Unsubscribes from previously subscribed value events.

---

## 🗂 Example of Usage

```csharp
// Track general entities for value additions, deletions, and modifications
var trigger = new ValueEntityTrigger(
    added: true,
    deleted: true,
    changed: true
);

// Usage with non-generic EntityFilter
var filter = new EntityFilter(
    allEntities,
    e => e.GetValue<int>("Health") > 0,
    trigger
);
```



<!--

# 🧩 ValueEntityTrigger

---


---


## Usage Examples

### Basic Value Change Monitoring

```csharp
// Monitor all tag changes (additions and deletions)
var valueTrigger = new ValueEntityTrigger();
valueTrigger.SetAction(entity =>
    Debug.Log($"Value change detected on entity: {entity.Name}"));

// Track entities
valueTrigger.Track(playerEntity);
valueTrigger.Track(enemyEntity);

// When values change on entities, the trigger automatically responds
playerEntity.AddValue("Inventory", new Inventory());      // Triggers callback
enemyEntity.RemoveValue("Inventory");   // Triggers callback
enemyEntity.SetValue("Inventory", new Inventory());   // Triggers callback
```

### Using Generic Value Change Monitoring
```csharp
// Monitor all tag changes (additions and deletions)
var valueTrigger = new ValueEntityTrigger<UnitEntity>();
valueTrigger.SetAction(entity =>
    Debug.Log($"Value change detected on entity: {entity.Name}"));

// Track entities
valueTrigger.Track(playerEntity);
valueTrigger.Track(enemyEntity);

// When values change on entities, the trigger automatically responds
playerEntity.AddValue("Inventory", new Inventory());      // Triggers callback
enemyEntity.RemoveValue("Inventory");   // Triggers callback
enemyEntity.SetValue("Health", 25);   // Triggers callback if "Health" value is alreay added!
```
### Addition-Only Value Monitoring

```csharp
// Monitor only when values are added
var additionTrigger = new ValueEntityTrigger(added: true, deleted: false, changed: false);
additionTrigger.SetAction(entity => 
    Debug.Log($"New tag added to entity: {entity.Name}"));

// This will only trigger when values are added, not removed
additionTrigger.Track(playerEntity);

// When values added on entities, the trigger automatically responds
playerEntity.AddValue("Inventory", new Inventory());      // Triggers callback
```

### Deletion-Only Value Monitoring

```csharp
// Monitor only when values are removed
var deletionTrigger = new ValueEntityTrigger(added: false, deleted: true, changed: false);
deletionTrigger.SetAction(entity =>
    Debug.Log($"Value removed from entity: {entity.Name}"));

deletionTrigger.Track(npcEntity);

// When values removed from entities, the trigger automatically responds
enemyEntity.RemoveValue("Inventory");   // Triggers callback
```

### Changing-Only Value Monitoring

```csharp
// Monitor only when values are removed
var deletionTrigger = new ValueEntityTrigger(added: false, deleted: false, changed: true);
deletionTrigger.SetAction(entity =>
    Debug.Log($"Value removed from entity: {entity.Name}"));

deletionTrigger.Track(npcEntity);

// When values removed from entities, the trigger automatically responds
enemyEntity.SetValue("Health", 10);   // Triggers callback if "Health" value is already added
```

---

-->