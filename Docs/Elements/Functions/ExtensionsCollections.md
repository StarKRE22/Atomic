# 🧩 Collections Function Extensions

Provide utility methods for adding and removing [functions](IFunctions.md) to collections.

---

#### `Add<R>(ICollection<Func<R>>, IFunction<R>)`
```csharp
public static void Add<R>(this ICollection<Func<R>> it, IFunction<R> member)
```
- **Description:** Adds a parameterless function to a collection of `<Func<R>>`. Wraps the `IFunction<R>.Invoke` method as a delegate.
- **Type Parameter:** `R` — The return type of the function.
- **Parameters:**
  - `it` — The target collection to add the function to.
  - `member` — The `IFunction<R>` whose `Invoke` method will be added.

---

#### `Remove<R>(ICollection<Func<R>>, IFunction<R>)`
```csharp
public static void Remove<R>(this ICollection<Func<R>> it, IFunction<R> member)
```
- **Description:** Removes a parameterless function from a collection of `<Func<R>>`. Wraps the `IFunction<R>.Invoke` method as a delegate.
- **Type Parameter:** `R` — The return type of the function.
- **Parameters:**
  - `it` — The target collection to remove the function from.
  - `member` — The `IFunction<R>` whose `Invoke` method will be removed.
