
<details>
  <summary>
    <h2>🧩 BaseRequest&lt;T1, T2, T3, T4&gt;</h2>
    <br> Represents a request action with <b>four input arguments</b>.
  </summary>

<br>

```csharp
public class BaseRequest<T1, T2, T3, T4> : IRequest<T1, T2, T3, T4>
```
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

#### `Arg4`

```csharp
public T4 Arg4 { get; }
```

- **Description:** The fourth argument.

---

### 🏹 Methods

#### `Invoke(T1, T2, T3, T4)`

```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```

- **Description:** Marks the request as required and stores all four arguments.
- **Parameters:** `arg1`, `arg2`, `arg3`, `arg4` — the input arguments.

#### `Consume(out T1, out T2, out T3, out T4)`

```csharp
public bool Consume(out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4);
```

- **Description:** Attempts to consume the request and retrieve all arguments.
- **Output parameters:** `arg1`, `arg2`, `arg3`, `arg4` — the stored arguments.
- **Returns:** `true` if the request was required and is now consumed.

#### `TryGet(out T1, out T2, out T3, out T4)`

```csharp
public bool TryGet(out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4);
```

- **Description:** Attempts to retrieve all arguments without consuming the request.
- **Output parameters:** `arg1`, `arg2`, `arg3`, `arg4` — the stored arguments.
- **Returns:** `true` if the request is currently required.

</details>