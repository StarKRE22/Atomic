# 🧩️ SceneEntityFactory\<E>

```csharp
public abstract class SceneEntityFactory<E> : MonoBehaviour, IEntityFactory<E> where E : IEntity
```

- **Description:** Abstract class for Unity-based factories that create
  entities with customizable initial settings.
- **Type Parameter:** `E` — The type of entity to create. Must implement [IEntity](../Entities/IEntity.md).
- **Inheritance:** `MonoBehaviour`, [IEntityFactory\<E>](IEntityFactory%601.md)
- **Notes:** Stores initial tag, value, and behaviour counts for optimization.

---

## 🛠 Inspector Settings

| Parameters              | Description                                          | 
|-------------------------|------------------------------------------------------|
| `initialTagCount`       | Initial number of tags to assign to the entity       |
| `initialValueCount`     | Initial number of values to assign to the entity     |
| `initialBehaviourCount` | Initial number of behaviours to assign to the entity |

> These parameters are primarily used for **Editor optimization** and scene baking workflows.

---

## 🧱 Fields

#### `InitialTagCount`

```csharp
[SerializeField] 
protected int InitialTagCount;
```

- **Description:** Initial number of tags to assign to the entity. Mainly used for **editor optimization** and scene
  baking.

#### `InitialValueCount`

```csharp
[SerializeField]
protected int InitialValueCount;
```

- **Description:** Initial number of values to assign to the entity.

#### `InitialBehaviourCount`

```csharp
[SerializeField] 
protected int InitialBehaviourCount;
```

- **Description:** Initial number of behaviours to assign to the entity.

---

## 🏹 Methods

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

### 1️⃣ Simple Scene Factory

```csharp
public class EnemySceneFactory : SceneEntityFactory<EnemyEntity>
{
    public override EnemyEntity Create()
    {
        var enemy = new EnemyEntity();
        enemy.AddValue<int>("Health", 100);
        enemy.AddValue<int>("Damage", 15);
        return enemy;
    }
}
```

### 2️⃣ Using Precompile for Editor Metadata

```csharp
public class WeaponSceneFactory : SceneEntityFactory<WeaponEntity>
{
    public override WeaponEntity Create()
    {
        var weapon = new WeaponEntity();
        weapon.AddValue<int>("Ammo", 30);
        return weapon;
    }

    [ContextMenu("Update Metadata")]
    protected override void Precompile()
    {
        base.Precompile();
        Debug.Log($"Weapon factory precompiled: Tags={InitialTagCount}, Values={InitialValueCount}");
    }
}
```