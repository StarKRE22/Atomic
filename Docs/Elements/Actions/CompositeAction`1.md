# 🧩 CompositeAction&lt;T&gt;

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

## 🏗️ Constructors

#### `CompositeAction()`

```csharp
public CompositeAction()
```

- **Description:** Initializes a new instance
- **Note:** This constructor is intended **only for use by the Unity Inspector** when using `[SerializeReference]`.

#### `CompositeAction(params IAction<T>[])`

```csharp
public CompositeAction(params IAction<T>[] actions)
```

- **Description:** Initializes a new instance with the specified array of actions.
- **Parameter:** `actions` – The actions to include.
- **Throws:** `ArgumentNullException` if `actions` is null.

#### `CompositeAction(IEnumerable<Action<T>)`

```csharp
public CompositeAction(IEnumerable<IAction<T>> actions)
```

- **Description:** Initializes a new instance with the specified collection of actions.
- **Parameter:** `actions` – The actions to include.
- **Throws:** `ArgumentNullException` if `actions` is null.

---

## 🏹 Methods

#### `Invoke(T)`

```csharp
public void Invoke(T arg)
```

- **Description:** Invokes all actions sequentially with the given argument.
- **Parameter:** `arg` – The input argument.

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