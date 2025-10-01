# 🧩 Entity Extensions




---

<details>
  <summary>
    <h2 id="-retrieval"> 🔍 Retrieval</h2>
    <br> Provides extension methods for entity searching and retrieving 
  </summary>

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

</details>