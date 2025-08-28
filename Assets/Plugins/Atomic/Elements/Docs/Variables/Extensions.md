# Extensions for Variable Wrappers

The **Extensions** class provides utility methods for creating **variable wrappers**, including standard, reactive, and proxy variables.  
These methods simplify the creation of variables that support encapsulation, reactivity, and indirect access.

---

## AsVariable

Wraps a value in a `BaseVariable<T>`.

```csharp
public static BaseVariable<T> AsVariable<T>(this T it)
```
### Type Parameters
- **T** – The type of the value to wrap.
### Parameters
- **it** – The value to wrap.
### Returns
- A `BaseVariable<T>` containing the given value.
### Example
```csharp
int number = 42;
BaseVariable<int> variable = number.AsVariable();
Console.WriteLine(variable.Value); // Output: 42
```
---
## AsReactiveVariable
Wraps a value in a `ReactiveVariable<T>` to support reactive subscriptions.
```csharp
public static ReactiveVariable<T> AsReactiveVariable<T>(this T it)
```
### Type Parameters
- **T** – The type of the value to wrap.
### Parameters
- **it** – The value to wrap.
### Returns
- A `ReactiveVariable<T>` containing the given value.
### Example
```csharp
ReactiveVariable<int> reactive = 10.AsReactiveVariable();
reactive.Observe(value => Console.WriteLine($"Current value: {value}"));

reactive.Value = 20; 
// Output:
// Current value: 10
// Current value: 20
```
---
## AsProxyVariable
Creates a `ProxyVariable<R>` that wraps access to a field or property of an object.
```csharp
public static ProxyVariable<R> AsProxyVariable<T, R>(this T it, Func<T, R> getter, Action<T, R> setter)
```
### Type Parameters
- **T** – The type of the source object.
- **R** – The type of the value being proxied.
### Parameters
- **it** – The source object.
- **getter** – A function to retrieve the value from the object.
- **setter** – An action to set the value on the object.
### Returns
- A `ProxyVariable<R>` that reflects the value through the provided getter and setter.
### Example
```csharp
class Player
{
    public int Health;
}

Player player = new Player { Health = 100 };
ProxyVariable<int> healthProxy = player.AsProxyVariable(
    p => p.Health,
    (p, value) => p.Health = value
);

Console.WriteLine(healthProxy.Value); // Output: 100
healthProxy.Value = 75;
Console.WriteLine(player.Health);     // Output: 75
```
### Notes
- **Encapsulation** – ProxyVariable allows controlled access to external fields or properties.
- **Reactivity** – Can be combined with reactive wrappers for change tracking.
- **Performance** – Methods are aggressively inlined for minimal call overhead.