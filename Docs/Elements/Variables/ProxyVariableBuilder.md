# 🧩 ProxyVariable\<T>.Builder

Fluent builder for constructing [ProxyVariable\<T>](ProxyVariable.md) instances. This allows creating
proxy variables by specifying getter and setter delegates in a clean, chainable way.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [WithGetter(Func<T>)](#withgetterfunct)
        - [WithSetter(Action<T>)](#withsetteractiont)
        - [Build()](#build)
- [Notes](#-notes)

---

## 🗂 Example of Usage

```csharp
// Using the builder to create a ProxyVariable
var proxy = ProxyVariable<int>.Builder()
.WithGetter(() => someValue)
.WithSetter(value => someValue = value)
.Build();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public struct Builder
````

- **Description:** Fluent builder for constructing `ProxyVariable<T>` instances.
- **See also:** [ProxyVariable<T>](ProxyVariable.md)

---

### 🏹 Methods

#### `WithGetter(Func<T>)` <div id="withgetterfunct"></div>

```csharp
public Builder WithGetter(Func<T> getter)
````

- **Description:** Assigns the getter function for the proxy variable.
- **Parameter:** `getter` – Function to retrieve the current value.
- **Returns:** The same builder instance for chaining.
- **Throws:** `ArgumentNullException` if `getter` is null.

#### `WithSetter(Action<T>)` <div id="withsetteractiont"></div>

```csharp
public Builder WithSetter(Action<T> setter)
````

- **Description:** Assigns the setter action for the proxy variable.
- **Parameter:** `setter` – Action to update the value.
- **Returns:** The same builder instance for chaining.
- **Throws:** `ArgumentNullException` if `setter` is null.

#### `Build()` <div id="build"></div>

```csharp
public ProxyVariable<T> Build()
````

- **Description:** Constructs and returns a new `ProxyVariable<T>` configured with the
  provided getter and setter.
- **Returns:** A new `ProxyVariable<T>` instance.
- **Throws:** `InvalidOperationException` if either getter or setter was not provided.

---

## 📝 Notes

- Ensures that getter and setter are always assigned before creating the proxy.
- Supports fluent syntax for concise and readable code.
- Useful for integrating external or existing properties/fields into systems expecting
  `IVariable<T>` without duplicating state.
