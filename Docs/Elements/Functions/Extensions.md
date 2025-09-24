# 🧩 Function Extensions

The **FunctionExtensions** class provides utility methods for composing and transforming [IFunction](IFunction.md) instances.  
One of the most useful helpers is `Invert`, which creates a new function that represents the **logical negation** of an existing boolean-returning function.

---

#### `Invert(IFunction<bool>)`
```csharp
public static InlineFunction<bool> Invert(this IFunction<bool> it)
`````
- **Description:** Creates a new function that returns the **negation** of the current boolean value.
- **Parameter:** `it` – The reactive boolean function to negate.
- **Returns:** A new `InlineFunction<bool>` that returns the opposite of the original function’s result.
- **Example of Usage:**
 
  ```csharp
  IFunction<bool> isAlive = new InlineFunction<bool>(() => health > 0);
  IFunction<bool> isDead = isAlive.Invert();

  bool dead = isDead.Invoke(); // true if health <= 0
  ````

---

#### `Invert<T>(IFunction<T, bool>)`
```csharp
public static InlineFunction<T, bool> Invert<T>(this IFunction<T, bool> it)
````
- **Description:** Creates a new function that returns the **negation** of a boolean predicate with one input argument.
- **Type Parameter:** `T` – The input parameter type.
- **Parameter:** `it` – The predicate function to negate.
- **Returns:** A new `InlineFunction<T, bool>` that returns the opposite of the original function’s result.
- **Example of Usage:**
  
  ```csharp
  IFunction<Character, bool> isEnemy = new InlineFunction<Character, bool>(c => c.Team != player.Team);
  IFunction<Character, bool> isAlly = isEnemy.Invert();

  bool ally = isAlly.Invoke(otherCharacter); // true if same team
  ````

---

#### `Invert<T1, T2>(IFunction<T1, T2, bool>)`
```csharp
public static InlineFunction<T1, T2, bool> Invert<T1, T2>(this IFunction<T1, T2, bool> it)
````
- **Description:** Creates a new function that returns the **negation** of a boolean predicate with two input arguments.
- **Type Parameters:**
    - `T1` – The first input parameter type.
    - `T2` – The second input parameter type.
- **Parameter:** `it` – The predicate function to negate.
- **Returns:** A new `InlineFunction<T1, T2, bool>` that returns the opposite of the original function’s result.
- **Example of Usage:**
  
  ```csharp
  IFunction<Character, Character, bool> isEnemyPair = new InlineFunction<Character, Character, bool>((a, b) => a.Team != b.Team);
  IFunction<Character, Character, bool> isAllyPair = isEnemyPair.Invert();

  bool allies = isAllyPair.Invoke(player, teammate); // true if same team
  ````
---

#### `Add<R>(ICollection<Func<R>>, IFunction<R>)`
```csharp
public static void Add<R>(this ICollection<Func<R>> it, IFunction<R> member) => it.Add(member.Invoke);  
```
- **Description:** Adds a parameterless function to a collection of `<Func<R>>`. Wraps the `IFunction<R>.Invoke` method as a delegate.
- **Type Parameter:** `R` — The return type of the function.
- **Parameters:**
  - `it` — The target collection to add the function to.
  - `member` — The `IFunction<R>` whose `Invoke` method will be added.

---

#### `Remove<R>(ICollection<Func<R>>, IFunction<R>)`
```csharp
public static void Remove<R>(this ICollection<Func<R>> it, IFunction<R> member) => it.Remove(member.Invoke);  
```
- **Description:** Removes a parameterless function from a collection of `<Func<R>>`. Wraps the `IFunction<R>.Invoke` method as a delegate.
- **Type Parameter:** `R` — The return type of the function.
- **Parameters:**
  - `it` — The target collection to remove the function from.
  - `member` — The `IFunction<R>` whose `Invoke` method will be removed.

---

## 📝 Notes
- **Null Safety** – `Invert` assumes that the input function is non-null. Passing `null` will result in an exception.
- **Performance** – Each `Invert` call creates a small wrapper around the original function. With `[MethodImpl(MethodImplOptions.AggressiveInlining)]`, the overhead is negligible.
- **Use Cases:**
    - Simplifying boolean logic by reusing existing functions.
    - Quickly flipping conditions without writing additional lambdas.
    - Maintaining cleaner and more readable predicate composition in reactive systems.  