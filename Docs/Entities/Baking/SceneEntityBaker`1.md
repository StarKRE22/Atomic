# 🧩 SceneEntityBaker\<E>

Abstract base class for MonoBehaviour-based *bakers* that convert a scene **GameObject** into a native
C# [IEntity](../Entities/IEntity.md) instance. Designed for runtime conversion workflows where scene-authored objects
are transformed into entities.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
    - [Baker Implementation](#ex1)
    - [Bake All](#ex2)
- [Inspector Settings](#-inspector-settings)
    - [Parameters](#-parameters)
    - [Context Menu](#-context-menu)
    - [Gizmos](#-gizmos)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Fields](#-fields)
        - [initialTagCapacity](#initialtagcapacity)
        - [initialValueCapacity](#initialvaluecapacity)
        - [initialBehaviourCapacity](#initialbehaviourcapacity)
    - [Methods](#-methods)
        - [Bake()](#bake)
        - [Create()](#create)
        - [Release()](#release)
        - [OnValidate()](#onvalidate)
        - [Reset()](#reset)
        - [BakeAll(bool)](#bakeallbool)
        - [BakeAll(ICollection<E>, bool)](#bakeallicollectione-bool)
        - [Bake(Scene, bool)](#bakescene-bool)
        - [Bake(Scene, ICollection<E>, bool)](#bakescene-icollectione-bool)
        - [Bake(GameObject, bool)](#bakegameobject-bool)
        - [Bake(GameObject, ICollection<E>, bool)](#bakegameobject-icollectione-bool)

---

## 🗂 Example of Usage

<div id="ex1"></div>

### 1️⃣ Baker Implementation

Below is an example of a MonoBehaviour-based baker that converts a `GameObject` into an `EnemyEntity`:

```csharp
public class EnemyBaker : SceneEntityBaker<EnemyEntity>
{
    protected override EnemyEntity Create()
    {
        var enemy = new EnemyEntity(
            this.name,
            tagCapacity: this.initialTagCapacity,
            valueCapacity: this.initialValueCapacity,
            behaviourCapacity: this.initialBehaviourCapacity
        );

        enemy.AddTag("Enemy");
        enemy.AddValue<int>("Health", 100);
        enemy.AddValue<float>("Speed", 5f);
        enemy.AddBehaviour<PatrolBehaviour>();

        return enemy;
    }
}
```

Then, at runtime:

```csharp
EnemyEntity bakedEnemy = GetComponent<EnemyBaker>().Bake();
```

This will create an `EnemyEntity` and automatically **destroy the original GameObject** after conversion.

<div id="ex2"></div>

### 2️⃣ Bake All

```csharp
// Create all associated enemies by EnemyBakers those found on all active scenes
EnemyEntity[] enemies = EnemyBaker.BakeAll();

//Assume we have entity world
EntityWorld world = new EntityWorld();

//Add enemies to entity world
world.AddRange(enemies);
```

---

## 🛠 Inspector Settings

<div id="-parameters"></div>

### 🎛️ Parameters

| Parameter                  | Description                                                                           |
|----------------------------|---------------------------------------------------------------------------------------|
| `autoCompile`              | If enabled, automatically precomputes capacities when values change in the Inspector. |
| `initialTagCapacity`       | Initial number of tags to allocate for the entity.                                    |
| `initialValueCapacity`     | Initial number of values to allocate for the entity.                                  |
| `initialBehaviourCapacity` | Initial number of behaviours to allocate for the entity.                              |

- **Note:** These parameters help optimize entity creation by reducing dynamic resizing during runtime.

---

<div id="-context-menu"></div>

### ⚙️ Context Menu

| Option         | Description                                                                                                                                                  |
|----------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------|
| **Precompile** | Creates a temporary preview entity to precompute capacity values for tags, values, and behaviours. Useful for previewing and optimizing in the Unity Editor. |
| **Reset**      | Resets all optimization parameters to default values.                                                                                                        |

---

### 🎨 Gizmos

The baker supports drawing **preview gizmos** in the Scene view for visual debugging.

| Setting              | Description                                                    |
|----------------------|----------------------------------------------------------------|
| `onlySelectedGizmos` | If enabled, draws gizmos only when the GameObject is selected. |
| `onlyEditModeGizmos` | If enabled, disables gizmo drawing while the game is running.  |

Each entity behaviour that implements [IEntityGizmos](../Behaviours/IEntityGizmos.md) will have its gizmos drawn
automatically during edit mode preview.

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public abstract partial class SceneEntityBaker<E> : MonoBehaviour where E : IEntity
```

- **Generic Parameter:** `E` — Type of entity produced by the baker. Must implement [IEntity](../Entities/IEntity.md).
- **Usage:** Attach this script to a `GameObject` in the scene to bake it into a pure C# entity at runtime.

### 🧱 Fields

#### `initialTagCapacity`

```csharp
[SerializeField]
protected int initialTagCount;
```

- **Description:** Initial number of tags to assign to the entity. Mainly used for **editor optimization** and asset
  baking.

#### `initialValueCapacity`

```csharp
[SerializeField]
protected int initialValueCount;
```

- **Description:** Initial number of values to assign to the entity.

#### `initialBehaviourCapacity`

```csharp
[SerializeField]
protected int initialBehaviourCount;
```

- **Description:** Initial number of behaviours to assign to the entity.

---

### 🏹 Methods

#### `Bake()`

```csharp
public E Bake();
```

- **Description:** Creates a new entity by calling [Create()](#create) and destroys the associated GameObject.
- **Returns:** A new instance of type `E`.
- **Remarks:** Intended for runtime conversion; removes the authoring GameObject from the scene after baking.

#### `Create()`

```csharp
protected abstract E Create();
```

- **Description:** Constructs and returns a new entity instance. Must be implemented by derived classes.
- **Returns:** A newly created entity of type `E`.
- **Remarks:** Define the entity’s initialization logic here (add tags, values, or behaviours).

#### `Release()`

```csharp
protected virtual void Release()
```
- **Description:** Handles cleanup after the entity has been created.
- **Remarks:**
  The default implementation destroys the GameObject this baker is attached to.
  Override this method if you need to preserve the GameObject
  or perform additional teardown logic.

#### `OnValidate()`

```csharp
protected virtual void OnValidate();
```

- **Description:** Called when the component is loaded or modified in the Inspector. Triggers `Precompile()` if
  automatic compilation is enabled.

#### `Reset()`

```csharp
protected virtual void Reset();
```

- **Description:** Resets optimization parameters to their default values. Useful for clearing editor-side cached data.

#### `BakeAll(bool)`

```csharp
public static E[] BakeAll(bool includeInactive = true);
```

- **Description:** Finds all `SceneEntityBaker<E>` components in the scene and bakes them into entities.
- **Parameter:** `includeInactive` — Whether to include inactive objects in the search.
- **Returns:** An array of baked entities.
- **Notes:** All corresponding `GameObject`s will be destroyed after baking.

#### `BakeAll(ICollection<E>, bool)`

```csharp
public static void BakeAll(ICollection<E> destination, bool includeInactive = true);
```

- **Description:** Collects entities from all `SceneEntityBaker<E>` components in the scene and adds them to the
  provided collection.
- **Type Parameter:** `E` — The type of entity created by the bakers.
- **Parameters:**
    - `destination` — The collection where baked entities will be stored. Must not be `null`.
    - `includeInactive` — Whether to include inactive GameObjects.
- **Exceptions:** Throws `ArgumentNullException` if `destination` is `null`.

#### `Bake(Scene, bool)`

```csharp
public static List<E> Bake(Scene scene, bool includeInactive = true);
```

- **Description:** Bakes all `SceneEntityBaker<E>`s in the specified scene and returns them as a list.
- **Parameters:**
    - `scene` — The scene whose root objects should be searched.
    - `includeInactive` — Whether to include inactive objects in the search.
- **Returns:** A list of baked entities.

#### `Bake(Scene, ICollection<E>, bool)`

```csharp
public static void Bake(Scene scene, ICollection<E> results, bool includeInactive = true);
```

- **Description:** Bakes all `SceneEntityBaker<E>`s in the specified scene and adds them to the provided collection.
- **Parameters:**
    - `scene` — The scene whose root objects should be searched.
    - `results` — The collection where baked entities will be added. Must not be `null`.
    - `includeInactive` — Whether to include inactive objects in the search.
- **Exceptions:** Throws `ArgumentNullException` if `results` is `null`.

#### `Bake(GameObject, bool)`

```csharp
public static E[] Bake(GameObject gameObject, bool includeInactive = true);
```

- **Description:** Bakes all `SceneEntityBaker<E>` components attached to or under the specified GameObject.
- **Parameters:**
    - `gameObject` — The GameObject to search.
    - `includeInactive` — Whether to include inactive objects in the search.
- **Returns:** An array of baked entities.

#### `Bake(GameObject, ICollection<E>, bool)`

```csharp
public static void Bake(GameObject gameObject, ICollection<E> results, bool includeInactive = true);
```

- **Description:** Bakes all `SceneEntityBaker<E>` components attached to or under the specified GameObject and adds
  them to the provided collection.
- **Parameters:**
    - `gameObject` — The GameObject to search.
    - `results` — The collection where baked entities will be added. Must not be `null`.
    - `includeInactive` — Whether to include inactive objects in the search.
- **Exceptions:** Throws `ArgumentNullException` if `results` is `null`.

<!--

# 🧩 SceneEntityBaker<E>

```csharp
public abstract partial class SceneEntityBaker<E> : MonoBehaviour, IEntityFactory<E>
    where E : IEntity
```

- **Description:** 

- **Type Parameter:** `E` — The type of entity to bake, must implement [IEntity](../Entities/IEntity.md).
- **Inheritance:** `MonoBehaviour`, [IEntityFactory<E>](../Factories/IEntityFactory%601.md)

- **Note:** Can be used to spawn or initialize entities in a Unity scene and immediately transfer them to runtime logic.

- **See also:** [ScriptableEntityFactory<E>](../Factories/ScriptableEntityFactory%601.md),
  [IEntityFactory<E>](../Factories/IEntityFactory%601.md)

---

## 🛠 Inspector Settings

| Parameter          | Description                                                           |
|--------------------|-----------------------------------------------------------------------|
| `destroyAfterBake` | Should destroy this GameObject after baking? Default is `true`.       |
| `factory`          | The `ScriptableEntityFactory<E>` that this baker will use / override. |

---

## 🏹 Methods

#### `Bake()`

```csharp
public E Bake();
```

- **Description:** Creates a new entity using the assigned factory, installs it, and optionally destroys the baker's
  GameObject.
- **Returns:** The created entity of type `E`.

#### `Install(E)`

```csharp
protected abstract void Install(E entity);
```

- **Description:** Abstract method to perform custom installation or initialization logic on the baked entity.
- **Parameter:** `entity` — The entity instance being installed.

#### `IEntityFactory<E>.Create()`

```csharp
E IEntityFactory<E>.Create();
```

- **Description:** Interface implementation that simply calls `Bake()`.

---

## 🗂 Example of Usage

```
public class EnemyBaker : SceneEntityBaker<EnemyEntity>
{
protected override void Install(EnemyEntity entity)
{
// Custom initialization for EnemyEntity
entity.Health = 100;
entity.SetPosition(this.transform.position);
}
}

// Usage in scene
EnemyBaker baker = FindObjectOfType<EnemyBaker>();
EnemyEntity enemy = baker.Bake();
```

-->