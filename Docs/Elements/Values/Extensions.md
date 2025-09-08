# 🧩 Extensions for Reactive Values and Constants

The **Extensions** class provides utility methods for working with **reactive values** (`IReactiveValue<T>`) and constants (`Const<T>`).  
These extensions simplify subscription patterns and value wrapping.

---

## AsConst

Wraps a value in a `Const<T>` instance.

```csharp
public static Const<T> AsConst<T>(this T it)
```

### Type Parameters
- **T** – The type of the value to wrap.

### Parameters
- **it** – The value to wrap.

### Returns
- A new `Const<T>` instance containing the provided value.
### Example

```csharp
int number = 42;
Const<int> constant = number.AsConst();
Console.WriteLine(constant.Value); // Output: 42
```

### Notes
- Useful for converting ordinary values into constant wrappers.
- Aggressively inlined for performance.
---

### `Observe()`
Subscribes to a reactive value and immediately invokes the callback with the current value.
```csharp
public static Subscription<T> Observe<T>(this IReactiveValue<T> it, Action<T> action)
```

### Type Parameters
- **T** – The type of the reactive value.
### Parameters
- **it** – The reactive value to observe.
- **action** – The callback to invoke on value changes and immediately with the current value.
### Returns
- A `Subscription<T>` representing the active subscription.

### Example
```csharp
IReactiveValue<int> reactive = new ReactiveValue<int>(10);

Subscription<int> subscription = reactive.Observe(value =>
{
    Console.WriteLine($"Current value: {value}");
});

reactive.Value = 20;

// Output:
// Current value: 10
// Current value: 20
```
### Notes
- **Immediate Invocation** – The callback is invoked immediately with the current value.
- **Subscription Management** – Returns a subscription object to allow later unsubscription.
- **Performance** – Marked with `[MethodImpl(MethodImplOptions.AggressiveInlining)]` for efficient calls.