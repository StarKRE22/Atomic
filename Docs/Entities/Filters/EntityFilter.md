


# 🧩 EntityFilter

`EntityFilter` provides a dynamic, observable filtered collection over a source entity collection. It automatically
maintains a subset of entities based on a predicate and optional triggers, updating in real-time as entities change.

## Key Features

* **Dynamic Filtering** — automatically updates as entities change
* **Predicate-Based Filtering** — filters entities using custom logic
* **Chained Filtering** — can use another `EntityFilter` as a data source for nested or complex filtering scenarios
* **Trigger System** — reacts to specific entity changes
* **Observable Events** — provides events for tracking filter changes
* **Lazy Evaluation** — evaluates only when accessed
* **Memory Efficiency** — does not duplicate entity storage
* **Collection support** – Implements `IReadOnlyEntityCollection<IEntity>` for checking, enumeration and copying.

## Classes

### `EntityFilter`

A **non-generic version** of [`EntityFilter<E>`](#) specialized for `IEntity`.  
Use this class when you do not need to specify a particular entity type.

#### Constructor

```csharp
public EntityFilter(
    IReadOnlyEntityCollection<IEntity> source,
    Predicate<IEntity> predicate,
    params IEntityTrigger<IEntity>[] triggers)
    : base(source, predicate, triggers)
{
}
```

| Parameter   | Type                                   | Description                                                                                |
|-------------|----------------------------------------|--------------------------------------------------------------------------------------------|
| `source`    | `IReadOnlyEntityCollection<IEntity>`   | The source collection of entities to observe and filter.                                   |
| `predicate` | `Predicate<IEntity>`                   | The predicate function that determines whether an entity should be included in the filter. |
| `triggers`  | `IEntityTrigger<IEntity>[] (optional)` | Optional triggers for dynamic change tracking. Can be zero or more triggers.               |

---

### `EntityFilter<E>`

A **generic class** representing a filter that automatically maintains an entity subset of type `E`.

#### Type Parameters

- `E` – The type of entity managed by this filter. Must implement [`IEntity`](#).

#### Constructor

```csharp
public EntityFilter(
    IReadOnlyEntityCollection<E> source,
    Predicate<E> predicate,
    params IEntityTrigger<E>[] triggers)
    : base(source, predicate, triggers)
{
}
```

| Parameter   | Type                             | Description                                                                                |
|-------------|----------------------------------|--------------------------------------------------------------------------------------------|
| `source`    | `IReadOnlyEntityCollection<E>`   | The source collection of entities to observe and filter.                                   |
| `predicate` | `Predicate<E>`                   | The predicate function that determines whether an entity should be included in the filter. |
| `triggers`  | `IEntityTrigger<E>[] (optional)` | Optional triggers for dynamic change tracking. Can be zero or more triggers.               |

---

## Events

| Event            | Parameters | Description                                |
|------------------|------------|--------------------------------------------|
| `OnStateChanged` | —          | Raised when entities are added or removed. |
| `OnAdded`        | `E entity` | Raised when an entity is added.            |
| `OnRemoved`      | `E entity` | Raised when an entity is removed.          |

## Properties

| Property | Type  | Description                      |
|----------|-------|----------------------------------|
| `Count`  | `int` | Count of entities in the filter. |

## Methods

| Method                              | Parameters                                             | Description                           |
|-------------------------------------|--------------------------------------------------------|---------------------------------------|
| `Contains(E entity)`                | `entity` – Entity to check                             | Returns `true` if entity exists.      |
| `CopyTo(E[] array, int arrayIndex)` | `array` — Destination array, `arrayIndex` — startIndex | Copies entities to an array.          |
| `CopyTo(ICollection<E> results)`    | `results`                                              | Copies entities to a collection.      |
| `Dispose()`                         | —                                                      | Disposes the filter and its entities. |






## Best Practices

1. **Reuse Filters** – Create once, use multiple times
2. **Chain Filters** – Use filtered results as source for other filters
3. **Simple Predicates** – Keep predicate logic fast and simple
4. **Appropriate Triggers** – Only use triggers for values that affect filter
5. **Dispose Filters** – Call Dispose() to unsubscribe from events
6. **Cache Results** – Store filter results if used multiple times per frame

## Performance Considerations

- **Lazy Evaluation** – Filter only evaluates when accessed
- **Predicate Cost** – Called for each entity on evaluation
- **Trigger Overhead** – Each trigger adds event subscriptions
- **Memory Efficient** – Doesn't duplicate entity references
- **Re-evaluation Cost** – Full re-filter when triggers fire

## Common Use Cases

- **Combat Targeting** – Find valid targets
- **AI Decision Making** – Filter relevant entities
- **UI Display** – Show filtered entity lists
- **Spatial Queries** – Entities in range
- **State Queries** – Entities with specific states
- **Team Management** – Filter by allegiance

## Trigger Types

### TagEntityTrigger
- Fires when specified tag is added/removed
- Use for state-based filtering

### ValueEntityTrigger
- Fires when specified value changes
- Use for data-based filtering

### SubscriptionEntityTrigger
- Custom trigger with manual control
- Use for complex conditions

