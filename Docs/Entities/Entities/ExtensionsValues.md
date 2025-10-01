# 🧩 Value Extensions

Provide extension methods for [IEntity](IEntity.md) to simplify operations with values.

---

## 🏹 Methods

#### `AddValue(string, object)`

```csharp
public static void AddValue(this IEntity entity, string key, object value)
```

- **Description:** Adds a value to the entity.
- **Parameters:**
    - `key` – The name of the value.
    - `value` – The value to add.

#### `AddValue(string, object, out int)`

```csharp
public static void AddValue(this IEntity entity, string key, object value, out int id)
```

- **Description:** Adds a value to the entity and returns the corresponding ID.
- **Parameters:**
    - `key` – The name of the value.
    - `value` – The value to add.
- **Output:** `id` – The numeric ID assigned to the key.

#### `AddValue<T>(string, T)`

```csharp
public static void AddValue<T>(this IEntity entity, string key, T value) where T : struct
```

- **Description:** Adds a strongly-typed value to the entity.
- **Parameters:**
    - `key` – The name of the value.
    - `value` – The strongly-typed value to add.

#### `AddValue<T>(string, T, out int)`

```csharp
public static void AddValue<T>(this IEntity entity, string key, T value, out int id) where T : struct
```

- **Description:** Adds a strongly-typed value and retrieves its ID.
- **Parameters:**
    - `key` – The name of the value.
    - `value` – The strongly-typed value to add.
- **Output:** `id` – The numeric ID assigned to the key.

#### `AddValues(IEnumerable<KeyValuePair<int, object>>)`

```csharp
public static void AddValues(this IEntity entity, IEnumerable<KeyValuePair<int, object>> values)
```

- **Description:** Adds multiple values to the entity.
- **Parameter:** `values` – Collection of key-value pairs (numeric keys) to add.

#### `AddValues(IEnumerable<KeyValuePair<string, object>>)`

```csharp
public static void AddValues(this IEntity entity, IEnumerable<KeyValuePair<string, object>> values)
```

- **Description:** Adds multiple values to the entity using string keys.
- **Parameter:** `values` – Collection of key-value pairs (string keys) to add.

#### `DelValue(string)`

```csharp
public static bool DelValue(this IEntity entity, string key)
```

- **Description:** Removes a value from the entity.
- **Parameter:** `key` – The name of the value to remove.
- **Returns:** `true` if the value was removed; otherwise, `false`.

#### `GetValue<T>(string)`

```csharp
public static T GetValue<T>(this IEntity entity, string key)
```

- **Description:** Retrieves a value of type `T` associated with the given key.
- **Parameter:** `key` – The name of the value.
- **Returns:** The value of type `T`.

#### `TryGetValue<T>(string, out T)`

```csharp
public static bool TryGetValue<T>(this IEntity entity, string key, out T value)
```

- **Description:** Tries to retrieve a value of type `T` associated with the given key.
- **Parameter:** `key` – The name of the value.
- **Output:** `value` – The retrieved value if successful.
- **Returns:** `true` if the value exists and was retrieved; otherwise, `false`.

#### `SetValue(string, object)`

```csharp
public static void SetValue(this IEntity entity, string key, object value)
```

- **Description:** Sets a value in the entity.
- **Parameters:**
    - `key` – The name of the value.
    - `value` – The value to set.

#### `SetValue<T>(string, T)`

```csharp
public static void SetValue<T>(this IEntity entity, string key, T value) where T : struct
```

- **Description:** Sets a strongly-typed value in the entity.
- **Parameters:**
    - `key` – The name of the value.
    - `value` – The strongly-typed value to set.

#### `HasValue(string)`

```csharp
public static bool HasValue(this IEntity entity, string key)
```

- **Description:** Checks if the entity has a value with the given key.
- **Parameter:** `key` – The name of the value.
- **Returns:** `true` if the value exists; otherwise, `false`.

#### `DisposeValues()`

```csharp
public static void DisposeValues(this IEntity entity)
```

- **Description:** Disposes all disposable values stored in the entity.
