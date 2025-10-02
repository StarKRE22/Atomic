# 🧩️ SceneEntityInstaller

```csharp
public abstract class SceneEntityInstaller : MonoBehaviour, IEntityInstaller
```

- **Description:** Represents a Unity `MonoBehaviour` that can be attached to a GameObject to
  perform **installation logic** on an [IEntity](../Entities/IEntity.md) during runtime or initialization.
- **Inheritance:** `MonoBehaviour`, [IEntityInstaller](IEntityInstaller.md)
- **Note:** It allows declarative configuration of entities placed in a scene.
- **See also:** [SceneEntityInstaller&lt;E&gt;](SceneEntityInstaller%601.md), [ScriptableEntityInstaller](ScriptableEntityInstaller.md)

> [!TIP]
> Use `SceneEntityInstaller` only if there are scene dependencies or if entity instances in the scene need to differ. In
> other cases, use [ScriptableEntityInstaller](ScriptableEntityInstaller.md) as a shared installer.

---

## 🏹 Methods

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

---

## 🗂 Example of Usage

#### 1. Create `CharacterInstaller` script

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

#### 2. Attach `CharacterInstaller` script to the GameObject

<img width="464" height="153" alt="изображение" src="https://github.com/user-attachments/assets/1967b1d8-b6b7-41c7-85db-5d6935f6443e" />

#### 3. Drag & drop `CharacterInstaller` into `installers` field of the entity

<img width="464" height="" alt="изображение" src="../../Images/SceneEntity%20Attach%20Installer.png" />

#### 4. Now your `Entity` has tags and properties.