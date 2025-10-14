# 🧩 InlinePredicate&lt;T1, T2&gt;

Represents a <b>parameterless</b> function that returns a result.

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

- **Description:** Represents a predicate with <b>two input arguments</b> that returns a boolean result.
- **Type Parameters:**
  - `T1` — the first input type
  - `T2` — the second input type
- **Inheritance:** [InlineFunction&lt;T1, T2, R&gt;](InlineFunction%602.md), [IPredicate&lt;T1, T2&gt;](IPredicate%602.md)
- **Note:** Supports Odin Inspector


```csharp
public class InlinePredicate<T1, T2> : InlineFunction<T1, T2, bool>, IPredicate<T1, T2>
```


---

### 🏗️ Constructors <div id="-constructors"></div>

#### `InlinePredicate(Func<T1, T2, bool>)`

```csharp
public InlinePredicate(Func<T1, T2, bool> func)
````

- **Description:** Initializes a new instance with the specified function.
- **Parameter:** `func` — the function that takes `T1` and `T2` and returns a boolean.
- **Throws:** `ArgumentNullException` if `func` is null.

---

## 🏹 Methods

#### `Invoke(T1, T2)`

```csharp
public bool Invoke(T1 arg1, T2 arg2)
````

- **Description:** Invokes the function with the provided arguments.
- **Parameters:**
    - `arg1` — the first input parameter
    - `arg2` — the second input parameter
- **Returns:** The logical result of the function.

#### `ToString()`

```csharp
public override string ToString();
````

- **Description:** Returns a string that represents the method name of the function.
- **Returns:** A string representation of the method name of delegate.

---

## 🪄 Operators

#### `operator InlinePredicate<T1, T2>(Func<T1, T2, bool>)`

```csharp
public static implicit operator InlinePredicate<T1, T2>(Func<T1, T2, bool> value);
````

- **Description:** Implicitly converts a delegate of type `Func<T1, T2, bool>` to an `InlinePredicate<T1, T2>`.
- **Parameter:** `value` — the delegate to wrap.
- **Returns:** A new `InlinePredicate<T1, T2>` containing the specified delegate.

---

## 🗂 Example of Usage

```csharp
Character player, enemy = ...
IPredicate<Character, Character> predicate = new InlinePredicate<Character, Character>(
    (a, b) => a.Team != b.Team
);
bool areEnemies = predicate.Invoke(player, enemy);
```

