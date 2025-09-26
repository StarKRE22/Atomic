
<details>
  <summary>
    <h2 id="idurationsource">🧩 IDurationSource</h2>
    <br> Represents a source that <b>has a total duration and can notify changes</b>.
  </summary>

<br>

```csharp
public interface IDurationSource
```

---

### ⚡ Events

#### `OnDurationChanged`

```csharp
public event Action<float> OnDurationChanged;
```

- **Description:** Invoked when the duration value changes.

---

### 🏹 Methods

#### `GetDuration()`

```csharp
public float GetDuration();  
```

- **Description:** Gets the total duration.
- **Returns:** The duration in seconds.

#### `SetDuration(float)`

```csharp
public void SetDuration(float duration);  
```

- **Description:** Sets the total duration.
- **Parameter:** `duration` — The new duration value in seconds.

</details>