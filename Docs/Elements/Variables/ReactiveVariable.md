# 🧩 ReactiveVariable&lt;T&gt;

`ReactiveVariable<T>` is a **serialized reactive variable** that raises events whenever its value changes. It implements [IReactiveVariable&lt;T&gt;](IReactiveVariable.md) and `IDisposable`, providing **read-write** access with **change notifications**.

---

## Type Parameter

- `T` – The type of the stored value.

---

## Constructors

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

## Events

#### `OnValueChanged`
```csharp
event Action<T> OnValueChanged
```
- **Description:** Triggered whenever the value changes.
- **Parameter**: `T` – The new value after the change.
- **Note:** Allows subscribers to react to value changes in a reactive programming pattern.

---

## Properties

#### `Value`
```csharp
new T Value { get; set; }
```
- **Description:** Gets or sets the current value.
- **Access:** Read-write
- **Notes:**
  - Implements [IVariable<T>.Value](IVariable.md#value) for read-write access.
  - Implements [IReactiveValue<T>.Value](../Values/IReactiveValue.md#value) for reactive observation.

---

## Methods

#### `Invoke()`
```csharp
T Invoke()
```
- **Description:** Invokes the variable and returns its current value.
- **Returns:** The current value of type `T`.
- **Note:** Default implementation comes from [IFunction<R>.Invoke()](../Functions/IFunction.md#invoke).

#### `Invoke(T arg)`
```csharp
void Invoke(T arg)
```
- **Description:** Sets the value of the variable to the provided argument.
- **Parameter:** `arg` – The new value to assign to the variable.
- **Notes:**
  - Acts as a setter method, complementing the `Value` property.
  - Default implementation comes from [IAction<T>.Invoke()](../Actions/IAction.md#invoket).

#### `Subscribe(Action)`
```csharp
Subscription<T> Subscribe(Action action)  
```
- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [Subscription&lt;T&gt;](../Signals/Subscription.md#subscriptiont) struct representing the active subscription.
- **Notes**: This is the default implementation from [ISignal&lt;T&gt;.Subscribe()](../Signals/ISignal.md#subscribetactiont)

#### `Unsubscribe(Action)`
```csharp
void `Unsubscribe(Action action)`  
```
- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.
- **Notes**: This is the default implementation from [ISignal&lt;T&gt;.Unsubscribe()](../Signals/ISignal.md#unsubscribetactiont)

#### `Dispose`
```csharp
void Dispose()
```
- **Description:** Clears all listeners and releases resources.

#### `ToString()`
```csharp
public override string ToString();
```
- **Description:** Returns a string that represents the wrapped constant value.
- **Returns:** A string representation of the constant value.

--- 

## Operators

#### `implicit operator ReactiveVariable<T>(T value)`
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

## 🧩 Specialized Reactive Variables
There are **specialized reactive variants** that **do not require an `EqualityComparer`** and allow slightly faster `Value` assignments.

- **Common types**
  - `ReactiveFloat`
  - `ReactiveInt`
  - `ReactiveBool`


- **Unity types**
  - `ReactiveVector2`
  - `ReactiveVector3`
  - `ReactiveQuaternion`


- **Unity Mathematics**
  - `float3Reactive`
  - `quaternionReactive`
