# 🧩  InlineSetter&lt;T&gt;

The **InlineSetter** class provides a wrapper around a standard `System.Action<T>` delegate. It implements the [ISetter&lt;T&gt;](ISetter.md) interface, enabling **value assignment** through a delegate.

---

## Type Parameter
- `T` – the type of the value to be set.
---

## Constructors

#### `InlineSetter(Action<T>)`
```csharp
public InlineSetter(Action<T> action)
```
- **Description:** Initializes a new instance of `InlineSetter<T>` with the specified action.
- **Parameter:** `action` — The action to invoke when the value is set.
- **Throws:** `ArgumentNullException` if `action` is null.

## Properties

#### `Value`
```csharp
T Value { set; }
```
- **Description:** Assigns the provided value.
- **Parameter:** `value` — the new value to be set.

## Methods

#### `Invoke(T arg)`

```csharp
void Invoke(T arg);
```
- **Description:** Invokes the setter by assigning the provided value.
- **Parameter:** `arg` — the value to set.
- **Notes:** Default implementation comes from [IAction&lt;T&gt;.Invoke()](../Actions/IAction.md#invoket).

#### `ToString()`
```csharp
public override string ToString();
```
- **Description:** Returns a string representing the method name of the underlying action.
- **Returns:** A string representation of the method name of the delegate.

## Operators

#### `implicit operator InlineSetter<T>(Action<T>)`
```csharp
public static implicit operator InlineSetter<T>(Action<T> action);
```
- **Description:** Implicitly converts a delegate of type `Action<T>` to an `InlineSetter<T>`.
- **Parameter:** `action` — The delegate to wrap.
- **Returns:** A new `InlineSetter<T>` instance containing the specified delegate.