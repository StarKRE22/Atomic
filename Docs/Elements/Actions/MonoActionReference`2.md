# 🧩 MonoActionReference&lt;T1, T2&gt;

A reference wrapper for a [MonoAction&lt;T1, T2&gt;](MonoAction%602.md) with <b>two parameters</b>.
Assign a `MonoAction<T1, T2>` component in the Inspector and invoke it using `Invoke()`.

---

## 📑 Table of Contents

- [Inspector Settings](#-inspector-settings)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [Constructor()](#monoactionreference)
        - [Constructor(MonoAction\<T1, T2>)](#monoactionreferencemonoactionabstractt1-t2)
    - [Fields](#-fields)
        - [Action](#action)
    - [Methods](#-methods)
        - [Invoke(T1, T2)](#invoket1-t2)

---

## 🛠 Inspector Settings

| Parameter | Description                           |
|-----------|---------------------------------------|
| `action`  | The referenced scene action to invoke |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public sealed class MonoActionReference<T1, T2> : IAction<T1, T2>
```

- **Description:** A reference wrapper for a [MonoAction&lt;T1, T2&gt;](MonoAction%602.md) with <b>two
  parameters</b>.
- **Inheritance:** [IAction&lt;T1, T2&gt;](IAction%602.md)
- **Type parameters:**
    - `T1` — first argument
    - `T2` — second argument
- **Notes:** Supports Unity serialization and Odin Inspector
- **Usage:**

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `MonoActionReference()`

```csharp
public MonoActionReference();
```

- **Description:** Default constructor, intended **only for use by the Unity Inspector**.
- **Usage:** Required for Unity to serialize the reference in the Inspector.

#### `MonoActionReference(MonoAction<T1, T2>)`

```csharp
public MonoActionReference(MonoAction<T1, T2> action);
```

- **Description:** Creates a new reference wrapping the specified `MonoAction<T1, T2>`.
- **Parameter:** `action` — The `MonoAction<T1, T2>` to reference.

---

### 🧱 Fields

#### `Action`

```csharp
public MonoAction<T1, T2> action;
```

- **Description:** Reference to the scene action to invoke.
- **Access:** Read / Write

---

### 🏹 Methods

#### `Invoke(T1, T2)`

```csharp
public void Invoke(T1 arg1, T2 arg2);
```

- **Description:** Invokes the referenced scene action with the provided arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument
