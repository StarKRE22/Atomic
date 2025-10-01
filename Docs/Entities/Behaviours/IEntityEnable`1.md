
<details>
  <summary>
    <h2 id="entity-enable-t"> 🧩 IEntityEnable&lt;E&gt;</h2>
    <br>Defines a strongly-typed behavior that executes logic when an <code>IEntity</code> of type <code>E</code> is enabled.
  </summary>

<br>

```csharp
public interface IEntityEnable<in E> : IEntityEnable where E : IEntity
```

- **Description:** Provides a strongly-typed version of `IEntityEnable` for handling enable logic for a specific `IEntity` type.
- **Type Parameter:** `E` – The concrete entity type this behavior is associated with.
- **Inherits:** [IEntityEnable](#entity-enable)
- **Remarks:** Automatically invoked by `IEntity.Enable` when the behavior is registered on an entity of type `E`.

---

## 🏹 Methods

#### `Enable(E)`

```csharp
public void Enable(E entity);
```

- **Description:** Called when the typed entity is enabled.
- **Parameter:** `entity` – The entity instance of type `E`.
- **Remarks:** Implements the base `IEntityEnable.Enable(IEntity)` explicitly by casting the `IEntity` to type `E`.

---

### 🗂 Example of Usage

Enable a `Renderer` for a unit entity

```csharp
public class UnitEntity : Entity
{
}
```

```csharp
public class EnableRendererBehaviour : IEntityEnable<UnitEntity>
{
    public void Enable(UnitEntity entity)
    {
        var renderer = entity.GetValue<Renderer>("Renderer");
        renderer.enabled = true;
    }
}
```

> Note: Uses the strongly-typed `UnitEntity`, so no casting from `IEntity` is required.

</details>
