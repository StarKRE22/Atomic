# 🧩 ScriptableMultiEntityFactory

A Unity `ScriptableObject`-based concrete implementation of specialized for `string`
keys, [IEntity](../Entities/IEntity.md) entities and factories of
type [ScriptableEntityFactory](ScriptableEntityFactory.md).

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [Inspector Settings](#-inspector-settings)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Create(string)](#createstring)
        - [TryCreate(string, out IEntity)](#trycreatestring-out-ientity)
        - [Contains(string)](#containsstring)
        - [GetKey(ScriptableEntityFactory)](#getkeyscriptableentityfactory)
- [Notes](#-notes)

---

## 🗂 Example of Usage

Below is an example of using `ScriptableMultiEntityFactory`

#### 1. Create the multi-factory asset

Right click to the project hierarchy and choose `Create/Atomic/Entities/MultiEntityFactory`

<img width="400" height="" alt="Entity component" src="../../Images/ScriptableMultiEntityFactory%20(Empty).png" />

#### 2. Assume we have some factories derived from [ScriptableEntityFactory](ScriptableEntityFactory.md):

```csharp
[CreateAssetMenu(
    fileName = "OrcEntityFactory",
    menuName = "Example/New OrcEntityFactory"
)]
public class OrcEntityFactory : ScriptableEntityFactory
{
    protected override void Install(IEntity entity)
    {
        // Some code...
    }
}
```

```csharp
[CreateAssetMenu(
    fileName = "GnomeEntityFactory",
    menuName = "Example/New GnomeEntityFactory"
)]
public class GnomeEntityFactory : ScriptableEntityFactory
{
    protected override void Install(IEntity entity)
    {
        // Some code...
    }
}
```

#### 3. Drag and drop this entity factories to the multi-factory asset

<img width="400" height="" alt="Entity component" src="../../Images/ScriptableMultiEntityFactory%20(Full).png" />

#### 4. Use the multi-entity factory in your project


```csharp
// Load the multi-entity factory asset from Resources, for example:
ScriptableMultiEntityFactory factory = Resources.Load<ScriptableMultiEntityFactory>("MultiEntityFactory");

if (factory.Contains("Orc"))
    IEntity orc = factory.Create("Orc");

if (factory.TryCreate("Goblin", out IEntity goblin))
    // use goblin entity
```

---

## 🛠 Inspector Settings

| Parameter   | Description                                                                                                                                   |
|-------------|-----------------------------------------------------------------------------------------------------------------------------------------------|
| `factories` | Array of [ScriptableEntityFactory](ScriptableEntityFactory.md) assets used for entity creation. Each factory is identified by its asset name. |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[CreateAssetMenu(
    fileName = "MultiEntityFactory",
    menuName = "Atomic/Entities/MultiEntityFactory"
)]
public class ScriptableMultiEntityFactory : ScriptableMultiEntityFactory<string, IEntity, ScriptableEntityFactory>,
    IMultiEntityFactory
```

- **Inheritance:** [ScriptableMultiEntityFactory<K, E, F>](ScriptableMultiEntityFactory%601.md),
  [IMultiEntityFactory](IMultiEntityFactory.md)
- **See also:** [ScriptableEntityFactory](ScriptableEntityFactory.md)
- **Note:** Can be created as an asset via **Unity Create Menu**: `Atomic/Entities/New EntityFactoryCatalog`.

---

### 🏹 Methods

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
- **Note:** Uses the **factory asset name** (`ScriptableEntityFactory.name`) as the lookup key by default.

---

## 📝 Notes

- Designed to serve as a central multi-factory of entity factories in Unity projects.
- Duplicate keys are overwritten with a warning in the Unity console.
- It can be used as a Flyweight pattern across the project, sharing a single instance for efficient entity creation.
