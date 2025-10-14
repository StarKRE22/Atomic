# 🧩 IRequest&lt;T1, T2&gt;

Represents a request action with <b>two input arguments</b>.

---

## 📑 Table of Contents

- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Properties](#-properties)
        - [Required](#required)
        - [Arg1](#arg1)
        - [Arg2](#arg2)
    - [Methods](#-methods)
        - [Invoke(T1, T2)](#invoket1-t2)
        - [Consume(out T1, out T2)](#consumeout-t1-out-t2)
        - [TryGet(out T1, out T2)](#trygetout-t1-out-t2)

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IRequest<T1, T2> : IAction<T1, T2>
```

- **Description:** Represents a request action with <b>two input arguments</b>.
- **Inheritance:** [IAction&lt;T1, T2&gt;](../Actions/IAction%602.md)

- **Type parameters:**
    - `T1` — first argument
    - `T2` — second argument

---

### 🔑 Properties

#### `Required`

```csharp
public bool Required { get; }
```

- **Description:** Indicates whether the request must be handled.

#### `Arg1`

```csharp
public T1 Arg1 { get; }
```

- **Description:** Get the first argument of the request.

#### `Arg2`

```csharp
public T2 Arg2 { get; }
```

- **Description:** Get the second argument of the request.

---

### 🏹 Methods

#### `Invoke(T1, T2)`

```csharp
public void Invoke(T1 arg1, T2 arg2);
```

- **Description:** Executes the request.
- **Parameters:**
    - `arg1` — the first input parameter
    - `arg2` — the second input parameter
- **Note:** This method derived from `IAction<T1, T2>`

#### `Consume(out T1, out T2)`

```csharp
public bool Consume(out T1 arg1, out T2 arg2);
```

- **Description:** Attempts to consume the request and retrieve the arguments.
- **Output:**
    - `arg1` — the first argument value if the request was consumed successfully.
    - `arg2` — the second argument value if the request was consumed successfully.
- **Returns:** `true` if successfully consumed.

#### `TryGet(out T1, out T2)`

```csharp
public bool TryGet(out T1 arg1, out T2 arg2);
```

- **Description:**  Attempts to retrieve both arguments.
- **Output:**
    - `arg1` — the first argument value if successfully retrieved.
    - `arg2` — the second argument value if successfully retrieved.
- **Returns:** `true` if the argument was retrieved successfully.