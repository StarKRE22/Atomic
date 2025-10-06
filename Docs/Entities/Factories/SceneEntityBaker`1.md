# 🧩️ SceneEntityBaker\<E>

```csharp
public abstract class SceneEntityBaker<E> : MonoBehaviour, IEntityFactory<E> where E : IEntity
```

- **Description:** Abstract class for Unity-based factories that create
  entities with customizable initial settings.
- **Type Parameter:** `E` — The type of entity to create. Must implement [IEntity](../Entities/IEntity.md).
- **Inheritance:** `MonoBehaviour`, [IEntityFactory\<E>](IEntityFactory%601.md)
- **Notes:** Stores initial tag, value, and behaviour capacities for optimization.
- **See also:** [SceneEntityBaker](SceneEntityBaker.md)

---

## 🛠 Inspector Settings

| Parameters                 | Description                                          | 
|----------------------------|------------------------------------------------------|
| `initialTagCapacity`       | Initial number of tags to assign to the entity       |
| `initialValueCapacity`     | Initial number of values to assign to the entity     |
| `initialBehaviourCapacity` | Initial number of behaviours to assign to the entity |

> These parameters are primarily used for **Editor optimization** and scene baking workflows.

---

## 🧱 Fields

#### `InitialTagCapacity`

```csharp
[SerializeField] 
protected int initialTagCount;
```

- **Description:** Initial number of tags to assign to the entity. Mainly used for **editor optimization** and scene
  baking.

#### `InitialValueCapacity`

```csharp
[SerializeField]
protected int initialValueCount;
```

- **Description:** Initial number of values to assign to the entity.

#### `InitialBehaviourCapacity`

```csharp
[SerializeField] 
protected int initialBehaviourCount;
```

- **Description:** Initial number of behaviours to assign to the entity.

---

## 🏹 Public Methods

#### `Create()`

```csharp
public abstract E Create();
```

- **Description:** Creates and returns a new instance of the entity type `E`.
- **Returns:** A new instance of type `E`.
- **Note:** Override this method to implement custom instantiation logic.

---

#### `OnValidate()`

```csharp
protected virtual void OnValidate();
```

- **Description:** Unity callback invoked when script values change in the Inspector. Updates cached metadata by calling
  `Precompile()` by default.
- **Remarks:** Only executed in the Editor outside of Play mode.

---

## 🏹 Static Methods

Represents static methods for baking entities under Unity scenes and GameObject domains.

#### `BakeAll(bool)`

```csharp
public static E[] BakeAll(bool includeInactive = true);
```

- **Description:** Finds all `SceneEntityBaker<E>` components in the scene and bakes them into entities.
- **Parameter:** `includeInactive` — Whether to include inactive objects in the search.
- **Returns:** An array of baked entities.
- **Notes:** All corresponding `GameObject`s will be destroyed after baking.

#### `BakeAll(ICollection<E> destination, bool)`

```csharp
public static void BakeAll(ICollection<E> destination, bool includeInactive = true);
```

- **Description:** Collects entities from all `SceneEntityBaker<E>` components in the scene and adds them to the provided collection.
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

- **Description:** Bakes all `SceneEntityBaker<E>` components attached to or under the specified GameObject and adds them to the provided collection.
- **Parameters:**
  - `gameObject` — The GameObject to search.
  - `results` — The collection where baked entities will be added. Must not be `null`.
  - `includeInactive` — Whether to include inactive objects in the search.
- **Exceptions:** Throws `ArgumentNullException` if `results` is `null`.

---

## ▶️ Context Menu

#### `Precompile()`

```csharp
[ContextMenu(nameof(Precompile))]
protected virtual void Precompile();
```

- **Description:** Creates a temporary entity using `Create()` and **precompiles capacities** such as tag count, value
  count, and behaviour count. Useful for editor previews, scene baking, and optimization.
- **Remarks:** Useful for optimization. Only executed in the Editor. Logs a warning if `Create()` returns `null`.

#### `Reset()`

```csharp
protected virtual void Reset();
```

- **Description:** Unity callback that resets factory fields to default values.
- **Remarks:** Only affects editor workflows.

---

## 🗂 Example of Usage

```csharp
public class EnemyBaker : SceneEntityBaker<EnemyEntity>
{
    public override EnemyEntity Create()
    {
        //Create an instance of entity with precomputed capacities
        var enemy = new EnemyEntity(
            this.name,
            this.initialTagCount,
            this.initialValueCount,
            this.initialBehaviourCount
        );
        
        enemy.AddTag("Enemy");
        enemy.AddValue<int>("Health", 100);
        enemy.AddValue<int>("Damage", 15);
        enemy.AddBehaviour<AttackBehaviour>();

        // Destroy unnecessary game object after creation
        Destroy(this.gameObject);
        
        return enemy;
    }
}
```

```csharp
// Create all associated enemies by EnemyBakers those found on all active scenes
EnemyEntity[] enemies = EnemyBaker.BakeAll();

//Assume we have entity world
EntityWorld world = new EntityWorld();

//Add enemies to entity world
world.AddRange(enemies);
```

<!--

# 🧩 SceneEntityBaker<E>

```csharp
public abstract partial class SceneEntityBaker<E> : MonoBehaviour, IEntityFactory<E>
    where E : IEntity
```

- **Description:** An abstract Unity `MonoBehaviour` that serves as a scene-based entity baker. It creates entities of
  type `E` using a [ScriptableEntityFactory\<E>](../Factories/ScriptableEntityFactory%601.md) and optionally destroys
  its GameObject after
  baking.

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

---

## 📝 Notes

- The baker uses the assigned `ScriptableEntityFactory<E>` to create entities.
- If `_destroyAfterBake` is `true`, the GameObject with the baker will be destroyed immediately after baking.
- Derived classes must implement `Install(E)` to define custom initialization logic for the baked entity.
- Can be used as a **scene-level factory** to pre-instantiate or configure entities in the Unity Editor.

-->