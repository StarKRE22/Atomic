# 🧩 InlineAction Classes

The **InlineAction** classes provide wrappers around standard `System.Action` delegates.  
They implement the corresponding `IAction` interfaces and allow invoking actions directly, optionally with parameters.  
They also support implicit conversion from the underlying `Action` delegates and, if using Odin Inspector, inline display and buttons.

---

## 🧩 InlineAction
!!!
public class InlineAction : IAction
!!!
- **Description:** Represents a **parameterless action** that can be invoked.

### Constructors

#### `InlineAction(Action action)`
!!!
public InlineAction(Action action)
!!!
- **Description:** Initializes a new instance with the specified action.
- **Parameter:** `action` – The action to invoke.
- **Throws:** `ArgumentNullException` if `action` is null.

### Methods

#### `Invoke()`
!!!
public void Invoke()
!!!
- **Description:** Invokes the wrapped action.

---

## 🧩 InlineAction<T>
!!!
public class InlineAction<T> : IAction<T>
!!!
- **Description:** Represents an action with one parameter that can be invoked.

### Constructors

#### `InlineAction(Action<T> action)`
!!!
public InlineAction(Action<T> action)
!!!
- **Description:** Initializes a new instance with the specified action.
- **Parameter:** `action` – The action to invoke.
- **Throws:** `ArgumentNullException` if `action` is null.

### Methods

#### `Invoke(T arg)`
!!!
public void Invoke(T arg)
!!!
- **Description:** Invokes the wrapped action with the specified argument.
- **Parameter:** `arg` – The argument to pass to the action.

---

## 🧩 InlineAction<T1, T2>
!!!
public class InlineAction<T1, T2> : IAction<T1, T2>
!!!
- **Description:** Represents an action with two parameters that can be invoked.

### Constructors

#### `InlineAction(Action<T1, T2> action)`
!!!
public InlineAction(Action<T1, T2> action)
!!!
- **Description:** Initializes a new instance with the specified action.
- **Parameter:** `action` – The action to invoke.
- **Throws:** `ArgumentNullException` if `action` is null.

### Methods

#### `Invoke(T1 arg1, T2 arg2)`
!!!
public void Invoke(T1 arg1, T2 arg2)
!!!
- **Description:** Invokes the wrapped action with the specified arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument

---

## 🧩 InlineAction<T1, T2, T3>
!!!
public class InlineAction<T1, T2, T3> : IAction<T1, T2, T3>
!!!
- **Description:** Represents an action with three parameters that can be invoked.

### Constructors

#### `InlineAction(Action<T1, T2, T3> action)`
!!!
public InlineAction(Action<T1, T2, T3> action)
!!!
- **Description:** Initializes a new instance with the specified action.
- **Parameter:** `action` – The action to invoke.
- **Throws:** `ArgumentNullException` if `action` is null.

### Methods

#### `Invoke(T1 arg1, T2 arg2, T3 arg3)`
!!!
public void Invoke(T1 arg1, T2 arg2, T3 arg3)
!!!
- **Description:** Invokes the wrapped action with the specified arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument
    - `arg3` – The third argument

---

## 🧩 InlineAction<T1, T2, T3, T4>
!!!
public class InlineAction<T1, T2, T3, T4> : IAction<T1, T2, T3, T4>
!!!
- **Description:** Represents an action with four parameters that can be invoked.

### Constructors

#### `InlineAction(Action<T1, T2, T3, T4> action)`
!!!
public InlineAction(Action<T1, T2, T3, T4> action)
!!!
- **Description:** Initializes a new instance with the specified action.
- **Parameter:** `action` – The action to invoke.
- **Throws:** `ArgumentNullException` if `action` is null.

### Methods

#### `Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)`
!!!
public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
!!!
- **Description:** Invokes the wrapped action with the specified arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument
    - `arg3` – The third argument
    - `arg4` – The fourth argument































# 🧩 InlineAction Classes

The **InlineAction** classes are lightweight wrappers around standard `System.Action` delegates. They implement the corresponding [IAction](IAction.md) interfaces, allowing actions to be executed and used polymorphically.




























### Key Features

- Wrap any `System.Action` or `System.Action<T>` delegate.
- Support implicit conversion from native `Action` types.
- Optional integration with Odin Inspector for inline property and button execution.
- Provides `ToString()` to return the method name of the wrapped delegate.
- Supports up to **four generic parameters** (`InlineAction<T1, T2, T3, T4>`).

> **Note:** `InlineAction` is ideal for game development scenarios where actions on game objects need to be *
*polymorphic**, such as event handling, command execution, or reactive systems.

### Example of Usage
Procedural polymorphism and composition over inheritance
```csharp
var tank = new Entity("Tank");
tank.AddValue<IAction<Vector3>>("MoveAction",
    new InlineAction<Vector3>(direction => MoveByRigidbody(tank, direction))
);

var ship = new Entity("Ship");
ship.AddValue<IAction<Vector3>>("MoveAction",
    new InlineAction<Vector3>(direction => MoveByTransform(ship, direction))
);

// Invoke actions
tank.GetValue<IAction<Vector3>>("MoveAction").Invoke(Vector3.forward); // Moves by Rigidbody
ship.GetValue<IAction<Vector3>>("MoveAction").Invoke(Vector3.forward); // Moves by Transform
```
