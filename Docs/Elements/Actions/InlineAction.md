# 🧩 InlineAction

Represents a <b>parameterless delegate action</b> that can be invoked.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
      - [InlineAction(Action)](#inlineactionaction)
    - [Methods](#-methods)
        - [Invoke()](#invoke)
        - [ToString()](#tostring)
    - [Operators](#-operators)
      - [InlineAction(Action)](#operator-inlineactionaction)

---

## 🗂 Example of Usage

Below is an example of using inline action for writing "Hello World" to the Console:

```csharp
IAction action = new InlineAction(() => Console.WriteLine("Hello World!"));
action.Invoke(); // Output: Hello World!
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class InlineAction : IAction
```

- **Description:** Represents a <b>parameterless delegate action</b> that can be invoked.
- **Inheritance:** [IAction](IAction.md)
- **Note:** Supports Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `InlineAction(Action)`

```csharp
public InlineAction(Action action)
```

- **Description:** Initializes a new instance with the specified action.
- **Parameter:** `action` – The action to invoke.
- **Throws:** `ArgumentNullException` if `action` is null.

---

### 🏹 Methods

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

### 🪄 Operators

#### `operator InlineAction(Action)`

```csharp
public static implicit operator InlineAction(Action action);
```

- **Description:** Implicitly converts a delegate of type `Action` to a `InlineAction`.
- **Parameter:** `action` – the delegate to wrap.
- **Returns:** A new `InlineAction` containing the specified delegate.