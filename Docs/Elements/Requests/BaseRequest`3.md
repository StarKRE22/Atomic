# 🧩 Request&lt;T1, T2, T3&gt;

Represents a request action with <b>three input arguments</b>.

---

## 📑 Table of Contents

- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Properties](#-properties)
        - [Required](#required)
        - [Arg1](#arg1)
        - [Arg2](#arg2)
        - [Arg3](#arg3)
    - [Methods](#-methods)
        - [Invoke(T1, T2, T3)](#invoket1-t2-t3)
        - [Consume(out T1, out T2, out T3)](#consumeout-t1-out-t2-out-t3)
        - [TryGet(out T1, out T2, out T3)](#trygetout-t1-out-t2-out-t3)

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public class Request<T1, T2, T3> : IRequest<T1, T2, T3>
```

- **Description:** Represents a request action with <b>three input arguments</b>.
- **Inheritance:** [IRequest&lt;T1, T2, T3&gt;](IRequest%603.md)
- **Type parameters:**
    - `T1` — first argument
    - `T2` — second argument
    - `T3` — third argument
- **Note:** Supports Unity serialization and Odin Inspector

---

### 🔑 Properties

#### `Required`

```csharp
public bool Required { get; }
```

- **Description:** Indicates whether the request is currently required.

#### `Arg1`

```csharp
public T1 Arg1 { get; }
```

- **Description:** The first argument.

#### `Arg2`

```csharp
public T2 Arg2 { get; }
```

- **Description:** The second argument.

#### `Arg3`

```csharp
public T3 Arg3 { get; }
```

- **Description:** The third argument.

---

### 🏹 Methods

#### `Invoke(T1, T2, T3)`

```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Marks the request as required and stores all three arguments.
- **Parameters:** `arg1`, `arg2`, `arg3` — the input arguments.

#### `Consume(out T1, out T2, out T3)`

```csharp
public bool Consume(out T1 arg1, out T2 arg2, out T3 arg3);
```

- **Description:** Attempts to consume the request and retrieve all arguments.
- **Output:** `arg1`, `arg2`, `arg3` — the stored arguments.
- **Returns:** `true` if the request was required and is now consumed.

#### `TryGet(out T1, out T2, out T3)`

```csharp
public bool TryGet(out T1 arg1, out T2 arg2, out T3 arg3);
```

- **Description:** Attempts to retrieve all arguments without consuming the request.
- **Output:** `arg1`, `arg2`, `arg3` — the stored arguments.
- **Returns:** `true` if the request is currently required.