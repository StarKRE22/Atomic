# 🧩 TagEntityTrigger

A specialized entity trigger that monitors tag-related changes on entities, responding to tag additions and deletions. Provides reactive monitoring for entity tag state changes, enabling automatic system responses to entity classification updates.

## Overview
`TagEntityTrigger` automatically tracks when tags are added to or removed from entities, triggering callbacks for reactive system updates. Essential for building systems that respond to entity state classification changes, role updates, or behavioral flag modifications.

---
## Key Features

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

---

## TagEntityTrigger

- A shortcut for `TagEntityTrigger<IEntity>`
- Simplifies working with basic `IEntity` instances
- Automatically handles tag-related events

```csharp
public class TagEntityTrigger : TagEntityTrigger<IEntity>
{
}
```

#### Constructor
```csharp
public TagEntityTrigger(bool added = true, bool deleted = true)
```
- **Parameters:**
  - `added` — Enables tracking of tag additions (default: true)
  - `deleted` — Enables tracking of tag removals (default: true)

## TagEntityTrigger&lt;E&gt;

- Generic version for specific entity types
- Inherits from `EntityTriggerBase<E>`
- Requires entities to implement `IEntity`

```csharp
public class TagEntityTrigger<E> : EntityTriggerBase<E> where E : IEntity
{
}
```

#### Constructor
```csharp
public TagEntityTrigger(bool added = true, bool deleted = true)
```
- **Parameters:**
    - `added` — Enables tracking of tag additions (default: true)
    - `deleted` — Enables tracking of tag removals (default: true)

## Methods

### Track
```csharp
public override void Track(E entity)
```
- **Functionality:**
  - Subscribes to `OnTagAdded` if `added` is true
  - Subscribes to `OnTagDeleted` if `deleted` is true
  - Sets up event listeners for tag changes

### Untrack
```csharp
public override void Untrack(E entity)
```
- **Functionality:**
  - Unsubscribes from `OnTagAdded` if previously subscribed
  - Unsubscribes from `OnTagDeleted` if previously subscribed
  - Cleans up event listeners

---

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

### Using Generic Tag Change Monitoring
```csharp
// Monitor all tag changes (additions and deletions)
var tagTrigger = new TagEntityTrigger<UnitEntity>();
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

---

## Best Practices

[//]: # (### Monitoring Configuration)

[//]: # (- Use specific monitoring &#40;added/deleted only&#41; when possible)

[//]: # (- Consider system requirements for trigger frequency)

[//]: # (- Balance between reactivity and performance)

[//]: # ()
[//]: # (### Integration Patterns)

[//]: # (- Combine with other trigger types for complex conditions)

[//]: # (- Use in conjunction with entity filters for reactive systems)

[//]: # (- Implement proper cleanup in disposal patterns)

[//]: # ()
[//]: # (### Event Handler Design)

[//]: # (- Keep trigger callbacks lightweight and fast)

[//]: # (- Avoid heavy operations in immediate callback execution)

[//]: # (- Consider queuing heavy operations for batch processing)

[//]: # (The `TagEntityTrigger` provides efficient, reactive monitoring of entity tag changes, enabling sophisticated tag-based systems and automatic responses to entity state classification updates within the Atomic framework.)