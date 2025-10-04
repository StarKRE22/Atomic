# 🧩️ SceneEntityBaker\<E> — Static API

Represents static methods for baking entities under Unity scenes and GameObject domains.

---

## 🏹 Methods

#### `BakeAll(bool)`

```csharp
public static E[] BakeAll(bool includeInactive = true);
```

- **Description:** Finds all `SceneEntityBaker<E>` components in the scene and bakes them into entities.
- **Parameter:** `includeInactive` — Whether to include inactive objects in the search.
- **Returns:** An array of baked entities.
- **Notes:** All corresponding `GameObject`s will be destroyed after baking.

#### `BakeAll(ICollection<E> destination, bool)`

```csharp
public static void BakeAll(ICollection<E> destination, bool includeInactive = true);
```

- **Description:** Collects entities from all `SceneEntityBaker<E>` components in the scene and adds them to the provided collection.
- **Type Parameter:** `E` — The type of entity created by the bakers.
- **Parameters:**
    - `destination` — The collection where baked entities will be stored. Must not be `null`.
    - `includeInactive` — Whether to include inactive GameObjects.
- **Exceptions:** Throws `ArgumentNullException` if `destination` is `null`.

#### `Bake(Scene, bool)`

```csharp
public static List<E> Bake(Scene scene, bool includeInactive = true);
```

- **Description:** Bakes all `SceneEntityBaker<E>`s in the specified scene and returns them as a list.
- **Parameters:**
    - `scene` — The scene whose root objects should be searched.
    - `includeInactive` — Whether to include inactive objects in the search.
- **Returns:** A list of baked entities.

#### `Bake(Scene, ICollection<E>, bool)`

```csharp
public static void Bake(Scene scene, ICollection<E> results, bool includeInactive = true);
```

- **Description:** Bakes all `SceneEntityBaker<E>`s in the specified scene and adds them to the provided collection.
- **Parameters:**
    - `scene` — The scene whose root objects should be searched.
    - `results` — The collection where baked entities will be added. Must not be `null`.
    - `includeInactive` — Whether to include inactive objects in the search.
- **Exceptions:** Throws `ArgumentNullException` if `results` is `null`.

#### `Bake(GameObject, bool)`

```csharp
public static E[] Bake(GameObject gameObject, bool includeInactive = true);
```

- **Description:** Bakes all `SceneEntityBaker<E>` components attached to or under the specified GameObject.
- **Parameters:**
    - `gameObject` — The GameObject to search.
    - `includeInactive` — Whether to include inactive objects in the search.
- **Returns:** An array of baked entities.

#### `Bake(GameObject, ICollection<E>, bool)`

```csharp
public static void Bake(GameObject gameObject, ICollection<E> results, bool includeInactive = true);
```

- **Description:** Bakes all `SceneEntityBaker<E>` components attached to or under the specified GameObject and adds them to the provided collection.
- **Parameters:**
    - `gameObject` — The GameObject to search.
    - `results` — The collection where baked entities will be added. Must not be `null`.
    - `includeInactive` — Whether to include inactive objects in the search.
- **Exceptions:** Throws `ArgumentNullException` if `results` is `null`.