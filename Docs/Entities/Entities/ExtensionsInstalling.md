# 🧩 IEntity Installing Extensions

Provide extension methods for [IEntity](IEntity.md) to simplify operations with entity configuration.

---

## 📑 Table of Contents

- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Install(IEntityInstaller)](#installientityinstaller)
        - [Install(IEnumerable\<IEntityInstaller>)](#installienumerableientityinstaller)
        - [InstallFromScene(Scene, bool)](#installfromscenescene-bool)
        - [InstallFromScene\<T>(Scene, bool)](#installfromscenetscene-bool)

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public partial class Extensions
```

---

### 🏹 Methods

#### `Install(IEntityInstaller)`

```csharp
public static IEntity Install(this IEntity entity, IEntityInstaller installer)
```

- **Description:** Installs logic from a single `IEntityInstaller` into the specified entity.
- **Parameters:**
    - `entity` – The entity to install the logic into.
    - `installer` – The installer that provides logic to install.
- **Returns:** The same `entity` after installation (supports chaining).
- **Remarks:** Delegates installation to the `IEntityInstaller.Install(IEntity)` method.

#### `Install(IEnumerable<IEntityInstaller>)`

```csharp
public static void Install(this IEntity entity, IEnumerable<IEntityInstaller> installers)
```

- **Description:** Installs logic from multiple `IEntityInstaller` instances into the specified entity.
- **Parameters:**
    - `entity` – The entity to install the logic into.
    - `installers` – Collection of installers. Can be `null`, in which case nothing is installed.
- **Remarks:** Each installer in `installers` will invoke its `Install(IEntity)` method.

#### `InstallFromScene(Scene, bool)`

```csharp
public static void InstallFromScene(this IEntity entity, Scene scene, bool includeInactive = true)
```

- **Description:** Installs logic from all `SceneEntityInstaller` components found in the specified scene.
- **Parameters:**
    - `entity` – The entity to install the logic into.
    - `scene` – The scene in which to search for installers.
    - `includeInactive` – If `true`, installers on inactive GameObjects are included; otherwise only active
      installers are considered.
- **Remarks:** Iterates over all root GameObjects in the scene and applies each found `SceneEntityInstaller` to the
  entity.

#### `InstallFromScene<T>(Scene, bool)`

```csharp
public static void InstallFromScene<T>(this T entity, Scene scene, bool includeInactive = true)
    where T : class, IEntity
```

- **Description:** Installs logic from all `SceneEntityInstaller<T>` components found in the specified scene for a
  generic entity type.
- **Type Parameter:** `T` – The entity type that implements `IEntity`.
- **Parameters:**
    - `entity` – The entity to install the logic into.
    - `scene` – The scene in which to search for installers.
    - `includeInactive` – If `true`, installers on inactive GameObjects are included; otherwise only active
      installers are considered.
- **Remarks:** Iterates over all root GameObjects in the scene and applies each found `SceneEntityInstaller<T>` to the
  entity. Useful for generic entities or strongly-typed scenarios.