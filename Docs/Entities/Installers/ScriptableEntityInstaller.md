# 🧩 ScriptableEntityInstaller Classes

Represents a Unity `ScriptableObject` that defines **reusable logic for installing or configuring**
an [IEntity](../Entities/IEntity.md). It is useful for shared configuration logic that can be applied to multiple
entities, such as setting default values, tags, or attaching behaviors. Supports both runtime and edit-time contexts via
utility methods.

Используй ScriptableEntityInstaller если нужен конфиг на кучу игровых объектов

> [!TIP]
> Use `SceneEntityInstaller` only if there are scene dependencies or if entity instances in the scene need to differ. In
> other cases, use [ScriptableEntityInstaller](ScriptableEntityInstaller.md) as a shared installer.

---

<details>
  <summary>
    <h2 id="scriptable-entity-installer"> 🧩 ScriptableEntityInstaller</h2>
    <br>Abstract ScriptableObject for reusable entity configuration logic.
  </summary>

<br>

```csharp
public abstract class ScriptableEntityInstaller : ScriptableObject, IEntityInstaller
```

- **Inheritance:** Implements [IEntityInstaller](IEntityInstaller.md) to allow configuration of entities via
  ScriptableObjects.
- **Remarks:** Provides a reusable, declarative way to define entity setup logic that can be shared across multiple
  entities.

---

### 🏹 Methods

#### `Install(IEntity)`

```csharp
public abstract void Install(IEntity entity);
```

- **Description:** Installs data, values, or behaviors into the specified entity.
- **Parameters:** `entity` – The entity to install configuration or components into.
- **Remarks:** Must be implemented by derived classes.

#### `Uninstall(IEntity)`

```csharp
public virtual void Uninstall(IEntity entity)
```

- **Description:** Optionally removes previously installed data or behavior from the specified entity.
- **Parameters:** `entity` – The entity to uninstall configuration, components, or behavior from.
- **Remarks:** Default implementation does nothing. Override this method to provide custom uninstall logic.

</details>

---

### 🗂 Example of Usage

#### 1. Create a new `GameObject`

<img width="360" height="255" alt="GameObject creation" src="https://github.com/user-attachments/assets/463a721f-e50d-4cb7-86be-a5d50a6bfa17" />

#### 2. Add `Entity` Component to the GameObject

<img width="464" height="346" alt="Entity component" src="https://github.com/user-attachments/assets/f74644ba-5858-4857-816e-ea47eed0e913" />

#### 3. Create `MoveInstaller` asset

```csharp
[CreateAssetMenu(
    fileName = "MoveInstaller",
    menuName = "SampleGame/New MoveInstaller"
)]
public sealed class MoveInstaller : ScriptableEntityInstaller
{
    [SerializeField] private Const<float> _moveSpeed = 5.0f; 

    public override void Install(IEntity entity)
    {
        entity.AddTag("Moveable");
        entity.AddValue("MoveSpeed", _moveSpeed);
        entity.AddValue("MoveDirection", new ReactiveVariable<Vector3>());
    }
}
```

#### 4. Drag & drop `MoveInstaller` into `installers` field of the entity

//TODO: картинка

#### 5. Now your `Entity` has tags and properties.

---

<details>
  <summary>
    <h2 id="scriptable-entity-installer-t"> 🧩 ScriptableEntityInstaller&lt;E&gt;</h2>
    <br>Strongly-typed variant of <code>ScriptableEntityInstaller</code>.
  </summary>

<br>

```csharp
public abstract class ScriptableEntityInstaller<E> : ScriptableEntityInstaller, IEntityInstaller<E>
    where E : class, IEntity
```

- **Type Parameter:** `E` – The specific entity type this installer supports.
- **Inheritance:** Inherits from [ScriptableEntityInstaller](#scriptable-entity-installer) and
  implements [IEntityInstaller&lt;E&gt;](IEntityInstaller.md/#entity-installer-t).
- **Remarks:** Eliminates the need for manual casting in derived installer classes.

---

### 🏹 Methods

#### `Install(E)`

```csharp
public abstract void Install(E entity);
```

- **Description:** Installs data, values, or behaviors into the strongly-typed entity.
- **Parameters:** `entity` – The entity of type `E` to install configuration or components into.
- **Remarks:** Must be implemented by derived classes.

#### `Uninstall(E)`

```csharp
public virtual void Uninstall(E entity)
```

- **Description:** Removes previously installed data or behavior from the strongly-typed entity.
- **Parameters:** `entity` – The entity of type `E` to uninstall configuration, components, or behavior from.
- **Remarks:** Default implementation does nothing. Override to provide custom uninstall behavior.

---

### 🗂 Example of Usage

```csharp
[CreateAssetMenu(
    fileName = "MoveInstaller",
    menuName = "SampleGame/New MoveInstaller"
)]
public sealed class MoveInstaller<UnitEntity> : ScriptableEntityInstaller<UnitEntity>
{
    [SerializeField] private Const<float> _moveSpeed = 5.0f; 

    public override void Install(UnitEntity entity)
    {
        entity.AddTag("Moveable");
        entity.AddValue("MoveSpeed", _moveSpeed);
        entity.AddValue("MoveDirection", new ReactiveVariable<Vector3>());
    }
}
```

> Note: Using the generic `UnitEntity` version allows type-safe access to entity-specific properties without casting.

</details>

---

## 📝 Notes

- **Shared Configuration** – Use `ScriptableEntityInstaller` for reusable entity setup logic across multiple entities.
- **Strongly-Typed Option** – `ScriptableEntityInstaller<E>` ensures type-safe installation for specific entity types.
- **Runtime & Edit-Time Support** – Can be used in both runtime and editor contexts.
- **Modular** – Can be combined with other installers or entity behaviors to create complex, composable setups.
- `ScriptableEntityInstaller` is intended for **shared and reusable entity configuration**.
- `ScriptableEntityInstaller<E>` is useful when the installer targets a specific entity type.
