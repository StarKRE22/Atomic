# 🧩 BaseRequest&lt;T1, T2&gt;

```csharp
public class BaseRequest<T1, T2> : IRequest<T1, T2>
```

- **Description:** Represents a request action with <b>two input arguments</b>.
- **Inheritance:** [IRequest&lt;T1, T2&gt;](IRequest%602.md)
- **Type parameters:**
    - `T1` — first argument
    - `T2` — second argument
- **Note:** Supports Odin Inspector

---

## 🔑 Properties

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

---

## 🏹 Methods

#### `Invoke(T1, T2)`

```csharp
public void Invoke(T1 arg1, T2 arg2);
```

- **Description:** Marks the request as required and stores both arguments.
- **Parameters:** `arg1`, `arg2` — the input arguments.

#### `Consume(out T1, out T2)`

```csharp
public bool Consume(out T1 arg1, out T2 arg2);
```

- **Description:** Attempts to consume the request and retrieve both arguments.
- **Output:** `arg1`, `arg2` — the stored arguments.
- **Returns:** `true` if the request was required and is now consumed.

#### `TryGet(out T1, out T2)`

```csharp
public bool TryGet(out T1 arg1, out T2 arg2);
```

- **Description:** Attempts to retrieve both arguments without consuming the request.
- **Output:** `arg1`, `arg2` — the stored arguments.
- **Returns:** `true` if the request is currently required.