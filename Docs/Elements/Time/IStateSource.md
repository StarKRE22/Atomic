
<details>
  <summary>
    <h2 id="istatesource">🧩 IStateSource&lt;T&gt;</h2>
    <br> Represents a source that <b>provides state notifications</b>.
  </summary>

<br>

```csharp
public interface IStateSource<T>
```

- **Type Parameter:** `T` — Enum type representing the state.

--- 

### ⚡ Events

#### `OnStateChanged`

```csharp
public event Action<T> OnStateChanged;  
```

- **Description:** Raised when the state changes.

---

### 🏹 Methods

#### `GetState()`

```csharp
public T GetState();  
```

- **Description:** Gets the current internal state.
- **Returns:** The current state of type `T`.

</details>