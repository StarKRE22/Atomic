# 🧩 Entity Tags

Manage lightweight categorization and filtering of entities. Tags are integer-based labels that can be added, removed,
enumerated, or checked. They are useful for grouping entities, querying, and driving logic based on assigned tags.

> [!IMPORTANT]
> Tags in the entity behave like a **HashSet of integers**. All operations such as add, check, or remove have **O(1)
> average time complexity**, and duplicate tags are **not allowed**.

---


## 📑 Table of Contents

- [Example of Usage](#-examples-of-usage)
  - [Using Numeric Keys](#ex1)
  - [Using String Names](#ex2)
  - [Using Entity API](#ex3)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - <details>
    <summary><a href="#-events">Events</a></summary>

    - [OnTagAdded](#ontagadded)
    - [OnTagDeleted](#ontagdeleted)

    </details>
  - <details>
    <summary><a href="#-properties">Properties</a></summary>

    - [TagCount](#tagcount)

    </details>
  - <details>
    <summary><a href="#-methods">Methods</a></summary>

    - [HasTag(int)](#hastagint)
    - [AddTag(int)](#addtagint)
    - [DelTag(int)](#deltagint)
    - [ClearTags()](#cleartags)
    - [GetTags()](#gettags)
    - [CopyTags(int[])](#copytagsint)
    - [GetTagEnumerator()](#gettagenumerator)

    </details>
---

## 🗂 Examples of Usage

This example demonstrates how to use tags with entity, including adding, removing, and checking tags. Three
approaches are shown:

<div id="ex1"></div>

### 1️⃣ Using Numeric Keys

By default, all tags use `int` keys because this avoids computing hash codes and is very fast; therefore, the example
below uses numeric keys as the default approach.

```csharp
//Define tag keys
const int Player tag = 1;
const int NPC tag = 2;
const int Ally ally = 3;
const int Merchant ally = 4;

// Create a new instance of entity
Entity entity = new Entity();

// Subscribe to tag events
entity.OnTagAdded += (e, tagId) => 
    Console.WriteLine($"Tag added: {tagId}");
entity.OnTagDeleted += (e, tagId) => 
    Console.WriteLine($"Tag removed: {tagId}");

entity.AddTag(Player);
entity.AddTag(NPC);

// Check tags
if (entity.HasTag(Player)) //Check if  Player tag exists
    Console.WriteLine("Entity has tag ID 1 (Player)");

// Remove a NPC tag
entity.DelTag(NPC);

// Add multiple tags
entity.AddTags(new int[] { Ally, Merchant }); // Ally, Merchant

// Enumerate all tags
foreach (int id in entity.GetTags())
    Console.WriteLine($"Entity tag ID: {id}");
```

---

<div id="ex2"></div>

### 2️⃣ Using String Names

In this example, for convenience, there are [extension methods](ExtensionsTags.md) for the entity. This format is more
user-friendly but slightly slower than using numeric keys.

```csharp
// Create a new instance of entity
Entity entity = new Entity();

// Add tags by string name
entity.AddTag("Player");
entity.AddTag("NPC");

// Check tags
if (entity.HasTag("Player"))
    Console.WriteLine("Entity is a Player");

// Remove a tag
entity.DelTag("NPC");

// Add multiple tags at once
entity.AddTags(new string[] { "Ally", "Merchant" });

// Enumerate all tags (numeric IDs)
foreach (int id in entity.GetTags())
    Console.WriteLine($"Entity tag ID: {id}");
```

---

<div id="ex3"></div>

### 3️⃣ Using Entity API

Sometimes managing tags by raw `int` keys or `string` names can get messy and error-prone, especially in big projects.
To make this process easier and **type-safe**, the Atomic Framework supports **code generation**.
This means you describe all your tags (and values) once in a small config file, and the framework will automatically
generate C# helpers. You can learn more about this in the Manual under
the [Entity API](../EntityAPI/Manual.md) feature.

```csharp
// Create a new instance of entity
Entity entity = new Entity();

// Add tags
entity.AddPlayerTag();
entity.AddNPCTag();

// Check tag
if (entity.HasPlayerTag())
    Console.WriteLine("Entity is a Player");

// Remove a tag
entity.DelNPCTag();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public partial class Entity
```

---

### ⚡ Events

#### `OnTagAdded`

```csharp
public event Action<IEntity, int> OnTagAdded
```

- **Description:** Triggered when a tag is added.
- **Parameters:**
    - `IEntity` — This entity.
    - `int` – The tag that was added.
- **Note:** Useful for reacting to dynamic tagging of entities.

#### `OnTagDeleted`

```csharp
public event Action<IEntity, int> OnTagDeleted
```

- **Description:** Triggered when a tag is removed.
- **Parameters:**
    - `IEntity` — This entity.
    - `int` – The tag that was removed.

- **Note:** Allows cleanup or logic adjustment when tags are deleted.

---

### 🔑 Properties

#### `TagCount`

```csharp
public int TagCount { get; }
```

- **Description:** Number of associated tags.
- **Note:** Reflects how many tags are currently attached to the entity.

---

### 🏹 Methods

#### `HasTag(int)`

```csharp
public bool HasTag(int tag)
```

- **Description:** Checks if the entity has the given tag.
- **Parameter:** `tag` – The tag to check for.
- **Returns:** `true` if the tag exists, otherwise `false`.

#### `AddTag(int)`

```csharp
public bool AddTag(int tag)
```

- **Description:** Adds a tag to the entity.
- **Parameter:** `int tag` – The tag to add.
- **Returns:** `true` if the tag was added, otherwise `false`.
- **Triggers:** `OnTagAdded` and `OnStateChanged`

#### `DelTag(int)`

```csharp
public bool DelTag(int tag)
```

- **Description:** Removes a tag from the entity.
- **Parameter:** `tag` – The tag to remove.
- **Returns:** `true` if the tag was removed, otherwise `false`.
- **Triggers:** `OnTagDeleted` and `OnStateChanged`

#### `ClearTags()`

```csharp
public void ClearTags()
```

- **Description:** Removes all tags from the entity.
- **Triggers:** `OnTagDeleted` and `OnStateChanged`

#### `GetTags()`

```csharp
public int[] GetTags()
```

- **Description:** Returns all tag keys associated with the entity.
- **Returns:** Array of tag keys.

#### `CopyTags(int[])`

```csharp
public int CopyTags(int[] results)
```

- **Description:** Copies tag keys into the provided array.
- **Parameter:** `results` – Array to copy the tags into.
- **Returns:** Number of tags copied.
- **Throws:** `ArgumentNullException` if `results` is null

#### `GetTagEnumerator()`

```csharp
public TagEnumerator GetTagEnumerator()
```

- **Description:** Enumerates all tags of the entity.
- **Returns:** `TagEnumerator` – Struct enumerator over tag keys.