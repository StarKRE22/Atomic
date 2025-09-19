# 🧩 ProxyVariable&lt;T&gt;

Provides a **read-write variable** that delegates its value to **external getter and setter functions. It
implements [IVariable&lt;T&gt;](IVariable.md), allowing you to **wrap existing data sources** and expose them
through a unified variable interface. This is useful when you want to integrate third-party or existing fields /
properties into systems expecting `IVariable<T>` without duplicating state.

```csharp
public class ProxyVariable<T> : IVariable<T>
```

- **Type Parameter:** `T` – The type of the value being proxied.

---

## 🏗️ Constructors

```csharp
public ProxyVariable(Func<T> getter, Action<T> setter)
```

- **Description:** Initializes a new instance of `ProxyVariable<T>` using the provided getter and setter functions.
- **Parameters:**
    - `getter` – A function to retrieve the value.
    - `setter` – An action to update the value.
- **Throws:** `ArgumentNullException` if either `getter` or `setter` is null.

---

## 🔑 Properties

#### `Value`

```csharp
public T Value { get; set; }
```

- **Description:** Gets or sets the current value.
- **Access:** Read-write
- **Notes:**
    - Implements [IValue&lt;T&gt;.Value](../Values/IValue.md#value) for read access.
    - Implements [ISetter&lt;T&gt;.Value](../Setters/ISetter.md/#value) for write access.

---

## 🏹 Methods

#### `Invoke()`

```csharp
public T Invoke();
```

- **Description:** Invokes the function and returns the value.
- **Returns:** The current value of type `T`.
- **Notes**: This is the default implementation from [IFunction&lt;R&gt;.Invoke()](../Functions/IFunction.md#invoke)

#### `Invoke(T arg)`

```csharp
public void Invoke(T arg)
```

- **Description:** Sets the value of the variable to the provided argument.
- **Parameter:** `arg` – The new value to assign to the variable.
- **Notes:**
    - Acts as a setter method, complementing the `Value` property.
    - Default implementation comes from [IAction&lt;R&gt;.Invoke()](../Actions/IAction.md#invoket).

---

## 👷‍♂️ Builder

**`ProxyVariable<T>` also includes a fluent builder to simplify creation:**

```csharp
IVariable<int> variable = ProxyVariable<int>
    .StartBuild()
    .WithGetter(() => myField)
    .WithSetter(v => myField = v)
    .Build();
```

---

## 🗂 Example of Usage

### Wrapping a Transform’s Position

```csharp
//Create a new proxy of Transform.position
IVariable<Vector3> position = new ProxyVariable<Vector3>(
    getter: () => transform.position,
    setter: value => transform.position = value
);

//Move position:
position.Value += Vector3.forward; 
```

### Using the Fluent Builder

```csharp
//Create a new proxy of Transform.position
IVariable<Vector3> position = ProxyVariable<Vector3>
    .StartBuild()
    .WithGetter(() => transform.position)
    .WithSetter(value => transform.position = value)
    .Build();

//Move position:
position.Value += Vector3.forward; 
```

---

## 🧩 Subclasses

For convenience, several specialized proxy variable implementations are provided.

### 🧩 Player Prefs

- `BoolPrefsVariable` – Boolean variable stored in PlayerPrefs
- `IntPrefsVariable` – Integer variable stored in PlayerPrefs
- `FloatPrefsVariable` – Float variable stored in PlayerPrefs
- `StringPrefsVariable` – String variable stored in PlayerPrefs

### 🧩 Transform

- `TransformParentVariable` – Stores a `Transform` parent reference
- `TransformPositionVariable` – Stores a `Vector3` position
- `TransformRotationVariable` – Stores a `Quaternion` rotation
- `TransformScaleVariable` – Stores a `Vector3` scale

---

## 📝 Notes

Below are some notes on when to use `ProxyVariable<T>`:

- Integrating external or third-party APIs (e.g., Unity’s `Transform`, networking states).
- Adapting existing properties / fields to `IVariable<T>` without refactoring.
- Testing: Makes it easy to substitute mock getters / setters in unit tests.

