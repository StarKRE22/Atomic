
---

<details>
  <summary>
    <h2>🧩 BaseRequest</h2>
    <br> Represents a <b>parameterless</b> request action.
  </summary>

<br>

```csharp
public class BaseRequest : IRequest
```

---

### 🔑 Properties

#### `Required`

```csharp
public bool Required { get; }
```

- **Description:** Indicates whether the request must be handled.

---

### 🏹 Methods

#### `Invoke()`

```csharp
public void Invoke();
```

- **Description:** Executes the request.

#### `Consume()`

```csharp
public bool Consume();
```

- **Description:** Attempts to consume the request.
- **Returns:** `true` if successfully consumed; otherwise `false`.

</details>

---


---



---


---


---