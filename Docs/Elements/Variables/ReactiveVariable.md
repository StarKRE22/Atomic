# 🧩 ReactiveVariable&lt;T&gt;

```csharp
[Serializable]
public class ReactiveVariable<T> : IReactiveVariable<T>, IDisposable
```

- **Description:** Represents a **serialized reactive variable** that raises events whenever its value changes.
- **Inheritance:** [IReactiveVariable&lt;T&gt;](IReactiveVariable.md), `IDisposable`,
- **Type Parameter:** `T` – The type of the value.
- **Notes:** Support Unity serialization and Odin Inspector

---

## 🏗️ Constructors

#### `ReactiveVariable()`

```csharp
public ReactiveVariable()
```

- **Description:** Initializes a new instance with the default value of `T`.

#### `ReactiveVariable(T value)`

```csharp
public ReactiveVariable(T value)
```

- **Description:** Initializes a new instance with a specified constant value `value`.
- **Parameter:** `value` – The initial value to initialize the instance with.

---

## ⚡ Events

#### `OnValueChanged`

```csharp
event Action<T> OnValueChanged
```

- **Description:** Triggered whenever the value changes.
- **Parameter**: `T` – The new value after the change.
- **Note:** Allows subscribers to react to value changes in a reactive programming pattern.

---

## 🔑 Properties

#### `Value`

```csharp
public T Value { get; set; }
```

- **Description:** Gets or sets the current value.
- **Access:** Read-write

---

## 🏹 Methods

#### `Invoke()`

```csharp
public T Invoke()
```

- **Description:** Invokes the variable and returns its current value.
- **Returns:** The current value of type `T`.

#### `Invoke(T)`

```csharp
public void Invoke(T arg)
```

- **Description:** Sets the value of the variable to the provided argument.
- **Parameter:** `arg` – The new value to assign to the variable.

#### `Subscribe(Action)`

```csharp
public Subscription<T> Subscribe(Action action)  
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [Subscription&lt;T&gt;](../Events/Subscription%601.md) struct representing the active
  subscription.

#### `Unsubscribe(Action)`

```csharp
public void Unsubscribe(Action action)  
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.

#### `Dispose`

```csharp
public void Dispose()
```

- **Description:** Clears all listeners and releases resources.

#### `ToString()`

```csharp
public override string ToString();
```

- **Description:** Returns a string that represents the wrapped constant value.
- **Returns:** A string representation of the constant value.

--- 

## 🪄 Operators

#### `operator ReactiveVariable<T>(T)`

```csharp
public static implicit operator ReactiveVariable<T>(T value);
```

- **Description:** Implicitly converts a value of type `T` to a `ReactiveVariable<T>`.
- **Parameter:** `value` – The value to wrap in a `ReactiveVariable<T>`.
- **Returns:** A new `ReactiveVariable<T>` containing the specified value.

---

## 🗂 Example of Usage

```csharp
// Initialize with a starting value
var score = new ReactiveVariable<int>(10);

// Subscribe to changes
score.Subscribe(newValue => Console.WriteLine("Score updated: " + newValue));

// Change the value
score.Value = 20; // Triggers subscription callback

 // Dispose to clear subscriptions
score.Dispose();
```