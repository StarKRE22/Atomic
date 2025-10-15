# 🧩 ProxyVariable&lt;T&gt;

Provides a **read-write variable** that delegates its value to **external getter and setter
functions**. This is useful when you want to integrate third-party or existing fields /
properties into systems expecting [IVariable\<T>](IVariable.md) without duplicating state.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Transform Position](#ex1)
    - [Using Builder](#ex2)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
    - [Properties](#-properties)
        - [Value](#value)
    - [Methods](#-methods)
        - [Invoke()](#invoke)
        - [Invoke(T arg)](#invoket-arg)
    - [Nested Types](#-nested-types)
        - [Builder](#builder)
- [Notes](#-notes)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Transform Position

```csharp
//Create a new proxy of Transform.position
IVariable<Vector3> position = new ProxyVariable<Vector3>(
    getter: () => transform.position,
    setter: value => transform.position = value
);

//Move position:
position.Value += Vector3.forward; 
```

Also, you can use the [fluent builder](ProxyVariableBuilder.md) for proxy creation:

<div id="ex2"></div>

### 2️⃣ Using Builder

```csharp
//Create a new proxy of Transform.position
IVariable<Vector3> position = ProxyVariable<Vector3>
    .StartBuild()
    .WithGetter(() => transform.position)
    .WithSetter(value => transform.position = value)
    .Build();

//Move position:
position.Value += Vector3.forward; 
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class ProxyVariable<T> : IVariable<T>
```

- **Description:** Provides a **read-write variable** that delegates its value to **external getter and setter
  functions**.
- **Inheritance:** [IVariable&lt;T&gt;](IVariable.md)
- **Type Parameter:** `T` – The type of the value.
- **Note:** Supports Odin Inspector

---

<div id="-constructors"></div>

### 🏗️ Constructors

```csharp
public ProxyVariable(Func<T> getter, Action<T> setter)
```

- **Description:** Initializes a new instance of `ProxyVariable<T>` using the provided getter and setter functions.
- **Parameters:**
    - `getter` – A function to retrieve the value.
    - `setter` – An action to update the value.
- **Throws:** `ArgumentNullException` if either `getter` or `setter` is null.

---

### 🔑 Properties

#### `Value`

```csharp
public T Value { get; set; }
```

- **Description:** Gets or sets the current value.
- **Access:** Read-write

---

### 🏹 Methods

#### `Invoke()`

```csharp
public T Invoke();
```

- **Description:** Invokes the function and returns the value.
- **Returns:** The current value of type `T`.

#### `Invoke(T arg)`

```csharp
public void Invoke(T arg)
```

- **Description:** Sets the value of the variable to the provided argument.
- **Parameter:** `arg` – The new value to assign to the variable.

---

### 🧩 Nested Types

#### `Builder`

```csharp
public struct Builder
```

- **Description:** Fluent builder for constructing `ProxyVariable<T>` instances.
- **See also:** [Builder Documentation](ProxyVariableBuilder.md)

---

## 📝 Notes

- Integrating external or third-party APIs (e.g., Unity’s `Transform`, networking states).
- Adapting existing properties / fields to [IVariable\<T>](IVariable.md) without refactoring.
- Testing: Makes it easy to substitute mock getters / setters in unit tests.