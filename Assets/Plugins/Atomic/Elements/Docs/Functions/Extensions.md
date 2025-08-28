# Extensions for Function Wrappers

The **Extensions** class provides utility methods for converting **delegates** and **reactive values** into function wrappers (`InlineFunction<T>`).  
These methods simplify function composition, contextual invocation, and boolean inversion.

---

## AsFunction (parameterless)

Wraps a `Func<R>` into an `InlineFunction<R>`.

```csharp
public static InlineFunction<R> AsFunction<R>(this Func<R> func)
```
### Type Parameters
- **R** – The return type of the function.
### Parameters
- **func** – The delegate to wrap.
### Returns
- An `InlineFunction<R>` that wraps the provided delegate.
### Example
```csharp
Func<int> getNumber = () => 42;
InlineFunction<int> funcWrapper = getNumber.AsFunction();
Console.WriteLine(funcWrapper.Invoke()); // Output: 42
```
---
## AsFunction (with context)
Wraps a function with one parameter and a context object into an `InlineFunction<R>`.
```csharp
public static InlineFunction<R> AsFunction<T, R>(this T it, Func<T, R> func)
```
### Type Parameters
- **T** – The type of the context object.
- **R** – The return type of the function.
### Parameters
- **it** – The context object to pass to the function.
- **func** – The function that accepts the context object.
### Returns
- An `InlineFunction<R>` that wraps the contextual invocation.
### Example
```csharp
class Player { public int Score = 100; }

Player player = new Player();
InlineFunction<int> getScoreFunc = player.AsFunction(p => p.Score);

Console.WriteLine(getScoreFunc.Invoke()); // Output: 100
```
---
## Invert
Creates a new function that returns the negation of the current `IFunction<bool>` value.
```csharp
public static InlineFunction<bool> Invert(this IFunction<bool> it)
```
### Parameters
- **it** – The reactive boolean value to negate.
### Returns
- An `InlineFunction<bool>` that returns the inverse of the current value.
### Example
```csharp
InlineFunction<bool> isActive = new InlineFunction<bool>(() => true);
InlineFunction<bool> isInactive = isActive.Invert();

Console.WriteLine(isInactive.Invoke()); // Output: False
```
## Notes
- **Composable** – These extensions allow chaining and functional composition.
- **Contextual Invocation** – `AsFunction<T, R>` enables wrapping functions that depend on a specific object instance.
- **Boolean Utilities** – Invert simplifies negation for reactive boolean functions.