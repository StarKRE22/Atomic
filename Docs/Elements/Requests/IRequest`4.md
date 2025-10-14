# 🧩 IRequest&lt;T1, T2, T3, T4&gt;

Represents a request action with <b>four input arguments</b>.

---

## 📑 Table of Contents

- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Properties](#-properties)
        - [Required](#required)
        - [Arg1](#arg1)
        - [Arg2](#arg2)
        - [Arg3](#arg3)
        - [Arg4](#arg4)
    - [Methods](#-methods)
        - [Invoke(T1, T2, T3, T4)](#invoket1-t2-t3-t4)
        - [Consume(out T1, out T2, out T3, out T4)](#consumeout-t1-out-t2-out-t3-out-t4)
        - [TryGet(out T1, out T2, out T3, out T4)](#trygetout-t1-out-t2-out-t3-out-t4)

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IRequest<T1, T2, T3, T4> : IAction<T1, T2, T3, T4>
```

- **Description:** Represents a request action with <b>four input arguments</b>.
- **Inheritance:** [IAction&lt;T1, T2, T3, T4&gt;](../Actions/IAction%604.md)
- **Type parameters:**
    - `T1` — first argument
    - `T2` — second argument
    - `T3` — third argument
    - `T4` — fourth argument

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

#### `Arg3`

```csharp
public T3 Arg3 { get; }
```

- **Description:** Get the third argument of the request.

#### `Arg4`

```csharp
public T4 Arg4 { get; }
```

- **Description:** Get the fourth argument of the request.

---

### 🏹 Methods

#### `Invoke(T1, T2, T3, T4)`

```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```

- **Description:** Executes the request.
- **Parameters:**
    - `arg1` — the first input parameter
    - `arg2` — the second input parameter
    - `arg3` — the third input parameter
    - `arg4` — the fourth input parameter
- **Note:** This method derived from `IAction<T1, T2, T3, T4>`

#### `Consume(out T1, out T2, out T3, out T4)`

```csharp
public bool Consume(out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4);
```

- **Description:** Attempts to consume the request and retrieve the arguments.
- **Output:**
    - `arg1` — the first argument value if the request was consumed successfully.
    - `arg2` — the second argument value if the request was consumed successfully.
    - `arg3` — the third argument value if the request was consumed successfully.
    - `arg4` — the fourth argument value if the request was consumed successfully.
- **Returns:** `true` if successfully consumed.

#### `TryGet(out T1, out T2, out T3, out T4)`

```csharp
public bool TryGet(out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4);
```

- **Description:**  Attempts to retrieve both arguments.
- **Output:**
    - `arg1` — the first argument value if successfully retrieved.
    - `arg2` — the second argument value if successfully retrieved.
    - `arg3` — the third argument value if successfully retrieved.
    - `arg4` — the fourth argument value if successfully retrieved.
- **Returns:** `true` if the argument was retrieved successfully.