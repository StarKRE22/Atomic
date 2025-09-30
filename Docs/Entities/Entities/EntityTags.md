
<details>
  <summary>
    <h2 id="-tag-members">🏷️ Tags</h2>
    <br> Manage lightweight categorization and filtering of entities. Tags are integer-based labels that can be added, removed,
         enumerated, or checked. They are useful for grouping entities, querying, and driving logic based on assigned tags.
  </summary>

<br>

> Tags in the entity behave like a **HashSet of integers**. All operations such as add, check, or remove have **O(1)
average time complexity**, and duplicate tags are **not allowed**.

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

---

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

#### `HasTag`

```csharp
public bool HasTag(int tag)
```

- **Description:** Checks if the entity has the given tag.
- **Parameter:** `tag` – The tag to check for.
- **Returns:** `true` if the tag exists, otherwise `false`.

#### `AddTag`

```csharp
public bool AddTag(int tag)
```

- **Description:** Adds a tag to the entity.
- **Parameter:** `int tag` – The tag to add.
- **Returns:** `true` if the tag was added, otherwise `false`.
- **Triggers:** `OnTagAdded` and `OnStateChanged`

#### `DelTag`

```csharp
public bool DelTag(int tag)
```

- **Description:** Removes a tag from the entity.
- **Parameter:** `tag` – The tag to remove.
- **Returns:** `true` if the tag was removed, otherwise `false`.
- **Triggers:** `OnTagDeleted` and `OnStateChanged`

#### `ClearTags`

```csharp
public void ClearTags()
```

- **Description:** Removes all tags from the entity.
- **Triggers:** `OnTagDeleted` and `OnStateChanged`

#### `GetTags`

```csharp
public int[] GetTags()
```

- **Description:** Returns all tag keys associated with the entity.
- **Returns:** Array of tag keys.

#### `CopyTags`

```csharp
public int CopyTags(int[] results)
```

- **Description:** Copies tag keys into the provided array.
- **Parameter:** `results` – Array to copy the tags into.
- **Returns:** Number of tags copied.
- **Throws:** `ArgumentNullException` if `results` is null

#### `GetTagEnumerator`

```csharp
public TagEnumerator GetTagEnumerator()
```

- **Description:** Enumerates all tags of the entity.
- **Returns:** `TagEnumerator` – Struct enumerator over tag keys.

---

### 🗂 Example of Usage

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

</details>