# 🧩 InlineAction

- **Description:** Represents a <b>parameterless delegate action</b> that can be invoked.
- **Inheritance:** [IAction](IAction.md)
- **Note:** Supports Odin Inspector

```csharp
public class InlineAction : IAction
```

---

## 🏗️ Constructors

#### `InlineAction(Action)`

```csharp
public InlineAction(Action action)
```

- **Description:** Initializes a new instance with the specified action.
- **Parameter:** `action` – The action to invoke.
- **Throws:** `ArgumentNullException` if `action` is null.

---

## 🏹 Methods

#### `Invoke()`

```csharp
public void Invoke()
```

- **Description:** Invokes the wrapped action.

#### `ToString()`

```csharp
public override string ToString();
```

- **Description:** Returns a string that represents the method name of action.
- **Returns:** A string representation of the method name of delegate.

---

## 🪄 Operators

#### `operator InlineAction(Action)`

```csharp
public static implicit operator InlineAction(Action action);
```

- **Description:** Implicitly converts a delegate of type `Action` to a `InlineAction`.
- **Parameter:** `action` – the delegate to wrap.
- **Returns:** A new `InlineAction` containing the specified delegate.

---

## 🗂 Example of Usage

```csharp
IAction action = new InlineAction(() => Console.WriteLine("Hello World!"));
action.Invoke(); // Output: Hello World!
```