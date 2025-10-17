# 🧩 TagEntityTrigger

A non-generic shortcut for [TagEntityTrigger\<E>](TagEntityTrigger%601.md). Subscribes to tag-related
events (`OnTagAdded`, `OnTagDeleted`) on general [IEntity](../Entities/IEntity.md) instances.


---

---

## 🗂 Example of Usage

```csharp
// Track general entities for tag additions / removals
var trigger = new TagEntityTrigger(added: true, deleted: true);

// Usage with non-generic EntityFilter
var filter = new EntityFilter(
    allEntities,
    e => e.HasTag("Enemy"),
    trigger
);
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class TagEntityTrigger : TagEntityTrigger<IEntity>
```

- **Inheritance:** [TagEntityTrigger\<E>](TagEntityTrigger%601.md)

---

<div id="-constructor"></div>

### 🏗️ Constructor

```csharp
public TagEntityTrigger(bool added = true, bool deleted = true)
```

- **Description:** Creates a new non-generic `TagEntityTrigger` instance.
- **Parameters:**
    - `added` — Whether to react to tag additions via the `OnTagAdded` event. Default is `true`.
    - `deleted` — Whether to react to tag removals via the `OnTagDeleted` event. Default is `true`.

---

### 🏹 Methods

#### `SetAction(Action<IEntity>)`

```csharp
public void SetAction(Action<IEntity> action);
```

- **Description:** Sets the callback action that will be invoked whenever a tracked entity’s tags change.
- **Parameter:** `action` — The delegate to invoke on the entity. Cannot be `null`.
- **Exception:** `ArgumentNullException` — Thrown if `action` is `null`.

#### `Track(IEntity)`

```csharp
public void Track(IEntity entity);
```

- **Description:** Starts tracking the specified entity for tag changes.
- **Parameter:** `entity` — The entity to track.
- **Note:** Subscribes to `OnTagAdded` and/or `OnTagDeleted` events depending on constructor parameters.

#### `Untrack(IEntity)`

```csharp
public void Untrack(IEntity entity);
```

- **Description:** Stops tracking the specified entity for tag changes.
- **Parameter:** `entity` — The entity to stop monitoring.
- **Note:** Unsubscribes from previously subscribed tag events.

---

## 📝 Notes

### Selective Monitoring

- Configurable tracking of tag additions only
- Configurable tracking of tag deletions only
- Full monitoring of both operations by default

### Automatic Event Management

- Automatic subscription to entity tag events
- Clean unsubscription for resource management
- Type-safe entity casting in callbacks

### Lightweight Operation

- Minimal overhead per tracked entity
- Event-driven architecture avoids polling
- Efficient for high-frequency tag changes

<!--

## Usage Examples

### Basic Tag Change Monitoring

```csharp
// Monitor all tag changes (additions and deletions)
var tagTrigger = new TagEntityTrigger();
tagTrigger.SetAction(entity =>
    Debug.Log($"Tag change detected on entity: {entity.Name}"));

// Track entities
tagTrigger.Track(playerEntity);
tagTrigger.Track(enemyEntity);

// When tags change on entities, the trigger automatically responds
playerEntity.AddTag("PoweredUp");      // Triggers callback
enemyEntity.RemoveTag("Aggressive");   // Triggers callback
```

### Addition-Only Tag Monitoring

```csharp
// Monitor only when tags are added
var additionTrigger = new TagEntityTrigger(added: true, deleted: false);
additionTrigger.SetAction(entity => 
    Debug.Log($"New tag added to entity: {entity.Name}"));

// This will only trigger when tags are added, not removed
additionTrigger.Track(playerEntity);

// When tags added on entities, the trigger automatically responds
playerEntity.AddTag("PoweredUp");      // Triggers callback
```

### Deletion-Only Tag Monitoring

```csharp
// Monitor only when tags are removed
var deletionTrigger = new TagEntityTrigger(added: false, deleted: true);
deletionTrigger.SetAction(entity =>
    Debug.Log($"Tag removed from entity: {entity.Name}"));

deletionTrigger.Track(npcEntity);

// When tags removed from entities, the trigger automatically responds
playerEntity.DelTag("PoweredUp");      // Triggers callback
```

-->
