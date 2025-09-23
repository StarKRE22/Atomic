# 🧩️ SceneEntityInstaller Classes

Represents a Unity `MonoBehaviour` that can be attached to a GameObject to **perform installation logic on an `IEntity`** during runtime or initialization. 
It allows declarative configuration of entities placed in a scene.

> [!TIP]
> Use `SceneEntityInstaller` only if there are scene dependencies or if entity instances in the scene need to differ.  
> In other cases, use [ScriptableEntityInstaller](ScriptableEntityInstaller.md) as a shared installer.

---

<details>
  <summary>
    <h2 id="scene-entity-installer"> 🧩 SceneEntityInstaller</h2>
    <br>Abstract MonoBehaviour for configuring an <code>IEntity</code> in a Unity scene.
  </summary>

<br>

```csharp
public abstract class SceneEntityInstaller : MonoBehaviour, IEntityInstaller
```
- **Inheritance:** Implements [IEntityInstaller](IEntityInstaller.md) to allow entity configuration via Unity components.
- **Remarks:** Supports editor refresh through `OnValidate` without entering Play Mode.

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
public virtual void Uninstall(IEntity entity);
```
- **Description:** Optionally removes previously installed data or behavior from the specified entity.
- **Parameters:** `entity` – The entity to uninstall configuration, components, or behavior from.
- **Remarks:** Default implementation does nothing. Override this method to provide custom uninstall logic.

#### `OnValidate()`
```csharp
protected virtual void OnValidate();
```
- **Description:** Called by Unity when the component is modified in the Inspector.
- **Note:** Runs only in the Unity Editor; does not execute at runtime.

### Example of Usage


#### 1. Create a new `GameObject`

<img width="360" height="255" alt="GameObject creation" src="https://github.com/user-attachments/assets/463a721f-e50d-4cb7-86be-a5d50a6bfa17" />

#### 2. Add `Entity` Component to the GameObject

<img width="464" height="346" alt="Entity component" src="https://github.com/user-attachments/assets/f74644ba-5858-4857-816e-ea47eed0e913" />

#### 3. Create `CharacterInstaller` script

 ```csharp
//Populates entity with tags, values and behaviours
public sealed class CharacterInstaller : SceneEntityInstaller
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Const<float> _moveSpeed = 5.0f; //Immutable variable
    [SerializeField] private ReactiveVariable<Vector3> _moveDirection; //Mutable variable with subscription

    public override void Install(IEntity entity)
    {
        //Add tags to a character
        entity.AddTag("Character");
        entity.AddTag("Moveable");

        //Add properties to a character
        entity.AddValue("Transform", _transform);
        entity.AddValue("MoveSpeed", _moveSpeed);
        entity.AddValue("MoveDirection", _moveDirection);
    }
}
```

#### 5. Attach `CharacterInstaller` script to the GameObject

<img width="464" height="153" alt="изображение" src="https://github.com/user-attachments/assets/1967b1d8-b6b7-41c7-85db-5d6935f6443e" />

#### 6. Drag & drop `CharacterInstaller` into `installers` field of the entity

<img width="464" height="" alt="изображение" src="../../Images/SceneEntity%20Attach%20Installer.png" />

#### 7. Now your `Entity` has tags and properties.

</details>























---

## Key Features

- **Scene Configuration** – Attach to a GameObject to configure entities in the scene.
- **Editor Support** – Automatically refreshes when properties are changed in the Inspector.
- **Runtime Installation** – Applies configuration and behaviors during runtime.
- **Strongly-Typed Option** – `SceneEntityInstaller<E>` ensures type-safe installation for specific entity types.

---

## Class: SceneEntityInstaller
```csharp
public abstract class SceneEntityInstaller : MonoBehaviour, IEntityInstaller
{
    public abstract void Install(IEntity entity);
}
```
- Implements `IEntityInstaller` to allow entity configuration via Unity components.
- Supports editor refresh through `OnValidate` without entering Play Mode.

---

## Class: SceneEntityInstaller&lt;E&gt;

```csharp
public abstract class SceneEntityInstaller<E> : SceneEntityInstaller where E : class, IEntity
{
    protected abstract void Install(E entity);
}
```
- Provides a **strongly-typed variant** for specific entity types.
- Eliminates the need for manual casting in derived installer classes.

---

## Example Usage

### Example #1. Non-Generic (IEntity)
```csharp
public sealed class CharacterInstaller : SceneEntityInstaller
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _moveSpeed = 5.0f;

    public override void Install(IEntity entity)
    {
        entity.AddTag("Character");
        entity.AddValue("Transform", _transform);
        entity.AddValue("MoveSpeed", _moveSpeed);
        entity.AddBehaviour<MoveBehaviour>();
    }
}
```

### Example #2. Generic with UnitEntity (strongly-typed)
```csharp
public sealed class CharacterInstaller : SceneEntityInstaller<UnitEntity>
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _moveSpeed = 5.0f;

    protected override void Install(UnitEntity entity)
    {
        entity.AddTag("Character");
        entity.AddValue("Transform", _transform);
        entity.AddValue("MoveSpeed", _moveSpeed);
        entity.AddBehaviour<MoveBehaviour>();
    }
}
```

> Note: Using the generic `UnitEntity` version allows type-safe access to entity-specific properties without casting.

---

## Remarks

- `SceneEntityInstaller` is intended for configuring or initializing entities **directly in the Unity scene**.
- `SceneEntityInstaller<E>` is useful when the installer is specific to a particular entity type.
- Supports editor workflows via `OnValidate` to refresh previews or dependent systems.
- Can be combined with other installers or entity behaviors to modularly set up complex entities.
