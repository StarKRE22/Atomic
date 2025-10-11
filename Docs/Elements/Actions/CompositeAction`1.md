# 🧩 CompositeAction&lt;T&gt;

Represents a group of actions <b>with one parameter</b> that are executed sequentially.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [Default Constructor](#default-constructor)
        - [Params Constructor](#params-constructor)
        - [IEnumerable Constructor](#ienumerable-constructor)
    - [Methods](#-methods)
        - [Invoke(T)](#invoket)

---

## 🗂 Example of Usage

```csharp
IAction<string> composite = new CompositeAction<string>(
    new InlineAction<string>(msg => Console.WriteLine("Hello " + msg)),
    new InlineAction<string>(msg => Console.WriteLine("Bye " + msg))
);

composite.Invoke("World");

// Output:
// Hello World
// Bye World
```

- **See also:** [InlineAction\<T>](InlineAction%601.md)

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public class CompositeAction<T> : IAction<T>
```

- **Description:** Represents a group of actions <b>with one parameter</b> that are executed sequentially.
- **Inheritance:** [IAction&lt;T&gt;](IAction%601.md)
- **Type parameter:** `T` — the input parameter.
- **Notes:** Supports Unity serialization and Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `Default Constructor`

```csharp
public CompositeAction()
```

- **Description:** Initializes a new instance
- **Note:** This constructor is intended **only for use by the Unity Inspector** when using `[SerializeReference]`.

#### `Params Constructor`

```csharp
public CompositeAction(params IAction<T>[] actions)
```

- **Description:** Initializes a new instance with the specified array of actions.
- **Parameter:** `actions` – The actions to include.
- **Throws:** `ArgumentNullException` if `actions` is null.

#### `IEnumerable Constructor`

```csharp
public CompositeAction(IEnumerable<IAction<T>> actions)
```

- **Description:** Initializes a new instance with the specified collection of actions.
- **Parameter:** `actions` – The actions to include.
- **Throws:** `ArgumentNullException` if `actions` is null.

---

### 🏹 Methods

#### `Invoke(T)`

```csharp
public void Invoke(T arg)
```

- **Description:** Invokes all actions sequentially with the given argument.
- **Parameter:** `arg` – The input argument.