# 🧩 Invert Extensions

Provide utility methods those create a new function that represents the **logical negation** of an existing
boolean-returning [function](IFunctions.md).

---

## 🏹 Methods

#### `Invert(IFunction<bool>)`

```csharp
public static InlineFunction<bool> Invert(this IFunction<bool> it)
`````

- **Description:** Creates a new function that returns the **negation** of the current boolean value.
- **Parameter:** `it` – The reactive boolean function to negate.
- **Returns:** A new `InlineFunction<bool>` that returns the opposite of the original function’s result.

#### `Invert<T>(IFunction<T, bool>)`

```csharp
public static InlineFunction<T, bool> Invert<T>(this IFunction<T, bool> it)
````

- **Description:** Creates a new function that returns the **negation** of a boolean predicate with one input argument.
- **Type Parameter:** `T` – The input parameter type.
- **Parameter:** `it` – The predicate function to negate.
- **Returns:** A new `InlineFunction<T, bool>` that returns the opposite of the original function’s result.

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

---

## 🗂 Examples of Usage

#### `IFunction<bool>` (no parameters)

```csharp
IFunction<bool> isAlive = new InlineFunction<bool>(() => health > 0);
IFunction<bool> isDead = isAlive.Invert();

bool dead = isDead.Invoke(); // true if health <= 0
```

#### `IFunction<T, bool>` (with one parameter)

```csharp
IFunction<Character, bool> isEnemy = new InlineFunction<Character, bool>(c => c.Team != player.Team);
IFunction<Character, bool> isAlly = isEnemy.Invert();

bool ally = isAlly.Invoke(otherCharacter); // true if same team
```

#### `IFunction<T1, T2, bool>` (with two parameters)

```csharp
IFunction<Character, Character, bool> isEnemyPair = new InlineFunction<Character, Character, bool>((a, b) => a.Team != b.Team);
IFunction<Character, Character, bool> isAllyPair = isEnemyPair.Invert();

bool allies = isAllyPair.Invoke(player, teammate); // true if same team
```
