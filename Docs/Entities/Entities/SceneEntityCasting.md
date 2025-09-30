
<details>
  <summary>
    <h2 id="-entity-casting"> 🪄 Casting</h2>
    <br> This section provides methods for safe casting between <code>IEntity</code> and <code>SceneEntity</code>.
  </summary>

### 🏹 Methods

#### `Cast(IEntity)`

```csharp
public static SceneEntity Cast(IEntity entity)  
```

- **Description:** Casts the specified `IEntity` to a `SceneEntity` if possible.
- **Parameter:** `entity` – The entity to cast.
- **Returns:** The entity cast to `SceneEntity`, or `null` if the input is `null`.
- **Exceptions:** Throws `InvalidCastException` if the entity cannot be cast to `SceneEntity`.
- **Note:** Uses `AggressiveInlining` for performance.

#### `Cast<E>(IEntity)`

```csharp
public static E Cast<E>(IEntity entity) where E : SceneEntity  
```

- **Description:** Casts the specified `IEntity` to the target type `E`. Supports direct `SceneEntity` instances and
  `SceneEntityProxy<E>` wrappers.
- **Type Parameter:** `E` – The type of `SceneEntity` to cast to.
- **Parameter:** `entity` – The entity to cast.
- **Returns:** The entity cast to type `E`, or `null` if the input is `null`.
- **Exceptions:** Throws `InvalidCastException` if the entity cannot be cast to the target type `E`.

#### `TryCast(IEntity, out SceneEntity)`

```csharp
public static bool TryCast(IEntity entity, out SceneEntity result)  
```

- **Description:** Attempts to cast the specified `IEntity` to a `SceneEntity`.
- **Parameters:**
    - `entity` – The entity to cast.
    - `result` – The cast result if successful; otherwise, `null`.
- **Returns:** `true` if the cast was successful; otherwise, `false`.

#### `TryCast<E>(IEntity, out E)`

```csharp
public static bool TryCast<E>(IEntity entity, out E result) where E : SceneEntity  
```

- **Description:** Attempts to cast the specified `IEntity` to the target type `E`. Supports direct `SceneEntity`
  instances and `SceneEntityProxy<E>` wrappers.
- **Type Parameter:** `E` – The type of `SceneEntity` to cast to.
- **Parameters:**
    - `entity` – The entity to cast.
    - `result` – The cast result if successful; otherwise, `null`.
- **Returns:** `true` if the cast was successful; otherwise, `false`.

---

### 🗂 Examples of Usage

#### Simple cast to `SceneEntity`

```csharp
IEntity entity = GetEntityFromRegistry();
SceneEntity sceneEntity = SceneEntity.Cast(entity);
```

> Throws an exception if `entity` is not a `SceneEntity`.

#### Generic cast to a specific `SceneEntity` type

```csharp
IEntity entity = GetEntityFromRegistry();
EnemyEntity enemy = SceneEntity.Cast<EnemyEntity>(entity);
```

> Throws an exception if entity is not of type `EnemyEntity` or a proxy of it.

#### Safe cast using `TryCast`

```csharp
IEntity entity = GetEntityFromRegistry();
if (SceneEntity.TryCast(entity, out SceneEntity sceneEntity))
    Debug.Log($"Successfully casted to SceneEntity: {sceneEntity.Name}");
else
    Debug.LogWarning("Entity is not a SceneEntity");
```

#### Safe generic cast using TryCast<E>

```csharp
IEntity entity = GetEntityFromRegistry();
if (SceneEntity.TryCast<EnemyEntity>(entity, out EnemyEntity enemy))
    Debug.Log($"Successfully casted to EnemyEntity: {enemy.Name}");
else
    Debug.LogWarning("Entity is not of type EnemyEntity");
```

</details>