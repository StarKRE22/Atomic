# 🧩 IEntity Core

Represent the fundamental identity and state of the entity. It includes unique identifiers, optional names for
debugging or tooling, and the main event for reactive state changes.

---

## ⚡ Events

#### `OnStateChanged`

```csharp
public event Action<IEntity> OnStateChanged
```

- **Description:** Triggered whenever the entity’s internal state changes.
- **Parameter:** `IEntity` – This entity.
- **Note:** Useful for reacting to lifecycle or state transitions of an entity.

---

## 🔑 Properties

#### `InstanceID`

```csharp
public int InstanceID { get; }
```

- **Description:** Runtime-generated unique identifier.
- **Notes:**
    - Ensures uniqueness of the entity instance during runtime.
    - Should not be used for persistence or serialization.

#### `Name`

```csharp
public string Name { get; set; }
```

- **Description:** Optional user-defined name for debugging or tooling.
- **Note:** Useful for logging, inspector display, or editor tooling.

--- 

## 🗂 Example of Usage

```csharp
// Assume we have instance of entity
IEntity entity = ...

// Subscribe to the OnStateChanged event
entity.OnStateChanged += (IEntity e) =>
{
    Console.WriteLine($"Entity {e.Name} (ID: {e.InstanceID}) changed state!");
};

// Change name
entity.Name = "Hero"; //Triggers state changed

// Read the unique runtime identifier
int id = entity.InstanceID;
Console.WriteLine($"Created entity '{entity.Name}' with ID: {id}");
```