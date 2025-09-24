# 🧩 InlinePredicate

```csharp
public class InlinePredicate : InlineFunction<bool>, IPredicate
```

- **Description:** Represents a <b>parameterless</b> predicate that returns a boolean result.
- **Inheritance:** [InlineFunction&lt;R&gt;](InlineFunction.md), [IPredicate](IPredicate.md)
- **Note:** Supports Odin Inspector

---

## 🏗️ Constructors

#### `InlinePredicate(Func<bool>)`

```csharp
public InlinePredicate(Func<bool> func)
```

- **Description:** Initializes a new instance with the specified boolean-returning function.
- **Parameter:** `func` — the function to invoke.
- **Throws:** `ArgumentNullException` if `func` is null.

---

## 🔑 Properties

#### `Value`

```csharp
public T Value { get; }
```

- **Description:** Invokes the wrapped function and returns the result.
- **Returns:** The result of type `T`.

---

## 🏹 Methods

#### `Invoke()`

```csharp
public bool Invoke()
```

- **Description:** Invokes the function and returns boolean result.
- **Returns:** The logical result of the function.

#### `ToString()`

```csharp
public override string ToString();
```

- **Description:** Returns a string that represents the method name of function.
- **Returns:** A string representation of the method name of delegate.

---

## 🪄 Operators

#### `operator InlinePredicate(Func<bool>)`

```csharp
public static implicit operator InlinePredicate(Func<bool> value);
```

- **Description:** Implicitly converts a delegate of type `Func<bool>` to an `InlinePredicate`.
- **Parameter:** `value` — the delegate to wrap.
- **Returns:** A new `InlinePredicate` containing the specified delegate.

---

## 🗂 Example of Usage

```csharp
GameObject gameObject = ...
IPredicate predicate = new InlinePredicate(
    () => gameObject.activeSelf
);

bool activeSelf = predicate.Invoke();
```