# 🧩 Optional&lt;T&gt;

```csharp
[Serializable]
public struct Optional<T>
```

- **Description:**  Represents a **Unity-friendly optional value** that can be serialized and displayed in the
  Inspector. It supports activation state, implicit conversions, and safe value access.
- **Type Parameter:** `T` – The type of the value to store.
- **Notes:** Support Unity serialization and Odin Inspector

---

## 🔑 Properties

#### `Value`

```csharp
public T Value { get; set; }
```

- **Description:** Accesses the underlying value. Assigning a value automatically sets the optional as active (if not
  `null`).
- **Remarks:** Use this property to modify or read the contained value safely.

#### `Active`

```csharp
public bool Active { get; set; }
```

- **Description:** Shows whether the optional contains a valid value.
- **Remarks:** You can manually activate or deactivate the optional.

---

## 🏹 Methods

#### `TryGetValue(out T)`

```csharp
public bool TryGetValue(out T value)
```

- **Description:** Attempts to retrieve the value.
- **Parameter:** `value` — Output parameter that will hold the optional's value if active.
- **Returns:** `true` if the optional is active; otherwise `false`.

#### `GetValueOrDefault(T)`

```csharp
public T GetValueOrDefault(T defaultValue)
```

- **Description:** Retrieves the value if active; otherwise returns a provided default value.
- **Parameter:** `defaultValue` — The fallback value to return if the optional is inactive.
- **Returns:** The active value or the provided default.

---

## 🪄 Operators

#### `operator Optional<T>(T)`

```csharp
public static implicit operator Optional<T>(T it)
```

- **Description:** Automatically wraps a value of type `T` in an active optional.
- **Remarks:** Useful for clean initialization and assignment.

#### `operator T(Optional<T>)`

```csharp
public static implicit operator T(Optional<T> it)
```

- **Description:** Extracts the underlying value from the optional.
- **Remarks:** Does not check for `Active`; ensure optional is active before using.

#### `operator true(Optional<T>)`

```csharp
public static bool operator true(Optional<T> it)
```

- **Description:** Returns `true` if the optional is active.

#### `operator false(Optional<T>)`

```csharp
public static bool operator false(Optional<T> it)
```

- **Description:** Returns `false` if the optional is not active.

---

## 📝 Notes

- Serializable and Unity Inspector friendly.
- Represents optional settings without relying on null references.
- Supports implicit conversion to and from the underlying type.
- Provides safe access methods and operators to check for presence.