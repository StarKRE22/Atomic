# 🧩 Entity Values

Manage dynamic key-value storage for the entity. Values can be of any type (structs or reference types) and are
identified by integer keys. This allows flexible runtime data storage, reactive updates, and modular logic.

> [!IMPORTANT]
> Values in the entity are stored as a **key-value collection with integer keys**. Access, addition, update, and
> removal
> operations generally have **dictionary-like time complexity**. Values can be of any type, including structs and
> reference types, and multiple types can coexist under different keys. Note that adding a struct through the generic
> API avoids boxing.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
    - [Using Numeric Keys](#ex1)
    - [Using String Names](#ex2)
    - [Using Entity API](#ex3)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - <details>
      <summary><a href="#-events">Events</a></summary>

        - [OnValueAdded](#onvalueadded)
        - [OnValueDeleted](#onvaluedeleted)
        - [OnValueChanged](#onvaluechanged)

      </details>
    - <details>
      <summary><a href="#-properties">Properties</a></summary>

        - [ValueCount](#valuecount)

      </details>
    - <details>
      <summary><a href="#-methods">Methods</a></summary>

        - [GetValue&lt;T&gt;(int)](#getvaluetint)
        - [GetValueUnsafe&lt;T&gt;(int)](#getvalueunsafetint)
        - [GetValue(int)](#getvalueint)
        - [TryGetValue&lt;T&gt;(int, out T)](#trygetvaluetint-out-t)
        - [TryGetValueUnsafe&lt;T&gt;(int, out T)](#trygetvalueunsafetint-out-t)
        - [TryGetValue(int, out object)](#trygetvalueint-out-object)
        - [SetValue&lt;T&gt;(int, T)](#setvaluetint-t)
        - [SetValue(int, object)](#setvalueint-object)
        - [HasValue(int)](#hasvalueint)
        - [AddValue&lt;T&gt;(int, T)](#addvaluetint-t)
        - [AddValue(int, object)](#addvalueint-object)
        - [DelValue(int)](#delvalueint)
        - [ClearValues()](#clearvalues)
        - [GetValues()](#getvalues)
        - [CopyValues(KeyValuePair&lt;int, object&gt;[])](#copyvalueskeyvaluepairint-object)
        - [GetValueEnumerator()](#getvalueenumerator)

      </details>

---

## 🗂 Examples of Usage

This example demonstrates how to use **values** with entity, including adding, retrieving, updating, and removing
values. Three approaches are shown:

<div id="ex1"></div>

### 1️⃣ Using Numeric Keys

By default, all values use `int` keys because this avoids computing hash codes and is very fast; therefore, the example
below uses numeric keys as the default approach.

```csharp
//Define value keys 
const int Health = 1;
const int Speed = 2;
const int Inventory = 3;

// Create a new instance of entity
Entity entity = new Entity();

// Subscribe to value events
entity.OnValueChanged += (e, key) => Console.WriteLine($"Value {key} changed");

//Add health property
entity.AddValue(Health, 100);

//Add speed property
entity.AddValue(Speed, 12.5f);

//Add inventory property
entity.AddValue(Inventory, new Inventory());

// Get a value
int health = entity.GetValue<int>(Health);
Console.WriteLine($"Health: {health}");

// Update a Health
entity.SetValue(Health, 150);

// Remove a Speed value
entity.DelValue(Speed);
```

---

<div id="ex2"></div>

### 2️⃣ Using String Names

In this example, for convenience, there are [extension methods](ExtensionsValues.md) for the entity. This format is
more user-friendly but slightly slower than using numeric keys.

```csharp
// Create a new instance of entity
Entity entity = new Entity();

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

---

<div id="ex3"></div>

### 3️⃣ Using Entity API

Managing values by raw `int` keys or `string` names can be error-prone, especially in larger projects. To make the
process easier and **type-safe**, the Atomic Framework supports **code generation**. You describe all your tags and
values once in a small config file, and the framework automatically generates
strongly-typed C# helpers. More details are in the Manual under
the [Entity API](../EntityAPI/Manual.md) section.

```csharp
// Create a new instance of entity
Entity entity = new Entity();

// Add values
entity.AddHealth(100);
entity.AddSpeed(12.5f);
entity.AddInventory(new GridInventory());

// Get a value
int health = entity.GetHealth();
Console.WriteLine($"Health: {health}");

// Update a value
entity.SetHealth(150);

// Remove a value
entity.DelInventory();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public partial class Entity
```

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