# 🧩 ReactiveInlineVariable<T>.Builder

Fluent builder for creating **ReactiveInlineVariable<T>** instances. Allows constructing
reactive proxy variables by specifying getter, setter, subscribe, and unsubscribe handlers
in a chainable and readable way.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [WithGetter(Func<T>)](#withgetterfunct)
        - [WithSetter(Action<T>)](#withsetteractiont)
        - [WithSubscribe(Action<Action<T>>)](#withsubscribeactionactiont)
        - [WithUnsubscribe(Action<Action<T>>)](#withunsubscribeactionactiont)
        - [Build()](#build)
- [Notes](#-notes)

---

## 🗂 Example of Usage

```csharp
// Using the builder to create a ReactiveInlineVariable
var reactiveProxy = ReactiveInlineVariable<int>
    .StartBuild()
    .WithGetter(() => someValue)
    .WithSetter(value => someValue = value)
    .WithSubscribe(handler => subscribeAction(handler))
    .WithUnsubscribe(handler => unsubscribeAction(handler))
    .Build();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public struct Builder
````

- **Description:** Fluent builder for constructing `ReactiveInlineVariable<T>` instances.
- **See also:** [ReactiveInlineVariable<T>](ReactiveInlineVariable.md)

---

### 🏹 Methods

#### `WithGetter(Func<T>)` <div id="withgetterfunct"></div>

```csharp
public Builder WithGetter(Func<T> getter)
````

- **Description:** Assigns the getter function for the reactive proxy variable.
- **Parameter:** `getter` – Function to retrieve the current value.
- **Returns:** The same builder instance for chaining.
- **Throws:** `ArgumentNullException` if `getter` is null.

#### `WithSetter(Action<T>)` <div id="withsetteractiont"></div>

```csharp
public Builder WithSetter(Action<T> setter)
````

- **Description:** Assigns the setter action for the reactive proxy variable.
- **Parameter:** `setter` – Action to update the value.
- **Returns:** The same builder instance for chaining.
- **Throws:** `ArgumentNullException` if `setter` is null.

#### `WithSubscribe(Action<Action<T>>)` <div id="withsubscribeactionactiont"></div>

```csharp
public Builder WithSubscribe(Action<Action<T>> subscribe)
````

- **Description:** Assigns the subscription handler for value changes.
- **Parameter:** `subscribe` – Action to handle subscription callbacks.
- **Returns:** The same builder instance for chaining.
- **Throws:** `ArgumentNullException` if `subscribe` is null.

#### `WithUnsubscribe(Action<Action<T>>)` <div id="withunsubscribeactionactiont"></div>

```csharp
public Builder WithUnsubscribe(Action<Action<T>> unsubscribe)
````

- **Description:** Assigns the unsubscription handler.
- **Parameter:** `unsubscribe` – Action to handle unsubscription callbacks.
- **Returns:** The same builder instance for chaining.
- **Throws:** `ArgumentNullException` if `unsubscribe` is null.

#### `Build()` <div id="build"></div>

```csharp
public ReactiveInlineVariable<T> Build()
````

- **Description:** Constructs and returns a new `ReactiveInlineVariable<T>` instance with the
  provided getter, setter, subscribe, and unsubscribe handlers.
- **Returns:** A new `ReactiveInlineVariable<T>` instance.
- **Throws:** `InvalidOperationException` if any of the required handlers (getter, setter,
  subscribe, unsubscribe) were not provided.

---

## 📝 Notes

- Ensures that all necessary handlers are assigned before creating the reactive proxy.
- Supports fluent syntax for concise and readable code.
- Useful for integrating external properties or third-party systems into reactive
  architectures without duplicating state.
