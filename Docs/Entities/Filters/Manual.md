# 🧩 Entity Filters

**Entity Filters** are core tools for **reactive entity management**. They allow observing and
filtering [IEntity](../Entities/IEntity.md) collections based on conditions, tags, values, behaviours, state changes, or
custom logic. Filter provides a **dynamic, observable view** over an existing entity collection, automatically keeping
track of entities that match a predicate.

- [EntityFilter](EntityFilter.md) <!-- + -->
- [EntityFilter\<E>](EntityFilter%601.md) <!-- + -->

---

## 🗂 Examples of Usage

### 1️⃣ Basic Filter Creation

```csharp
// Create entities
var entity1 = new Entity();
entity1.AddTag("Unit");

var entity2 = new Entity();
entity2.AddTag("Unit");

var entity3 = new Entity();
entity2.AddTag("Building");

// Create a source collection
var allEntities = new EntityCollection();

// Add some entities to the source
allEntities.Add(entity1);
allEntities.Add(entity2);
allEntities.Add(entity3);

// Define a simple predicate
Predicate<IEntity> unitPredicate = entity => entity.HasTag("Unit");

// Create a basic filter
var unitFilter = new EntityFilter(
    allEntities,
    unitPredicate
);

// Access filtered entities
foreach (IEntity entity in unitFilter)
    Console.WriteLine($"Unit: {entity}");

//Assert
Assert.IsTrue(unitFilter.Contains(entity1));
Assert.IsTrue(unitFilter.Contains(entity2));
Assert.IsFalse(unitFilter.Contains(entity3));
```

### 2️⃣ Filter with Tag Trigger

```csharp
var entity1 = new Entity();
entity1.AddTag("Player");

var entity2 = new Entity();
entity2.AddTag("Player");

var entity3 = new Entity();
entity3.AddTag("Enemy");

// Create a source collection
var entityCollection = new EntityCollection();

// Add some entities to the source
entityCollection.Add(entity1);
entityCollection.Add(entity2);
entityCollection.Add(entity3);

// Create a basic filter
var playerFilter = new EntityFilter(
    entityCollection,
    e => e.HasTag("Player"),
    new TagEntityTrigger()
);

//Assert
Assert.IsTrue(playerFilter.Contains(entity1));
Assert.IsTrue(playerFilter.Contains(entity2));
Assert.IsFalse(playerFilter.Contains(entity3));

//Add "Player" tag for the entity3
entity3.AddTag("Player");

//Assert
Assert.IsTrue(playerFilter.Contains(entity3));
```

### 3️⃣ Filter with Value Trigger

```csharp
// Create entites
var entity1 = new Entity();
entity1.AddValue("Health", 50);

var entity2 = new Entity();
entity2.AddValue("Health", 10);

var entity3 = new Entity();
entity3.AddValue("Health", 0);

// Create a source collection
var entityCollection = new EntityCollection<IEntity>();

// Add some entities to the source
entityCollection.Add(entity1);
entityCollection.Add(entity2);
entityCollection.Add(entity3);

// Define a simple predicate
Predicate<IEntity> alivePredicate = entity => 
    entity.GetValue<int>("Health") > 0;

// Create a basic filter
var aliveFilter = new EntityFilter(
    entityCollection,
    alivePredicate, 
    new ValueEntityTrigger()
);

//Assert
Assert.IsTrue(aliveFilter.Contains(entity1));
Assert.IsTrue(aliveFilter.Contains(entity2));
Assert.IsFalse(aliveFilter.Contains(entity3));

//Make "entity3" alive
entity3.SetValue("Health", 20);

//Assert
Assert.IsTrue(aliveFilter.Contains(entity3));
```

### 4️⃣ Chained Filtering Example

```csharp
// Create a first filter
var playerUnitsFilter = new EntityFilter(
    sourceCollection,
    entity => entity.HasTag("Player");
);

// Create a second filter using the first one as a source
var buildingsFilter = new EntityFilter(
    playerUnitsFilter, // Using another filter as a source
    entity => entity.HasTag("Building")
);
```

### 5️⃣ Advanced Usage with Events

```csharp
// Create a filter with event handling
var eventFilter = new EntityFilter(
    sourceCollection,
    entity => entity.GetValue<int>("Health") > 0
);

// Handle filter changes
eventFilter.OnStateChanged += () => Console.WriteLine("Filter state changed!");

// Handle specific entity additions/removals
eventFilter.OnAdded += entity => Console.WriteLine($"Added entity with health: {entity.GetValue<int>("Health")}");
eventFilter.OnRemoved += entity => Console.WriteLine($"Removed entity with health: {entity.GetValue<int>("Health")}");

```

<!--

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

# 🧩 IEntityTrigger

The `IEntityTrigger` interface defines a mechanism for monitoring specific aspects of an entity's state and signaling
when an entity should be re-evaluated by a filter. It comes in two forms:

* **Non-generic** version (`IEntityTrigger`) for working with `IEntity`
* **Generic** version (`IEntityTrigger<E>`) for specific entity types

---

## Key Features

### Action-Based Callbacks

- Configurable callback system for re-evaluation notifications
- Generic action support for type-safe entity handling
- Flexible trigger response patterns

### Entity Tracking

- Track/untrack lifecycle for entity monitoring
- Multiple entity support per trigger instance
- Clean resource management patterns

### Type Safety

- Generic interface for specific entity types
- Compile-time type checking for callbacks
- Non-generic convenience interface available

---

## IEntityTrigger

**A shorthand interface for `IEntityTrigger<IEntity>`.**

```csharp
public interface IEntityTrigger : IEntityTrigger<IEntity>
{
}
```

## IEntityTrigger&lt;E&gt;

**A generic interface for tracking specific entity types.**

```csharp
public interface IEntityTrigger<E> where E : IEntity
{
    void SetAction(Action<E> action);
    void Track(E entity);
    void Untrack(E entity);
}
```

---

## Methods

### SetAction

```csharp
void SetAction(Action<E> action);
```

- **Purpose**: Sets the callback for re-evaluation
- **Parameter**:
    - `action` — The action to invoke when re-evaluation is needed
- **Usage**: Defines what happens when a tracked condition is met

### Track

```csharp
void Track(E entity);
```

- **Purpose**: Begins monitoring the specified entity
- **Parameter:**
    - `entity` — The entity to track
- **Behavior**: Starts observing changes relevant to the trigger

### Untrack

```csharp
void Untrack(E entity);
```

- **Purpose:** Stops monitoring the specified entity
- **Parameter:**
    - `entity` — The entity to stop tracking
- **Behavior**: Removes the entity from monitoring

---

## Example Usage

```csharp
//Create a simple tag trigger
public class TagEntityTrigger : IEntityTrigger
{
    private Action<IEntity> _callback;

    public void SetAction(Action<IEntity> action) =>
        _callback = action ?? throw new ArgumentNullException(nameof(action));
    
    public void Track(IEntity entity) => 
        entity.OnTagAdded += _callback.Invoke;
    
    public void Untrack(IEntity entity) =>
         entity.OnTagAdded -= _callback.Invoke;
}
```

## Usage Examples

### Non-Generic Usage

```csharp
public class PlayerTrigger : EntityTriggerBase
{
    public override void Track(IEntity entity)
    {
        // Subscribe to custom events and call "InvokeAction(entity)" in a certain place
    }

    public override void Untrack(IEntity entity)
    {
        // Unsubscribe from custom events
    }
}
```

### Generic Usage

```csharp
public class PlayerTrigger : EntityTriggerBase<PlayerEntity>
{
    public override void Track(PlayerEntity entity)
    {
        // Subscribe to custom events and call "InvokeAction(entity)" in a certain place
    }

    public override void Untrack(PlayerEntity entity)
    {
        // Unsubscribe from custom events
    }
}
```
-->