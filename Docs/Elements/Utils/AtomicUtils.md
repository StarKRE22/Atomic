# 🧩 AtomicUtils

Provides **utility methods** for checking the current Unity application mode. Useful for conditional logic that depends
on whether the game is running in **Play Mode** or **Edit Mode**.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [IsPlayMode()](#ex1)
    - [IsEditMode()](#ex2)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [IsPlayMode()](#isplaymode)
        - [IsEditMode()](#iseditmode)
- [Best Practices](#-best-practices)

---

## 🗂 Examples of Usage

<div id="ex1"></div>

### 1️⃣ IsPlayMode()

```csharp
if (AtomicUtils.IsPlayMode())
    Console.WriteLine("Application is running in Play Mode.");
```

<div id="ex2"></div>

### 2️⃣ IsEditMode()

```csharp
if (AtomicUtils.IsEditMode())
    Console.WriteLine("Application is in Edit Mode and not compiling.");
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public static class AtomicUtils
`````

### 🏹 Methods

#### `IsPlayMode()`

```csharp
public static bool IsPlayMode()
````

- **Description:** Determines whether the application is currently in **Play Mode**.
- **Returns:**
    - `true` if the application is in Play Mode.
    - `false` otherwise.
    - In builds (outside the editor), always returns `true`.

#### `IsEditMode()`

```csharp
public static bool IsEditMode()
````

- **Description:** Determines whether the application is currently in **Edit Mode** and not compiling.
- **Returns:**
    - `true` if the application is in Edit Mode and not compiling.
    - `false` otherwise.
    - In builds (outside the editor), always returns `false`.

---

## 📌 Best Practices

- [Using AtomicUtils for EntityInstallers](../../BestPractices/UsingUtilsForEntityInstallers.md)