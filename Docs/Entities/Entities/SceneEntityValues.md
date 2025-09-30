
<details>
  <summary>
    <h2 id="-values">🔑 Values</h2>
    <br> Manage dynamic key-value storage for the entity. Values can be of any type (structs or reference types) and are
         identified by integer keys. This allows flexible runtime data storage, reactive updates, and modular logic.

  </summary>

<br>

> Values in the entity are stored as a **key-value collection with integer keys**. Access, addition, update, and
> removal
> operations generally have **dictionary-like time complexity**. Values can be of any type, including structs and
> reference types, and multiple types can coexist under different keys. Note that adding a struct through the generic
> API
> avoids boxing.

---

### 🛠 Inspector Settings

| Parameters             | Description                                                               |
|------------------------|---------------------------------------------------------------------------|
| `initialValueCapacity` | Initial capacity for values to optimize memory allocation. Default is `1` |

---

### ⚡ Events

#### `OnValueAdded`

```csharp
public event Action<IEntity, int> OnValueAdded  
```

- **Description:** Triggered when a value is added.
- **Parameters:**
    - `IEntity` – The entity where the value was added.
    - `int` – The key of the value that was added.
- **Note:** Allows subscribers to react whenever a new key-value pair is inserted.

#### `OnValueDeleted`

```csharp
public event Action<IEntity, int> OnValueDeleted  
```

- **Description:** Triggered when a value is deleted.
- **Parameters:**
    - `IEntity` – The entity where the value was deleted.
    - `int` – The key of the value that was removed.
- **Note:** Useful for cleanup or reactive updates when values are removed.

#### `OnValueChanged`

```csharp
public event Action<IEntity, int> OnValueChanged  
```

- **Description:** Triggered when a value is changed.
- **Parameters:**
    - `IEntity` – The entity where the value was changed.
    - `int` – The key of the value that was updated.
- **Note:** Enables reactive programming patterns when values are updated.

---

### 🔑 Properties

#### `ValueCount`

```csharp
public int ValueCount { get; }  
```

- **Description:** Number of stored values in the entity.
- **Note:** Provides a quick way to check how many key-value pairs are currently stored.

---

### 🏹 Methods

#### `GetValue<T>(int)`

```csharp
public T GetValue<T>(int key)  
```

- **Description:** Retrieves a value by key and casts it to the specified type.
- **Parameters:** `key` – The key of the value to retrieve.
- **Returns:** `T` – The value associated with the key.
- **Exceptions:** Throws if the key does not exist or cannot be cast.

#### `GetValueUnsafe<T>(int)`

```csharp
public ref T GetValueUnsafe<T>(int key)  
```

- **Description:** Retrieves a value by key as a reference (unsafe, no boxing).
- **Parameters:** `key` – The key of the value to retrieve.
- **Returns:** `ref T` – Reference to the stored value.
- **Exceptions:** Throws if the key does not exist or cannot be cast.

#### `GetValue(int)`

```csharp
public object GetValue(int key)  
```

- **Description:** Retrieves a value by key as an `object`.
- **Parameters:** `key` – The key of the value to retrieve.
- **Returns:** `object` – The value stored at the key.
- **Exceptions:** Throws if the key does not exist.

#### `TryGetValue<T>(int, out T)`

```csharp
public bool TryGetValue<T>(int key, out T value)  
```

- **Description:** Tries to retrieve a typed value by key.
- **Parameters:**
    - `key` – The key of the value to retrieve.
    - `out value` – Output parameter for the retrieved value.
- **Returns:** `true` if the value exists and is of type `T`, otherwise `false`.

#### `TryGetValueUnsafe<T>(int, out T)`

```csharp
public bool TryGetValueUnsafe<T>(int key, out T value)  
```

- **Description:** Tries to retrieve a value by reference (unsafe).
- **Parameters:**
    - `key` – The key of the value.
    - `out value` – Output reference to the value.
- **Returns:** `true` if the value exists and is of type `T`, otherwise `false`.

#### `TryGetValue(int, out object)`

```csharp
public bool TryGetValue(int key, out object value)  
```

- **Description:** Tries to retrieve a value as `object`.
- **Parameters:**
    - `key` – The key of the value.
    - `out value` – Output parameter for the value.
- **Returns:** `true` if the key exists, otherwise `false`.

#### `SetValue<T>(int, T)`

```csharp
public void SetValue<T>(int key, T value) where T : struct  
```

- **Description:** Sets or updates a struct value.
- **Parameters:**
    - `key` – The key to set.
    - `value` – The value to store.
- **Triggers:**
    - `OnValueAdded` if the key did not exist.
    - `OnValueChanged` if the key already existed.
    - `OnStateChanged` in both cases.
- **Exceptions:** Throws if key is invalid.

#### `SetValue(int, object)`

```csharp
public void SetValue(int key, object value)  
```

- **Description:** Sets or updates a reference value.
- **Parameters:**
    - `key` – The key to set.
    - `value` – The value to store.
- **Triggers:**
    - `OnValueAdded` if the key did not exist.
    - `OnValueChanged` if the key already existed.
    - `OnStateChanged` in both cases.
- **Exceptions:** Throws if key is invalid or value is null.

#### `HasValue(int)`

```csharp
public bool HasValue(int key)  
```

- **Description:** Checks if a value exists for the given key.
- **Parameters:** `key` – The key to check.
- **Returns:** `true` if the key exists, otherwise `false`.
- **Triggers:** None.
- **Exceptions:** None.

#### `AddValue<T>(int, T)`

```csharp
public void AddValue<T>(int key, T value) where T : struct  
```

- **Description:** Adds a struct value.
- **Parameters:**
    - `key` – The key to add.
    - `value` – The value to add.
- **Triggers:** `OnValueAdded` and `OnStateChanged`.
- **Exceptions:** Throws if key already exists.

#### `AddValue(int, object)`

```csharp
public void AddValue(int key, object value)  
```

- **Description:** Adds a reference value.
- **Parameters:**
    - `key` – The key to add.
    - `value` – The value to add.
- **Triggers:** `OnValueAdded` and `OnStateChanged`.
- **Exceptions:** Throws if key already exists or value is null.

#### `DelValue(int)`

```csharp
public bool DelValue(int key)  
```

- **Description:** Deletes a value by key.
- **Parameters:** `key` – The key to delete.
- **Returns:** `true` if the value existed and was removed, otherwise `false`.
- **Triggers:** `OnValueDeleted` and `OnStateChanged` if the value existed.

#### `ClearValues()`

```csharp
public void ClearValues()  
```

- **Description:** Clears all values from the entity.
- **Triggers:** `OnValueDeleted` for each key removed and `OnStateChanged`.

#### `GetValues()`

```csharp
public KeyValuePair<int, object>[] GetValues()  
```

- **Description:** Returns all key-value pairs currently stored.
- **Returns:** Array of `KeyValuePair<int, object>`.

#### `CopyValues(KeyValuePair<int, object>[])`

```csharp
public int CopyValues(KeyValuePair<int, object>[] results)  
```

- **Description:** Copies all key-value pairs into the provided array.
- **Parameters:** `results` – Array to copy key-value pairs into.
- **Returns:** Number of values copied.
- **Exceptions:** Throws if `results` is null or too small.

#### `GetValueEnumerator()`

```csharp
public ValueEnumerator GetValueEnumerator()  
```

- **Description:** Enumerates all key-value pairs.
- **Returns:** Struct enumerator for iterating through stored values.

### 🗂 Example of Usage

```csharp
// Assume we have an instance of entity
SceneEntity entity = ...

// Add values by string key
entity.AddValue("Health", 100);
entity.AddValue("Speed", 12.5f);
entity.AddValue("Inventory", new Inventory());

// Get a value
int health = entity.GetValue<int>("Health");
Console.WriteLine($"Health: {health}");

// Update a value
entity.SetValue("Health", 150);

// Remove a value
entity.DelValue("Inventory");
```

</details>