# 🧩️ ScriptableEntityFactory

```csharp
public abstract class ScriptableEntityFactory : ScriptableEntityFactory<IEntity>, IEntityFactory
```

- **Description:** Abstract class for ScriptableObject-based factories that create and
  configure [Entity](../Entities/Entity.md) instances.
- **Inheritance:** [ScriptableEntityFactory\<IEntity>](ScriptableEntityFactory%601.md),
  [IEntityFactory](IEntityFactory.md)
- **Notes:** 
  - Provides the `Install(IEntity)` method to inject custom configuration logic after entity creation.
  -  Can be reused across multiple objects without heavy dependencies.

---

## 🛠 Inspector Settings

| Parameters                 | Description                                          | 
|----------------------------|------------------------------------------------------|
| `initialTagCapacity`       | Initial number of tags to assign to the entity       |
| `initialValueCapacity`     | Initial number of values to assign to the entity     |
| `initialBehaviourCapacity` | Initial number of behaviours to assign to the entity |

> These parameters are primarily used for **Editor optimization** and asset baking workflows.

---

## 🧱 Fields

#### `InitialTagCapacity`

```csharp
[SerializeField]
protected int initialTagCount;
```

- **Description:** Initial number of tags to assign to the entity. Mainly used for **editor optimization** and asset
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

## 🏹 Methods

#### `Create()`

```csharp
public sealed override IEntity Create();
```

- **Description:** Creates a new [Entity](../Entities/Entity.md) using predefined initialization values and then applies
  custom logic via the `Install` method.
- **Returns:** A new instance of [IEntity](../Entities/IEntity.md).
- **Note:** This method is `sealed`; override `Install(IEntity)` for custom configuration.

#### `Install(IEntity)`

```csharp
protected abstract void Install(IEntity entity);
```

- **Description:** Called after entity creation to add tags, values, or behaviours.
- **Parameter:** `entity` — The [IEntity](../Entities/IEntity.md) instance to configure.
- **Note:** Must be implemented by derived classes to provide custom setup logic.

#### `OnValidate()`

```csharp
protected virtual void OnValidate();
```

- **Description:** ScriptableObject callback invoked when values change in the Inspector. Updates cached metadata by
  calling `Precompile()` by default.
- **Remarks:** Only executed in the Editor outside of Play mode.

---

## ▶️ Context Menu

#### `Precompile()`

```csharp
[ContextMenu(nameof(Precompile))]
protected virtual void Precompile();
```

- **Description:** Creates a temporary entity using `Create()` and **precompiles capacities** such as tag count, value
  count, and behaviour count. Useful for editor previews, asset baking, and optimization.
- **Remarks:** Only executed in the Editor. Logs a warning if `Create()` returns `null`.

#### `Reset()`

```csharp
protected virtual void Reset();
```

- **Description:** Resets factory fields to default values.
- **Remarks:** Only affects editor workflows.

---

## 🗂 Example of Usage

```csharp
[CreateAssetMenu(menuName = "Examples/Player Factory")]
public class PlayerScriptableFactory : ScriptableEntityFactory
{
    protected override void Install(IEntity entity)
    {
        entity.AddTag("Player");
        entity.AddValue<int>("Health", 200);
        entity.AddValue<string>("Name", "Hero");
        entity.AddBehaviour<DeathBehaviour>();
    }
}
```

> This pattern allows creating a fully configured `Entity` via ScriptableObject-based workflows, combining predefined
> capacities with custom logic via `Install()`.