# 🧩 ExpressionMember

A family of lightweight **structs** that represent a single member of an expression. Each member wraps a delegate that
produces a value, and optionally stores the source object that registered the delegate.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [ExpressionMember&lt;R&gt;](#-expressionmemberr)
  - [ExpressionMember&lt;T, R&gt;](#-expressionmembert-r)
  - [ExpressionMember&lt;T1, T2, R&gt;](#-expressionmembert1-t2-r)
- [Notes](#-notes)

---

## 🗂 Example of Usage

```csharp
// Parameterless member
ExpressionMember<bool> member = new ExpressionMember<bool>(() => true);
bool result = member.Invoke();
```

```csharp
// Member with source object
ExpressionMember<int, bool> healthCheck = new ExpressionMember<int, bool>(
    player,
    health => health > 0
);

bool alive = healthCheck.Invoke(50);
```

```csharp
// Member with two input parameters
ExpressionMember<int, int, bool> damageCheck = new ExpressionMember<int, int, bool>(
    enemy,
    (armor, damage) => damage > armor
);

bool penetrates = damageCheck.Invoke(10, 25);
```

---

## 🔍 API Reference

### 🏛️ ExpressionMember&lt;R&gt;

```csharp
[Serializable]
public struct ExpressionMember<R>
```

- **Description:** Represents a parameterless expression member that produces a value of type `R`.
- **Type Parameter:** `R` — The return type of the expression.
- **Notes:** Supports Odin Inspector through `ShowInInspector` on the `Source` property.

#### 🔑 Properties

##### `Source`

```csharp
public object Source { get; }
```

- **Description:** Gets the object associated with this member, typically used to identify the owner or registration source.
- **Returns:** The source object, or `null` if none was provided.

#### 🏗️ Constructors

##### `ExpressionMember(Func<R>)`

```csharp
public ExpressionMember(Func<R> func)
```

- **Description:** Initializes the member from a delegate.
- **Parameter:** `func` — The delegate that produces the result.

##### `ExpressionMember(IFunction<R>)`

```csharp
public ExpressionMember(IFunction<R> func)
```

- **Description:** Initializes the member from an `IFunction<R>` wrapper.
- **Parameter:** `func` — The function wrapper to invoke.

##### `ExpressionMember(object, Func<R>)`

```csharp
public ExpressionMember(object source, Func<R> func)
```

- **Description:** Initializes the member with an associated source object and delegate.

##### `ExpressionMember(object, IFunction<R>)`

```csharp
public ExpressionMember(object source, IFunction<R> func)
```

- **Description:** Initializes the member with an associated source object and function wrapper.

#### 🏹 Methods

##### `EqualsFunction(Func<R>)`

```csharp
public readonly bool EqualsFunction(Func<R> func)
```

- **Description:** Determines whether this member wraps the specified delegate instance.
- **Returns:** `true` if the wrapped delegate matches; otherwise `false`.

##### `Invoke()`

```csharp
public readonly R Invoke()
```

- **Description:** Invokes the wrapped delegate.
- **Returns:** The value returned by the delegate.

---

### 🏛️ ExpressionMember&lt;T, R&gt;

```csharp
[Serializable]
public struct ExpressionMember<T, R>
```

- **Description:** Represents an expression member with one input parameter.
- **Type Parameters:**
  - `T` — The input parameter type.
  - `R` — The return type.

#### 🔑 Properties

##### `Source`

```csharp
public object Source { get; }
```

- **Description:** Gets the object associated with this member.

#### 🏗️ Constructors

##### `ExpressionMember(Func<T, R>)`

```csharp
public ExpressionMember(Func<T, R> func)
```

- **Description:** Initializes the member from a delegate.

##### `ExpressionMember(object, Func<T, R>)`

```csharp
public ExpressionMember(object source, Func<T, R> func)
```

- **Description:** Initializes the member with an associated source object and delegate.

#### 🏹 Methods

##### `EqualsFunction(Func<T, R>)`

```csharp
public readonly bool EqualsFunction(Func<T, R> func)
```

- **Description:** Determines whether this member wraps the specified delegate instance.

##### `Invoke(T)`

```csharp
public readonly R Invoke(T arg)
```

- **Description:** Invokes the wrapped delegate with the specified argument.
- **Parameter:** `arg` — The input argument.
- **Returns:** The value returned by the delegate.

---

### 🏛️ ExpressionMember&lt;T1, T2, R&gt;

```csharp
[Serializable]
public struct ExpressionMember<T1, T2, R>
```

- **Description:** Represents an expression member with two input parameters.
- **Type Parameters:**
  - `T1` — The type of the first input parameter.
  - `T2` — The type of the second input parameter.
  - `R` — The return type.

#### 🔑 Properties

##### `Source`

```csharp
public object Source { get; }
```

- **Description:** Gets the object associated with this member.

#### 🏗️ Constructors

##### `ExpressionMember(Func<T1, T2, R>)`

```csharp
public ExpressionMember(Func<T1, T2, R> func)
```

- **Description:** Initializes the member from a delegate.

##### `ExpressionMember(object, Func<T1, T2, R>)`

```csharp
public ExpressionMember(object source, Func<T1, T2, R> func)
```

- **Description:** Initializes the member with an associated source object and delegate.

#### 🏹 Methods

##### `EqualsFunction(Func<T1, T2, R>)`

```csharp
public readonly bool EqualsFunction(Func<T1, T2, R> func)
```

- **Description:** Determines whether this member wraps the specified delegate instance.

##### `Invoke(T1, T2)`

```csharp
public readonly R Invoke(T1 arg1, T2 arg2)
```

- **Description:** Invokes the wrapped delegate with the specified arguments.
- **Parameters:**
  - `arg1` — The first input argument.
  - `arg2` — The second input argument.
- **Returns:** The value returned by the delegate.

---

## 📝 Notes

- `ExpressionMember` is a **struct**, so it is allocated on the stack when used as a local variable.
- The `Source` property is useful for tracking which object registered a delegate, for example when building
  expression-based conditions or predicates.
- For parameterless members, prefer the `Func<R>` constructor; for members backed by `IFunction<R>`, the wrapper
  automatically uses `func.Invoke` as the delegate.
