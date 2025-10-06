# 🧩 EntityViewCatalog<E, V>

```csharp
public abstract class EntityViewCatalog<E, V> : ScriptableObject
    where E : class, IEntity
    where V : EntityView<E>
```

- **Description:** A `ScriptableObject` that serves as a centralized **catalog of EntityView prefabs**.  
  Provides storage and retrieval of prefabs by index or by name.
- **Type Parameters:**
    - `E` — The type of entity associated with the views. Must implement `IEntity`.
    - `V` — The type of entity view (`EntityView<E>`) stored in the catalog.
- **Inheritance:** `ScriptableObject`
- **Usage:** Use for managing and reusing collections of prefabs (e.g., UI elements, game units).

---

## 🛠 Inspector Settings

| Parameter  | Description                                          |
|------------|------------------------------------------------------|
| `prefabs`  | List of `EntityView` prefabs available in the catalog. Serialized and editable in the inspector. |

---

## 🔑 Properties

#### `Count`

```csharp
public int Count { get; }
```

- **Description:** Gets the total number of prefabs stored in the catalog.

---

## 🏹 Public Methods

#### `GetPrefab(int)`

```csharp
public KeyValuePair<string, V> GetPrefab(int index);
````

- **Description:** Retrieves a prefab at the specified index along with its name.
- **Parameter:** `index` — Index of the prefab.
- **Returns:** A `KeyValuePair<string, V>` where:
    - Key = prefab’s name (from `GetName(V)`),
    - Value = the prefab instance.
- **Throws:** `ArgumentOutOfRangeException` if the index is out of range.

#### `GetPrefab(string)`

```csharp
public V GetPrefab(string name);
````

- **Description:** Retrieves a prefab by its name.
- **Parameter:** `name` — The name of the prefab to search for.
- **Returns:** A prefab of type `V`.
- **Throws:** `Exception` if no prefab with the given name exists.

---

## 🏹 Protected Methods

#### `GetName(V)`

```csharp
protected virtual string GetName(V prefab);
```

- **Description:** Extracts the name of a given prefab.
- **Parameter:** `prefab` — The prefab to get the name of.
- **Returns:** The prefab name.
- **Default Implementation:** Returns `EntityView<E>.Name`.
- **Override:** Customize to use tags, metadata, or localization for prefab naming.

---

## 🗂 Example of Usage

### 1️⃣ Creating a Catalog

```csharp
[CreateAssetMenu(menuName = "Game/PlayerViewCatalog")]
public class PlayerViewCatalog : EntityViewCatalog<PlayerEntity, PlayerView> { }
````

---

### 2️⃣ Loading and Using the Catalog

```csharp
// Load from Resources
PlayerViewCatalog catalog = Resources.Load<PlayerViewCatalog>("PlayerViewCatalog");

// Get by index
var kv = catalog.GetPrefab(0);
Debug.Log($"Prefab {kv.Key} -> {kv.Value}");

// Get by name
PlayerView playerPrefab = catalog.GetPrefab("PlayerHUD");
````