# 🧩 CompositeAction&lt;T1, T2&gt;

Represents a group of actions <b>with two parameters</b> that are executed sequentially.

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
        - [Invoke(T1, T2)](#invoket1-t2)

---

## 🗂 Example of Usage

```csharp
IAction<int, int> composite = new CompositeAction<int, int>(
    new InlineAction<int, int>((a, b) => Console.WriteLine(a + b)),
    new InlineAction<int, int>((a, b) => Console.WriteLine(a * b))
);z

composite.Invoke(3, 4);

// Output:
// 7
// 12
```

- **See also:** [InlineAction\<T1, T2>](InlineAction%601.md)

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public class CompositeAction<T1, T2> : IAction<T1, T2>
```

- **Description:** Represents a group of actions <b>with two parameters</b> that are executed sequentially.
- **Inheritance:** [IAction&lt;T1, T2&gt;](IAction%602.md)
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
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
public CompositeAction(params IAction<T1, T2>[] actions)
```

- **Description:** Initializes a new instance with the specified array of actions.
- **Parameter:** `actions` – The actions to include.
- **Throws:** `ArgumentNullException` if `actions` is null.

#### `IEnumerable Constructor`

```csharp
public CompositeAction(IEnumerable<IAction<T1, T2>> actions)
```

- **Description:** Initializes a new instance with the specified collection of actions.
- **Parameter:** `actions` – The actions to include.
- **Throws:** `ArgumentNullException` if `actions` is null.

---

### 🏹 Methods

#### `Invoke(T1, T2)`

```csharp
public void Invoke(T1 arg1, T2 arg2)
```

- **Description:** Invokes all actions sequentially with the given arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument