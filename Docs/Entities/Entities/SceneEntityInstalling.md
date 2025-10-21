# 🧩 SceneEntity Installing

Provides configuration for `SceneEntity` with <b>tags</b>, <b>values</b>, and <b>behaviours</b> at
runtime or in the editor. It also manages child entities through installers, ensuring that all dependencies are properly configured and applied.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [Inspector Settings](#-inspector-settings)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Properties](#-properties)
    - [Installed](#installed)
  - [Public Methods](#-public-methods)
    - [Install()](#install)
    - [Uninstall()](#uninstall)
  - [Protected Methods](#-protected-methods)
    - [OnInstall()](#oninstall)
    - [OnUninstall()](#onuninstall)
  - [Static Methods](#-static-methods)
    - [InstallAll(Scene)](#installallscene)
    - [InstallAll\<E>(Scene)](#installallescene)


---


## 🗂 Example of Usage

Below is an example how to install `SceneEntity` and populate it wit **tags** and **values**:

#### 1. Create a script that populates the entity with tags, values and behaviours

 ```csharp
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

<img width="450" height="" alt="изображение" src="https://github.com/user-attachments/assets/1967b1d8-b6b7-41c7-85db-5d6935f6443e" />

#### 3. Drag & drop `CharacterInstaller` into `installers` field of the entity

<img width="450" height="" alt="изображение" src="https://github.com/user-attachments/assets/1967b1d8-b6b7-41c7-85db-5d6935f6443e" />

#### 4. Now your `SceneEntity` has tags and properties.

---

## 🛠 Inspector Settings

| Parameter              | Description                                                                                                                                                                                                                                    |
|------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `installOnAwake`       | If enabled, `Install()` is automatically called in `Awake()`. Default is `true`                                                                                                                                                                |
| `uninstallOnDestroy`   | If enabled, `Uninstall()` is automatically called in `OnDestroy`. Default is `true`                                                                                                                                                            |
| `sceneInstallers`      | List of MonoBehaviour installers that configure values and systems in this entity. Installers are executed in the order they appear in the array. Null references are automatically skipped, making partially configured lists safe to use.    |
| `scriptableInstallers` | List of ScriptableObject installers that configure values and systems in this entity. Installers are executed in the order they appear in the array. Null references are automatically skipped, making partially configured lists safe to use. |
| `children`             | Child entities installed together with this entity. Children are executed in the order they appear in the array. Null references are automatically skipped, making partially configured lists safe to use.                                     |

---


## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public partial class SceneEntity
```

---

### 🔑 Properties

#### `Installed`

```csharp
public bool Installed { get; }
```

- **Description:** Returns true if the entity already has been installed.

---

### 🏹 Public Methods

#### `Install()`

```csharp
public void Install()  
```

- **Description:** Installs all configured installers and child entities into this `SceneEntity`. Ensures that tags,
  values, and behaviours are properly set up at runtime or in the editor.
- **Warnings:** Logs warnings when null references are found.
- **Notes:** Skips null installers and null children.

#### `Uninstall()`

```csharp
public void Uninstall()  
```

- **Description:** Uninstalls all configured installers and child entities from this `SceneEntity`. Marks the entity as
  not installed, allowing it to be reinstalled.
- **Warnings:** Warnings are logged for null references to help debugging.
- **Notes:** Null installers and null children are safely skipped.

---

### 🏹 Protected Methods

#### `OnInstall()`

```csharp
protected virtual void OnInstall()  
```

- **Description:** Called during the installation process of a `SceneEntity`. Provides a hook for derived classes to
  execute custom logic when the entity is being installed.
- **Notes:** This method is invoked by `Install()` before processing installers and child entities.


#### `OnUninstall()`

```csharp
protected virtual void OnUninstall()  
```

- **Description:** Called during the uninstallation process of a `SceneEntity`. Provides a hook for derived classes to
  execute custom logic when the entity is being uninstalled.
- **Notes:** This method is invoked by `Uninstall()` before processing installers and child entities.

---

### 🏹 Static Methods

There are also static methods that allow installing entities globally in a scene.

#### `InstallAll(Scene)`

```csharp
public static void InstallAll(Scene scene)  
```

- **Description:** Installs all `SceneEntity` instances found in the given `Scene` that are not yet installed. This is a
  convenience method that calls the generic version `InstallAll<SceneEntity>(scene)`.
- **Parameter:** `scene` – The `Scene` in which to search for `SceneEntity` instances.
- **Exception:** Throws if `scene` is not valid or not loaded.
- **Note:**
    - Skips entities that are already installed.
    - Null GameObjects are skipped.
    - Entities that are already installed are ignored.

#### `InstallAll<E>(Scene)`

```csharp
public static void InstallAll<E>(Scene scene) where E : SceneEntity  
```

- **Description:** Installs all `SceneEntity` instances of type `<E>` found in the specified `Scene` that are not yet
  installed. Iterates through all root GameObjects and all child objects to find entities of type `<E>`.
- **Type Parameters:** `E` – The type of `SceneEntity` to search for and install.
- **Parameter:** `scene` – The `Scene` in which to search for `<E>` instances.
- **Exception:** Throws if `scene` is not valid or not loaded.
- **Note:**
    - Skips entities that are already installed.
    - Null GameObjects are skipped.
    - Entities that are already installed are ignored.