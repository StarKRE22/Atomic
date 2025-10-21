# 🧩 IEntity Retrieval Extensions

Provides extension methods for [IEntity](IEntity.md) searching and retrieving.

---

## 📑 Table of Contents

- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [TryGetEntity(GameObject, out IEntity)](#trygetentitygameobject-out-ientity)
        - [TryGetEntity(Component, out IEntity)](#trygetentitycomponent-out-ientity)
        - [TryGetEntity(Collision2D, out IEntity)](#trygetentitycollision2d-out-ientity)
        - [TryGetEntity(Collision, out IEntity)](#trygetentitycollision-out-ientity)
        - [FindEntityInParent(GameObject, out IEntity)](#findentityinparentgameobject-out-ientity)
        - [FindEntityInParent(Component, out IEntity)](#findentityinparentcomponent-out-ientity)
        - [FindEntityInParent(Collision2D, out IEntity)](#findentityinparentcollision2d-out-ientity)
        - [FindEntityInParent(Collision, out IEntity)](#findentityinparentcollision-out-ientity)

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public partial class Extensions
```

---

### 🏹 Methods

#### `TryGetEntity(GameObject, out IEntity)`

```csharp
public static bool TryGetEntity(this GameObject gameObject, out IEntity entity)
```

- **Description:** Tries to retrieve the `IEntity` component from the specified GameObject.
- **Parameter:** `gameObject` – The GameObject to search.
- **Output:** `entity` – The retrieved IEntity component if found.
- **Returns:** `true` if an IEntity component was found; otherwise, `false`.

#### `TryGetEntity(Component, out IEntity)`

```csharp
public static bool TryGetEntity(this Component component, out IEntity entity)
```

- **Description:** Tries to retrieve the `IEntity` component from the specified Component.
- **Parameter:** `component` – The Component to search.
- **Output:** `entity` – The retrieved IEntity component if found.
- **Returns:** `true` if an IEntity component was found; otherwise, `false`.

#### `TryGetEntity(Collision2D, out IEntity)`

```csharp
public static bool TryGetEntity(this Collision2D collision2D, out IEntity entity)
```

- **Description:** Tries to retrieve the `IEntity` component from a 2D collision.
- **Parameter:** `collision2D` – The 2D collision object.
- **Output:** `entity` – The retrieved IEntity component if found.
- **Returns:** `true` if an IEntity component was found; otherwise, `false`.

#### `TryGetEntity(Collision, out IEntity)`

```csharp
public static bool TryGetEntity(this Collision collision, out IEntity entity)
```

- **Description:** Tries to retrieve the `IEntity` component from a 3D collision.
- **Parameter:** `collision` – The 3D collision object.
- **Output:** `entity` – The retrieved IEntity component if found.
- **Returns:** `true` if an IEntity component was found; otherwise, `false`.

#### `FindEntityInParent(GameObject, out IEntity)`

```csharp
public static bool FindEntityInParent(this GameObject gameObject, out IEntity entity)
```

- **Description:** Finds an `IEntity` in the parent hierarchy of the GameObject.
- **Parameter:** `gameObject` – The GameObject to search.
- **Output:** `entity` – The retrieved IEntity component if found.
- **Returns:** `true` if an IEntity component was found in the parent hierarchy; otherwise, `false`.

#### `FindEntityInParent(Component, out IEntity)`

```csharp
public static bool FindEntityInParent(this Component component, out IEntity entity)
```

- **Description:** Finds an `IEntity` in the parent hierarchy of the Component.
- **Parameter:** `component` – The Component to search.
- **Output:** `entity` – The retrieved IEntity component if found.
- **Returns:** `true` if an IEntity component was found in the parent hierarchy; otherwise, `false`.

#### `FindEntityInParent(Collision2D, out IEntity)`

```csharp
public static bool FindEntityInParent(this Collision2D collision2D, out IEntity entity)
```

- **Description:** Finds an `IEntity` in the parent hierarchy from a 2D collision.
- **Parameter:** `collision2D` – The 2D collision object.
- **Output:** `entity` – The retrieved IEntity component if found.
- **Returns:** `true` if an IEntity component was found in the parent hierarchy; otherwise, `false`.

#### `FindEntityInParent(Collision, out IEntity)`

```csharp
public static bool FindEntityInParent(this Collision collision, out IEntity entity)
```

- **Description:** Finds an `IEntity` in the parent hierarchy from a 3D collision.
- **Parameter:** `collision` – The 3D collision object.
- **Output:** `entity` – The retrieved IEntity component if found.
- **Returns:** `true` if an IEntity component was found in the parent hierarchy; otherwise, `false`.