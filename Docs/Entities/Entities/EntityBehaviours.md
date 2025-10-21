# 🧩 Entity Behaviours

Manage modular logic attached to the entity. Behaviours can be added, removed, queried, or enumerated at runtime. This
allows flexible composition of entity
logic, enabling dynamic functionality without changing the core entity structure.

> [!IMPORTANT]
> For behaviours entity acts as a container using a **List**, which means that all algorithmic operations have
> **List-like time complexity**.
> Additionally, the entity **can store multiple references to the same behaviour instance**,
> so duplicate entries are allowed.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Basic Usage](#ex1)
    - [Using Extension Methods](#ex2)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - <details>
      <summary><a href="#-events">Events</a></summary>

        - [OnBehaviourAdded](#onbehaviouradded)
        - [OnBehaviourDeleted](#onbehaviourdeleted)

      </details>
    - <details>
      <summary><a href="#-properties">Properties</a></summary>

        - [BehaviourCount](#behaviourcount)

      </details>
    - <details>
      <summary><a href="#-methods">Methods</a></summary>

        - [AddBehaviour(IEntityBehaviour)](#addbehaviourientitybehaviour)
        - [GetBehaviour&lt;T&gt;()](#getbehaviourt)
        - [GetBehaviourAt(int)](#getbehaviouratint)
        - [TryGetBehaviour&lt;T&gt;(out T)](#trygetbehaviourtout-t)
        - [HasBehaviour(IEntityBehaviour)](#hasbehaviourientitybehaviour)
        - [HasBehaviour&lt;T&gt;()](#hasbehaviourt)
        - [DelBehaviour(IEntityBehaviour)](#delbehaviourientitybehaviour)
        - [DelBehaviour&lt;T&gt;()](#delbehaviourt)
        - [DelBehaviours&lt;T&gt;()](#delbehaviourst)
        - [ClearBehaviours()](#clearbehaviours)
        - [GetBehaviours()](#getbehaviours)
        - [GetBehaviours&lt;T&gt;()](#getbehaviourst)
        - [CopyBehaviours(IEntityBehaviour[])](#copybehavioursientitybehaviour)
        - [CopyBehaviours&lt;T&gt;(T[])](#copybehaviourstt)
        - [GetBehaviourEnumerator()](#getbehaviourcenumerator)

      </details>

---

## 🗂 Examples of Usage

Below are examples of working with behaviours in the entity.

<div id="ex1"></div>

### 1️⃣ Basic Usage

```csharp
// Create a new instance of entity
Entity entity = new Entity();

// Subscribe to events
player.OnBehaviourAdded += (e, b) => 
    Console.WriteLine($"Behaviour {b.GetType().Name} added to {e.Id}");

player.OnBehaviourDeleted += (e, b) => 
    Console.WriteLine($"Behaviour {b.GetType().Name} removed from {e.Id}");

// Add behaviours
player.AddBehaviour(new MovementBehaviour());
player.AddBehaviour(new RotationBehaviour());

// Check count
Console.WriteLine($"Total behaviours: {player.BehaviourCount}");

// Retrieve behaviour by type
MovementBehaviour movementBehaviour = player.GetBehaviour<MovementBehaviour>();

// Try to retrieve behaviour by type
if (player.TryGetBehaviour<RotationBehaviour>(out var rotation))
    Console.WriteLine("Found RotationBehaviour");

// Remove behaviour
player.DelBehaviour<MovementBehaviour>();

// Clear all behaviours
player.ClearBehaviours();

// Enumerate all behaviours
foreach (IEntityBehaviour behaviour in player.GetBehaviourEnumerator())
    Console.WriteLine($"Behaviour: {behaviour.GetType().Name}");

// Get array of behaviours
IEntityBehaviour[] behaviours = player.GetBehaviours();

// Copy to array
IEntityBehaviour[] buffer = new IEntityBehaviour[10];
int copied = player.CopyBehaviours(buffer);

Console.WriteLine($"Copied {copied} behaviours into buffer");
```

<div id="ex2"></div>

### 2️⃣ Using Extension Methods

The framework also provides [extension methods](ExtensionsBehaviours.md) for convenient handling of behaviours.

```csharp
// Create a new instance of entity
Entity entity = new Entity();

// Add behaviour by type (using new T())
enemy.AddBehaviour<MoveBehaviour>();

// Add multiple behaviours at once
var attackBehaviour = new AttackBehaviour();
var defenseBehaviour = new DefenseBehaviour();

enemy.AddBehaviours(new IEntityBehaviour[] {
    attackBehaviour, defenseBehaviour
});

// Remove multiple behaviours at once
enemy.DelBehaviours(new IEntityBehaviour[] {
    attackBehaviour, defenseBehaviour
});
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public partial class Entity
```

---

### ⚡ Events

#### `OnBehaviourAdded`

```csharp
public event Action<IEntity, IEntityBehaviour> OnBehaviourAdded  
```

- **Description:** Triggered when a behaviour is added to the entity.
- **Parameters:**
    - `IEntity` – The entity where the behaviour was added.
    - `IEntityBehaviour` – The behaviour that was added.
- **Note:** Allows subscribers to react whenever a new behaviour is attached.

#### `OnBehaviourDeleted`

```csharp
public event Action<IEntity, IEntityBehaviour> OnBehaviourDeleted  
```

- **Description:** Triggered when a behaviour is removed from the entity.
- **Parameters:**
    - `IEntity` – The entity where the behaviour was removed.
    - `IEntityBehaviour` – The behaviour that was removed.
- **Note:** Useful for cleanup or reactive updates when behaviours are detached.

---

### 🔑 Properties

#### `BehaviourCount`

```csharp
public int BehaviourCount { get; }  
```

- **Description:** Number of behaviours currently attached to the entity.
- **Note:** Provides a quick way to check how many behaviours are associated with this entity.

---

### 🏹 Methods

#### `AddBehaviour(IEntityBehaviour)`

```csharp
public void AddBehaviour(IEntityBehaviour behaviour)  
```

- **Description:** Adds a behaviour to the entity.
- **Parameters:** `behaviour` – The behaviour instance to attach.
- **Triggers:** `OnBehaviourAdded` and `OnStateChanged`.
- **Exceptions:** Throws if `behaviour` is null.
- **Note:** Allows to add existing behaviours.

#### `GetBehaviour<T>()`

```csharp
public T GetBehaviour<T>() where T : IEntityBehaviour  
```

- **Description:** Gets the first behaviour of the specified type.
- **Returns:** The first attached behaviour of type `T`.
- **Exceptions:** Throws if no behaviour of type `T` exists.

#### `GetBehaviourAt(int)`

```csharp
public IEntityBehaviour GetBehaviour(int index)  
```

- **Description:** Returns the behaviour instance at the given index.
- **Parameters:** `index` – The zero-based index of the behaviour.
- **Returns:** The behaviour at the specified index.
- **Exceptions:** Throws if `index` is out of range.

#### `TryGetBehaviour<T>(out T)`

```csharp
public bool TryGetBehaviour<T>(out T behaviour) where T : IEntityBehaviour  
```

- **Description:** Tries to get a behaviour of the specified type.
- **Parameters:** `out behaviour` – Output parameter for the behaviour.
- **Returns:** `true` if a behaviour of type `T` exists, otherwise `false`.

#### `HasBehaviour(IEntityBehaviour)`

```csharp
public bool HasBehaviour(IEntityBehaviour behaviour)  
```

- **Description:** Checks if a specific behaviour exists.
- **Parameters:** `behaviour` – The behaviour instance to check.
- **Returns:** `true` if the behaviour is attached, otherwise `false`.

#### `HasBehaviour<T>()`

```csharp
public bool HasBehaviour<T>() where T : IEntityBehaviour  
```

- **Description:** Checks if a behaviour of the specified type exists.
- **Returns:** `true` if any behaviour of type `T` is attached, otherwise `false`.

#### `DelBehaviour(IEntityBehaviour)`

```csharp
public bool DelBehaviour(IEntityBehaviour behaviour)  
```

- **Description:** Removes a specific behaviour.
- **Parameters:** `behaviour` – The behaviour to remove.
- **Returns:** `true` if the behaviour existed and was removed, otherwise `false`.
- **Triggers:** `OnBehaviourDeleted` and `OnStateChanged`.

#### `DelBehaviour<T>()`

```csharp
public bool DelBehaviour<T>() where T : IEntityBehaviour  
```

- **Description:** Removes a behaviour of the specified type.
- **Returns:** `true` if a behaviour of type `T` was removed, otherwise `false`.
- **Triggers:** `OnBehaviourDeleted` and `OnStateChanged`.

#### `DelBehaviours<T>()`

```csharp
public void DelBehaviours<T>() where T : IEntityBehaviour  
```

- **Description:** Removes all behaviours of the specified type.
- **Triggers:** `OnBehaviourDeleted` and `OnStateChanged` for each removed behaviour.

#### `ClearBehaviours()`

```csharp
public void ClearBehaviours()  
```

- **Description:** Clears all behaviours from the entity.
- **Triggers:** `OnBehaviourDeleted` and `OnStateChanged` for each removed behaviour.

#### `GetBehaviours()`

```csharp
public IEntityBehaviour[] GetBehaviours()  
```

- **Description:** Returns all behaviours attached to the entity.
- **Returns:** Array of all behaviours.

#### `GetBehaviours<T>()`

```csharp
public T[] GetBehaviours<T>() where T : IEntityBehaviour  
```

- **Description:** Returns all behaviours of type `T` attached to the entity.
- **Returns:** Array of behaviours of type `T`.

#### `CopyBehaviours(IEntityBehaviour[])`

```csharp
public int CopyBehaviours(IEntityBehaviour[] results)  
```

- **Description:** Copies all behaviours into the provided array.
- **Parameters:** `results` – Array to copy behaviours into.
- **Returns:** Number of behaviours copied.
- **Exceptions:** Throws if `results` is null or too small.

#### `CopyBehaviours<T>(T[])`

```csharp
public int CopyBehaviours<T>(T[] results) where T : IEntityBehaviour  
```

- **Description:** Copies behaviours of type `T` into the provided array.
- **Parameters:** `results` – Array to copy behaviours into.
- **Returns:** Number of behaviours copied.
- **Exceptions:** Throws if `results` is null or too small.

#### `GetBehaviourEnumerator()`

```csharp
public BehaviourEnumerator GetBehaviourEnumerator()  
```

- **Description:** Enumerates all behaviours attached to the entity.
- **Returns:** Struct enumerator for iterating through behaviours.