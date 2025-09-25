# 🧩 Value Extensions

The **Extensions** class provides utility methods for working with **reactive values** [IReactiveValue&lt;T&gt;](IReactiveValue.md) and constants [Const&lt;T&gt;](Const.md). These extensions simplify subscription patterns and value wrapping.

---

#### `AsConst<T>(T)`
```csharp
public static Const<T> AsConst<T>(this T it)
```
- **Description:** Wraps a value in a `Const<T>` instance.
- **Type Parameter:** `T` – The type of the value to wrap.
- **Parameter:** - `it` – The value to wrap. 
- **Returns:** A new `Const<T>` instance containing the provided value.
- **Notes:**
  - Useful for converting ordinary values into constant wrappers.
  - Aggressively inlined for performance.

#### `Observe()`
```csharp
public static Subscription<T> Observe<T>(this IReactiveValue<T> it, Action<T> action)
```
- **Description:** Subscribes to a reactive value and immediately invokes the callback with the current value.
- **Type Parameter:** `T` – The type of the reactive value.
- **Parameters:**
  - `it` – The reactive value to observe.
  - `action` – The callback to invoke on value changes and immediately with the current value.
- **Returns:** A [Subscription&lt;T&gt;](../Signals/Subscription.md#subscriptiont) representing the active subscription.

---

## Examples of Usage

#### Using `AsConst`
   
```csharp
int number = 42;
Const<int> constant = number.AsConst();
Console.WriteLine(constant.Value); // Output: 42
```
#### Using `Observe`
    
```csharp
IReactiveValue<int> reactive = new ReactiveValue<int>(10);
Subscription<int> subscription = reactive.Observe(
   value => Console.WriteLine($"Current value: {value}")
);

// Output:
// Current value: 10

reactive.Value = 20;

// Output:
// Current value: 20
```   
