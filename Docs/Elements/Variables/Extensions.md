# 🧩 Variable Extensions

The **Extensions** class provides utility methods for creating **variable wrappers**, including standard, reactive, and
proxy variables. These methods simplify the creation of variables that support encapsulation, reactivity, and indirect
access.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [AsVariable()](#ex1)
    - [AsReactiveVariable()](#ex2)
    - [AsProxyVariable()](#ex3)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [AsVariable<T>()](#asvariablet)
        - [AsReactiveVariable<T>()](#asreactivevariablet)
        - [AsProxyVariable<T, R>()](#asproxyvariablet-r)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Using AsVariable()

```csharp
BaseVariable<int> variable = 42.AsVariable();
Console.WriteLine(variable.Value); // Output: 42
```

<div id="ex2"></div>

### 2️⃣ Using  AsReactiveVariable()

```csharp
ReactiveVariable<int> reactiveVariable = 10.AsReactiveVariable();
reactiveVariable.Subscribe(value => Console.WriteLine($"Current value: {value}"));
reactiveVariable.Value = 20; 

// Output:
// Current value: 20
```

<div id="ex3"></div>

### 3️⃣ Using  AsProxyVariable()

```csharp
ProxyVariable<Vector3> positionProxy = transform.AsProxyVariable(
    getter: t => t.position, 
    setter: (t, value) => t.position = value
);

positionProxy.Value = Vector3.zero;
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public static class Extensions
```

---

### 🏹 Methods

#### `AsVariable<T>()`

```csharp
public static BaseVariable<T> AsVariable<T>(this T it)
```

- **Description:** Wraps a value in a `BaseVariable<T>`.
- **Type Parameter**: `T` – The type of the value to wrap
- **Parameter:** `it` – The value to wrap.
- **Returns:** A `BaseVariable<T>` containing the given value.

#### `AsReactiveVariable<T>()`

```csharp
public static ReactiveVariable<T> AsReactiveVariable<T>(this T it)
```

- **Description:** Wraps a value in a `ReactiveVariable<T>` to support reactive subscriptions.
- **Type Parameter:** `T` – The type of the value to wrap.
- **Parameter:** `it` – The value to wrap.
- **Returns:** A `ReactiveVariable<T>` containing the given value.

#### `AsProxyVariable<T, R>()`

```csharp
public static ProxyVariable<R> AsProxyVariable<T, R>(
    this T it,
    Func<T, R> getter,
    Action<T, R> setter
)
```

- **Description:** Creates a `ProxyVariable<R>` that wraps access to a field or property of an object.
- **Type Parameters**:
    - **T** – The type of the source object.
    - **R** – The type of the value being proxied.
- **Parameters:**
    - **it** – The source object.
    - **getter** – A function to retrieve the value from the object.
    - **setter** – An action to set the value on the object.
- **Returns:** A `ProxyVariable<R>` that reflects the value through the provided getter and setter.