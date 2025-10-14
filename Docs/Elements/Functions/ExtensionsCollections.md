# 🧩 IFunction Collections Extensions

Provide utility methods for adding and removing [functions](IFunctions.md) to collections.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Add Function](#ex1)
    - [Remove Function](#ex2)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Add<R>(ICollection<Func<R>>, IFunction<R>)](#addricollectionfuncr-ifunctionr)
        - [Remove<R>(ICollection<Func<R>>, IFunction<R>)](#removericollectionfuncr-ifunctionr)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ Add Function

```csharp
// Assume we have a colletion of `Func<R>` delegates
ICollection<Func<R>> functions = ...;

// Assume we have a some function of type `IFunction<R>`
IFunction<R> myFunction = ...

// Add this function to the collection
functions.Add(myFunction);
```

<div id="ex2"></div>

### 2️⃣ Remove Function

```csharp
// Assume we have a colletion of `Func<R>` delegates
ICollection<Func<R>> functions = ...;

// Assume we have a some function of type `IFunction<R>`
IFunction<R> myFunction = ...

// Remove this function from the collection
functions.Remove(myFunction);
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public static class Extensions
```

---

### 🏹 Methods

#### `Add<R>(ICollection<Func<R>>, IFunction<R>)`

```csharp
public static void Add<R>(this ICollection<Func<R>> it, IFunction<R> member)
```

- **Description:** Adds a parameterless function to a collection of `<Func<R>>`. Wraps the `IFunction<R>.Invoke` method
  as a delegate.
- **Type Parameter:** `R` — The return type of the function.
- **Parameters:**
    - `it` — The target collection to add the function to.
    - `member` — The `IFunction<R>` whose `Invoke` method will be added.

#### `Remove<R>(ICollection<Func<R>>, IFunction<R>)`

```csharp
public static void Remove<R>(this ICollection<Func<R>> it, IFunction<R> member)
```

- **Description:** Removes a parameterless function from a collection of `<Func<R>>`. Wraps the `IFunction<R>.Invoke`
  method as a delegate.
- **Type Parameter:** `R` — The return type of the function.
- **Parameters:**
    - `it` — The target collection to remove the function from.
    - `member` — The `IFunction<R>` whose `Invoke` method will be removed.