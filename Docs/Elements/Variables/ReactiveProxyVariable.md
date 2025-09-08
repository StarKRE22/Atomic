# 🧩 ReactiveProxyVariable&lt;T&gt;

`ReactiveProxyVariable<T>` is a **reactive proxy variable** that delegates reading, writing, and subscription operations to external handlers.  

This is useful when you need to **wrap an existing data source or event system** and expose it through the unified [IReactiveVariable&lt;T&gt;](IReactiveVariable.md) interface.

---

## Type Parameter
- `T` – the value type.

---

## Events

#### `OnValueChanged`
```csharp
event Action<T> OnValueChanged
```
- **Description:** Triggered whenever the value changes.
- **Parameter**: `T` – The new value after the change.
- **Note:** Allows subscribers to react to value changes .

---

## Constructor

```csharp
public ReactiveProxyVariable(Func<T> getter, Action<T> setter)
```
- **Description:** Initializes a new instance of `ProxyVariable<T>` using the provided getter and setter functions.
- **Parameters:**
    - `getter` – A function to retrieve the value.
    - `setter` – An action to update the value.
    - `subscribe` – An action to handle the subscription.
    - `unsubscribe` – An action to handle the unsubscription.
- **Throws:** `ArgumentNullException` if either `getter`, `setter` `subscription` or `unsubscription` is null.

## Properties

#### `Value`
```csharp
new T Value { get; set; }
```
- **Description:** Gets or sets the current value.
- **Access:** Read-write
- **Notes:**
    - Implements [IValue<T>.Value](../Values/IValue.md#value) for read access.
    - Implements [ISetter<T>.Value](../Setters/ISetter.md/#value) for write access.

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

---

## Builder
**`ReactiveProxyVariable<T>` provides a convenient fluent builder**

```csharp
IReactiveVariable<int> variable = ReactiveProxyVariable<int>
    .StartBuild()
    .WithGetter(() => someInt)
    .WithSetter(v => someInt = v)
    .WithSubscribe(cb => myEvent += cb)
    .WithUnsubscribe(cb => myEvent -= cb)
    .Build();
```