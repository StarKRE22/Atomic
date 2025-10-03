# 🧩 ScriptableMultiEntityFactory

```csharp
[CreateAssetMenu(
    fileName = "EntityFactoryCatalog",
    menuName = "Atomic/Entities/New EntityFactoryCatalog"
)]
public class ScriptableMultiEntityFactory : ScriptableMultiEntityFactory<string, IEntity, ScriptableEntityFactory>,
    IMultiEntityFactory
```

- **Description:** A Unity `ScriptableObject`-based concrete implementation of
  specialized for `string` keys , [IEntity](../Entities/IEntity.md)
  entities and [ScriptableEntityFactory](ScriptableEntityFactory.md) factories.
- **Inheritance:** [ScriptableMultiEntityFactory<K, E, F>](ScriptableMultiEntityFactory%601.md),
  [IMultiEntityFactory](IMultiEntityFactory.md)
- **Notes:**
    - Can be used as a Flyweight pattern across the project, sharing a single instance for efficient entity creation.
    - Designed to serve as a central multi-factory of entity factories in Unity projects.
    - Can be created as an asset via **Unity Create Menu**:  
      `Atomic/Entities/New EntityFactoryCatalog`.

- **See also:** [ScriptableEntityFactory](ScriptableEntityFactory.md)

---

## 🛠 Inspector Settings

| Parameter   | Description                                                                                                                                   |
|-------------|-----------------------------------------------------------------------------------------------------------------------------------------------|
| `factories` | Array of [ScriptableEntityFactory](ScriptableEntityFactory.md) assets used for entity creation. Each factory is identified by its asset name. |

---

## 🏹 Methods

#### `Create(string)`

```csharp
public IEntity Create(string key);
```

- **Description:** Creates a new entity associated with the specified factory asset name.
- **Parameter:** `key` — The asset name identifying the factory.
- **Returns:** A new instance of `IEntity`.

#### `TryCreate(string, out IEntity)`

```csharp
public bool TryCreate(string key, out IEntity entity);
```

- **Description:** Attempts to create a new entity associated with the specified factory asset name.
- **Parameters:**
    - `key` — The asset name identifying the factory.
    - `entity` — When the method returns, contains the created entity if the factory exists; otherwise, `null`.
- **Returns:** `true` if the entity was created successfully; otherwise, `false`.

#### `Contains(string)`

```csharp
public bool Contains(string key);
```

- **Description:** Determines whether a factory with the given asset name exists.
- **Parameter:** `key` — The asset name to check.
- **Returns:** `true` if a factory with the given asset name exists; otherwise, `false`.

#### `GetKey(ScriptableEntityFactory)`

```csharp
protected override string GetKey(ScriptableEntityFactory factory);
```

- **Description:** Extracts the string key for a given factory.
- **Parameter:** `factory` — The factory instance.
- **Returns:** The factory’s Unity asset name.

---

## 🗂 Example of Usage

```csharp
ScriptableMultiEntityFactory factory = Resources.Load<ScriptableMultiEntityFactory>("EntityFactoryCatalog");

if (factory.Contains("Orc"))
{
    IEntity orc = factory.Create("Orc");
}

if (factory.TryCreate("Goblin", out IEntity goblin))
{
    // use goblin entity
}
```

---

## 📝 Notes

- This is a **concrete Unity ScriptableObject implementation** that can be instantiated as an asset.
- Uses the **factory asset name** (`ScriptableEntityFactory.name`) as the lookup key.
- The internal dictionary of factories is initialized lazily on first access.
- Duplicate keys are overwritten with a warning in the Unity console.
- Can be used as a **Flyweight** across the project for efficient shared entity creation.