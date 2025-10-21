# 🧩 Entity Core

Represents the fundamental identity and state of the entity. It includes unique identifiers, optional names for
debugging or tooling, and the main event for reactive state changes.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Basic Usage](#ex1)
    - [String-keyed Constructor](#ex2)
    - [Capacity-based Constructor](#ex3)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [String-keyed Constructor](#string-keyed-constructor)
        - [Int-keyed Constructor](#int-keyed-constructor)
        - [Capacity-based Constructor](#capacity-based-constructor)
    - [Events](#-events)
        - [OnStateChanged](#onstatechanged)
    - [Properties](#-properties)
        - [InstanceID](#instanceid)
        - [Name](#name)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

#### 1️⃣ Basic Usage

```csharp
// Create a new instance of entity
Entity entity = new Entity();

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

<div id="ex2"></div>

#### 2️⃣ String-keyed Constructor

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

<div id="ex3"></div>

#### 3️⃣ Capacity-based Constructor

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

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public partial class Entity
```

---

<div id="-constructors"></div>

### 🏗️ Constructors

Below are represent three ways of creating entity. When an entity is created through its constructor, it automatically registers itself in a special registry that stores
all entities — [EntityRegistry](../Registry/EntityRegistry.md)

#### `String-keyed Constructor`

```csharp
public Entity(
    string name,
    IEnumerable<string> tags,
    IEnumerable<KeyValuePair<string, object>> values,
    IEnumerable<IEntityBehaviour> behaviours,
    Settings? settings = null
) 
```

- **Description:** Creates a new entity with the specified name, string tags, values, and behaviours. Initializes
  internal capacities and immediately adds all specified tags, values, and behaviours.
- **Parameters:**
    - `name` – The name of the entity. If `null`, an empty string is used.
    - `tags` – Optional collection of string tag identifiers.
    - `values` – Optional collection of key-value pairs.
    - `behaviours` – Optional collection of behaviours to attach.
    - `settings` – Optional entity settings. If `null`, `Settings.disposeValues` defaults to `true`.

#### `Int-keyed Constructor`

```csharp
public Entity(
    string name,
    IEnumerable<int> tags,
    IEnumerable<KeyValuePair<int, object>> values,
    IEnumerable<IEntityBehaviour> behaviours,
    Settings? settings = null
)
```

- **Description:** Creates a new entity with the specified name, integer tags, values, and behaviours. Initializes
  internal capacities and immediately adds all specified tags, values, and behaviours.
- **Parameters:**
    - `name` – The name of the entity. If `null`, an empty string is used.
    - `tags` – Optional collection of integer tag identifiers.
    - `values` – Optional collection of key-value pairs with integer keys.
    - `behaviours` – Optional collection of behaviours to attach.
    - `settings` – Optional entity settings. If `null`, `Settings.disposeValues` defaults to `true`.

#### `Capacity-based Constructor`

```csharp
public Entity(
    string name = null,
    int tagCapacity = 0,
    int valueCapacity = 0,
    int behaviourCapacity = 0,
    Settings? settings = null
) 
```

- **Description:** Creates a new entity with the specified name and initial capacities for tags, values, and behaviours.
  Initializes internal structures efficiently.
- **Parameters:**
    - `name` – The name of the entity. If `null`, an empty string is used.
    - `tagCapacity` – Initial capacity for tag storage to minimize memory allocations.
    - `valueCapacity` – Initial capacity for value storage to minimize memory allocations.
    - `behaviourCapacity` – Initial capacity for behaviour storage to minimize memory allocations.
    - `settings` – Optional entity settings. If `null`, `Settings.disposeValues` defaults to `true`.

---

### ⚡ Events

#### `OnStateChanged`

```csharp
public event Action<IEntity> OnStateChanged
```

- **Description:** Triggered whenever the entity’s internal state changes.
- **Parameter:** `IEntity` – This entity.
- **Note:** Useful for reacting to lifecycle or state transitions of an entity.

---

### 🔑 Properties

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