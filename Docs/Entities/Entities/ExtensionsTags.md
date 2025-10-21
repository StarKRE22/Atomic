# 🧩 IEntity Tag Extensions

Provide extension methods for [IEntity](IEntity.md) to simplify operations with tags.

---

## 📑 Table of Contents

- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [AddTag(string)](#addtagstring)
        - [AddTag(string, out int)](#addtagstring-out-int)
        - [AddTags(IEnumerable<int>)](#addtagsienumerableint)
        - [AddTags(IEnumerable<string>)](#addtagsienumerablestring)
        - [DelTag(string)](#deltagstring)
        - [HasTag(string)](#hastagstring)
        - [HasAllTags(params int[])](#hasalltagsparams-int)
        - [HasAllTags(params string[])](#hasalltagsparams-string)
        - [HasAnyTag(params string[])](#hasanytagparams-string)
        - [HasAnyTag(params int[])](#hasanytagparams-int)

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public partial class Extensions
```

---

### 🏹 Methods

#### `AddTag(string)`

```csharp
public static bool AddTag(this IEntity entity, string key)
```

- **Description:** Adds a tag to the entity by name.
- **Parameter:** `key` – The name of the tag to add.
- **Returns:** `true` if the tag was successfully added; otherwise, `false`.

#### `AddTag(string, out int)`

```csharp
public static bool AddTag(this IEntity entity, string key, out int id)
```

- **Description:** Adds a tag to the entity and returns its numeric ID.
- **Parameter:** `key` – The name of the tag to add.
- **Output:** `id` – The numeric ID assigned to the tag.
- **Returns:** `true` if the tag was successfully added; otherwise, `false`.

#### `AddTags(IEnumerable<int>)`

```csharp
public static void AddTags(this IEntity entity, IEnumerable<int> tags)
```

- **Description:** Adds multiple tags to the entity.
- **Parameter:** `tags` – Collection of numeric tag IDs to add.

#### `AddTags(IEnumerable<string>)`

```csharp
public static void AddTags(this IEntity entity, IEnumerable<string> tags)
```

- **Description:** Adds multiple tags to the entity by string identifiers.
- **Parameter:** `tags` – Collection of tag names to add.

#### `DelTag(string)`

```csharp
public static bool DelTag(this IEntity entity, string tag)
```

- **Description:** Removes a tag from the entity.
- **Parameter:** `tag` – The name of the tag to remove.
- **Returns:** `true` if the tag was successfully removed; otherwise, `false`.

#### `HasTag(string)`

```csharp
public static bool HasTag(this IEntity entity, string key)
```

- **Description:** Checks if the entity has the specified tag.
- **Parameter:** `key` – The name of the tag to check.
- **Returns:** `true` if the entity has the tag; otherwise, `false`.

#### `HasAllTags(params int[])`

```csharp
public static bool HasAllTags(this IEntity entity, params int[] tags)
```

- **Description:** Checks if the entity contains all the specified numeric tags.
- **Parameter:** `tags` – Array of numeric tag IDs.
- **Returns:** `true` if the entity has all the tags; otherwise, `false`.

#### `HasAllTags(params string[])`

```csharp
public static bool HasAllTags(this IEntity entity, params string[] tags)
```

- **Description:** Checks if the entity has all the specified tags by name.
- **Parameter:** `tags` – Array of tag names.
- **Returns:** `true` if the entity has all the tags; otherwise, `false`.

#### `HasAnyTag(params string[])`

```csharp
public static bool HasAnyTag(this IEntity entity, params string[] tags)
```

- **Description:** Checks if the entity has any of the specified tags by name.
- **Parameter:** `tags` – Array of tag names.
- **Returns:** `true` if the entity has at least one of the tags; otherwise, `false`.

#### `HasAnyTag(params int[])`

```csharp
public static bool HasAnyTag(this IEntity entity, params int[] tags)
```

- **Description:** Checks if the entity contains any of the specified numeric tags.
- **Parameter:** `tags` – Array of numeric tag IDs.
- **Returns:** `true` if the entity has at least one of the tags; otherwise, `false`.