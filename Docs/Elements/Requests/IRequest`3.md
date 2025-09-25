
<details>
  <summary>
    <h2>🧩 IRequest&lt;T1, T2, T3&gt;</h2>
    <br> Represents a request action with <b>three input arguments</b>.
  </summary>

<br>

```csharp
public interface IRequest<T1, T2, T3> : IAction<T1, T2, T3>
```

- **Type parameters:**
    - `T1` — first argument
    - `T2` — second argument
    - `T3` — third argument

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

---

### 🏹 Methods

#### `Invoke(T1, T2, T3)`

```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Executes the request.
- **Parameters:**
    - `arg1` — the first input parameter
    - `arg2` — the second input parameter
    - `arg3` — the third input parameter
- **Note:** This method derived from `IAction<T1, T2, T3>`

#### `Consume(out T1, out T2, out T3)`

```csharp
public bool Consume(out T1 arg1, out T2 arg2, out T3 arg3);
```

- **Description:** Attempts to consume the request and retrieve the arguments.
- **Output parameters:**
    - `arg1` — the first argument value if the request was consumed successfully.
    - `arg2` — the second argument value if the request was consumed successfully.
    - `arg3` — the third argument value if the request was consumed successfully.
- **Returns:** `true` if successfully consumed.

#### `TryGet(out T1, out T2, out T3)`

```csharp
public bool TryGet(out T1 arg1, out T2 arg2, out T3 arg3);
```

- **Description:**  Attempts to retrieve both arguments.
- **Output parameters:**
    - `arg1` — the first argument value if successfully retrieved.
    - `arg2` — the second argument value if successfully retrieved.
    - `arg3` — the third argument value if successfully retrieved.
- **Returns:** `true` if the argument was retrieved successfully.

</details>